using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TISFAT_Zero
{
	partial class Toolbox : Form
	{
		public bool isPlaying = false;
		public byte frameRate = 30;
		public bool inMenu = false;

		public Panel slideOutObject;

		public string[] panels = new String[] { "None", "Stick", "Line", "Rect", "Poly", "Paint" };
		public string pnlOpen = "None";

		private bool reOpen = false;
		private Panel reOpenPanel;

		public Toolbox()
		{
			InitializeComponent();
		}

		#region Buttons

		private void button1_MouseClick(object sender, MouseEventArgs e)
		{
			lbl_selectionDummy.Focus();
		}

		private void btn_playPause_Click(object sender, System.EventArgs e)
		{
			/*if (isPlaying)
				Program.TheTimeline.stopTimer();
			else
				Program.TheTimeline.startTimer(frameRate);*/

			isPlaying = !isPlaying;
			btn_playPause.Text = isPlaying ? "Pause" : "Play";
		}

		private void drawButton_MouseClick(object sender, MouseEventArgs e)
		{
			lbl_selectionDummy.Focus();
		}

		public void addButton_Click(object sender, EventArgs e)
		{
			pnl_mainTools.Enabled = false;
			slideOutObject = pnl_addTools;
			animTimer.Start();
		}

		private void btn_cancelButton_Click(object sender, EventArgs e)
		{
			slideOutObject = pnl_addTools;
			animTimer.Start();
		}

		private void drawButton_Click(object sender, EventArgs e)
		{
			lbl_selectionDummy.Focus();
		}

		private void fPropButton_Click(object sender, EventArgs e)
		{
			lbl_selectionDummy.Focus();
		}

		private void scaleButton_Click(object sender, EventArgs e)
		{
			lbl_selectionDummy.Focus();
		}

		private void poserButton_Click(object sender, EventArgs e)
		{
			lbl_selectionDummy.Focus();
		}

		private void BGButton_Click(object sender, EventArgs e)
		{
			lbl_selectionDummy.Focus();
		}

		private void drawButton_Click_1(object sender, EventArgs e)
		{
			pnl_mainTools.Enabled = false;
			slideOutObject = pnl_Drawing;
			animTimer.Start();
		}

		private void btn_cancelButtonDraw_Click(object sender, EventArgs e)
		{
			slideOutObject = pnl_Drawing;
			animTimer.Start();
		}

		#endregion Buttons

		private void animTimer_Tick(object sender, EventArgs e)
		{
			Point endPosA = new Point(6, 142);
			Point endPosB = new Point(198, 142);

			if (!inMenu)
			{
				if (!(slideOutObject.Location.Equals(endPosA)))
				{
					pnl_mainTools.Left -= 32;
					slideOutObject.Left -= 32;
				}
				else
				{
					inMenu = true;
					animTimer.Stop();
				}
			}
			else
			{
				if (!(slideOutObject.Location.Equals(endPosB)))
				{
					pnl_mainTools.Left += 32;
					slideOutObject.Left += 32;
				}
				else
				{
					inMenu = false;
					slideOutObject.Enabled = true;
					pnl_mainTools.Enabled = true;
					animTimer.Stop();
					if (reOpen)
					{
						slideOutObject = reOpenPanel;
						reOpen = false;
						animTimer.Start();
					}
				}
			}
			Program.TheCanvas.glGraphics.Refresh();
		}

		private void Toolbox_Load(object sender, EventArgs e)
		{
			Size = new Size(179, 375);
			trv_addView.ExpandAll();
		}

		private void AddObject(object sender, TreeNodeMouseClickEventArgs e)
		{
			Type layerType = null;
			TreeNode theSender = trv_addView.SelectedNode;

			switch (theSender.Tag.ToString())
			{
				case "0":
					layerType = typeof(StickLayer); break;
				case "1":
					/* Open the stick editor.
					layerType = typeof(CustomLayer);
					Timeline.selectedLayer_Ind = Timeline.Layers.Count - 1;

					StickEditor f = new StickEditor();
					f.ShowDialog(this); */
					break;

				case "2":
					layerType = typeof(LineLayer); break;
				case "3":
					layerType = typeof(RectLayer); break;
				case "4":
					//layerType = typeof(LightLayer);
					return;

				case "5":
					//layerType = typeof(TextLayer);
					return;

				default:
					return;
			}

			int i = 1;

			foreach (Layer x in Timeline.Layers)
				if (x.GetType() == layerType)
					i++;

			Timeline.addNewLayer(layerType, layerType.ToString().Substring(0, layerType.ToString().IndexOf("Layer")) + " Layer " + i);
			Program.TheTimeline.Refresh();

			pnl_addTools.Enabled = false;
			slideOutObject = pnl_addTools;
			animTimer.Start();
		}

		private void maskedTextBox1_ValueChanged(object sender, EventArgs e)
		{
			frameRate = (byte)maskedTextBox1.Value;
		}

		private void pic_pnlLine_color_Click(object sender, EventArgs e)
		{
			if (!(dlg_Color.ShowDialog() == DialogResult.OK))
				return;

			pic_pnlLine_color.BackColor = dlg_Color.Color;
			Canvas.activeFigure.setJointsColor(dlg_Color.Color);
			Program.TheCanvas.Refresh();
		}

		private void num_pnlLine_thickness_ValueChanged(object sender, EventArgs e)
		{
			StickLine line = (StickLine)Canvas.activeFigure;
			if (!(num_pnlLine_thickness.Value == -1))
			{
				line.setThickness((int)num_pnlLine_thickness.Value);
				Program.TheCanvas.Refresh();
			}
		}

		private void pic_pnlStick_color_Click(object sender, EventArgs e)
		{
			if (!(dlg_Color.ShowDialog() == DialogResult.OK))
				return;
			pic_pnlStick_color.BackColor = dlg_Color.Color;
			Canvas.activeFigure.setJointsColor(dlg_Color.Color);
			((StickFrame)Program.TheTimeline.selectedKeyFrame).figColor = dlg_Color.Color;
			Program.TheCanvas.Refresh();
		}

		private void fPropButton_Click_1(object sender, EventArgs e)
		{
			if (Program.TheTimeline.selectedKeyFrame != null)
			{
				if (Canvas.activeFigure.figureType == 1)
				{
					pnl_Properties_Stick.Visible = true;
					pnl_Properties_Rect.Visible = false;
					pnl_Properties_Line.Visible = false;
					pic_pnlStick_color.BackColor = ((StickFrame)Program.TheTimeline.selectedKeyFrame).figColor;
					tkb_alpha.Value = ((StickFrame)Program.TheTimeline.selectedKeyFrame).figColor.A;
					num_alpha.Value = ((StickFrame)Program.TheTimeline.selectedKeyFrame).figColor.A;
					pnlOpen = panels[1];
				}
				else if (Canvas.activeFigure.figureType == 2)
				{
					pnl_Properties_Stick.Visible = false;
					pnl_Properties_Rect.Visible = false;
					pnl_Properties_Line.Visible = true;
					pic_pnlLine_color.BackColor = Canvas.activeFigure.FigureJoints[0].jointColor;
					num_pnlLine_thickness.Value = Canvas.activeFigure.FigureJoints[0].thickness;
					pnlOpen = panels[2];
				}
				else if (Canvas.activeFigure.figureType == 3)
				{
					pnl_Properties_Stick.Visible = false;
					pnl_Properties_Rect.Visible = true;
					pnl_Properties_Line.Visible = false;

					pic_rectFillColor.BackColor = Canvas.activeFigure.figColor;
					pic_rectOLColor.BackColor = Canvas.activeFigure.FigureJoints[0].jointColor;
					chk_rectFilled.Checked = ((StickRect)Canvas.activeFigure).isFilled;

					num_rectFillAlpha.Value = Canvas.activeFigure.figColor.A;
					num_rectOLAlpha.Value = Canvas.activeFigure.FigureJoints[0].jointColor.A;
					num_rectOLThickness.Value = Canvas.activeFigure.FigureJoints[0].thickness;

					pnlOpen = panels[3];
				}
				else if (Canvas.activeFigure.figureType == 4)
				{
					//StickEditor f = new StickEditor(true);
					//f.loadFigure((StickCustom)Canvas.activeFigure);
					//f.ShowDialog();
					return;
				}

				pnl_mainTools.Enabled = false;
				slideOutObject = pnl_Properties;
				animTimer.Start();
			}
		}

		private void btn_pnlLine_Cancel_Click(object sender, EventArgs e)
		{
			pnlOpen = "None";
			slideOutObject = pnl_Properties;
			animTimer.Start();
		}

		private void tkb_alpha_Scroll(object sender, EventArgs e)
		{
			num_alpha.Value = tkb_alpha.Value;
			setFigureAlpha(tkb_alpha.Value);
		}

		private void num_alpha_ValueChanged(object sender, EventArgs e)
		{
			tkb_alpha.Value = (int)num_alpha.Value;
			setFigureAlpha((int)num_alpha.Value);
		}

		private void setFigureAlpha(int value)
		{
			if (Program.TheTimeline.selectedKeyFrame == null || Program.TheTimeline.selectedLayer.LayerType == 4)
				return;

			int[] argb = new int[4];

			argb[0] = value;
			argb[1] = Canvas.activeFigure.figColor.R;
			argb[2] = Canvas.activeFigure.figColor.G;
			argb[3] = Canvas.activeFigure.figColor.B;

			if (Timeline.Layers[Timeline.selectedLayer_Ind].LayerType != 3)
			{
				List<StickJoint> sf = Canvas.activeFigure.FigureJoints;

				foreach (StickJoint a in sf)
					a.jointColor = Color.FromArgb(argb[0], argb[1], argb[2], argb[3]);
			}

			(Program.TheTimeline.selectedKeyFrame).figColor = Color.FromArgb(argb[0], argb[1], argb[2], argb[3]);

			if (Canvas.activeFigure.GetType() == typeof(StickRect))
				Canvas.activeFigure.figColor = (Color.FromArgb(argb[0], argb[1], argb[2], argb[3]));
			else
				Canvas.activeFigure.setJointsColor(Color.FromArgb(argb[0], argb[1], argb[2], argb[3]));
			Program.TheCanvas.Refresh();
		}

		private void setOLAlpha(int value)
		{
			if (Program.TheTimeline.selectedKeyFrame == null | Program.TheTimeline.selectedKeyFrame.GetType() != typeof(RectFrame))
				return;

			int[] argb = new int[4];

			argb[0] = value;
			argb[1] = Canvas.activeFigure.FigureJoints[0].jointColor.R;
			argb[2] = Canvas.activeFigure.FigureJoints[0].jointColor.G;
			argb[3] = Canvas.activeFigure.FigureJoints[0].jointColor.B;

			List<StickJoint> sf = Program.TheTimeline.selectedKeyFrame.FrameJoints;

			foreach (StickJoint a in sf)
				a.jointColor = Color.FromArgb(argb[0], argb[1], argb[2], argb[3]);

			Canvas.activeFigure.setJointsColor(Color.FromArgb(argb[0], argb[1], argb[2], argb[3]));
			Program.TheCanvas.Refresh();
		}

		public void setJointsColor(Color thecolor)
		{
			pic_pnlLine_color.BackColor = thecolor;
			pic_pnlStick_color.BackColor = thecolor;

			tkb_alpha.Value = thecolor.A;
		}

		private void btn_BGButton_Click(object sender, EventArgs e)
		{
			if (!(dlg_Color.ShowDialog() == DialogResult.OK))
				return;
			Program.TheCanvas.setBackgroundColor(dlg_Color.Color);
		}

		public void updateOpenPanel()
		{
			if (pnlOpen == "None")
				return;
			int type = Canvas.activeFigure.figureType;
			setJointsColor(Program.TheTimeline.selectedKeyFrame.figColor);

			if (type == 1)
			{
				pic_pnlStick_color.BackColor = Canvas.activeFigure.figColor;
				tkb_alpha.Value = Canvas.activeFigure.figColor.A;
				num_alpha.Value = Canvas.activeFigure.figColor.A;
				if (pnlOpen != panels[1])
				{
					pnl_Properties_Stick.Visible = true;
					pnl_Properties_Rect.Visible = false;
					pnl_Properties_Line.Visible = false;

					slideOutObject = pnl_Properties;
					reOpen = true;
					reOpenPanel = pnl_Properties;

					pnlOpen = panels[1];

					animTimer.Start();
				}
			}
			else if (type == 2)
			{
				pic_pnlLine_color.BackColor = Canvas.activeFigure.FigureJoints[0].jointColor;
				num_pnlLine_thickness.Value = Canvas.activeFigure.FigureJoints[0].thickness;
				if (pnlOpen != panels[2])
				{
					pnl_Properties_Stick.Visible = false;
					pnl_Properties_Rect.Visible = false;
					pnl_Properties_Line.Visible = true;

					slideOutObject = pnl_Properties;
					reOpen = true;
					reOpenPanel = pnl_Properties;

					pnlOpen = panels[2];

					animTimer.Start();
				}
			}
			else if (type == 3)
			{
				pic_rectFillColor.BackColor = Canvas.activeFigure.figColor;
				pic_rectOLColor.BackColor = Canvas.activeFigure.FigureJoints[0].jointColor;
				chk_rectFilled.Checked = ((StickRect)Canvas.activeFigure).isFilled;

				num_rectFillAlpha.Value = Canvas.activeFigure.figColor.A;
				num_rectOLAlpha.Value = Canvas.activeFigure.FigureJoints[0].jointColor.A;
				num_rectOLThickness.Value = Canvas.activeFigure.FigureJoints[0].thickness;
				if (pnlOpen != panels[3])
				{
					pnl_Properties_Stick.Visible = false;
					pnl_Properties_Rect.Visible = true;
					pnl_Properties_Line.Visible = false;

					slideOutObject = pnl_Properties;
					reOpen = true;
					reOpenPanel = pnl_Properties;

					pnlOpen = panels[3];

					animTimer.Start();
				}
			}
		}

		private void pic_rectFillColor_Click(object sender, EventArgs e)
		{
			if (!(dlg_Color.ShowDialog() == DialogResult.OK))
				return;
			pic_rectFillColor.BackColor = dlg_Color.Color;
			Canvas.activeFigure.figColor = (dlg_Color.Color);
			Program.TheTimeline.selectedKeyFrame.figColor = dlg_Color.Color;

			Program.TheCanvas.Refresh();
		}

		private void chk_rectFilled_CheckedChanged(object sender, EventArgs e)
		{
			((StickRect)Canvas.activeFigure).isFilled = chk_rectFilled.Checked;

			Program.TheCanvas.Refresh();
		}

		private void pic_rectOLColor_Click(object sender, EventArgs e)
		{
			if (!(dlg_Color.ShowDialog() == DialogResult.OK))
				return;
			pic_rectOLColor.BackColor = dlg_Color.Color;
			Canvas.activeFigure.setJointsColor(dlg_Color.Color);
			for (int i = 0; i < Program.TheTimeline.selectedKeyFrame.FrameJoints.Count; i++)
				Program.TheTimeline.selectedKeyFrame.FrameJoints[i].jointColor = dlg_Color.Color;

			Program.TheCanvas.Refresh();
		}

		private void num_rectFillAlpha_ValueChanged(object sender, EventArgs e)
		{
			setFigureAlpha((int)num_rectFillAlpha.Value);
			Program.TheCanvas.Refresh();
		}

		private void num_rectOLAlpha_ValueChanged(object sender, EventArgs e)
		{
			setOLAlpha((int)num_rectOLAlpha.Value);
			Program.TheCanvas.Refresh();
		}
	}
}