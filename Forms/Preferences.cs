using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Net;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;

namespace TISFAT_Zero
{
	partial class Preferences : Form
	{
		private readonly string saveFile = Environment.SpecialFolder.MyDocuments + "/TISFAT - Zero/Preferences.txt";

		public static byte currentBuild = 2; //current build version. 0 = stable, 1 = beta, 2 = nightly
		public static string[] buildNames = new string[] { "stable", "beta", "nightly" };

		public static Attributes properties = new Attributes();

		public Preferences()
		{
			InitializeComponent();
		}

		//TODO: Fix all this crap.

		private void Preferences_Load(object sender, EventArgs e)
		{
			if(!File.Exists(saveFile))
			{
				properties.addAttribute(new Size(460, 360), "CanvasSize");
				properties.addAttribute(Color.White, "CanvasColor");
				properties.addAttribute(true, "CheckForUpdates");
				properties.addAttribute("200 190 245 255 0 0 170 170 170 255 0 0 70 120 255 0 0 0 0 0 0 255 255 255 255 0 0 220 220 220 255 0 0 40 230 255 255 192 203 0 0 0 140 140 140 200 200 200 30 100 255",
										"Theme");

				//Gets all the picture boxes in the theme panel, then set the colors
				PictureBox[] boxes2 = GetAll(Controls.Find("pnl_themeScrollPanel", true)[0], typeof(PictureBox)).Cast<PictureBox>().ToArray();

				foreach (PictureBox x in boxes2)
					x.BackColor = Timeline.Colors[Int32.Parse((string)x.Tag)];

				return;
			}
			
			StreamReader filereader = new StreamReader(saveFile);


			/*
			buildName = buildNames[currentBuild];

			label1.Text = "Build Version: v" + Program.Version;

			listView1.Items[0].Selected = true;

			pic_ColorBox.BackColor = Properties.User.Default.CanvasColor;
			lbl_ColorBox.Text = Properties.User.Default.CanvasColor.Name;

			num_Width.Value = Properties.User.Default.CanvasSize.Width;
			num_Height.Value = Properties.User.Default.CanvasSize.Height;

			string bld_Installed = Properties.User.Default.buildsInstalled;

			if (bld_Installed != buildName)
			{
				Properties.User.Default.buildsInstalled = Properties.User.Default.selectedBuilds = buildName;
				Properties.User.Default.buildLocations = Application.ExecutablePath;
			}

			comboBox1.SelectedIndex = currentBuild;
			checkBox1.Checked = Properties.User.Default.autoCheck;*/

			//Load the current theme into the timeline colors
			string theme = Properties.User.Default.theme;

			string[] rgbs = theme.Split(' ');
			Color[] colors = new Color[17];

			for (int a = 0; a < rgbs.Length; a += 3)
				colors[a / 3] = Color.FromArgb(Int32.Parse(rgbs[a]), Int32.Parse(rgbs[a + 1]), Int32.Parse(rgbs[a + 2]));

			Timeline.Colors = colors;

			//Gets all the picture boxes in the theme panel, then set the colors
			PictureBox[] boxes = GetAll(Controls.Find("pnl_themeScrollPanel", true)[0], typeof(PictureBox)).Cast<PictureBox>().ToArray();

			foreach (PictureBox x in boxes)
				x.BackColor = Timeline.Colors[Int32.Parse((string)x.Tag)];
		}

		public IEnumerable<Control> GetAll(Control control, Type type)
		{
			var controls = control.Controls.Cast<Control>();

			return controls.SelectMany(ctrl => GetAll(ctrl, type))
									  .Concat(controls)
									  .Where(c => c.GetType() == type);
		}

		private void btn_submitButton_Click(object sender, EventArgs e)
		{
			Properties.User.Default.DefaultSavePath = txt_defaultSavePath.Text;
			Properties.User.Default.CanvasColor = pic_ColorBox.BackColor;
			Properties.User.Default.CanvasSize = new System.Drawing.Size((int)num_Width.Value, (int)num_Height.Value);
			
			/* Disabled until I get everything working
			if (comboBox1.SelectedIndex != currentBuild)
			{
				Properties.User.Default.Save();

				WebClient downloader = new WebClient();
				downloader.Proxy = new WebProxy();
				downloader.DownloadFileCompleted += new AsyncCompletedEventHandler(Downloader_Done);
				downloader.DownloadFileAsync(new Uri("https://dl.dropboxusercontent.com/s/31h1ysf1k32ssue/T0Updater.exe"), "T0Updater.exe");

				return;
			}
			*/

			Properties.User.Default.Save();
			this.Close();
		}

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listView1.Items[0].Selected)
				pnl_General.BringToFront();
			if (listView1.Items[1].Selected)
			{
				pnl_Updates.BringToFront();
				checkBox1_CheckedChanged(new object(), new EventArgs());
			}
			if (listView1.Items[2].Selected)
				pnl_TimelineTheme.BringToFront();
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			label2.Text = "TISFAT:Zero will" + (checkBox1.Checked ? " " : " not ") + "automatically check for updates for checked builds upon opening the program.";
			Properties.User.Default.autoCheck = checkBox1.Checked;
		}

		private void Downloader_Done(object sender, AsyncCompletedEventArgs e)
		{
			Process x = new Process();
			x.StartInfo = new ProcessStartInfo("T0Updater.exe", "\"" + Path.GetFileName(Application.ExecutablePath) + "\" " + buildNames[comboBox1.SelectedIndex] + " " + Program.Version + " True");
			x.Start();
			Application.Exit();
		}

		private void ColorBoxItem_Click(object sender, EventArgs e)
		{
			if (!(sender.GetType() == typeof(PictureBox)))
				return;
			PictureBox thisBox = (PictureBox)sender;
			Label thisLabel = (Label)(Controls.Find("lbl_" + thisBox.Name.Substring(4), true))[0];

			if (!(dlg_colorDialog.ShowDialog() == DialogResult.OK))
				return;

			thisBox.BackColor = dlg_colorDialog.Color;
			thisLabel.Text = "(" + thisBox.BackColor.R + "," + thisBox.BackColor.G + "," + thisBox.BackColor.B + ")";

			if (thisBox.Tag != null)
			{
				Timeline.Colors[Int32.Parse((string)thisBox.Tag)] = thisBox.BackColor;

				Program.TheTimeline.Timeline_Resize(sender, e);
				Program.TheTimeline.Timeline_Refresh();
			}
		}
	}

	namespace NumericEditBox
	{
		/// <summary>
		/// Summary description for Class1.
		/// </summary>
		public class NumericEditBox : System.Windows.Forms.NumericUpDown
		{
			public NumericEditBox()
			{
				Controls[0].Visible = false;
			}
		}
	}
}