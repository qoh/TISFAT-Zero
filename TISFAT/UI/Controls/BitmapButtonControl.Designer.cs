namespace TISFAT.Controls
{
    partial class BitmapButtonControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_MainButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_MainButton
            // 
            this.btn_MainButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_MainButton.FlatAppearance.BorderSize = 0;
            this.btn_MainButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_MainButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_MainButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_MainButton.Location = new System.Drawing.Point(0, 0);
            this.btn_MainButton.Name = "btn_MainButton";
            this.btn_MainButton.Size = new System.Drawing.Size(24, 24);
            this.btn_MainButton.TabIndex = 0;
            this.btn_MainButton.UseVisualStyleBackColor = true;
            this.btn_MainButton.Click += new System.EventHandler(this.btn_MainButton_Click);
            this.btn_MainButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MainButton_MouseDown);
            this.btn_MainButton.MouseEnter += new System.EventHandler(this.btn_MainButton_MouseEnter);
            this.btn_MainButton.MouseLeave += new System.EventHandler(this.btn_MainButton_MouseLeave);
            this.btn_MainButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_MainButton_MouseUp);
            // 
            // BitmapButtonControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_MainButton);
            this.Name = "BitmapButtonControl";
            this.Size = new System.Drawing.Size(24, 24);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.BitmapButtonControl_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_MainButton;
    }
}
