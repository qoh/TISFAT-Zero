using System;
using System.Drawing;
using System.Windows.Forms;

namespace TISFAT_ZERO
{
	partial class Timeline
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/*protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
				return cp;
			}
		}*/

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
			this.cxt_Menu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tst_insertKeyframe = new System.Windows.Forms.ToolStripMenuItem();
			this.tst_insertKeyframeAtPose = new System.Windows.Forms.ToolStripMenuItem();
			this.tst_removeKeyframe = new System.Windows.Forms.ToolStripMenuItem();
			this.tst_setPosePrvKfrm = new System.Windows.Forms.ToolStripMenuItem();
			this.tst_setPoseNxtKfrm = new System.Windows.Forms.ToolStripMenuItem();
			this.tst_onionSkinning = new System.Windows.Forms.ToolStripMenuItem();
			this.tst_separator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tst_insertFrameset = new System.Windows.Forms.ToolStripMenuItem();
			this.tst_removeFrameset = new System.Windows.Forms.ToolStripMenuItem();
			this.tst_separator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tst_moveLayerUp = new System.Windows.Forms.ToolStripMenuItem();
			this.tst_moveLayerDown = new System.Windows.Forms.ToolStripMenuItem();
			this.tst_insertLayer = new System.Windows.Forms.ToolStripMenuItem();
			this.tst_removeLayer = new System.Windows.Forms.ToolStripMenuItem();
			this.tst_separator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.tst_keyFrameAction = new System.Windows.Forms.ToolStripMenuItem();
			this.tst_separator4 = new System.Windows.Forms.ToolStripSeparator();
			this.tst_hideLayer = new System.Windows.Forms.ToolStripMenuItem();
			this.tst_showLayer = new System.Windows.Forms.ToolStripMenuItem();
			this.tst_separator5 = new System.Windows.Forms.ToolStripSeparator();
			this.tst_gotoFrame = new System.Windows.Forms.ToolStripMenuItem();
			this.playTimer = new System.Windows.Forms.Timer(this.components);
			this.cxt_Menu.SuspendLayout();
			this.SuspendLayout();
			// 
			// cxt_Menu
			// 
			this.cxt_Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tst_insertKeyframe,
            this.tst_insertKeyframeAtPose,
            this.tst_removeKeyframe,
            this.tst_setPosePrvKfrm,
            this.tst_setPoseNxtKfrm,
            this.tst_onionSkinning,
            this.tst_separator1,
            this.tst_insertFrameset,
            this.tst_removeFrameset,
            this.tst_separator2,
            this.tst_moveLayerUp,
            this.tst_moveLayerDown,
            this.tst_insertLayer,
            this.tst_removeLayer,
            this.tst_separator3,
            this.toolStripMenuItem1,
            this.tst_keyFrameAction,
            this.tst_separator4,
            this.tst_hideLayer,
            this.tst_showLayer,
            this.tst_separator5,
            this.tst_gotoFrame});
			this.cxt_Menu.Name = "cxt_Menu";
			this.cxt_Menu.Size = new System.Drawing.Size(256, 408);
			this.cxt_Menu.Opening += new System.ComponentModel.CancelEventHandler(this.cxt_Menu_Opening);
			this.cxt_Menu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cxt_Menu_ItemClicked);
			// 
			// tst_insertKeyframe
			// 
			this.tst_insertKeyframe.Enabled = false;
			this.tst_insertKeyframe.Name = "tst_insertKeyframe";
			this.tst_insertKeyframe.Size = new System.Drawing.Size(255, 22);
			this.tst_insertKeyframe.Text = "Insert Keyframe";
			// 
			// tst_insertKeyframeAtPose
			// 
			this.tst_insertKeyframeAtPose.Name = "tst_insertKeyframeAtPose";
			this.tst_insertKeyframeAtPose.Size = new System.Drawing.Size(255, 22);
			this.tst_insertKeyframeAtPose.Text = "Insert Keyframe With Current Pose";
			// 
			// tst_removeKeyframe
			// 
			this.tst_removeKeyframe.Enabled = false;
			this.tst_removeKeyframe.Name = "tst_removeKeyframe";
			this.tst_removeKeyframe.Size = new System.Drawing.Size(255, 22);
			this.tst_removeKeyframe.Text = "Remove Keyframe";
			// 
			// tst_setPosePrvKfrm
			// 
			this.tst_setPosePrvKfrm.Enabled = false;
			this.tst_setPosePrvKfrm.Name = "tst_setPosePrvKfrm";
			this.tst_setPosePrvKfrm.Size = new System.Drawing.Size(255, 22);
			this.tst_setPosePrvKfrm.Text = "Set Pose to Previous Keyframe";
			// 
			// tst_setPoseNxtKfrm
			// 
			this.tst_setPoseNxtKfrm.Enabled = false;
			this.tst_setPoseNxtKfrm.Name = "tst_setPoseNxtKfrm";
			this.tst_setPoseNxtKfrm.Size = new System.Drawing.Size(255, 22);
			this.tst_setPoseNxtKfrm.Text = "Set Pose to Next Keyframe";
			// 
			// tst_onionSkinning
			// 
			this.tst_onionSkinning.Enabled = false;
			this.tst_onionSkinning.Name = "tst_onionSkinning";
			this.tst_onionSkinning.Size = new System.Drawing.Size(255, 22);
			this.tst_onionSkinning.Text = "Onion Skinning ..";
			// 
			// tst_separator1
			// 
			this.tst_separator1.Name = "tst_separator1";
			this.tst_separator1.Size = new System.Drawing.Size(252, 6);
			// 
			// tst_insertFrameset
			// 
			this.tst_insertFrameset.Enabled = false;
			this.tst_insertFrameset.Name = "tst_insertFrameset";
			this.tst_insertFrameset.Size = new System.Drawing.Size(255, 22);
			this.tst_insertFrameset.Text = "Insert Frameset";
			// 
			// tst_removeFrameset
			// 
			this.tst_removeFrameset.Enabled = false;
			this.tst_removeFrameset.Name = "tst_removeFrameset";
			this.tst_removeFrameset.Size = new System.Drawing.Size(255, 22);
			this.tst_removeFrameset.Text = "Remove Frameset";
			// 
			// tst_separator2
			// 
			this.tst_separator2.Name = "tst_separator2";
			this.tst_separator2.Size = new System.Drawing.Size(252, 6);
			// 
			// tst_moveLayerUp
			// 
			this.tst_moveLayerUp.Enabled = false;
			this.tst_moveLayerUp.Name = "tst_moveLayerUp";
			this.tst_moveLayerUp.Size = new System.Drawing.Size(255, 22);
			this.tst_moveLayerUp.Text = "Move Layer Up";
			// 
			// tst_moveLayerDown
			// 
			this.tst_moveLayerDown.Enabled = false;
			this.tst_moveLayerDown.Name = "tst_moveLayerDown";
			this.tst_moveLayerDown.Size = new System.Drawing.Size(255, 22);
			this.tst_moveLayerDown.Text = "Move Layer Down";
			// 
			// tst_insertLayer
			// 
			this.tst_insertLayer.Enabled = false;
			this.tst_insertLayer.Name = "tst_insertLayer";
			this.tst_insertLayer.Size = new System.Drawing.Size(255, 22);
			this.tst_insertLayer.Text = "Insert Layer..";
			// 
			// tst_removeLayer
			// 
			this.tst_removeLayer.Enabled = false;
			this.tst_removeLayer.Name = "tst_removeLayer";
			this.tst_removeLayer.Size = new System.Drawing.Size(255, 22);
			this.tst_removeLayer.Text = "Remove Layer..";
			// 
			// tst_separator3
			// 
			this.tst_separator3.Name = "tst_separator3";
			this.tst_separator3.Size = new System.Drawing.Size(252, 6);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Enabled = false;
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(255, 22);
			this.toolStripMenuItem1.Text = "Keyframe Sound..";
			// 
			// tst_keyFrameAction
			// 
			this.tst_keyFrameAction.Enabled = false;
			this.tst_keyFrameAction.Name = "tst_keyFrameAction";
			this.tst_keyFrameAction.Size = new System.Drawing.Size(255, 22);
			this.tst_keyFrameAction.Text = "KeyFrame Action..";
			// 
			// tst_separator4
			// 
			this.tst_separator4.Name = "tst_separator4";
			this.tst_separator4.Size = new System.Drawing.Size(252, 6);
			// 
			// tst_hideLayer
			// 
			this.tst_hideLayer.Enabled = false;
			this.tst_hideLayer.Name = "tst_hideLayer";
			this.tst_hideLayer.Size = new System.Drawing.Size(255, 22);
			this.tst_hideLayer.Text = "Hide Layer";
			// 
			// tst_showLayer
			// 
			this.tst_showLayer.Enabled = false;
			this.tst_showLayer.Name = "tst_showLayer";
			this.tst_showLayer.Size = new System.Drawing.Size(255, 22);
			this.tst_showLayer.Text = "Show Layer";
			// 
			// tst_separator5
			// 
			this.tst_separator5.Name = "tst_separator5";
			this.tst_separator5.Size = new System.Drawing.Size(252, 6);
			// 
			// tst_gotoFrame
			// 
			this.tst_gotoFrame.Enabled = false;
			this.tst_gotoFrame.Name = "tst_gotoFrame";
			this.tst_gotoFrame.Size = new System.Drawing.Size(255, 22);
			this.tst_gotoFrame.Text = "Goto frame";
			// 
			// playTimer
			// 
			this.playTimer.Tick += new System.EventHandler(this.playTimer_Tick);
			// 
			// Timeline
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(220, 114);
			this.ContextMenuStrip = this.cxt_Menu;
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Timeline";
			this.Text = "Timeline";
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.Timeline_Paint);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Timeline_MouseDown);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Timeline_MouseMove);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Timeline_MouseUp);
			this.cxt_Menu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

        private ContextMenuStrip cxt_Menu;
        private ToolStripMenuItem tst_insertKeyframe;
        private ToolStripMenuItem tst_removeKeyframe;
        private ToolStripMenuItem tst_setPosePrvKfrm;
        private ToolStripMenuItem tst_setPoseNxtKfrm;
        private ToolStripMenuItem tst_onionSkinning;
        private ToolStripSeparator tst_separator1;
        private ToolStripMenuItem tst_insertFrameset;
        private ToolStripMenuItem tst_removeFrameset;
        private ToolStripSeparator tst_separator2;
        private ToolStripMenuItem tst_moveLayerUp;
        private ToolStripMenuItem tst_moveLayerDown;
        private ToolStripMenuItem tst_insertLayer;
        private ToolStripMenuItem tst_removeLayer;
        private ToolStripSeparator tst_separator3;
        private ToolStripMenuItem tst_keyFrameAction;
        private ToolStripSeparator tst_separator4;
        private ToolStripMenuItem tst_hideLayer;
        private ToolStripMenuItem tst_showLayer;
        private ToolStripSeparator tst_separator5;
        private ToolStripMenuItem tst_gotoFrame;
        private ToolStripMenuItem toolStripMenuItem1;
        private Timer playTimer;
		private ToolStripMenuItem tst_insertKeyframeAtPose;
	}
}