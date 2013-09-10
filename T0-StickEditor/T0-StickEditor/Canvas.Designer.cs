using System;
using System.Drawing;
using System.Windows.Forms;

namespace T0_StickEditor
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
			this.SuspendLayout();
			// 
			// Canvas
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.ClientSize = new System.Drawing.Size(0, 0);
			this.ControlBox = false;
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Canvas";
			this.Text = "Stick Editor";
			this.Load += new System.EventHandler(this.Canvas_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.Canvas_Paint);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseDown);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseMove);
			this.ResumeLayout(false);

		}

		#endregion
	}
}

