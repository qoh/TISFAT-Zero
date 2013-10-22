namespace T0Updater
{
	partial class Downloader
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
			this.lbl_DlTitle = new System.Windows.Forms.Label();
			this.pgr_totalProgress = new System.Windows.Forms.ProgressBar();
			this.pgr_fileProgress = new System.Windows.Forms.ProgressBar();
			this.lbl_DlSpeed = new System.Windows.Forms.Label();
			this.lbl_TimeRemaining = new System.Windows.Forms.Label();
			this.lbl_p = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lbl_DlTitle
			// 
			this.lbl_DlTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.lbl_DlTitle.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_DlTitle.Location = new System.Drawing.Point(12, 5);
			this.lbl_DlTitle.Name = "lbl_DlTitle";
			this.lbl_DlTitle.Size = new System.Drawing.Size(385, 20);
			this.lbl_DlTitle.TabIndex = 0;
			this.lbl_DlTitle.Tag = "";
			this.lbl_DlTitle.Text = "Now Downloading: ";
			this.lbl_DlTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pgr_totalProgress
			// 
			this.pgr_totalProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pgr_totalProgress.Location = new System.Drawing.Point(12, 78);
			this.pgr_totalProgress.Name = "pgr_totalProgress";
			this.pgr_totalProgress.Size = new System.Drawing.Size(385, 23);
			this.pgr_totalProgress.TabIndex = 2;
			// 
			// pgr_fileProgress
			// 
			this.pgr_fileProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pgr_fileProgress.Location = new System.Drawing.Point(12, 107);
			this.pgr_fileProgress.Name = "pgr_fileProgress";
			this.pgr_fileProgress.Size = new System.Drawing.Size(385, 23);
			this.pgr_fileProgress.TabIndex = 3;
			// 
			// lbl_DlSpeed
			// 
			this.lbl_DlSpeed.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lbl_DlSpeed.AutoSize = true;
			this.lbl_DlSpeed.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_DlSpeed.Location = new System.Drawing.Point(41, 51);
			this.lbl_DlSpeed.Name = "lbl_DlSpeed";
			this.lbl_DlSpeed.Size = new System.Drawing.Size(99, 13);
			this.lbl_DlSpeed.TabIndex = 4;
			this.lbl_DlSpeed.Text = "Download Speed:";
			// 
			// lbl_TimeRemaining
			// 
			this.lbl_TimeRemaining.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lbl_TimeRemaining.AutoSize = true;
			this.lbl_TimeRemaining.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_TimeRemaining.Location = new System.Drawing.Point(215, 51);
			this.lbl_TimeRemaining.Name = "lbl_TimeRemaining";
			this.lbl_TimeRemaining.Size = new System.Drawing.Size(117, 13);
			this.lbl_TimeRemaining.TabIndex = 5;
			this.lbl_TimeRemaining.Text = "Approximate DL Time:";
			// 
			// lbl_p
			// 
			this.lbl_p.Location = new System.Drawing.Point(12, 34);
			this.lbl_p.Name = "lbl_p";
			this.lbl_p.Size = new System.Drawing.Size(385, 17);
			this.lbl_p.TabIndex = 6;
			this.lbl_p.Text = "Progress: x/x";
			this.lbl_p.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Downloader
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(409, 140);
			this.Controls.Add(this.lbl_p);
			this.Controls.Add(this.lbl_TimeRemaining);
			this.Controls.Add(this.lbl_DlSpeed);
			this.Controls.Add(this.pgr_fileProgress);
			this.Controls.Add(this.pgr_totalProgress);
			this.Controls.Add(this.lbl_DlTitle);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Downloader";
			this.Text = "Updating";
			this.Load += new System.EventHandler(this.Downloader_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lbl_DlTitle;
		private System.Windows.Forms.ProgressBar pgr_totalProgress;
		private System.Windows.Forms.ProgressBar pgr_fileProgress;
		private System.Windows.Forms.Label lbl_DlSpeed;
		private System.Windows.Forms.Label lbl_TimeRemaining;
		private System.Windows.Forms.Label lbl_p;
	}
}