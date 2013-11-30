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
		/// </summary>
		private void InitializeComponent()
		{
			this.glgraphics = new OpenTK.GLControl();
			this.SuspendLayout();
			// 
			// glgraphics
			// 
			this.glgraphics.BackColor = System.Drawing.Color.Black;
			this.glgraphics.Location = new System.Drawing.Point(0, 0);
			this.glgraphics.Name = "glgraphics";
			this.glgraphics.Size = new System.Drawing.Size(127, 129);
			this.glgraphics.TabIndex = 0;
			this.glgraphics.VSync = false;
			// 
			// Timeline
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(128, 128);
			this.ControlBox = false;
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
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Timeline_MouseDown);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Timeline_MouseMove);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Timeline_MouseUp);
			this.Resize += new System.EventHandler(this.Timeline_Resize);
			this.ResumeLayout(false);

		}

		#endregion

        private OpenTK.GLControl glgraphics;

	}
}