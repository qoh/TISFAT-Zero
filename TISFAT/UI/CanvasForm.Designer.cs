namespace TISFAT
{
    partial class CanvasForm
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
			this.SuspendLayout();
			// 
			// CanvasForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.AutoSize = true;
			this.CausesValidation = false;
			this.ClientSize = new System.Drawing.Size(444, 321);
			this.ControlBox = false;
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CanvasForm";
			this.ShowIcon = false;
			this.Text = "Canvas";
			this.Load += new System.EventHandler(this.Canvas_Load);
			this.Scroll += new System.Windows.Forms.ScrollEventHandler(this.CanvasForm_Scroll);
			this.Enter += new System.EventHandler(this.CanvasForm_Enter);
			this.Resize += new System.EventHandler(this.CanvasForm_Resize);
			this.ResumeLayout(false);

        }

		#endregion
	}
}