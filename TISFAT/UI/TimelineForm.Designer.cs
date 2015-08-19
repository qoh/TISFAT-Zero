namespace TISFAT
{
    partial class TimelineForm
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
            this.pnl_ScrollSquare = new System.Windows.Forms.Panel();
            this.btn_Start = new System.Windows.Forms.Button();
            this.scrl_VTimeline = new System.Windows.Forms.VScrollBar();
            this.scrl_HTimeline = new System.Windows.Forms.HScrollBar();
            this.btn_Rewind = new System.Windows.Forms.Button();
            this.btn_FastForward = new System.Windows.Forms.Button();
            this.btn_End = new System.Windows.Forms.Button();
            this.GLContext = new OpenTK.GLControl();
            this.pnl_ToolboxPanel = new System.Windows.Forms.Panel();
            this.btn_PlayPause = new TISFAT.Controls.BitmapButtonControl();
            this.cxtm_Timeline = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.insertKeyframeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeKeyframeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.insertFramesetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeFramesetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.moveLayerUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveLayerDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.gotoFrameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnl_ToolboxPanel.SuspendLayout();
            this.cxtm_Timeline.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_ScrollSquare
            // 
            this.pnl_ScrollSquare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_ScrollSquare.BackColor = System.Drawing.SystemColors.Control;
            this.pnl_ScrollSquare.Location = new System.Drawing.Point(735, 132);
            this.pnl_ScrollSquare.Margin = new System.Windows.Forms.Padding(0);
            this.pnl_ScrollSquare.Name = "pnl_ScrollSquare";
            this.pnl_ScrollSquare.Size = new System.Drawing.Size(17, 17);
            this.pnl_ScrollSquare.TabIndex = 9;
            // 
            // btn_Start
            // 
            this.btn_Start.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Start.FlatAppearance.BorderSize = 0;
            this.btn_Start.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Start.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Start.Image = global::TISFAT.Properties.Resources.seek_start;
            this.btn_Start.Location = new System.Drawing.Point(333, 3);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(24, 24);
            this.btn_Start.TabIndex = 2;
            this.btn_Start.UseVisualStyleBackColor = true;
            // 
            // scrl_VTimeline
            // 
            this.scrl_VTimeline.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scrl_VTimeline.LargeChange = 1;
            this.scrl_VTimeline.Location = new System.Drawing.Point(735, 1);
            this.scrl_VTimeline.Maximum = 0;
            this.scrl_VTimeline.Name = "scrl_VTimeline";
            this.scrl_VTimeline.Size = new System.Drawing.Size(17, 131);
            this.scrl_VTimeline.TabIndex = 8;
            this.scrl_VTimeline.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrl_Timeline_Scroll);
            // 
            // scrl_HTimeline
            // 
            this.scrl_HTimeline.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scrl_HTimeline.LargeChange = 1;
            this.scrl_HTimeline.Location = new System.Drawing.Point(0, 132);
            this.scrl_HTimeline.Maximum = 0;
            this.scrl_HTimeline.Name = "scrl_HTimeline";
            this.scrl_HTimeline.Size = new System.Drawing.Size(735, 17);
            this.scrl_HTimeline.TabIndex = 7;
            this.scrl_HTimeline.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrl_Timeline_Scroll);
            // 
            // btn_Rewind
            // 
            this.btn_Rewind.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Rewind.FlatAppearance.BorderSize = 0;
            this.btn_Rewind.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Rewind.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Rewind.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Rewind.Image = global::TISFAT.Properties.Resources.rewind_normal;
            this.btn_Rewind.Location = new System.Drawing.Point(303, 3);
            this.btn_Rewind.Name = "btn_Rewind";
            this.btn_Rewind.Size = new System.Drawing.Size(24, 24);
            this.btn_Rewind.TabIndex = 5;
            this.btn_Rewind.UseVisualStyleBackColor = true;
            // 
            // btn_FastForward
            // 
            this.btn_FastForward.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_FastForward.FlatAppearance.BorderSize = 0;
            this.btn_FastForward.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_FastForward.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_FastForward.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_FastForward.Image = global::TISFAT.Properties.Resources.forward_normal;
            this.btn_FastForward.Location = new System.Drawing.Point(423, 3);
            this.btn_FastForward.Name = "btn_FastForward";
            this.btn_FastForward.Size = new System.Drawing.Size(24, 24);
            this.btn_FastForward.TabIndex = 4;
            this.btn_FastForward.UseVisualStyleBackColor = true;
            // 
            // btn_End
            // 
            this.btn_End.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_End.FlatAppearance.BorderSize = 0;
            this.btn_End.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_End.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_End.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_End.Image = global::TISFAT.Properties.Resources.seek_end;
            this.btn_End.Location = new System.Drawing.Point(393, 3);
            this.btn_End.Name = "btn_End";
            this.btn_End.Size = new System.Drawing.Size(24, 24);
            this.btn_End.TabIndex = 3;
            this.btn_End.UseVisualStyleBackColor = true;
            // 
            // GLContext
            // 
            this.GLContext.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GLContext.BackColor = System.Drawing.Color.Black;
            this.GLContext.Location = new System.Drawing.Point(1, 1);
            this.GLContext.Margin = new System.Windows.Forms.Padding(0);
            this.GLContext.Name = "GLContext";
            this.GLContext.Size = new System.Drawing.Size(751, 148);
            this.GLContext.TabIndex = 5;
            this.GLContext.VSync = false;
            this.GLContext.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GLContext_MouseDown);
            this.GLContext.MouseLeave += new System.EventHandler(this.GLContext_MouseLeave);
            this.GLContext.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GLContext_MouseMove);
            this.GLContext.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GLContext_MouseUp);
            // 
            // pnl_ToolboxPanel
            // 
            this.pnl_ToolboxPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_ToolboxPanel.BackColor = System.Drawing.SystemColors.Control;
            this.pnl_ToolboxPanel.Controls.Add(this.btn_PlayPause);
            this.pnl_ToolboxPanel.Controls.Add(this.btn_Rewind);
            this.pnl_ToolboxPanel.Controls.Add(this.btn_FastForward);
            this.pnl_ToolboxPanel.Controls.Add(this.btn_End);
            this.pnl_ToolboxPanel.Controls.Add(this.btn_Start);
            this.pnl_ToolboxPanel.Location = new System.Drawing.Point(1, 149);
            this.pnl_ToolboxPanel.Margin = new System.Windows.Forms.Padding(0);
            this.pnl_ToolboxPanel.Name = "pnl_ToolboxPanel";
            this.pnl_ToolboxPanel.Size = new System.Drawing.Size(751, 30);
            this.pnl_ToolboxPanel.TabIndex = 6;
            // 
            // btn_PlayPause
            // 
            this.btn_PlayPause.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_PlayPause.Checked = false;
            this.btn_PlayPause.ImageDefault = global::TISFAT.Properties.Resources.play_normal;
            this.btn_PlayPause.ImageDown = global::TISFAT.Properties.Resources.play_normal;
            this.btn_PlayPause.ImageHover = global::TISFAT.Properties.Resources.play_hover;
            this.btn_PlayPause.ImageOn = global::TISFAT.Properties.Resources.pause_normal;
            this.btn_PlayPause.ImageOnDown = global::TISFAT.Properties.Resources.pause_normal;
            this.btn_PlayPause.ImageOnHover = global::TISFAT.Properties.Resources.pause_hover;
            this.btn_PlayPause.Location = new System.Drawing.Point(363, 3);
            this.btn_PlayPause.Name = "btn_PlayPause";
            this.btn_PlayPause.Size = new System.Drawing.Size(24, 24);
            this.btn_PlayPause.TabIndex = 10;
            this.btn_PlayPause.ToggleButton = true;
            this.btn_PlayPause.Click += new System.EventHandler(this.btn_PlayPause_Click);
            // 
            // cxtm_Timeline
            // 
            this.cxtm_Timeline.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertKeyframeToolStripMenuItem,
            this.removeKeyframeToolStripMenuItem,
            this.toolStripSeparator4,
            this.insertFramesetToolStripMenuItem,
            this.removeFramesetToolStripMenuItem,
            this.toolStripSeparator6,
            this.moveLayerUpToolStripMenuItem,
            this.moveLayerDownToolStripMenuItem,
            this.removeLayerToolStripMenuItem,
            this.toolStripSeparator5,
            this.gotoFrameToolStripMenuItem});
            this.cxtm_Timeline.Name = "cxtm_Timeline";
            this.cxtm_Timeline.Size = new System.Drawing.Size(171, 198);
            // 
            // insertKeyframeToolStripMenuItem
            // 
            this.insertKeyframeToolStripMenuItem.Name = "insertKeyframeToolStripMenuItem";
            this.insertKeyframeToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.insertKeyframeToolStripMenuItem.Text = "Insert Keyframe";
            this.insertKeyframeToolStripMenuItem.Click += new System.EventHandler(this.insertKeyframeToolStripMenuItem_Click);
            // 
            // removeKeyframeToolStripMenuItem
            // 
            this.removeKeyframeToolStripMenuItem.Name = "removeKeyframeToolStripMenuItem";
            this.removeKeyframeToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.removeKeyframeToolStripMenuItem.Text = "Remove Keyframe";
            this.removeKeyframeToolStripMenuItem.Click += new System.EventHandler(this.removeKeyframeToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(167, 6);
            // 
            // insertFramesetToolStripMenuItem
            // 
            this.insertFramesetToolStripMenuItem.Name = "insertFramesetToolStripMenuItem";
            this.insertFramesetToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.insertFramesetToolStripMenuItem.Text = "Insert Frameset";
            this.insertFramesetToolStripMenuItem.Click += new System.EventHandler(this.insertFramesetToolStripMenuItem_Click);
            // 
            // removeFramesetToolStripMenuItem
            // 
            this.removeFramesetToolStripMenuItem.Name = "removeFramesetToolStripMenuItem";
            this.removeFramesetToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.removeFramesetToolStripMenuItem.Text = "Remove Frameset";
            this.removeFramesetToolStripMenuItem.Click += new System.EventHandler(this.removeFramesetToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(167, 6);
            // 
            // moveLayerUpToolStripMenuItem
            // 
            this.moveLayerUpToolStripMenuItem.Name = "moveLayerUpToolStripMenuItem";
            this.moveLayerUpToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.moveLayerUpToolStripMenuItem.Text = "Move Layer Up";
            this.moveLayerUpToolStripMenuItem.Click += new System.EventHandler(this.moveLayerUpToolStripMenuItem_Click);
            // 
            // moveLayerDownToolStripMenuItem
            // 
            this.moveLayerDownToolStripMenuItem.Name = "moveLayerDownToolStripMenuItem";
            this.moveLayerDownToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.moveLayerDownToolStripMenuItem.Text = "Move Layer Down";
            this.moveLayerDownToolStripMenuItem.Click += new System.EventHandler(this.moveLayerDownToolStripMenuItem_Click);
            // 
            // removeLayerToolStripMenuItem
            // 
            this.removeLayerToolStripMenuItem.Name = "removeLayerToolStripMenuItem";
            this.removeLayerToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.removeLayerToolStripMenuItem.Text = "Remove Layer..";
            this.removeLayerToolStripMenuItem.Click += new System.EventHandler(this.removeLayerToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(167, 6);
            // 
            // gotoFrameToolStripMenuItem
            // 
            this.gotoFrameToolStripMenuItem.Enabled = false;
            this.gotoFrameToolStripMenuItem.Name = "gotoFrameToolStripMenuItem";
            this.gotoFrameToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.gotoFrameToolStripMenuItem.Text = "Goto Frame";
            // 
            // TimelineForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 179);
            this.ControlBox = false;
            this.Controls.Add(this.pnl_ScrollSquare);
            this.Controls.Add(this.scrl_VTimeline);
            this.Controls.Add(this.scrl_HTimeline);
            this.Controls.Add(this.GLContext);
            this.Controls.Add(this.pnl_ToolboxPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(768, 136);
            this.Name = "TimelineForm";
            this.Text = "Timeline";
            this.Load += new System.EventHandler(this.TimelineForm_Load);
            this.Enter += new System.EventHandler(this.TimelineForm_Enter);
            this.Resize += new System.EventHandler(this.TimelineForm_Resize);
            this.pnl_ToolboxPanel.ResumeLayout(false);
            this.cxtm_Timeline.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_ScrollSquare;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.VScrollBar scrl_VTimeline;
        private System.Windows.Forms.HScrollBar scrl_HTimeline;
        private System.Windows.Forms.Button btn_Rewind;
        private System.Windows.Forms.Button btn_FastForward;
        private System.Windows.Forms.Button btn_End;
        private OpenTK.GLControl GLContext;
        private System.Windows.Forms.Panel pnl_ToolboxPanel;
        private System.Windows.Forms.ContextMenuStrip cxtm_Timeline;
        private System.Windows.Forms.ToolStripMenuItem insertKeyframeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeKeyframeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem insertFramesetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeFramesetToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem moveLayerUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveLayerDownToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeLayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem gotoFrameToolStripMenuItem;
        private Controls.BitmapButtonControl btn_PlayPause;
    }
}