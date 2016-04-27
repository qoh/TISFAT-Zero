using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TISFAT.Util
{
	public static class Exporting
	{
		public static void ExportGIF(Project ActiveProject, ProgressDialog dlg, string fileName)
		{
			float fps = ActiveProject.FPS;
			float delta = 1.0f / fps;
			float endTime = 0.0f;

			string temp = Path.GetTempPath() + Path.GetRandomFileName();
			Directory.CreateDirectory(temp);

			foreach (Layer layer in ActiveProject.Layers)
				endTime = Math.Max(endTime, layer.Framesets[layer.Framesets.Count - 1].EndTime);


			// Step 1: Grab images from scene and save to files
			int n = 0;
			int nt = (int)Math.Ceiling(endTime / ActiveProject.AnimSpeed / delta);
			bool cancelled = false;
			EventHandler cancelHandler = (_1, _2) => { cancelled = true; };

			dlg.Title = "Rendering Frames..";
			dlg.ProgressStyle = ProgressBarStyle.Continuous;
			dlg.Canceled += cancelHandler;
			dlg.Work = () =>
			{
				for (float time = 0; time <= endTime / ActiveProject.AnimSpeed && !cancelled; time += delta)
				{
					dlg.DetailText = "Frame " + (n + 1) + " of " + (nt + 1);
					dlg.ProgressValue = n * 100 / nt;

					Program.Form_Canvas.DrawFrame(time * ActiveProject.AnimSpeed, true, true);
					Image.FromHbitmap(Program.Form_Canvas.TakeScreenshot()).Save(temp + "\\" + n + ".bmp", System.Drawing.Imaging.ImageFormat.Bmp);
					Application.DoEvents();
					n++;
				}

				if (cancelled)
				{
					Directory.Delete(temp, true);
					dlg.Close();
					return;
				}

				dlg.Canceled -= cancelHandler;
				dlg.Canceled += dlg.Finish;

				dlg.Title = "Encoding Video..";
				dlg.ProgressStyle = ProgressBarStyle.Marquee;
				dlg.DetailText = "Waiting for ffmpeg..";


				// Step 2: Convert image sequences to video
				ProcessStartInfo startInfo = new ProcessStartInfo();
				startInfo.FileName = "ffmpeg.exe";
				startInfo.Arguments = $"-y -r {fps} -f image2 -i {temp}\\%d.bmp {temp}\\exported-vid.avi";
				startInfo.UseShellExecute = false;
				startInfo.CreateNoWindow = true;
				startInfo.RedirectStandardOutput = true;
				startInfo.RedirectStandardError = true;

				// Start process
				bool finished = false;

				Process processTemp = new Process();
				processTemp.StartInfo = startInfo;
				processTemp.EnableRaisingEvents = true;

				processTemp.OutputDataReceived += (s, x) =>
				{
					Console.WriteLine(x.Data);
				};
				processTemp.ErrorDataReceived += (s, x) =>
				{
					MessageBox.Show(x.Data, "FFMPEG Error");
				};
				processTemp.Exited += (s, x) =>
				{
					finished = true;
				};
				processTemp.Start();

				while(!finished)
					continue;

				File.Copy($"{temp}\\exported-vid.avi", Path.GetDirectoryName(fileName) + "\\exported-vid.avi", true);

				// Step 2.5: Generate pallete from the AVI
				startInfo.Arguments = $"-y -ss 30 -t 3 -i {temp}\\exported-vid.avi \\ -vf scale=320:-1:flags=lanczos,palettegen {temp}\\palette.png";

				finished = false;

				processTemp = new Process();
				processTemp.StartInfo = startInfo;
				processTemp.EnableRaisingEvents = true;

				processTemp.OutputDataReceived += (s, x) =>
				{
					Console.WriteLine(x.Data);
				};
				processTemp.ErrorDataReceived += (s, x) =>
				{
					MessageBox.Show(x.Data, "FFMPEG Error");
				};
				processTemp.Exited += (s, x) =>
				{
					finished = true;
				};
				processTemp.Start();

				while (!finished)
					continue;

				// Step 3: Convert created AVI to GIF
				// startInfo.Arguments = $"-y -i {temp}\\exported-vid.avi -pix_fmt rgb24 {fileName}";
				startInfo.Arguments = $"-ss 30 -t 3 -i {temp}\\exported-vid.avi -i {temp}\\palette.png -filter_complex \"scale=320:-1:flags=lanczos[x];[x][1:v]paletteuse\" {fileName}";

				processTemp = new Process();
				processTemp.StartInfo = startInfo;

				processTemp.OutputDataReceived += (s, x) =>
				{
					Console.WriteLine(x.Data);
				};
				processTemp.ErrorDataReceived += (s, x) =>
				{
					MessageBox.Show(x.Data, "FFMPEG Error");
				};
				processTemp.Exited += (s, x) =>
				{
					dlg.Finish(s, x);
					Directory.Delete(temp, true);
				};
				processTemp.Start();
			};
			
			dlg.ShowDialog();
		}
	}
}
