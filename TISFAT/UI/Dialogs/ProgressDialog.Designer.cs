namespace TISFAT
{
	partial class ProgressDialog
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
			this.prg_Display = new System.Windows.Forms.ProgressBar();
			this.btn_Cancel = new System.Windows.Forms.Button();
			this.lbl_Text = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// prg_Display
			// 
			this.prg_Display.Location = new System.Drawing.Point(12, 44);
			this.prg_Display.Name = "prg_Display";
			this.prg_Display.Size = new System.Drawing.Size(260, 23);
			this.prg_Display.TabIndex = 0;
			// 
			// btn_Cancel
			// 
			this.btn_Cancel.Location = new System.Drawing.Point(105, 76);
			this.btn_Cancel.Name = "btn_Cancel";
			this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
			this.btn_Cancel.TabIndex = 1;
			this.btn_Cancel.Text = "Cancel";
			this.btn_Cancel.UseVisualStyleBackColor = true;
			this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
			// 
			// lbl_Text
			// 
			this.lbl_Text.Location = new System.Drawing.Point(12, 9);
			this.lbl_Text.Name = "lbl_Text";
			this.lbl_Text.Size = new System.Drawing.Size(260, 32);
			this.lbl_Text.TabIndex = 2;
			this.lbl_Text.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ProgressDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 111);
			this.ControlBox = false;
			this.Controls.Add(this.lbl_Text);
			this.Controls.Add(this.btn_Cancel);
			this.Controls.Add(this.prg_Display);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ProgressDialog";
			this.Text = "ProgressDialog";
			this.Shown += new System.EventHandler(this.ProgressDialog_Shown);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ProgressBar prg_Display;
		private System.Windows.Forms.Button btn_Cancel;
		private System.Windows.Forms.Label lbl_Text;
	}
}