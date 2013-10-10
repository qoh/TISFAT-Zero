using System;
using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;

namespace TISFAT_ZERO
{
	partial class Canvas
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
			this.components = new System.ComponentModel.Container();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.moveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyPoseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.applyPoseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.setPoseToPreviousKeyframeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.setPoseToNextKeyframeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.changeMouthStateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.changeFaceDirectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.flipHorizontallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.flipVerticallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.flipLegsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.flipArmsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.GL_GRAPHICS = new OpenTK.GLControl(new GraphicsMode(32, 0, 1, maxaa), 3, 0, GraphicsContextFlags.Default);
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.moveToolStripMenuItem,
			this.copyPoseToolStripMenuItem,
			this.applyPoseToolStripMenuItem,
			this.setPoseToPreviousKeyframeToolStripMenuItem,
			this.setPoseToNextKeyframeToolStripMenuItem,
			this.toolStripSeparator1,
			this.changeMouthStateToolStripMenuItem,
			this.changeFaceDirectionToolStripMenuItem,
			this.toolStripSeparator2,
			this.flipHorizontallyToolStripMenuItem,
			this.flipVerticallyToolStripMenuItem,
			this.flipLegsToolStripMenuItem,
			this.flipArmsToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(233, 258);
			// 
			// moveToolStripMenuItem
			// 
			this.moveToolStripMenuItem.Name = "moveToolStripMenuItem";
			this.moveToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
			this.moveToolStripMenuItem.Text = "Move";
			// 
			// copyPoseToolStripMenuItem
			// 
			this.copyPoseToolStripMenuItem.Name = "copyPoseToolStripMenuItem";
			this.copyPoseToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
			this.copyPoseToolStripMenuItem.Text = "Copy Pose";
			// 
			// applyPoseToolStripMenuItem
			// 
			this.applyPoseToolStripMenuItem.Name = "applyPoseToolStripMenuItem";
			this.applyPoseToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
			this.applyPoseToolStripMenuItem.Text = "Apply Pose";
			// 
			// setPoseToPreviousKeyframeToolStripMenuItem
			// 
			this.setPoseToPreviousKeyframeToolStripMenuItem.Name = "setPoseToPreviousKeyframeToolStripMenuItem";
			this.setPoseToPreviousKeyframeToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
			this.setPoseToPreviousKeyframeToolStripMenuItem.Text = "Set pose to previous keyframe";
			// 
			// setPoseToNextKeyframeToolStripMenuItem
			// 
			this.setPoseToNextKeyframeToolStripMenuItem.Name = "setPoseToNextKeyframeToolStripMenuItem";
			this.setPoseToNextKeyframeToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
			this.setPoseToNextKeyframeToolStripMenuItem.Text = "Set pose to next keyframe";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(229, 6);
			// 
			// changeMouthStateToolStripMenuItem
			// 
			this.changeMouthStateToolStripMenuItem.Name = "changeMouthStateToolStripMenuItem";
			this.changeMouthStateToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
			this.changeMouthStateToolStripMenuItem.Text = "Change mouth state";
			// 
			// changeFaceDirectionToolStripMenuItem
			// 
			this.changeFaceDirectionToolStripMenuItem.Name = "changeFaceDirectionToolStripMenuItem";
			this.changeFaceDirectionToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
			this.changeFaceDirectionToolStripMenuItem.Text = "Change face direction";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(229, 6);
			// 
			// flipHorizontallyToolStripMenuItem
			// 
			this.flipHorizontallyToolStripMenuItem.Name = "flipHorizontallyToolStripMenuItem";
			this.flipHorizontallyToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
			this.flipHorizontallyToolStripMenuItem.Text = "Flip Horizontally";
			// 
			// flipVerticallyToolStripMenuItem
			// 
			this.flipVerticallyToolStripMenuItem.Name = "flipVerticallyToolStripMenuItem";
			this.flipVerticallyToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
			this.flipVerticallyToolStripMenuItem.Text = "Flip Vertically";
			// 
			// flipLegsToolStripMenuItem
			// 
			this.flipLegsToolStripMenuItem.Name = "flipLegsToolStripMenuItem";
			this.flipLegsToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
			this.flipLegsToolStripMenuItem.Text = "Flip Legs";
			this.flipLegsToolStripMenuItem.Click += new System.EventHandler(this.flipLegsToolStripMenuItem_Click);
			// 
			// flipArmsToolStripMenuItem
			// 
			this.flipArmsToolStripMenuItem.Name = "flipArmsToolStripMenuItem";
			this.flipArmsToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
			this.flipArmsToolStripMenuItem.Text = "Flip Arms";
			this.flipArmsToolStripMenuItem.Click += new System.EventHandler(this.flipArmsToolStripMenuItem_Click);
			// 
			// GL_GRAPHICS
			// 
			this.GL_GRAPHICS.BackColor = System.Drawing.Color.Black;
			this.GL_GRAPHICS.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GL_GRAPHICS.Location = new System.Drawing.Point(0, 0);
			this.GL_GRAPHICS.Name = "GL_GRAPHICS";
			this.GL_GRAPHICS.Size = new System.Drawing.Size(444, 321);
			this.GL_GRAPHICS.TabIndex = 1;
			this.GL_GRAPHICS.VSync = false;
			this.GL_GRAPHICS.Paint += new System.Windows.Forms.PaintEventHandler(this.GL_GRAPHICS_Paint);
			this.GL_GRAPHICS.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseDown);
			this.GL_GRAPHICS.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseMove);
			this.GL_GRAPHICS.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseUp);
			// 
			// Canvas
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(444, 321);
			this.ControlBox = false;
			this.Controls.Add(this.GL_GRAPHICS);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "Canvas";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Canvas";
			this.Load += new System.EventHandler(this.Canvas_Load);
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem moveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyPoseToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem applyPoseToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem setPoseToPreviousKeyframeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem setPoseToNextKeyframeToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem changeMouthStateToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem changeFaceDirectionToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem flipHorizontallyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem flipVerticallyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem flipLegsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem flipArmsToolStripMenuItem;
		private GLControl GL_GRAPHICS;
	}
}