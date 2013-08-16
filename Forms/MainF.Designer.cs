using System;
using System.Drawing;
using System.Windows.Forms;

namespace TISFAT_ZERO
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
            this.mnu_strip1 = new System.Windows.Forms.MenuStrip();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawStickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
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
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newMovieToolStripMenuItem
            // 
            this.newMovieToolStripMenuItem.Name = "newMovieToolStripMenuItem";
            this.newMovieToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.newMovieToolStripMenuItem.Text = "New Movie ...";
            // 
            // openMovieToolStripMenuItem
            // 
            this.openMovieToolStripMenuItem.Name = "openMovieToolStripMenuItem";
            this.openMovieToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openMovieToolStripMenuItem.Text = "Open Movie ...";
            // 
            // saveMovieToolStripMenuItem
            // 
            this.saveMovieToolStripMenuItem.Name = "saveMovieToolStripMenuItem";
            this.saveMovieToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveMovieToolStripMenuItem.Text = "Save Movie ...";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveAsToolStripMenuItem.Text = "Save As ...";
            // 
            // closeMovieToolStripMenuItem
            // 
            this.closeMovieToolStripMenuItem.Name = "closeMovieToolStripMenuItem";
            this.closeMovieToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.closeMovieToolStripMenuItem.Text = "Close Movie ...";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(143, 6);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(143, 6);
            // 
            // movieToolStripMenuItem
            // 
            this.movieToolStripMenuItem.Name = "movieToolStripMenuItem";
            this.movieToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.movieToolStripMenuItem.Text = "Movie";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(143, 6);
            // 
            // exitTISFATToolStripMenuItem
            // 
            this.exitTISFATToolStripMenuItem.Name = "exitTISFATToolStripMenuItem";
            this.exitTISFATToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.exitTISFATToolStripMenuItem.Text = "Exit TISFAT";
            this.exitTISFATToolStripMenuItem.Click += new System.EventHandler(this.exitTISFATToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.toolStripSeparator4,
            this.copyFramesetToolStripMenuItem,
            this.pasteFramesetToolStripMenuItem,
            this.toolStripSeparator6,
            this.preferencesToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(146, 6);
            // 
            // copyFramesetToolStripMenuItem
            // 
            this.copyFramesetToolStripMenuItem.Name = "copyFramesetToolStripMenuItem";
            this.copyFramesetToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.copyFramesetToolStripMenuItem.Text = "Copy Frameset";
            // 
            // pasteFramesetToolStripMenuItem
            // 
            this.pasteFramesetToolStripMenuItem.Name = "pasteFramesetToolStripMenuItem";
            this.pasteFramesetToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.pasteFramesetToolStripMenuItem.Text = "Paste Frameset";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(146, 6);
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.preferencesToolStripMenuItem.Text = "Preferences..";
            this.preferencesToolStripMenuItem.Click += new System.EventHandler(this.preferencesToolStripMenuItem_Click);
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
            this.layerToolStripMenuItem.Name = "layerToolStripMenuItem";
            this.layerToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.layerToolStripMenuItem.Text = "Layer ...";
            // 
            // framesetToolStripMenuItem
            // 
            this.framesetToolStripMenuItem.Name = "framesetToolStripMenuItem";
            this.framesetToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.framesetToolStripMenuItem.Text = "Frameset";
            // 
            // keyframeToolStripMenuItem
            // 
            this.keyframeToolStripMenuItem.Name = "keyframeToolStripMenuItem";
            this.keyframeToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.keyframeToolStripMenuItem.Text = "Keyframe";
            // 
            // keyframeWithCurrentPoseToolStripMenuItem
            // 
            this.keyframeWithCurrentPoseToolStripMenuItem.Name = "keyframeWithCurrentPoseToolStripMenuItem";
            this.keyframeWithCurrentPoseToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.keyframeWithCurrentPoseToolStripMenuItem.Text = "Keyframe with current pose";
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.layerToolStripMenuItem1,
            this.framesetToolStripMenuItem1,
            this.keyframeToolStripMenuItem1});
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.removeToolStripMenuItem.Text = "Remove";
            // 
            // layerToolStripMenuItem1
            // 
            this.layerToolStripMenuItem1.Name = "layerToolStripMenuItem1";
            this.layerToolStripMenuItem1.Size = new System.Drawing.Size(120, 22);
            this.layerToolStripMenuItem1.Text = "Layer";
            // 
            // framesetToolStripMenuItem1
            // 
            this.framesetToolStripMenuItem1.Name = "framesetToolStripMenuItem1";
            this.framesetToolStripMenuItem1.Size = new System.Drawing.Size(120, 22);
            this.framesetToolStripMenuItem1.Text = "Frameset";
            // 
            // keyframeToolStripMenuItem1
            // 
            this.keyframeToolStripMenuItem1.Name = "keyframeToolStripMenuItem1";
            this.keyframeToolStripMenuItem1.Size = new System.Drawing.Size(120, 22);
            this.keyframeToolStripMenuItem1.Text = "Keyframe";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem1,
            this.toolStripSeparator5,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(103, 22);
            this.helpToolStripMenuItem1.Text = "Help";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(100, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.aboutToolStripMenuItem.Text = "About";
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
            this.mnu_strip1.Size = new System.Drawing.Size(642, 24);
            this.mnu_strip1.TabIndex = 0;
            this.mnu_strip1.Text = "menuStrip1";
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.drawStickToolStripMenuItem});
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.debugToolStripMenuItem.Text = "Debug";
            // 
            // drawStickToolStripMenuItem
            // 
            this.drawStickToolStripMenuItem.Name = "drawStickToolStripMenuItem";
            this.drawStickToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.drawStickToolStripMenuItem.Text = "Draw Stick";
            this.drawStickToolStripMenuItem.Click += new System.EventHandler(this.drawStickToolStripMenuItem_Click);
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
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.White;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.LightGray;
            this.splitContainer1.Size = new System.Drawing.Size(642, 510);
            this.splitContainer1.SplitterDistance = 135;
            this.splitContainer1.TabIndex = 1;
            // 
            // MainF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 534);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.mnu_strip1);
            this.MainMenuStrip = this.mnu_strip1;
            this.MaximumSize = new System.Drawing.Size(2000, 2000);
            this.MinimumSize = new System.Drawing.Size(650, 450);
            this.Name = "MainF";
            this.Text = "TISFAT : Zero";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResizeEnd += new System.EventHandler(this.Main_ResizeEnd);
            this.mnu_strip1.ResumeLayout(false);
            this.mnu_strip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newMovieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openMovieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMovieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeMovieToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem movieToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem exitTISFATToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem copyFramesetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteFramesetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem layerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem framesetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem keyframeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem keyframeWithCurrentPoseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem layerToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem framesetToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem keyframeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.MenuStrip mnu_strip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drawStickToolStripMenuItem;
		public System.Windows.Forms.SplitContainer splitContainer1;
    }
}

