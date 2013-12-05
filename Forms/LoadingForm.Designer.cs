namespace TISFAT_Zero.Forms
{
	partial class LoadingForm
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
			this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
			this.loadingBar = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
			this.SuspendLayout();
			// 
			// shapeContainer1
			// 
			this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
			this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
			this.shapeContainer1.Name = "shapeContainer1";
			this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.loadingBar});
			this.shapeContainer1.Size = new System.Drawing.Size(400, 245);
			this.shapeContainer1.TabIndex = 0;
			this.shapeContainer1.TabStop = false;
			// 
			// loadingBar
			// 
			this.loadingBar.BorderColor = System.Drawing.Color.DodgerBlue;
			this.loadingBar.FillColor = System.Drawing.Color.DodgerBlue;
			this.loadingBar.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
			this.loadingBar.Location = new System.Drawing.Point(65, 192);
			this.loadingBar.Name = "loadingBar";
			this.loadingBar.Size = new System.Drawing.Size(75, 18);
			// 
			// LoadingForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.BackgroundImage = global::TISFAT_Zero.Properties.Resources.loading;
			this.ClientSize = new System.Drawing.Size(400, 245);
			this.Controls.Add(this.shapeContainer1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "LoadingForm";
			this.Text = "LoadingForm";
			this.ResumeLayout(false);

		}

		#endregion

		private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
		private Microsoft.VisualBasic.PowerPacks.RectangleShape loadingBar;

	}
}