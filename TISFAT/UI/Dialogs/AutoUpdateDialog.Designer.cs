namespace TISFAT
{
	partial class AutoUpdateDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoUpdateDialog));
			this.btn_updateNow = new System.Windows.Forms.Button();
			this.btn_updateOnClose = new System.Windows.Forms.Button();
			this.btn_noThanks = new System.Windows.Forms.Button();
			this.rtb_updateDesc = new System.Windows.Forms.RichTextBox();
			this.lbl_updateAvailable = new System.Windows.Forms.Label();
			this.lbl_Version = new System.Windows.Forms.Label();
			this.pnl_updateDesc = new System.Windows.Forms.Panel();
			this.pnl_updateDesc.SuspendLayout();
			this.SuspendLayout();
			// 
			// btn_updateNow
			// 
			this.btn_updateNow.Location = new System.Drawing.Point(318, 29);
			this.btn_updateNow.Name = "btn_updateNow";
			this.btn_updateNow.Size = new System.Drawing.Size(116, 23);
			this.btn_updateNow.TabIndex = 0;
			this.btn_updateNow.Text = "Update Now";
			this.btn_updateNow.UseVisualStyleBackColor = true;
			this.btn_updateNow.Click += new System.EventHandler(this.btn_updateNow_Click);
			// 
			// btn_updateOnClose
			// 
			this.btn_updateOnClose.Location = new System.Drawing.Point(318, 58);
			this.btn_updateOnClose.Name = "btn_updateOnClose";
			this.btn_updateOnClose.Size = new System.Drawing.Size(116, 23);
			this.btn_updateOnClose.TabIndex = 1;
			this.btn_updateOnClose.Text = "Update on Close";
			this.btn_updateOnClose.UseVisualStyleBackColor = true;
			this.btn_updateOnClose.Click += new System.EventHandler(this.btn_updateOnClose_Click);
			// 
			// btn_noThanks
			// 
			this.btn_noThanks.Location = new System.Drawing.Point(318, 218);
			this.btn_noThanks.Name = "btn_noThanks";
			this.btn_noThanks.Size = new System.Drawing.Size(116, 23);
			this.btn_noThanks.TabIndex = 2;
			this.btn_noThanks.Text = "No Thanks";
			this.btn_noThanks.UseVisualStyleBackColor = true;
			this.btn_noThanks.Click += new System.EventHandler(this.btn_noThanks_Click);
			// 
			// rtb_updateDesc
			// 
			this.rtb_updateDesc.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.rtb_updateDesc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtb_updateDesc.Location = new System.Drawing.Point(1, 1);
			this.rtb_updateDesc.Name = "rtb_updateDesc";
			this.rtb_updateDesc.Size = new System.Drawing.Size(298, 210);
			this.rtb_updateDesc.TabIndex = 3;
			this.rtb_updateDesc.Text = resources.GetString("rtb_updateDesc.Text");
			// 
			// lbl_updateAvailable
			// 
			this.lbl_updateAvailable.Location = new System.Drawing.Point(12, 9);
			this.lbl_updateAvailable.Name = "lbl_updateAvailable";
			this.lbl_updateAvailable.Size = new System.Drawing.Size(300, 17);
			this.lbl_updateAvailable.TabIndex = 4;
			this.lbl_updateAvailable.Text = "There\'s an update available for download!  Changes include:\r\n";
			this.lbl_updateAvailable.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// lbl_Version
			// 
			this.lbl_Version.AutoSize = true;
			this.lbl_Version.Location = new System.Drawing.Point(325, 99);
			this.lbl_Version.Name = "lbl_Version";
			this.lbl_Version.Size = new System.Drawing.Size(103, 26);
			this.lbl_Version.TabIndex = 5;
			this.lbl_Version.Text = "Your Version: v1.4.5\r\nNew Version: v1.4.5\r\n";
			// 
			// pnl_updateDesc
			// 
			this.pnl_updateDesc.BackColor = System.Drawing.SystemColors.ButtonShadow;
			this.pnl_updateDesc.Controls.Add(this.rtb_updateDesc);
			this.pnl_updateDesc.Location = new System.Drawing.Point(12, 29);
			this.pnl_updateDesc.Name = "pnl_updateDesc";
			this.pnl_updateDesc.Padding = new System.Windows.Forms.Padding(1);
			this.pnl_updateDesc.Size = new System.Drawing.Size(300, 212);
			this.pnl_updateDesc.TabIndex = 6;
			// 
			// AutoUpdateDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(444, 251);
			this.ControlBox = false;
			this.Controls.Add(this.pnl_updateDesc);
			this.Controls.Add(this.lbl_Version);
			this.Controls.Add(this.lbl_updateAvailable);
			this.Controls.Add(this.btn_noThanks);
			this.Controls.Add(this.btn_updateOnClose);
			this.Controls.Add(this.btn_updateNow);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "AutoUpdateDialog";
			this.Text = "Auto Update";
			this.pnl_updateDesc.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btn_updateNow;
		private System.Windows.Forms.Button btn_updateOnClose;
		private System.Windows.Forms.Button btn_noThanks;
		private System.Windows.Forms.RichTextBox rtb_updateDesc;
		private System.Windows.Forms.Label lbl_updateAvailable;
		private System.Windows.Forms.Label lbl_Version;
		private System.Windows.Forms.Panel pnl_updateDesc;
	}
}