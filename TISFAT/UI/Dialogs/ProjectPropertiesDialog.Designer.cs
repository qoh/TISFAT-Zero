namespace TISFAT
{
	partial class ProjectPropertiesDialog
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
			this.btn_OK = new System.Windows.Forms.Button();
			this.btn_Cancel = new System.Windows.Forms.Button();
			this.num_CanvasWidth = new System.Windows.Forms.NumericUpDown();
			this.num_CanvasHeight = new System.Windows.Forms.NumericUpDown();
			this.lbl_CanvasWidth = new System.Windows.Forms.Label();
			this.lbl_CanvasHeight = new System.Windows.Forms.Label();
			this.grp_Canvas = new System.Windows.Forms.GroupBox();
			this.lbl_CanvasColorNumbers = new System.Windows.Forms.Label();
			this.lbl_CanvasColor = new System.Windows.Forms.Label();
			this.pnl_CanvasColor = new System.Windows.Forms.Panel();
			this.lbl_AnimSpeed = new System.Windows.Forms.Label();
			this.num_AnimSpeed = new System.Windows.Forms.NumericUpDown();
			this.lbl_AnimSpeedDesc = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.num_CanvasWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.num_CanvasHeight)).BeginInit();
			this.grp_Canvas.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.num_AnimSpeed)).BeginInit();
			this.SuspendLayout();
			// 
			// btn_OK
			// 
			this.btn_OK.Location = new System.Drawing.Point(77, 194);
			this.btn_OK.Name = "btn_OK";
			this.btn_OK.Size = new System.Drawing.Size(75, 23);
			this.btn_OK.TabIndex = 0;
			this.btn_OK.Text = "OK";
			this.btn_OK.UseVisualStyleBackColor = true;
			this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
			// 
			// btn_Cancel
			// 
			this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn_Cancel.Location = new System.Drawing.Point(182, 194);
			this.btn_Cancel.Name = "btn_Cancel";
			this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
			this.btn_Cancel.TabIndex = 1;
			this.btn_Cancel.Text = "Cancel";
			this.btn_Cancel.UseVisualStyleBackColor = true;
			// 
			// num_CanvasWidth
			// 
			this.num_CanvasWidth.Location = new System.Drawing.Point(48, 33);
			this.num_CanvasWidth.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
			this.num_CanvasWidth.Name = "num_CanvasWidth";
			this.num_CanvasWidth.Size = new System.Drawing.Size(100, 20);
			this.num_CanvasWidth.TabIndex = 5;
			// 
			// num_CanvasHeight
			// 
			this.num_CanvasHeight.Location = new System.Drawing.Point(165, 33);
			this.num_CanvasHeight.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
			this.num_CanvasHeight.Name = "num_CanvasHeight";
			this.num_CanvasHeight.Size = new System.Drawing.Size(100, 20);
			this.num_CanvasHeight.TabIndex = 6;
			// 
			// lbl_CanvasWidth
			// 
			this.lbl_CanvasWidth.AutoSize = true;
			this.lbl_CanvasWidth.Location = new System.Drawing.Point(45, 17);
			this.lbl_CanvasWidth.Name = "lbl_CanvasWidth";
			this.lbl_CanvasWidth.Size = new System.Drawing.Size(35, 13);
			this.lbl_CanvasWidth.TabIndex = 7;
			this.lbl_CanvasWidth.Text = "Width";
			// 
			// lbl_CanvasHeight
			// 
			this.lbl_CanvasHeight.AutoSize = true;
			this.lbl_CanvasHeight.Location = new System.Drawing.Point(162, 17);
			this.lbl_CanvasHeight.Name = "lbl_CanvasHeight";
			this.lbl_CanvasHeight.Size = new System.Drawing.Size(38, 13);
			this.lbl_CanvasHeight.TabIndex = 8;
			this.lbl_CanvasHeight.Text = "Height";
			// 
			// grp_Canvas
			// 
			this.grp_Canvas.Controls.Add(this.lbl_CanvasColorNumbers);
			this.grp_Canvas.Controls.Add(this.lbl_CanvasColor);
			this.grp_Canvas.Controls.Add(this.pnl_CanvasColor);
			this.grp_Canvas.Controls.Add(this.num_CanvasHeight);
			this.grp_Canvas.Controls.Add(this.lbl_CanvasHeight);
			this.grp_Canvas.Controls.Add(this.num_CanvasWidth);
			this.grp_Canvas.Controls.Add(this.lbl_CanvasWidth);
			this.grp_Canvas.Location = new System.Drawing.Point(12, 12);
			this.grp_Canvas.Name = "grp_Canvas";
			this.grp_Canvas.Size = new System.Drawing.Size(310, 110);
			this.grp_Canvas.TabIndex = 9;
			this.grp_Canvas.TabStop = false;
			this.grp_Canvas.Text = "Canvas";
			// 
			// lbl_CanvasColorNumbers
			// 
			this.lbl_CanvasColorNumbers.Location = new System.Drawing.Point(133, 84);
			this.lbl_CanvasColorNumbers.Name = "lbl_CanvasColorNumbers";
			this.lbl_CanvasColorNumbers.Size = new System.Drawing.Size(132, 13);
			this.lbl_CanvasColorNumbers.TabIndex = 11;
			this.lbl_CanvasColorNumbers.Text = "255, 255, 255, 255";
			// 
			// lbl_CanvasColor
			// 
			this.lbl_CanvasColor.AutoSize = true;
			this.lbl_CanvasColor.Location = new System.Drawing.Point(120, 62);
			this.lbl_CanvasColor.Name = "lbl_CanvasColor";
			this.lbl_CanvasColor.Size = new System.Drawing.Size(70, 13);
			this.lbl_CanvasColor.TabIndex = 10;
			this.lbl_CanvasColor.Text = "Canvas Color";
			// 
			// pnl_CanvasColor
			// 
			this.pnl_CanvasColor.BackColor = System.Drawing.Color.White;
			this.pnl_CanvasColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnl_CanvasColor.Location = new System.Drawing.Point(105, 78);
			this.pnl_CanvasColor.Name = "pnl_CanvasColor";
			this.pnl_CanvasColor.Size = new System.Drawing.Size(24, 24);
			this.pnl_CanvasColor.TabIndex = 9;
			// 
			// lbl_AnimSpeed
			// 
			this.lbl_AnimSpeed.AutoSize = true;
			this.lbl_AnimSpeed.Location = new System.Drawing.Point(124, 123);
			this.lbl_AnimSpeed.Name = "lbl_AnimSpeed";
			this.lbl_AnimSpeed.Size = new System.Drawing.Size(87, 13);
			this.lbl_AnimSpeed.TabIndex = 10;
			this.lbl_AnimSpeed.Text = "Animation Speed";
			// 
			// num_AnimSpeed
			// 
			this.num_AnimSpeed.Location = new System.Drawing.Point(132, 139);
			this.num_AnimSpeed.Maximum = new decimal(new int[] {
            240,
            0,
            0,
            0});
			this.num_AnimSpeed.Name = "num_AnimSpeed";
			this.num_AnimSpeed.Size = new System.Drawing.Size(71, 20);
			this.num_AnimSpeed.TabIndex = 9;
			// 
			// lbl_AnimSpeedDesc
			// 
			this.lbl_AnimSpeedDesc.Location = new System.Drawing.Point(58, 162);
			this.lbl_AnimSpeedDesc.Name = "lbl_AnimSpeedDesc";
			this.lbl_AnimSpeedDesc.Size = new System.Drawing.Size(219, 32);
			this.lbl_AnimSpeedDesc.TabIndex = 11;
			this.lbl_AnimSpeedDesc.Text = "This is how many frames on the timeline pass per second during playback.\r\n";
			this.lbl_AnimSpeedDesc.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// ProjectPropertiesDialog
			// 
			this.AcceptButton = this.btn_OK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btn_Cancel;
			this.ClientSize = new System.Drawing.Size(334, 221);
			this.ControlBox = false;
			this.Controls.Add(this.lbl_AnimSpeedDesc);
			this.Controls.Add(this.num_AnimSpeed);
			this.Controls.Add(this.lbl_AnimSpeed);
			this.Controls.Add(this.grp_Canvas);
			this.Controls.Add(this.btn_Cancel);
			this.Controls.Add(this.btn_OK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ProjectPropertiesDialog";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Project Properties";
			this.Load += new System.EventHandler(this.ProjectPropertiesDialog_Load);
			((System.ComponentModel.ISupportInitialize)(this.num_CanvasWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.num_CanvasHeight)).EndInit();
			this.grp_Canvas.ResumeLayout(false);
			this.grp_Canvas.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.num_AnimSpeed)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btn_OK;
		private System.Windows.Forms.Button btn_Cancel;
		private System.Windows.Forms.NumericUpDown num_CanvasWidth;
		private System.Windows.Forms.NumericUpDown num_CanvasHeight;
		private System.Windows.Forms.Label lbl_CanvasWidth;
		private System.Windows.Forms.Label lbl_CanvasHeight;
		private System.Windows.Forms.GroupBox grp_Canvas;
		private System.Windows.Forms.Label lbl_CanvasColorNumbers;
		private System.Windows.Forms.Label lbl_CanvasColor;
		private System.Windows.Forms.Panel pnl_CanvasColor;
		private System.Windows.Forms.Label lbl_AnimSpeed;
		private System.Windows.Forms.NumericUpDown num_AnimSpeed;
		private System.Windows.Forms.Label lbl_AnimSpeedDesc;
	}
}