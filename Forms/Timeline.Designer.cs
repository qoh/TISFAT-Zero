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
			this.aSDFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fDSAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.aSDFToolStripMenuItem,
            this.fDSAToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(103, 48);
			// 
			// aSDFToolStripMenuItem
			// 
			this.aSDFToolStripMenuItem.Name = "aSDFToolStripMenuItem";
			this.aSDFToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
			this.aSDFToolStripMenuItem.Text = "ASDF";
			// 
			// fDSAToolStripMenuItem
			// 
			this.fDSAToolStripMenuItem.Name = "fDSAToolStripMenuItem";
			this.fDSAToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
			this.fDSAToolStripMenuItem.Text = "FDSA";
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
		private ToolStripMenuItem aSDFToolStripMenuItem;
		private ToolStripMenuItem fDSAToolStripMenuItem;

	}
}