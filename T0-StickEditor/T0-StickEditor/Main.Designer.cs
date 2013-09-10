namespace T0_StickEditor
{
	partial class Main
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
			this.pnl_toolBox = new System.Windows.Forms.Panel();
			this.pnl_Main = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// pnl_toolBox
			// 
			this.pnl_toolBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnl_toolBox.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnl_toolBox.Location = new System.Drawing.Point(0, 0);
			this.pnl_toolBox.Name = "pnl_toolBox";
			this.pnl_toolBox.Size = new System.Drawing.Size(722, 40);
			this.pnl_toolBox.TabIndex = 0;
			// 
			// pnl_Main
			// 
			this.pnl_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnl_Main.Location = new System.Drawing.Point(0, 40);
			this.pnl_Main.Name = "pnl_Main";
			this.pnl_Main.Size = new System.Drawing.Size(722, 408);
			this.pnl_Main.TabIndex = 1;
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(722, 448);
			this.Controls.Add(this.pnl_Main);
			this.Controls.Add(this.pnl_toolBox);
			this.Name = "Main";
			this.Text = "Stick Editor";
			this.Load += new System.EventHandler(this.Main_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnl_toolBox;
		private System.Windows.Forms.Panel pnl_Main;
	}
}