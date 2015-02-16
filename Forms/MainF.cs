using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Gif.Components;
using TISFAT_ZERO.Forms;
using TISFAT_ZERO.Forms.Dialogs;

namespace TISFAT_ZERO
{
	public partial class MainF : Form
	{

		#region Variables
		bool bChanged;
		bool bOld;

		public int layersCount = 1; 
		#endregion

		#region Form/Class Events
		public MainF()
		{
			Program.MainformForm = this;

			InitializeComponent();
			this.IsMdiContainer = true;
		}

		private void Main_Load(object sender, EventArgs e)
		{
			//This is just temporary, until I get proper build selection and installation implemented.
			Properties.User.Default.selectedBuilds = Preferences.buildNames[Preferences.currentBuild];

			Program.ToolboxForm = new Toolbox();
			Program.ToolboxForm.Size = new Size(179, 375);
			Program.ToolboxForm.TopLevel = false;
			Program.ToolboxForm.Parent = this.splitContainer1.Panel2;
			Program.ToolboxForm.StartPosition = FormStartPosition.Manual;
			Program.ToolboxForm.Location = new Point(0, 10);
			splitContainer1.Panel2.Controls.Add(Program.ToolboxForm);

			Program.CanvasForm = new Canvas();
			Program.CanvasForm.Size = Properties.User.Default.CanvasSize;
			Program.CanvasForm.BackColor = Properties.User.Default.CanvasColor;
			Program.CanvasForm.TopLevel = false;
			Program.CanvasForm.Parent = this.splitContainer1.Panel2;
			Program.CanvasForm.StartPosition = FormStartPosition.Manual;
			Program.CanvasForm.Location = new Point(Program.ToolboxForm.Location.X + 4 + Program.ToolboxForm.Width, 10);
			splitContainer1.Panel2.Controls.Add(Program.CanvasForm);

			Program.RenderViewForm = new RenderView();
			Program.RenderViewForm.Size = Properties.User.Default.CanvasSize;
			Program.RenderViewForm.BackColor = Properties.User.Default.CanvasColor;
			Program.RenderViewForm.TopLevel = false;
			Program.RenderViewForm.Parent = this.splitContainer1.Panel2;
			Program.RenderViewForm.StartPosition = FormStartPosition.Manual;
			Program.RenderViewForm.Location = new Point(Program.CanvasForm.Location.X + 4 + Program.CanvasForm.Width, 10);
			splitContainer1.Panel2.Controls.Add(Program.RenderViewForm);

			//TODO: Finish scenes
			/*
			Program.ScenesForm = new Scenes();
			Program.ScenesForm.TopLevel = false;
			Program.ScenesForm.Parent = this.splitContainer1.Panel2;
			Program.ScenesForm.StartPosition = FormStartPosition.Manual;
			Program.ScenesForm.Location = new Point(Program.CanvasForm.Location.X + 4 + Program.CanvasForm.Width, 10);
			splitContainer1.Panel2.Controls.Add(Program.ScenesForm);
			*/

			Program.TimelineForm = new Timeline();
			Program.TimelineForm.TopLevel = false;
			Program.TimelineForm.Parent = this.splitContainer1.Panel1;
			Program.TimelineForm.Size = new Size(this.splitContainer1.Width - 2, splitContainer1.Panel1.Height);
			Program.TimelineForm.StartPosition = FormStartPosition.Manual;
			Program.TimelineForm.Location = new Point(0, 0);
			splitContainer1.Panel1.Controls.Add(Program.TimelineForm);

			int timelineLength = 1024;
			framesPanel.Location = new Point(timelineLength * 9, 0);


			//Timeline needs to be loaded first.
			Program.TimelineForm.Show();

			Program.ToolboxForm.Show();
			Program.CanvasForm.Show();

			//Program.RenderViewForm.Show();
			//Program.ScenesForm.Show();

			if (Program.loadFile != "")
				Loader.loadProjectFile(Program.loadFile);

			//If the auto check updates value is true, then start the update checker in the background.
			if (Properties.User.Default.autoCheck)
			{
				//Create the checker with an argument that makes the form auto-close if no updates are available
				CheckUpdateForm checker = new CheckUpdateForm(true);
				checker.Show();
				this.Focus();
			}
		}
		
		public void resetTimeline(bool loading)
		{
			//TODO: Update this function

			splitContainer1.Panel1.Controls.RemoveAt(0);
			Program.TimelineForm.Dispose();

			Program.TimelineForm = new Timeline();
			Program.TimelineForm.TopLevel = false;
			Program.TimelineForm.Parent = this.splitContainer1.Panel1;
			Program.TimelineForm.Size = new Size(this.splitContainer1.Width - 2, splitContainer1.Panel1.Height);
			Program.TimelineForm.StartPosition = FormStartPosition.Manual;
			Program.TimelineForm.Location = new Point(0, 0);
			splitContainer1.Panel1.Controls.Add(Program.TimelineForm);

			Canvas.figureList = new List<StickObject>();
			Canvas.tweenFigs = new List<StickObject>();
			Canvas.lights = new List<LightObject>();

			Program.TimelineForm.addStickLayer("Stick Layer 1");

			Program.TimelineForm.Show();

			if (loading)
			{
				Canvas.figureList.Clear();
				Canvas.tweenFigs.Clear();
				Canvas.lights.Clear();
			}
		}

		public void doneLoading()
		{
			Program.TimelineForm.setFrame(0);
			Program.CanvasForm.Refresh();
		}
		#endregion

		#region Functions
		private void onFrameSelected()
		{
			//Do stuff here
		}
		#endregion

		#region Form Controls
		private void MainF_Resize(object sender, EventArgs e)
		{
			int height = layersCount * 16;

			Program.TimelineForm.Size = new Size(this.splitContainer1.Width - 2, height);
			Program.TimelineForm.Refresh();
		}

		private void splitContainer1_Panel1_Resize(object sender, EventArgs e)
		{
			if (splitContainer1.Panel1.Height < 51)
				splitContainer1.SplitterDistance = 51;

			if(Program.TimelineForm != null)
				Program.TimelineForm.Size = new Size(this.Width, splitContainer1.SplitterDistance - 20);
		}

		private void splitContainer1_Panel1_Scroll(object sender, ScrollEventArgs e)
		{
			Program.TimelineForm.Location = new Point(0, 0);
			Program.TimelineForm.Refresh();
		}

		private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(Color.White);
		}
		#endregion

		#region Menu Bar
		private void editToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Preferences f = new Preferences();
			f.ShowDialog();
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			About f = new About();
			f.ShowDialog();
		}

		private void exitTISFATToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void removeLayerCallback(object sender, EventArgs e)
		{
			if (Timeline.layer_cnt < 1)
				return;

			Layer toRemove = Timeline.layers[Timeline.layer_sel];

			Canvas.figureList.Remove(toRemove.fig);
			Canvas.tweenFigs.Remove(toRemove.tweenFig);

			Timeline.layers.RemoveAt(Timeline.layer_sel);
			Timeline.layer_cnt--;
			if (Timeline.layer_sel == Timeline.layer_cnt)
				Timeline.layer_sel--;

			Program.TimelineForm.Refresh();
		}
		#endregion

		private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
		{
			lbl_selectionDummy.Focus();
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			dlg_saveFile.InitialDirectory = Properties.User.Default.DefaultSavePath;
			dlg_saveFile.ShowDialog();
		}

		private void dlg_saveFile_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Saver.saveProject(dlg_saveFile.FileName, Timeline.layers);
		}

		private void openMovieToolStripMenuItem_Click(object sender, EventArgs e)
		{
			dlg_openFile.ShowDialog();
		}

		private void dlg_openFile_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Loader.loadProjectFile(dlg_openFile.FileName);
		}

		private void newMovieToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Timeline.resetEverything(false);
		}

		private void stickEditorToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		public void updateByLayers(int layercount)
		{
			layersCount = layercount;
			panel1.Location = new Point(0, layersCount * 16 + 17);
		}

		private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CheckUpdateForm f = new CheckUpdateForm();
			f.ShowDialog();
		}

		private void animatedGifToolStripMenuItem_Click(object sender, EventArgs e)
		{
			dlg_exportFile.ShowDialog();
		}

		private void dlg_exportFile_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{
			AnimatedGifEncoder x = new AnimatedGifEncoder();
			x.Start(dlg_exportFile.FileName);
			x.SetDelay(1000 / Program.ToolboxForm.frameRate);
			x.SetRepeat(0);

			if (dlg_exportFile.FileName.EndsWith(".gif"))
			{
				for (int i = 0;Program.TimelineForm.hasFrames(i);i++)
				{
					x.AddFrame(Program.TimelineForm.saveFrame(i));
				}
			}
			
			x.Finish();
		}

		private void canvasSizeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CanvasSizePrompt p = new CanvasSizePrompt();
			p.ShowDialog();
		}

		private void splitContainer1_Panel2_Scroll(object sender, ScrollEventArgs e)
		{
			Program.CanvasForm.Refresh();
		}
	}
}