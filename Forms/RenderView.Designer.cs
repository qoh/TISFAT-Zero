namespace TISFAT_ZERO.Forms
{
	partial class RenderView
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
			this.glGraphics = new OpenTK.GLControl();
			this.SuspendLayout();
			// 
			// glGraphics
			// 
			this.glGraphics.BackColor = System.Drawing.Color.Black;
			this.glGraphics.Dock = System.Windows.Forms.DockStyle.Fill;
			this.glGraphics.Location = new System.Drawing.Point(0, 0);
			this.glGraphics.Name = "glGraphics";
			this.glGraphics.Size = new System.Drawing.Size(444, 321);
			this.glGraphics.TabIndex = 0;
			this.glGraphics.VSync = false;
			// 
			// RenderView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(444, 321);
			this.ControlBox = false;
			this.Controls.Add(this.glGraphics);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "RenderView";
			this.Text = "RenderView";
			this.ResumeLayout(false);

		}

		#endregion

		private OpenTK.GLControl glGraphics;
	}
}