using System;
using System.Drawing;
using System.Windows.Forms;

namespace TISFAT_Zero
{
	partial class MainF
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainF));
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newMovieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openMovieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveMovieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.closeMovieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.movieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.exitTISFATToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.copyFramesetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteFramesetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.insertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.layerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.framesetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.keyframeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.keyframeWithCurrentPoseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.layerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.framesetToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.keyframeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnu_strip1 = new System.Windows.Forms.MenuStrip();
			this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.dlg_saveFile = new System.Windows.Forms.SaveFileDialog();
			this.dlg_openFile = new System.Windows.Forms.OpenFileDialog();
			this.mnu_strip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMovieToolStripMenuItem,
            this.openMovieToolStripMenuItem,
            this.saveMovieToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.closeMovieToolStripMenuItem,
            this.toolStripSeparator1,
            this.exportToolStripMenuItem,
            this.toolStripSeparator2,
            this.movieToolStripMenuItem,
            this.toolStripSeparator3,
            this.exitTISFATToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// newMovieToolStripMenuItem
			// 
			this.newMovieToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newMovieToolStripMenuItem.Image")));
			this.newMovieToolStripMenuItem.Name = "newMovieToolStripMenuItem";
			this.newMovieToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.newMovieToolStripMenuItem.Text = "New Movie ...";
			// 
			// openMovieToolStripMenuItem
			// 
			this.openMovieToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openMovieToolStripMenuItem.Image")));
			this.openMovieToolStripMenuItem.Name = "openMovieToolStripMenuItem";
			this.openMovieToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.openMovieToolStripMenuItem.Text = "Open Movie ...";
			// 
			// saveMovieToolStripMenuItem
			// 
			this.saveMovieToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveMovieToolStripMenuItem.Image")));
			this.saveMovieToolStripMenuItem.Name = "saveMovieToolStripMenuItem";
			this.saveMovieToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.saveMovieToolStripMenuItem.Text = "Save Movie ...";
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveAsToolStripMenuItem.Image")));
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.saveAsToolStripMenuItem.Text = "Save As ...";
			// 
			// closeMovieToolStripMenuItem
			// 
			this.closeMovieToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("closeMovieToolStripMenuItem.Image")));
			this.closeMovieToolStripMenuItem.Name = "closeMovieToolStripMenuItem";
			this.closeMovieToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.closeMovieToolStripMenuItem.Text = "Close Movie ...";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
			// 
			// exportToolStripMenuItem
			// 
			this.exportToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exportToolStripMenuItem.Image")));
			this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
			this.exportToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.exportToolStripMenuItem.Text = "Export";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
			// 
			// movieToolStripMenuItem
			// 
			this.movieToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("movieToolStripMenuItem.Image")));
			this.movieToolStripMenuItem.Name = "movieToolStripMenuItem";
			this.movieToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.movieToolStripMenuItem.Text = "Movie";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
			// 
			// exitTISFATToolStripMenuItem
			// 
			this.exitTISFATToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exitTISFATToolStripMenuItem.Image")));
			this.exitTISFATToolStripMenuItem.Name = "exitTISFATToolStripMenuItem";
			this.exitTISFATToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.exitTISFATToolStripMenuItem.Text = "Exit TISFAT";
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator4,
            this.copyFramesetToolStripMenuItem,
            this.pasteFramesetToolStripMenuItem,
            this.toolStripSeparator6,
            this.preferencesToolStripMenuItem});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.editToolStripMenuItem.Text = "Edit";
			// 
			// undoToolStripMenuItem
			// 
			this.undoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("undoToolStripMenuItem.Image")));
			this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
			this.undoToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.undoToolStripMenuItem.Text = "Undo";
			// 
			// redoToolStripMenuItem
			// 
			this.redoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("redoToolStripMenuItem.Image")));
			this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
			this.redoToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.redoToolStripMenuItem.Text = "Redo";
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(150, 6);
			// 
			// copyFramesetToolStripMenuItem
			// 
			this.copyFramesetToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyFramesetToolStripMenuItem.Image")));
			this.copyFramesetToolStripMenuItem.Name = "copyFramesetToolStripMenuItem";
			this.copyFramesetToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.copyFramesetToolStripMenuItem.Text = "Copy Frameset";
			// 
			// pasteFramesetToolStripMenuItem
			// 
			this.pasteFramesetToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteFramesetToolStripMenuItem.Image")));
			this.pasteFramesetToolStripMenuItem.Name = "pasteFramesetToolStripMenuItem";
			this.pasteFramesetToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.pasteFramesetToolStripMenuItem.Text = "Paste Frameset";
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(150, 6);
			// 
			// preferencesToolStripMenuItem
			// 
			this.preferencesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("preferencesToolStripMenuItem.Image")));
			this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
			this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.preferencesToolStripMenuItem.Tag = "0";
			this.preferencesToolStripMenuItem.Text = "Preferences..";
			this.preferencesToolStripMenuItem.Click += new System.EventHandler(this.OpenMenuObject);
			// 
			// insertToolStripMenuItem
			// 
			this.insertToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.layerToolStripMenuItem,
            this.framesetToolStripMenuItem,
            this.keyframeToolStripMenuItem,
            this.keyframeWithCurrentPoseToolStripMenuItem});
			this.insertToolStripMenuItem.Name = "insertToolStripMenuItem";
			this.insertToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
			this.insertToolStripMenuItem.Text = "Insert";
			// 
			// layerToolStripMenuItem
			// 
			this.layerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("layerToolStripMenuItem.Image")));
			this.layerToolStripMenuItem.Name = "layerToolStripMenuItem";
			this.layerToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
			this.layerToolStripMenuItem.Text = "Layer ...";
			// 
			// framesetToolStripMenuItem
			// 
			this.framesetToolStripMenuItem.Name = "framesetToolStripMenuItem";
			this.framesetToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
			this.framesetToolStripMenuItem.Text = "Frameset";
			// 
			// keyframeToolStripMenuItem
			// 
			this.keyframeToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("keyframeToolStripMenuItem.Image")));
			this.keyframeToolStripMenuItem.Name = "keyframeToolStripMenuItem";
			this.keyframeToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
			this.keyframeToolStripMenuItem.Text = "Keyframe";
			// 
			// keyframeWithCurrentPoseToolStripMenuItem
			// 
			this.keyframeWithCurrentPoseToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("keyframeWithCurrentPoseToolStripMenuItem.Image")));
			this.keyframeWithCurrentPoseToolStripMenuItem.Name = "keyframeWithCurrentPoseToolStripMenuItem";
			this.keyframeWithCurrentPoseToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
			this.keyframeWithCurrentPoseToolStripMenuItem.Text = "Keyframe with current pose";
			// 
			// removeToolStripMenuItem
			// 
			this.removeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.layerToolStripMenuItem1,
            this.framesetToolStripMenuItem1,
            this.keyframeToolStripMenuItem1});
			this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
			this.removeToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
			this.removeToolStripMenuItem.Text = "Remove";
			// 
			// layerToolStripMenuItem1
			// 
			this.layerToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("layerToolStripMenuItem1.Image")));
			this.layerToolStripMenuItem1.Name = "layerToolStripMenuItem1";
			this.layerToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
			this.layerToolStripMenuItem1.Text = "Layer";
			// 
			// framesetToolStripMenuItem1
			// 
			this.framesetToolStripMenuItem1.Name = "framesetToolStripMenuItem1";
			this.framesetToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
			this.framesetToolStripMenuItem1.Text = "Frameset";
			// 
			// keyframeToolStripMenuItem1
			// 
			this.keyframeToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("keyframeToolStripMenuItem1.Image")));
			this.keyframeToolStripMenuItem1.Name = "keyframeToolStripMenuItem1";
			this.keyframeToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
			this.keyframeToolStripMenuItem1.Text = "Keyframe";
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem1,
            this.toolStripSeparator5,
            this.aboutToolStripMenuItem,
            this.checkForUpdatesToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// helpToolStripMenuItem1
			// 
			this.helpToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripMenuItem1.Image")));
			this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
			this.helpToolStripMenuItem1.Size = new System.Drawing.Size(171, 22);
			this.helpToolStripMenuItem1.Tag = "1";
			this.helpToolStripMenuItem1.Text = "Help";
			this.helpToolStripMenuItem1.Click += new System.EventHandler(this.OpenMenuObject);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(168, 6);
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("aboutToolStripMenuItem.Image")));
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
			this.aboutToolStripMenuItem.Tag = "2";
			this.aboutToolStripMenuItem.Text = "About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.OpenMenuObject);
			// 
			// checkForUpdatesToolStripMenuItem
			// 
			this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
			this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
			this.checkForUpdatesToolStripMenuItem.Tag = "3";
			this.checkForUpdatesToolStripMenuItem.Text = "Check for Updates";
			this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.OpenMenuObject);
			// 
			// mnu_strip1
			// 
			this.mnu_strip1.BackColor = System.Drawing.Color.White;
			this.mnu_strip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.insertToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.debugToolStripMenuItem});
			this.mnu_strip1.Location = new System.Drawing.Point(0, 0);
			this.mnu_strip1.Name = "mnu_strip1";
			this.mnu_strip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.mnu_strip1.Size = new System.Drawing.Size(750, 24);
			this.mnu_strip1.TabIndex = 0;
			this.mnu_strip1.Text = "menuStrip1";
			// 
			// debugToolStripMenuItem
			// 
			this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
			this.debugToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
			this.debugToolStripMenuItem.Text = "Debug";
			// 
			// splitContainer1
			// 
			this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 24);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.BackColor = System.Drawing.Color.White;
			this.splitContainer1.Panel1.Resize += new System.EventHandler(this.splitContainer1_Panel1_Resize);
			this.splitContainer1.Panel1MinSize = 100;
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.AutoScroll = true;
			this.splitContainer1.Panel2.BackColor = System.Drawing.Color.LightGray;
			this.splitContainer1.Size = new System.Drawing.Size(750, 526);
			this.splitContainer1.SplitterDistance = 134;
			this.splitContainer1.TabIndex = 1;
			this.splitContainer1.SplitterMoving += new System.Windows.Forms.SplitterCancelEventHandler(this.splitContainer1_SplitterMoving);
			// 
			// dlg_saveFile
			// 
			this.dlg_saveFile.Filter = "TISFAT:Zero Saves (*.tzs)|*.tzs";
			this.dlg_saveFile.Title = "Open File..";
			// 
			// dlg_openFile
			// 
			this.dlg_openFile.Filter = "TISFAT:Zero Saves (*.tzs)|*.tzs";
			// 
			// MainF
			// 
			this.ClientSize = new System.Drawing.Size(750, 550);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.mnu_strip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.mnu_strip1;
			this.MaximumSize = new System.Drawing.Size(2000, 2000);
			this.MinimumSize = new System.Drawing.Size(630, 500);
			this.Name = "MainF";
			this.Text = "TISFAT : Zero";
			this.Load += new System.EventHandler(this.MainF_Load);
			this.mnu_strip1.ResumeLayout(false);
			this.mnu_strip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ToolStripMenuItem fileToolStripMenuItem;
		private ToolStripMenuItem newMovieToolStripMenuItem;
		private ToolStripMenuItem openMovieToolStripMenuItem;
		private ToolStripMenuItem saveMovieToolStripMenuItem;
		private ToolStripMenuItem saveAsToolStripMenuItem;
		private ToolStripMenuItem closeMovieToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripMenuItem exportToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator2;
		private ToolStripMenuItem movieToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator3;
		private ToolStripMenuItem exitTISFATToolStripMenuItem;
		private ToolStripMenuItem editToolStripMenuItem;
		private ToolStripMenuItem undoToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator4;
		private ToolStripMenuItem copyFramesetToolStripMenuItem;
		private ToolStripMenuItem pasteFramesetToolStripMenuItem;
		private ToolStripMenuItem insertToolStripMenuItem;
		private ToolStripMenuItem layerToolStripMenuItem;
		private ToolStripMenuItem framesetToolStripMenuItem;
		private ToolStripMenuItem keyframeToolStripMenuItem;
		private ToolStripMenuItem keyframeWithCurrentPoseToolStripMenuItem;
		private ToolStripMenuItem removeToolStripMenuItem;
		private ToolStripMenuItem layerToolStripMenuItem1;
		private ToolStripMenuItem framesetToolStripMenuItem1;
		private ToolStripMenuItem keyframeToolStripMenuItem1;
		private ToolStripMenuItem helpToolStripMenuItem;
		private ToolStripMenuItem helpToolStripMenuItem1;
		private ToolStripSeparator toolStripSeparator5;
		private ToolStripMenuItem aboutToolStripMenuItem;
		private MenuStrip mnu_strip1;
		private ToolStripSeparator toolStripSeparator6;
		private ToolStripMenuItem preferencesToolStripMenuItem;
		private ToolStripMenuItem debugToolStripMenuItem;
		public SplitContainer splitContainer1;
		private ToolStripMenuItem redoToolStripMenuItem;
		private SaveFileDialog dlg_saveFile;
		private OpenFileDialog dlg_openFile;
		private ToolStripMenuItem checkForUpdatesToolStripMenuItem;
	}
}

