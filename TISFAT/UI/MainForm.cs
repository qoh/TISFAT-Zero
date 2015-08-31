using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TISFAT.Entities;
using TISFAT.Util;

namespace TISFAT
{
	public enum EditMode
	{
		Default, Onion, Phase, Diff
	}

	public partial class MainForm : Form
	{
		#region Properties
		public Project ActiveProject;
		private string ProjectFileName;
		private bool ProjectDirty;

		private Stack<IAction> UndoList;
		private Stack<IAction> RedoList;

		public TimelineForm Form_Timeline;
		public CanvasForm Form_Canvas;
		public ToolboxForm Form_Toolbox;

		private EditMode _ActiveEditMode;
		public EditMode ActiveEditMode
		{
			get { return _ActiveEditMode; }
			set
			{
				btn_EditModeDefault.Checked = value == EditMode.Default;
				btn_EditModeOnion.Checked = value == EditMode.Onion;
				btn_EditModePhase.Checked = value == EditMode.Phase;
				btn_EditModeDiff.Checked = value == EditMode.Diff;

				_ActiveEditMode = value;
				Form_Timeline.MainTimeline.GLContext.Invalidate();
			}
		}

		public Timeline MainTimeline
		{
			get { return Form_Timeline == null ? null : Form_Timeline.MainTimeline; }
		}

		private static Random Why = new Random();
		#endregion

		public MainForm()
		{
			DoubleBuffered = true;

			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			ProjectNew();

			// Create and show forms
			Form_Timeline = new TimelineForm(sc_MainContainer.Panel2);
			Form_Toolbox = new ToolboxForm(sc_MainContainer.Panel2);
			Form_Canvas = new CanvasForm(sc_MainContainer.Panel2);

			Form_Timeline.Show();
			Form_Toolbox.Show();
			Form_Canvas.Show();

			Form_Timeline.Location = new Point(5, 0);
			Form_Toolbox.Location = new Point(50, Form_Timeline.Location.Y + Form_Timeline.Height + 4);
			Form_Canvas.Location = new Point(Form_Toolbox.Location.X + Form_Toolbox.Width + 20, Form_Timeline.Location.Y + Form_Timeline.Height + 4);
		}

		public void SetDirty(bool dirty)
		{
			ProjectDirty = dirty;
			Text = "TISFAT Zero - " + (Path.GetFileNameWithoutExtension(ProjectFileName) ?? "Untitled") + (dirty ? " *" : "");
		}

		private void SetFileName(string filename)
		{
			ProjectFileName = filename;
			SetDirty(filename == null);
		}

		public void Do(IAction action)
		{
			if (!action.Do())
				return;

			UndoList.Push(action);
			RedoList.Clear();

			UpdateUndoRedoButtons();

			if (UndoList.Count % 10 == 0)
				AutoSave();
		}

		private void UpdateUndoRedoButtons()
		{
			if (UndoList.Count > 0)
				btn_Undo.ImageDefault = Properties.Resources.undo;
			else
				btn_Undo.ImageDefault = Properties.Resources.undo_gray;

			if (RedoList.Count > 0)
				btn_Redo.ImageDefault = Properties.Resources.redo;
			else
				btn_Redo.ImageDefault = Properties.Resources.redo_gray;
		}

		private void Undo()
		{
			if (UndoList.Count < 1)
				return;

			IAction item = UndoList.Pop();
			RedoList.Push(item);
			item.Undo();

			UpdateUndoRedoButtons();
		}

		private void Redo()
		{
			if (RedoList.Count < 1)
				return;

			IAction item = RedoList.Pop();
			UndoList.Push(item);
			item.Do();

			UpdateUndoRedoButtons();
		}

		#region File Saving / Loading
		public void ProjectNew()
		{
			ActiveProject = new Project();

			UndoList = new Stack<IAction>();
			RedoList = new Stack<IAction>();

			UpdateUndoRedoButtons();

			SetFileName(null);

			StickFigure defaultFig = new StickFigure();
			ActiveProject.Layers.Add(defaultFig.CreateDefaultLayer(0, 20, new LayerCreationArgs(0, "")));

			if(MainTimeline != null)
				MainTimeline.GLContext.Invalidate();
		}

		public void ProjectOpen(string filename)
		{
			if (MainTimeline == null)
				return;

			ActiveProject = new Project();

			UndoList = new Stack<IAction>();
			RedoList = new Stack<IAction>();

			UpdateUndoRedoButtons();

			using (var reader = new BinaryReader(new FileStream(filename, FileMode.Open)))
			{
				UInt16 version = reader.ReadUInt16();
				ActiveProject.Read(reader, version);
			}

			SetFileName(filename);

			if (MainTimeline != null)
				MainTimeline.GLContext.Invalidate();
		}

		public void ProjectSave(string filename)
		{
			if (MainTimeline == null)
				return;

			using (var writer = new BinaryWriter(new FileStream(filename, FileMode.Create)))
			{
				writer.Write(FileFormat.Version);
				ActiveProject.Write(writer);
			}

			SetFileName(filename);
			ProjectFileName = filename;
		}

		public void AutoSave()
		{
			ProjectSave("autosave.tzp");
		}

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ProjectNew();
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.AddExtension = true;
			dialog.Filter = "TISFAT Zero Project|*.tzp";

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				ProjectOpen(dialog.FileName);
			}
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ProjectFileName != null)
				ProjectSave(ProjectFileName);
			else
				saveAsToolStripMenuItem_Click(sender, e);
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileDialog dialog = new SaveFileDialog();
			dialog.AddExtension = true;
			dialog.Filter = "TISFAT Zero Project|*.tzp";

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				ProjectSave(dialog.FileName);
			}
		}
		#endregion

		private void exportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.Filter = "Animated GIF|*.gif|Animated PNG|*.png|MPEG-4|*.mp4|AVI|*.avi|WebM|*.webm|Flash Video|*.flv|Windows Media Video|*.wmv";

			if (dlg.ShowDialog() != DialogResult.OK)
				return;
			
			int fps = 60;
			float delta = 1.0f / fps;

			float endTime = 0.0f;

			string temp = Path.GetTempPath() + Path.GetRandomFileName();
			Directory.CreateDirectory(temp);

			foreach (Layer layer in ActiveProject.Layers)
			{
				endTime = Math.Max(endTime, layer.Framesets[layer.Framesets.Count - 1].EndTime);
			}

			int n = 0;
			int nt = (int)Math.Ceiling(endTime / ActiveProject.FPS / delta);
			
			ProgressDialog progress = new ProgressDialog();

			bool frameCanceled = false;
			EventHandler frameCancelHandler = (_1, _2) => { frameCanceled = true; };

			progress.Title = "Rendering Frames..";
			progress.ProgressStyle = ProgressBarStyle.Continuous;
			progress.Canceled += frameCancelHandler;
			progress.Work = () =>
			{
				for (float time = 0; time <= endTime / ActiveProject.FPS && !frameCanceled; time += delta)
				{
					progress.DetailText = "Frame " + (n + 1) + " of " + (nt + 1);
					progress.ProgressValue = n * 100 / nt;

					Form_Canvas.DrawFrame(time * ActiveProject.FPS, true);
					Image.FromHbitmap(Form_Canvas.TakeScreenshot()).Save(temp + "\\" + n + ".bmp", System.Drawing.Imaging.ImageFormat.Bmp);
					Application.DoEvents();
					n++;
				}

				if (frameCanceled)
				{
					Directory.Delete(temp, true);
					progress.Close();
					return;
				}

				progress.Canceled -= frameCancelHandler;
				progress.Canceled += progress.Finish;
				
				progress.Title = "Encoding Video..";
				progress.ProgressStyle = ProgressBarStyle.Marquee;
				progress.DetailText = "Waiting for ffmpeg..";

				ProcessStartInfo startInfo = new ProcessStartInfo();
				startInfo.FileName = "ffmpeg.exe";
				startInfo.Arguments = "-y -r " + fps + " -i \"" + temp + "\\%d.bmp\" \"" + dlg.FileName + "\"";
				startInfo.UseShellExecute = false;
				startInfo.CreateNoWindow = true;

				Process processTemp = new Process();
				processTemp.StartInfo = startInfo;
				processTemp.EnableRaisingEvents = true;

				processTemp.Exited += (sender2, e2) =>
				{
					progress.Finish(sender2, e2);
					Directory.Delete(temp, true);
				};

				processTemp.Start();
			};

			progress.ShowDialog();
		}

		private void btn_EditModeDefault_Click(object sender, EventArgs e)
		{
			ActiveEditMode = EditMode.Default;
		}

		private void btn_EditModeOnion_Click(object sender, EventArgs e)
		{
			ActiveEditMode = EditMode.Onion;
		}

		private void btn_EditModePhase_Click(object sender, EventArgs e)
		{
			ActiveEditMode = EditMode.Phase;
		}

		private void btn_EditModeDiff_Click(object sender, EventArgs e)
		{
			ActiveEditMode = EditMode.Diff;
		}

		private void btn_RemoveLayer_Click(object sender, EventArgs e)
		{
			Form_Timeline.MainTimeline.RemoveLayer();
		}

		private void btn_AddLayer_Click(object sender, EventArgs e)
		{
			AddLayerDialog dlg = new AddLayerDialog();

			dlg.ShowDialog();
		}

		private void btn_Undo_Click(object sender, EventArgs e)
		{
			Undo();
		}

		private void btn_Redo_Click(object sender, EventArgs e)
		{
			Redo();
		}

		private void projectPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ProjectPropertiesDialog dlg = new ProjectPropertiesDialog();

			dlg.ShowDialog();
		}

		private void openColorPickerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ColorPickerDialog dlg = new ColorPickerDialog();

			dlg.ShowDialog();
		}
	}
}
