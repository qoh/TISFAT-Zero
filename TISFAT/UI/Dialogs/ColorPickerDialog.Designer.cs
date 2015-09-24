namespace TISFAT
{
	partial class ColorPickerDialog
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
			this.colorGrid1 = new Cyotek.Windows.Forms.ColorGrid();
			this.btn_OK = new System.Windows.Forms.Button();
			this.btn_Cancel = new System.Windows.Forms.Button();
			this.colorWheel1 = new Cyotek.Windows.Forms.ColorWheel();
			this.btn_ExpandWindow = new System.Windows.Forms.Button();
			this.pnl_ColorBorder = new System.Windows.Forms.Panel();
			this.pnl_Color = new System.Windows.Forms.Panel();
			this.colorEditorManager1 = new Cyotek.Windows.Forms.ColorEditorManager();
			this.colorEditor1 = new Cyotek.Windows.Forms.ColorEditor();
			this.pnl_ColorBorder.SuspendLayout();
			this.SuspendLayout();
			// 
			// colorGrid1
			// 
			this.colorGrid1.AutoSize = false;
			this.colorGrid1.CellBorderStyle = Cyotek.Windows.Forms.ColorCellBorderStyle.None;
			this.colorGrid1.Location = new System.Drawing.Point(3, 173);
			this.colorGrid1.Name = "colorGrid1";
			this.colorGrid1.Padding = new System.Windows.Forms.Padding(0);
			this.colorGrid1.Palette = Cyotek.Windows.Forms.ColorPalette.Paint;
			this.colorGrid1.SelectedCellStyle = Cyotek.Windows.Forms.ColorGridSelectedCellStyle.Standard;
			this.colorGrid1.ShowCustomColors = false;
			this.colorGrid1.Size = new System.Drawing.Size(192, 24);
			this.colorGrid1.Spacing = new System.Drawing.Size(0, 0);
			this.colorGrid1.TabIndex = 0;
			// 
			// btn_OK
			// 
			this.btn_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btn_OK.Location = new System.Drawing.Point(12, 203);
			this.btn_OK.Name = "btn_OK";
			this.btn_OK.Size = new System.Drawing.Size(75, 23);
			this.btn_OK.TabIndex = 1;
			this.btn_OK.Text = "OK";
			this.btn_OK.UseVisualStyleBackColor = true;
			this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
			// 
			// btn_Cancel
			// 
			this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn_Cancel.Location = new System.Drawing.Point(111, 203);
			this.btn_Cancel.Name = "btn_Cancel";
			this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
			this.btn_Cancel.TabIndex = 2;
			this.btn_Cancel.Text = "Cancel";
			this.btn_Cancel.UseVisualStyleBackColor = true;
			// 
			// colorWheel1
			// 
			this.colorWheel1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.colorWheel1.ColorStep = 1;
			this.colorWheel1.Location = new System.Drawing.Point(54, 33);
			this.colorWheel1.Margin = new System.Windows.Forms.Padding(0);
			this.colorWheel1.Name = "colorWheel1";
			this.colorWheel1.Size = new System.Drawing.Size(141, 137);
			this.colorWheel1.TabIndex = 3;
			// 
			// btn_ExpandWindow
			// 
			this.btn_ExpandWindow.Location = new System.Drawing.Point(111, 7);
			this.btn_ExpandWindow.Name = "btn_ExpandWindow";
			this.btn_ExpandWindow.Size = new System.Drawing.Size(75, 23);
			this.btn_ExpandWindow.TabIndex = 4;
			this.btn_ExpandWindow.Text = "More >>";
			this.btn_ExpandWindow.UseVisualStyleBackColor = true;
			this.btn_ExpandWindow.Click += new System.EventHandler(this.btn_ExpandWindow_Click);
			// 
			// pnl_ColorBorder
			// 
			this.pnl_ColorBorder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnl_ColorBorder.Controls.Add(this.pnl_Color);
			this.pnl_ColorBorder.Location = new System.Drawing.Point(12, 125);
			this.pnl_ColorBorder.Name = "pnl_ColorBorder";
			this.pnl_ColorBorder.Padding = new System.Windows.Forms.Padding(1);
			this.pnl_ColorBorder.Size = new System.Drawing.Size(32, 32);
			this.pnl_ColorBorder.TabIndex = 5;
			// 
			// pnl_Color
			// 
			this.pnl_Color.BackColor = System.Drawing.Color.Black;
			this.pnl_Color.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnl_Color.Location = new System.Drawing.Point(1, 1);
			this.pnl_Color.Margin = new System.Windows.Forms.Padding(0);
			this.pnl_Color.Name = "pnl_Color";
			this.pnl_Color.Size = new System.Drawing.Size(28, 28);
			this.pnl_Color.TabIndex = 6;
			// 
			// colorEditorManager1
			// 
			this.colorEditorManager1.ColorEditor = this.colorEditor1;
			this.colorEditorManager1.ColorGrid = this.colorGrid1;
			this.colorEditorManager1.ColorWheel = this.colorWheel1;
			this.colorEditorManager1.ColorChanged += new System.EventHandler(this.colorEditorManager1_ColorChanged);
			// 
			// colorEditor1
			// 
			this.colorEditor1.Location = new System.Drawing.Point(201, 20);
			this.colorEditor1.Name = "colorEditor1";
			this.colorEditor1.Size = new System.Drawing.Size(243, 234);
			this.colorEditor1.TabIndex = 6;
			this.colorEditor1.Visible = false;
			// 
			// ColorPickerDialog
			// 
			this.AcceptButton = this.btn_OK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btn_Cancel;
			this.ClientSize = new System.Drawing.Size(198, 232);
			this.ControlBox = false;
			this.Controls.Add(this.colorEditor1);
			this.Controls.Add(this.pnl_ColorBorder);
			this.Controls.Add(this.btn_ExpandWindow);
			this.Controls.Add(this.colorWheel1);
			this.Controls.Add(this.btn_Cancel);
			this.Controls.Add(this.btn_OK);
			this.Controls.Add(this.colorGrid1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ColorPickerDialog";
			this.Text = "Pick a Color";
			this.Load += new System.EventHandler(this.ColorPickerDialog_Load);
			this.pnl_ColorBorder.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Cyotek.Windows.Forms.ColorGrid colorGrid1;
		private System.Windows.Forms.Button btn_OK;
		private System.Windows.Forms.Button btn_Cancel;
		private Cyotek.Windows.Forms.ColorWheel colorWheel1;
		private System.Windows.Forms.Button btn_ExpandWindow;
		private System.Windows.Forms.Panel pnl_ColorBorder;
		private System.Windows.Forms.Panel pnl_Color;
		private Cyotek.Windows.Forms.ColorEditorManager colorEditorManager1;
		private Cyotek.Windows.Forms.ColorEditor colorEditor1;
	}
}