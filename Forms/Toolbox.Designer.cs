namespace TISFAT_ZERO
{
    partial class Toolbox
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lbl_stickFigures = new System.Windows.Forms.Label();
            this.lbl_jointLength = new System.Windows.Forms.Label();
            this.lbl_selectedJoint = new System.Windows.Forms.Label();
            this.lbl_yPos = new System.Windows.Forms.Label();
            this.lbl_xPos = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lbl_selectionDummy = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.lbl_selectionDummy);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(164, 356);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(164, 140);
            this.panel2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(195, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Toolbox";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(164, 140);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.LightGray;
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(156, 114);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Control";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.LightGray;
            this.tabPage2.Controls.Add(this.lbl_stickFigures);
            this.tabPage2.Controls.Add(this.lbl_jointLength);
            this.tabPage2.Controls.Add(this.lbl_selectedJoint);
            this.tabPage2.Controls.Add(this.lbl_yPos);
            this.tabPage2.Controls.Add(this.lbl_xPos);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(156, 114);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Debug";
            // 
            // lbl_stickFigures
            // 
            this.lbl_stickFigures.AutoSize = true;
            this.lbl_stickFigures.Location = new System.Drawing.Point(3, 65);
            this.lbl_stickFigures.Name = "lbl_stickFigures";
            this.lbl_stickFigures.Size = new System.Drawing.Size(85, 13);
            this.lbl_stickFigures.TabIndex = 9;
            this.lbl_stickFigures.Text = "StickFigure List: ";
            // 
            // lbl_jointLength
            // 
            this.lbl_jointLength.AutoSize = true;
            this.lbl_jointLength.Location = new System.Drawing.Point(3, 52);
            this.lbl_jointLength.Name = "lbl_jointLength";
            this.lbl_jointLength.Size = new System.Drawing.Size(71, 13);
            this.lbl_jointLength.TabIndex = 8;
            this.lbl_jointLength.Text = "Joint Length: ";
            // 
            // lbl_selectedJoint
            // 
            this.lbl_selectedJoint.AutoSize = true;
            this.lbl_selectedJoint.Location = new System.Drawing.Point(3, 39);
            this.lbl_selectedJoint.Name = "lbl_selectedJoint";
            this.lbl_selectedJoint.Size = new System.Drawing.Size(80, 13);
            this.lbl_selectedJoint.TabIndex = 7;
            this.lbl_selectedJoint.Text = "Selected Joint: ";
            // 
            // lbl_yPos
            // 
            this.lbl_yPos.AutoSize = true;
            this.lbl_yPos.Location = new System.Drawing.Point(3, 17);
            this.lbl_yPos.Name = "lbl_yPos";
            this.lbl_yPos.Size = new System.Drawing.Size(47, 13);
            this.lbl_yPos.TabIndex = 6;
            this.lbl_yPos.Text = "Y Pos: 0";
            // 
            // lbl_xPos
            // 
            this.lbl_xPos.AutoSize = true;
            this.lbl_xPos.Location = new System.Drawing.Point(3, 4);
            this.lbl_xPos.Name = "lbl_xPos";
            this.lbl_xPos.Size = new System.Drawing.Size(47, 13);
            this.lbl_xPos.TabIndex = 5;
            this.lbl_xPos.Text = "X Pos: 0";
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button5.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(129)))), ((int)(((byte)(145)))));
            this.button5.Location = new System.Drawing.Point(85, 254);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(73, 50);
            this.button5.TabIndex = 18;
            this.button5.TabStop = false;
            this.button5.Text = "BG Color";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button1_MouseClick);
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button6.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(129)))), ((int)(((byte)(145)))));
            this.button6.Location = new System.Drawing.Point(6, 254);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(73, 50);
            this.button6.TabIndex = 17;
            this.button6.TabStop = false;
            this.button6.Text = "Figure Poser";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button1_MouseClick);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(129)))), ((int)(((byte)(145)))));
            this.button3.Location = new System.Drawing.Point(85, 198);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(73, 50);
            this.button3.TabIndex = 16;
            this.button3.TabStop = false;
            this.button3.Text = "Scale Figure";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button1_MouseClick);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button4.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(129)))), ((int)(((byte)(145)))));
            this.button4.Location = new System.Drawing.Point(6, 198);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(73, 50);
            this.button4.TabIndex = 15;
            this.button4.TabStop = false;
            this.button4.Text = "Figure Properties";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button1_MouseClick);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(129)))), ((int)(((byte)(145)))));
            this.button2.Location = new System.Drawing.Point(85, 142);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(73, 50);
            this.button2.TabIndex = 14;
            this.button2.TabStop = false;
            this.button2.Text = "Drawing";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button1_MouseClick);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(129)))), ((int)(((byte)(145)))));
            this.button1.Location = new System.Drawing.Point(6, 142);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(73, 50);
            this.button1.TabIndex = 13;
            this.button1.TabStop = false;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button1_MouseClick);
            // 
            // lbl_selectionDummy
            // 
            this.lbl_selectionDummy.AutoSize = true;
            this.lbl_selectionDummy.Location = new System.Drawing.Point(74, 198);
            this.lbl_selectionDummy.Name = "lbl_selectionDummy";
            this.lbl_selectionDummy.Size = new System.Drawing.Size(0, 13);
            this.lbl_selectionDummy.TabIndex = 19;
            // 
            // Toolbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(164, 356);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Toolbox";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Toolbox";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        public System.Windows.Forms.Label lbl_stickFigures;
        public System.Windows.Forms.Label lbl_jointLength;
        public System.Windows.Forms.Label lbl_selectedJoint;
        public System.Windows.Forms.Label lbl_yPos;
        public System.Windows.Forms.Label lbl_xPos;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbl_selectionDummy;
    }
}