namespace TISFAT_ZERO.Forms
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
			this.components = new System.ComponentModel.Container();
			this.label1 = new System.Windows.Forms.Label();
			this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
			this.btn_downloadButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.btn_cancelButton = new System.Windows.Forms.Button();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.rtxt_changelog = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(31, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(272, 42);
			this.label1.TabIndex = 0;
			this.label1.Text = "There\'s a new version of TISFAT : Zero\r\nAvailable for download!\r\n";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label1.UseMnemonic = false;
			// 
			// btn_downloadButton
			// 
			this.btn_downloadButton.Location = new System.Drawing.Point(247, 213);
			this.btn_downloadButton.Name = "btn_downloadButton";
			this.btn_downloadButton.Size = new System.Drawing.Size(75, 23);
			this.btn_downloadButton.TabIndex = 5;
			this.btn_downloadButton.Text = "Download";
			this.btn_downloadButton.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 67);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(61, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Changelog:";
			// 
			// btn_cancelButton
			// 
			this.btn_cancelButton.Location = new System.Drawing.Point(16, 213);
			this.btn_cancelButton.Name = "btn_cancelButton";
			this.btn_cancelButton.Size = new System.Drawing.Size(75, 23);
			this.btn_cancelButton.TabIndex = 6;
			this.btn_cancelButton.Text = "Shaddup pls";
			this.btn_cancelButton.UseVisualStyleBackColor = true;
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// rtxt_changelog
			// 
			this.rtxt_changelog.BackColor = System.Drawing.Color.White;
			this.rtxt_changelog.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.rtxt_changelog.Location = new System.Drawing.Point(16, 83);
			this.rtxt_changelog.Name = "rtxt_changelog";
			this.rtxt_changelog.ReadOnly = true;
			this.rtxt_changelog.Size = new System.Drawing.Size(306, 124);
			this.rtxt_changelog.TabIndex = 7;
			this.rtxt_changelog.Text = "";
			// 
			// UpdateNotification
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(334, 244);
			this.Controls.Add(this.rtxt_changelog);
			this.Controls.Add(this.btn_cancelButton);
			this.Controls.Add(this.btn_downloadButton);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "UpdateNotification";
			this.Text = "There\'s an update available!";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.IO.Ports.SerialPort serialPort1;
		private System.Windows.Forms.Button btn_downloadButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btn_cancelButton;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.RichTextBox rtxt_changelog;
	}
}