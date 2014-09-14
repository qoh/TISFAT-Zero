namespace TISFAT_ZERO.Forms.Dialogs
{
	partial class CanvasSizePrompt
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
			this.num_Width = new System.Windows.Forms.NumericUpDown();
			this.num_Height = new System.Windows.Forms.NumericUpDown();
			this.lbl_Width = new System.Windows.Forms.Label();
			this.lbl_Height = new System.Windows.Forms.Label();
			this.btn_OK = new System.Windows.Forms.Button();
			this.btn_Cancel = new System.Windows.Forms.Button();
			this.lbl_default = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.num_Width)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.num_Height)).BeginInit();
			this.SuspendLayout();
			// 
			// num_Width
			// 
			this.num_Width.Location = new System.Drawing.Point(77, 25);
			this.num_Width.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.num_Width.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.num_Width.Name = "num_Width";
			this.num_Width.Size = new System.Drawing.Size(53, 20);
			this.num_Width.TabIndex = 0;
			this.num_Width.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// num_Height
			// 
			this.num_Height.Location = new System.Drawing.Point(158, 25);
			this.num_Height.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.num_Height.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.num_Height.Name = "num_Height";
			this.num_Height.Size = new System.Drawing.Size(53, 20);
			this.num_Height.TabIndex = 1;
			this.num_Height.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// lbl_Width
			// 
			this.lbl_Width.AutoSize = true;
			this.lbl_Width.Location = new System.Drawing.Point(74, 9);
			this.lbl_Width.Name = "lbl_Width";
			this.lbl_Width.Size = new System.Drawing.Size(35, 13);
			this.lbl_Width.TabIndex = 2;
			this.lbl_Width.Text = "Width";
			// 
			// lbl_Height
			// 
			this.lbl_Height.AutoSize = true;
			this.lbl_Height.Location = new System.Drawing.Point(155, 9);
			this.lbl_Height.Name = "lbl_Height";
			this.lbl_Height.Size = new System.Drawing.Size(38, 13);
			this.lbl_Height.TabIndex = 3;
			this.lbl_Height.Text = "Height";
			// 
			// btn_OK
			// 
			this.btn_OK.Location = new System.Drawing.Point(29, 66);
			this.btn_OK.Name = "btn_OK";
			this.btn_OK.Size = new System.Drawing.Size(75, 23);
			this.btn_OK.TabIndex = 4;
			this.btn_OK.Text = "OK";
			this.btn_OK.UseVisualStyleBackColor = true;
			this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
			// 
			// btn_Cancel
			// 
			this.btn_Cancel.Location = new System.Drawing.Point(180, 66);
			this.btn_Cancel.Name = "btn_Cancel";
			this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
			this.btn_Cancel.TabIndex = 5;
			this.btn_Cancel.Text = "Cancel";
			this.btn_Cancel.UseVisualStyleBackColor = true;
			this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
			// 
			// lbl_default
			// 
			this.lbl_default.AutoSize = true;
			this.lbl_default.ForeColor = System.Drawing.SystemColors.ButtonShadow;
			this.lbl_default.Location = new System.Drawing.Point(81, 50);
			this.lbl_default.Name = "lbl_default";
			this.lbl_default.Size = new System.Drawing.Size(123, 13);
			this.lbl_default.TabIndex = 6;
			this.lbl_default.Text = "* The default is 460, 360";
			// 
			// CanvasSizePrompt
			// 
			this.AcceptButton = this.btn_OK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btn_Cancel;
			this.ClientSize = new System.Drawing.Size(284, 101);
			this.Controls.Add(this.lbl_default);
			this.Controls.Add(this.btn_Cancel);
			this.Controls.Add(this.btn_OK);
			this.Controls.Add(this.lbl_Height);
			this.Controls.Add(this.lbl_Width);
			this.Controls.Add(this.num_Height);
			this.Controls.Add(this.num_Width);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CanvasSizePrompt";
			this.ShowIcon = false;
			this.Text = "Canvas Size";
			this.Load += new System.EventHandler(this.CanvasSizePrompt_Load);
			((System.ComponentModel.ISupportInitialize)(this.num_Width)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.num_Height)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NumericUpDown num_Width;
		private System.Windows.Forms.NumericUpDown num_Height;
		private System.Windows.Forms.Label lbl_Width;
		private System.Windows.Forms.Label lbl_Height;
		private System.Windows.Forms.Button btn_OK;
		private System.Windows.Forms.Button btn_Cancel;
		private System.Windows.Forms.Label lbl_default;
	}
}