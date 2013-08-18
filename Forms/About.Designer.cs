namespace TISFAT_ZERO.Forms
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnl_About = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pnl_Thanks = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.pnl_Developers = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnl_About.SuspendLayout();
            this.pnl_Thanks.SuspendLayout();
            this.pnl_Developers.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkGray;
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(364, 211);
            this.panel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 34);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.splitContainer1.Panel1.Controls.Add(this.pnl_About);
            this.splitContainer1.Panel1.Controls.Add(this.pnl_Thanks);
            this.splitContainer1.Panel1.Controls.Add(this.pnl_Developers);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.LightGray;
            this.splitContainer1.Panel2.Controls.Add(this.listBox1);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(1);
            this.splitContainer1.Size = new System.Drawing.Size(364, 177);
            this.splitContainer1.SplitterDistance = 240;
            this.splitContainer1.TabIndex = 2;
            // 
            // pnl_About
            // 
            this.pnl_About.Controls.Add(this.label2);
            this.pnl_About.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_About.Location = new System.Drawing.Point(1, 1);
            this.pnl_About.Name = "pnl_About";
            this.pnl_About.Size = new System.Drawing.Size(236, 173);
            this.pnl_About.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(236, 173);
            this.label2.TabIndex = 0;
            this.label2.Text = resources.GetString("label2.Text");
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl_Thanks
            // 
            this.pnl_Thanks.Controls.Add(this.label3);
            this.pnl_Thanks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Thanks.Location = new System.Drawing.Point(1, 1);
            this.pnl_Thanks.Name = "pnl_Thanks";
            this.pnl_Thanks.Size = new System.Drawing.Size(236, 173);
            this.pnl_Thanks.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(4, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(233, 164);
            this.label3.TabIndex = 0;
            this.label3.Text = "A Special thanks goes to the following members of The Sektorz community for showi" +
    "ng their support by donating:\r\n\r\nChalla\r\nCANDYdisturber\r\n";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl_Developers
            // 
            this.pnl_Developers.Controls.Add(this.label4);
            this.pnl_Developers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Developers.Location = new System.Drawing.Point(1, 1);
            this.pnl_Developers.Name = "pnl_Developers";
            this.pnl_Developers.Padding = new System.Windows.Forms.Padding(3);
            this.pnl_Developers.Size = new System.Drawing.Size(236, 173);
            this.pnl_Developers.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(230, 167);
            this.label4.TabIndex = 0;
            this.label4.Text = resources.GetString("label4.Text");
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.LightGray;
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            "- About TISFAT",
            "- Meet the Devs",
            "- Special Thanks"});
            this.listBox1.Location = new System.Drawing.Point(1, 1);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(116, 173);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.LightGray;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 34);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(364, 177);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Sans", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label1.Location = new System.Drawing.Point(122, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "TISFAT:Zero";
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(364, 211);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "About";
            this.Load += new System.EventHandler(this.About_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnl_About.ResumeLayout(false);
            this.pnl_Thanks.ResumeLayout(false);
            this.pnl_Developers.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel pnl_About;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnl_Thanks;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel pnl_Developers;
        private System.Windows.Forms.Label label4;
    }
}