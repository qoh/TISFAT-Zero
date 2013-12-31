using System.Windows.Forms;
using System.Threading;

namespace TISFAT_Zero
{
	partial class Timeline
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
		/// this.glgraphics = new OpenTK.GLControl(new OpenTK.Graphics.GraphicsMode(new OpenTK.Graphics.ColorFormat(32), 0, 1, 0));
		/// if (glgraphics.GraphicsMode == null) ; //For some reason the graphicsmode variable only shows up when it's accessed in some way.. so if we don't do this it'll screw up.
		/// this.glgraphics.Size = Screen.PrimaryScreen.Bounds.Size;
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();

			this.glgraphics = new OpenTK.GLControl(new OpenTK.Graphics.GraphicsMode(new OpenTK.Graphics.ColorFormat(32), 0, 1, 0));
			if (glgraphics.GraphicsMode == null) ; //For some reason the graphicsmode variable only shows up when it's accessed in some way.. so if we don't do this it'll screw up.

			this.renameBox = new System.Windows.Forms.TextBox();
			this.TimelineTimer = new System.Windows.Forms.Timer(this.components);
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.insertKeyframeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.insertKeyframeWithPoseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.removeKeyframeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.insertFramesetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.removeFramesetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.moveAllFramesetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.setPoseToPreviousKeyframeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.setPoseToNextKeyframeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.deleteLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.renameLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.moveLayerUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.moveLayerDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.onionSkinningToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.gotoFrameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// glgraphics
			// 
			this.glgraphics.BackColor = System.Drawing.Color.Black;
			this.glgraphics.Location = new System.Drawing.Point(0, 0);
			this.glgraphics.Name = "glgraphics";
			this.glgraphics.Size = Screen.PrimaryScreen.Bounds.Size;
			this.glgraphics.TabIndex = 0;
			this.glgraphics.VSync = false;
			this.glgraphics.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Timeline_MouseDown);
			this.glgraphics.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Timeline_MouseMove);
			this.glgraphics.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Timeline_MouseUp);
			this.glgraphics.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.OnMouseWheel);
			// 
			// renameBox
			// 
			this.renameBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.renameBox.Location = new System.Drawing.Point(16, 36);
			this.renameBox.MaxLength = 256;
			this.renameBox.Name = "renameBox";
			this.renameBox.Size = new System.Drawing.Size(100, 22);
			this.renameBox.TabIndex = 1;
			this.renameBox.Visible = false;
			// 
			// TimelineTimer
			// 
			this.TimelineTimer.Tick += new System.EventHandler(this.TimelineTimer_Tick);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertKeyframeToolStripMenuItem,
            this.insertKeyframeWithPoseToolStripMenuItem,
            this.removeKeyframeToolStripMenuItem,
            this.toolStripSeparator1,
            this.insertFramesetToolStripMenuItem,
            this.removeFramesetToolStripMenuItem,
            this.moveAllFramesetsToolStripMenuItem,
            this.toolStripSeparator3,
            this.setPoseToPreviousKeyframeToolStripMenuItem,
            this.setPoseToNextKeyframeToolStripMenuItem,
            this.toolStripSeparator2,
            this.deleteLayerToolStripMenuItem,
            this.renameLayerToolStripMenuItem,
            this.toolStripSeparator4,
            this.moveLayerUpToolStripMenuItem,
            this.moveLayerDownToolStripMenuItem,
            this.toolStripSeparator5,
            this.onionSkinningToolStripMenuItem,
            this.gotoFrameToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.ShowImageMargin = false;
			this.contextMenuStrip1.Size = new System.Drawing.Size(209, 364);
			this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
			this.contextMenuStrip1.Opened += new System.EventHandler(this.contextMenuStrip1_Opened);
			// 
			// insertKeyframeToolStripMenuItem
			// 
			this.insertKeyframeToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.insertKeyframeToolStripMenuItem.Name = "insertKeyframeToolStripMenuItem";
			this.insertKeyframeToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
			this.insertKeyframeToolStripMenuItem.Tag = "0;insertnorm";
			this.insertKeyframeToolStripMenuItem.Text = "Insert Keyframe";
			this.insertKeyframeToolStripMenuItem.Click += new System.EventHandler(this.YayyyToolStripItem_ClickEvent);
			// 
			// insertKeyframeWithPoseToolStripMenuItem
			// 
			this.insertKeyframeWithPoseToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.insertKeyframeWithPoseToolStripMenuItem.Name = "insertKeyframeWithPoseToolStripMenuItem";
			this.insertKeyframeWithPoseToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
			this.insertKeyframeWithPoseToolStripMenuItem.Tag = "0;insertpose";
			this.insertKeyframeWithPoseToolStripMenuItem.Text = "Insert Keyframe With Pose";
			this.insertKeyframeWithPoseToolStripMenuItem.Click += new System.EventHandler(this.YayyyToolStripItem_ClickEvent);
			// 
			// removeKeyframeToolStripMenuItem
			// 
			this.removeKeyframeToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.removeKeyframeToolStripMenuItem.Name = "removeKeyframeToolStripMenuItem";
			this.removeKeyframeToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
			this.removeKeyframeToolStripMenuItem.Tag = "0;remove";
			this.removeKeyframeToolStripMenuItem.Text = "Remove Keyframe";
			this.removeKeyframeToolStripMenuItem.Click += new System.EventHandler(this.YayyyToolStripItem_ClickEvent);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(205, 6);
			this.toolStripSeparator1.Tag = "0";
			this.toolStripSeparator1.Enabled = false;
			// 
			// insertFramesetToolStripMenuItem
			// 
			this.insertFramesetToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.insertFramesetToolStripMenuItem.Name = "insertFramesetToolStripMenuItem";
			this.insertFramesetToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
			this.insertFramesetToolStripMenuItem.Tag = "0;insertset";
			this.insertFramesetToolStripMenuItem.Text = "Insert Frameset";
			this.insertFramesetToolStripMenuItem.Click += new System.EventHandler(this.YayyyToolStripItem_ClickEvent);
			// 
			// removeFramesetToolStripMenuItem
			// 
			this.removeFramesetToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.removeFramesetToolStripMenuItem.Name = "removeFramesetToolStripMenuItem";
			this.removeFramesetToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
			this.removeFramesetToolStripMenuItem.Tag = "0;removeset";
			this.removeFramesetToolStripMenuItem.Text = "Remove Frameset";
			this.removeFramesetToolStripMenuItem.Click += new System.EventHandler(this.YayyyToolStripItem_ClickEvent);
			// 
			// moveAllFramesetsToolStripMenuItem
			// 
			this.moveAllFramesetsToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.moveAllFramesetsToolStripMenuItem.Name = "moveAllFramesetsToolStripMenuItem";
			this.moveAllFramesetsToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
			this.moveAllFramesetsToolStripMenuItem.Tag = "0;moveall";
			this.moveAllFramesetsToolStripMenuItem.Text = "Move All Framesets";
			this.moveAllFramesetsToolStripMenuItem.Click += new System.EventHandler(this.YayyyToolStripItem_ClickEvent);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(205, 6);
			this.toolStripSeparator3.Tag = "0";
			this.toolStripSeparator3.Enabled = false;
			// 
			// setPoseToPreviousKeyframeToolStripMenuItem
			// 
			this.setPoseToPreviousKeyframeToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.setPoseToPreviousKeyframeToolStripMenuItem.Name = "setPoseToPreviousKeyframeToolStripMenuItem";
			this.setPoseToPreviousKeyframeToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
			this.setPoseToPreviousKeyframeToolStripMenuItem.Tag = "0;poseprevious";
			this.setPoseToPreviousKeyframeToolStripMenuItem.Text = "Set Pose to Previous Keyframe";
			this.setPoseToPreviousKeyframeToolStripMenuItem.Click += new System.EventHandler(this.YayyyToolStripItem_ClickEvent);
			// 
			// setPoseToNextKeyframeToolStripMenuItem
			// 
			this.setPoseToNextKeyframeToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.setPoseToNextKeyframeToolStripMenuItem.Name = "setPoseToNextKeyframeToolStripMenuItem";
			this.setPoseToNextKeyframeToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
			this.setPoseToNextKeyframeToolStripMenuItem.Tag = "0;posenext";
			this.setPoseToNextKeyframeToolStripMenuItem.Text = "Set Pose to Next Keyframe";
			this.setPoseToNextKeyframeToolStripMenuItem.Click += new System.EventHandler(this.YayyyToolStripItem_ClickEvent);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(205, 6);
			this.toolStripSeparator2.Tag = "0";
			this.toolStripSeparator2.Enabled = false;
			// 
			// deleteLayerToolStripMenuItem
			// 
			this.deleteLayerToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.deleteLayerToolStripMenuItem.Name = "deleteLayerToolStripMenuItem";
			this.deleteLayerToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
			this.deleteLayerToolStripMenuItem.Tag = "01;deletelayer";
			this.deleteLayerToolStripMenuItem.Text = "Delete Layer";
			this.deleteLayerToolStripMenuItem.Click += new System.EventHandler(this.YayyyToolStripItem_ClickEvent);
			// 
			// renameLayerToolStripMenuItem
			// 
			this.renameLayerToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.renameLayerToolStripMenuItem.Name = "renameLayerToolStripMenuItem";
			this.renameLayerToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
			this.renameLayerToolStripMenuItem.Tag = "01;rename";
			this.renameLayerToolStripMenuItem.Text = "Rename Layer";
			this.renameLayerToolStripMenuItem.Click += new System.EventHandler(this.YayyyToolStripItem_ClickEvent);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(205, 6);
			this.toolStripSeparator4.Tag = "01";
			this.toolStripSeparator4.Enabled = false;
			// 
			// moveLayerUpToolStripMenuItem
			// 
			this.moveLayerUpToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.moveLayerUpToolStripMenuItem.Name = "moveLayerUpToolStripMenuItem";
			this.moveLayerUpToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
			this.moveLayerUpToolStripMenuItem.Tag = "01;moveup";
			this.moveLayerUpToolStripMenuItem.Text = "Move Layer Up";
			this.moveLayerUpToolStripMenuItem.Click += new System.EventHandler(this.YayyyToolStripItem_ClickEvent);
			// 
			// moveLayerDownToolStripMenuItem
			// 
			this.moveLayerDownToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.moveLayerDownToolStripMenuItem.Name = "moveLayerDownToolStripMenuItem";
			this.moveLayerDownToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
			this.moveLayerDownToolStripMenuItem.Tag = "01;movedown";
			this.moveLayerDownToolStripMenuItem.Text = "Move Layer Down";
			this.moveLayerDownToolStripMenuItem.Click += new System.EventHandler(this.YayyyToolStripItem_ClickEvent);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(205, 6);
			this.toolStripSeparator5.Tag = "01";
			this.toolStripSeparator5.Enabled = false;
			// 
			// onionSkinningToolStripMenuItem
			// 
			this.onionSkinningToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.onionSkinningToolStripMenuItem.Name = "onionSkinningToolStripMenuItem";
			this.onionSkinningToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
			this.onionSkinningToolStripMenuItem.Tag = "013;onion";
			this.onionSkinningToolStripMenuItem.Text = "Onion Skinning ..";
			this.onionSkinningToolStripMenuItem.Click += new System.EventHandler(this.YayyyToolStripItem_ClickEvent);
			// 
			// gotoFrameToolStripMenuItem
			// 
			this.gotoFrameToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.gotoFrameToolStripMenuItem.Name = "gotoFrameToolStripMenuItem";
			this.gotoFrameToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
			this.gotoFrameToolStripMenuItem.Tag = "3;goto";
			this.gotoFrameToolStripMenuItem.Text = "Goto Frame #";
			this.gotoFrameToolStripMenuItem.Click += new System.EventHandler(this.YayyyToolStripItem_ClickEvent);
			// 
			// Timeline
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(128, 128);
			this.ContextMenuStrip = this.contextMenuStrip1;
			this.ControlBox = false;
			this.Controls.Add(this.renameBox);
			this.Controls.Add(this.glgraphics);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Timeline";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "TIMELINE";
			this.Load += new System.EventHandler(this.Timeline_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.Timeline_Paint);
			this.Resize += new System.EventHandler(this.Timeline_Resize);
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenTK.GLControl glgraphics;
		private TextBox renameBox;
		private System.Windows.Forms.Timer TimelineTimer;
		private ContextMenuStrip contextMenuStrip1;
		private ToolStripMenuItem insertKeyframeToolStripMenuItem;
		private ToolStripMenuItem insertKeyframeWithPoseToolStripMenuItem;
		private ToolStripMenuItem removeKeyframeToolStripMenuItem;
		private ToolStripMenuItem setPoseToPreviousKeyframeToolStripMenuItem;
		private ToolStripMenuItem setPoseToNextKeyframeToolStripMenuItem;
		private ToolStripMenuItem onionSkinningToolStripMenuItem;
		private ToolStripMenuItem deleteLayerToolStripMenuItem;
		private ToolStripMenuItem renameLayerToolStripMenuItem;
		private ToolStripMenuItem moveLayerUpToolStripMenuItem;
		private ToolStripMenuItem moveLayerDownToolStripMenuItem;
		private ToolStripMenuItem gotoFrameToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripMenuItem insertFramesetToolStripMenuItem;
		private ToolStripMenuItem removeFramesetToolStripMenuItem;
		private ToolStripMenuItem moveAllFramesetsToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator3;
		private ToolStripSeparator toolStripSeparator2;
		private ToolStripSeparator toolStripSeparator4;
		private ToolStripSeparator toolStripSeparator5;

	}
}