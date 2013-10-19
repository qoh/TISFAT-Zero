namespace TISFAT_ZERO
{
	partial class UpdateNotification
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateNotification));
			this.label1 = new System.Windows.Forms.Label();
			this.btn_downloadButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.btn_cancelButton = new System.Windows.Forms.Button();
			this.rtxt_changelog = new System.Windows.Forms.RichTextBox();
			this.lbl_version = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(31, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(272, 42);
			this.label1.TabIndex = 0;
			this.label1.Text = "There\'s a new version of TISFAT : Zero\r\nAvailable for download!\r\n";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label1.UseMnemonic = false;
			// 
			// btn_downloadButton
			// 
			this.btn_downloadButton.Location = new System.Drawing.Point(247, 323);
			this.btn_downloadButton.Name = "btn_downloadButton";
			this.btn_downloadButton.Size = new System.Drawing.Size(75, 23);
			this.btn_downloadButton.TabIndex = 5;
			this.btn_downloadButton.Text = "Download";
			this.btn_downloadButton.UseVisualStyleBackColor = true;
			this.btn_downloadButton.Click += new System.EventHandler(this.btn_downloadButton_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 82);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(110, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Changelog for version";
			// 
			// btn_cancelButton
			// 
			this.btn_cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn_cancelButton.Location = new System.Drawing.Point(12, 324);
			this.btn_cancelButton.Name = "btn_cancelButton";
			this.btn_cancelButton.Size = new System.Drawing.Size(75, 23);
			this.btn_cancelButton.TabIndex = 6;
			this.btn_cancelButton.Text = "Shaddup pls";
			this.btn_cancelButton.UseVisualStyleBackColor = true;
			this.btn_cancelButton.Click += new System.EventHandler(this.btn_cancelButton_Click);
			// 
			// rtxt_changelog
			// 
			this.rtxt_changelog.BackColor = System.Drawing.Color.White;
			this.rtxt_changelog.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.rtxt_changelog.Location = new System.Drawing.Point(12, 98);
			this.rtxt_changelog.Name = "rtxt_changelog";
			this.rtxt_changelog.ReadOnly = true;
			this.rtxt_changelog.Size = new System.Drawing.Size(310, 220);
			this.rtxt_changelog.TabIndex = 7;
			this.rtxt_changelog.Text = "";
			// 
			// lbl_version
			// 
			this.lbl_version.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_version.Location = new System.Drawing.Point(12, 55);
			this.lbl_version.Name = "lbl_version";
			this.lbl_version.Size = new System.Drawing.Size(310, 18);
			this.lbl_version.TabIndex = 8;
			this.lbl_version.Text = "Current Version: v2.0.0.0  New Version: v2.0.0.0";
			this.lbl_version.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// UpdateNotification
			// 
			this.AcceptButton = this.btn_downloadButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btn_cancelButton;
			this.ClientSize = new System.Drawing.Size(334, 351);
			this.Controls.Add(this.lbl_version);
			this.Controls.Add(this.rtxt_changelog);
			this.Controls.Add(this.btn_cancelButton);
			this.Controls.Add(this.btn_downloadButton);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "UpdateNotification";
			this.Text = "There\'s an update available!";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btn_downloadButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btn_cancelButton;
		private System.Windows.Forms.RichTextBox rtxt_changelog;
		private System.Windows.Forms.Label lbl_version;
	}
}