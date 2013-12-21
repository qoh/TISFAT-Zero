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

			this.renameBox = new TextBox();
			this.TimelineTimer = new System.Windows.Forms.Timer(this.components);
			this.contextMenuStrip1 = new ContextMenuStrip(this.components);
			this.insertKeyframeToolStripMenuItem = new ToolStripMenuItem();
			this.insertKeyframeWithPoseToolStripMenuItem = new ToolStripMenuItem();
			this.removeKeyframeToolStripMenuItem = new ToolStripMenuItem();
			this.setPoseToPreviousKeyframeToolStripMenuItem = new ToolStripMenuItem();
			this.setPoseToNextKeyframeToolStripMenuItem = new ToolStripMenuItem();
			this.onionSkinningToolStripMenuItem = new ToolStripMenuItem();
			this.deleteLayerToolStripMenuItem = new ToolStripMenuItem();
			this.renameLayerToolStripMenuItem = new ToolStripMenuItem();
			this.moveLayerUpToolStripMenuItem = new ToolStripMenuItem();
			this.moveLayerDownToolStripMenuItem = new ToolStripMenuItem();
			this.gotoFrameToolStripMenuItem = new ToolStripMenuItem();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// glgraphics
			// 
			this.glgraphics.BackColor = System.Drawing.Color.Black;
			this.glgraphics.Location = new System.Drawing.Point(0, 0);
			this.glgraphics.Name = "glgraphics";
			this.glgraphics.Size = new System.Drawing.Size(1920, 1080);
			this.glgraphics.TabIndex = 0;
			this.glgraphics.VSync = false;
			this.glgraphics.MouseDown += new MouseEventHandler(this.Timeline_MouseDown);
			this.glgraphics.MouseMove += new MouseEventHandler(this.Timeline_MouseMove);
			this.glgraphics.MouseUp += new MouseEventHandler(this.Timeline_MouseUp);
			this.glgraphics.MouseWheel += new MouseEventHandler(this.OnMouseWheel);
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
			this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] {
            this.insertKeyframeToolStripMenuItem,
            this.insertKeyframeWithPoseToolStripMenuItem,
            this.removeKeyframeToolStripMenuItem,
            this.setPoseToPreviousKeyframeToolStripMenuItem,
            this.setPoseToNextKeyframeToolStripMenuItem,
            this.onionSkinningToolStripMenuItem,
            this.deleteLayerToolStripMenuItem,
            this.renameLayerToolStripMenuItem,
            this.moveLayerUpToolStripMenuItem,
            this.moveLayerDownToolStripMenuItem,
            this.gotoFrameToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(234, 268);
			this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
			// 
			// insertKeyframeToolStripMenuItem
			// 
			this.insertKeyframeToolStripMenuItem.Name = "insertKeyframeToolStripMenuItem";
			this.insertKeyframeToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.insertKeyframeToolStripMenuItem.Text = "Insert Keyframe";
			// 
			// insertKeyframeWithPoseToolStripMenuItem
			// 
			this.insertKeyframeWithPoseToolStripMenuItem.Name = "insertKeyframeWithPoseToolStripMenuItem";
			this.insertKeyframeWithPoseToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.insertKeyframeWithPoseToolStripMenuItem.Text = "Insert Keyframe With Pose";
			// 
			// removeKeyframeToolStripMenuItem
			// 
			this.removeKeyframeToolStripMenuItem.Name = "removeKeyframeToolStripMenuItem";
			this.removeKeyframeToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.removeKeyframeToolStripMenuItem.Text = "Remove Keyframe";
			// 
			// setPoseToPreviousKeyframeToolStripMenuItem
			// 
			this.setPoseToPreviousKeyframeToolStripMenuItem.Name = "setPoseToPreviousKeyframeToolStripMenuItem";
			this.setPoseToPreviousKeyframeToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.setPoseToPreviousKeyframeToolStripMenuItem.Text = "Set Pose to Previous Keyframe";
			// 
			// setPoseToNextKeyframeToolStripMenuItem
			// 
			this.setPoseToNextKeyframeToolStripMenuItem.Name = "setPoseToNextKeyframeToolStripMenuItem";
			this.setPoseToNextKeyframeToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.setPoseToNextKeyframeToolStripMenuItem.Text = "Set Pose to Next Keyframe";
			// 
			// onionSkinningToolStripMenuItem
			// 
			this.onionSkinningToolStripMenuItem.Name = "onionSkinningToolStripMenuItem";
			this.onionSkinningToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.onionSkinningToolStripMenuItem.Text = "Onion Skinning ..";
			// 
			// deleteLayerToolStripMenuItem
			// 
			this.deleteLayerToolStripMenuItem.Name = "deleteLayerToolStripMenuItem";
			this.deleteLayerToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.deleteLayerToolStripMenuItem.Text = "Delete Layer";
			// 
			// renameLayerToolStripMenuItem
			// 
			this.renameLayerToolStripMenuItem.Name = "renameLayerToolStripMenuItem";
			this.renameLayerToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.renameLayerToolStripMenuItem.Text = "Rename Layer";
			// 
			// moveLayerUpToolStripMenuItem
			// 
			this.moveLayerUpToolStripMenuItem.Name = "moveLayerUpToolStripMenuItem";
			this.moveLayerUpToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.moveLayerUpToolStripMenuItem.Text = "Move Layer Up";
			// 
			// moveLayerDownToolStripMenuItem
			// 
			this.moveLayerDownToolStripMenuItem.Name = "moveLayerDownToolStripMenuItem";
			this.moveLayerDownToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.moveLayerDownToolStripMenuItem.Text = "Move Layer Down";
			// 
			// gotoFrameToolStripMenuItem
			// 
			this.gotoFrameToolStripMenuItem.Name = "gotoFrameToolStripMenuItem";
			this.gotoFrameToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.gotoFrameToolStripMenuItem.Text = "Goto Frame #";
			// 
			// Timeline
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(128, 128);
			this.ContextMenuStrip = this.contextMenuStrip1;
			this.ControlBox = false;
			this.Controls.Add(this.renameBox);
			this.Controls.Add(this.glgraphics);
			this.FormBorderStyle = FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Timeline";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = FormStartPosition.Manual;
			this.Text = "TIMELINE";
			this.Load += new System.EventHandler(this.Timeline_Load);
			this.Paint += new PaintEventHandler(this.Timeline_Paint);
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

	}
}