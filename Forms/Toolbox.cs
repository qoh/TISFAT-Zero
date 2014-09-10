using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Globalization;
using TISFAT_ZERO.Forms.Dialogs;

namespace TISFAT_ZERO
{
	public partial class Toolbox : Form
	{
		public bool isPlaying = false;
		public byte frameRate = 30;
		public bool inMenu = false;

		public Panel slideOutObject;

		public string[] panels = new String[] { "None", "Stick", "Line", "Rect", "Poly", "Paint", "Custom" };
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
			if (isPlaying)
				Program.TimelineForm.stopTimer();
			else
				Program.TimelineForm.startTimer(frameRate);

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
		#endregion

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
			Program.CanvasForm.glGraphics.Refresh();
		}

		private void Toolbox_Load(object sender, EventArgs e)
		{
			Size = new Size(179, 375);
			trv_addView.ExpandAll();
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
			Canvas.activeFigure.setColor(dlg_Color.Color);
			Program.CanvasForm.Refresh();
		}

		private void num_pnlLine_thickness_ValueChanged(object sender, EventArgs e)
		{
			StickLine line = (StickLine)Canvas.activeFigure;
			if (!(num_pnlLine_thickness.Value == -1))
			{
				line.setThickness((int)num_pnlLine_thickness.Value);
				Program.CanvasForm.Refresh();
			}
		}

		private void pic_pnlStick_color_Click(object sender, EventArgs e)
		{
			if (!(dlg_Color.ShowDialog() == DialogResult.OK))
				return;
			pic_pnlStick_color.BackColor = dlg_Color.Color;
			Canvas.activeFigure.setColor(dlg_Color.Color);
			((StickFrame)Program.TimelineForm.frm_selected).figColor = dlg_Color.Color;
			Program.CanvasForm.Refresh();
		}

		private void fPropButton_Click_1(object sender, EventArgs e)
		{
			if (Program.TimelineForm.frm_selected != null)
			{
				if (Canvas.activeFigure.type == 1)
				{
					pnl_Properties_Stick.Visible = true;
					pnl_Properties_Rect.Visible = false;
					pnl_Properties_Line.Visible = false;
					pnl_Properties_Custom.Visible = false;

					pic_pnlStick_color.BackColor = ((StickFrame)Program.TimelineForm.frm_selected).figColor;
					tkb_alpha.Value = ((StickFrame)Program.TimelineForm.frm_selected).figColor.A;
					num_alpha.Value = ((StickFrame)Program.TimelineForm.frm_selected).figColor.A;
					pnlOpen = panels[1];
				}
				else if (Canvas.activeFigure.type == 2)
				{
					pnl_Properties_Stick.Visible = false;
					pnl_Properties_Rect.Visible = false;
					pnl_Properties_Line.Visible = true;
					pnl_Properties_Custom.Visible = false;

					pic_pnlLine_color.BackColor = Canvas.activeFigure.Joints[0].color;
					num_pnlLine_thickness.Value = Canvas.activeFigure.Joints[0].thickness;
					pnlOpen = panels[2];
				}
				else if (Canvas.activeFigure.type == 3 || Canvas.activeFigure.type == 6)
				{
					pnl_Properties_Stick.Visible = false;
					pnl_Properties_Rect.Visible = true;
					pnl_Properties_Line.Visible = false;
					pnl_Properties_Custom.Visible = false;

					pic_rectFillColor.BackColor = Canvas.activeFigure.figColor;
					pic_rectOLColor.BackColor = Canvas.activeFigure.Joints[0].color;

					if (Canvas.activeFigure.type == 3)
						chk_rectFilled.Checked = ((StickRect)Canvas.activeFigure).filled;
					else
						chk_rectFilled.Checked = ((StickPoly)Canvas.activeFigure).filled;

					num_rectFillAlpha.Value = Canvas.activeFigure.figColor.A;
					num_rectOLAlpha.Value = Canvas.activeFigure.Joints[0].color.A;
					num_rectOLThickness.Value = Canvas.activeFigure.Joints[0].thickness;

					pnlOpen = panels[3];
				}
				else if (Canvas.activeFigure.type == 4)
				{
					pnl_Properties_Stick.Visible = false;
					pnl_Properties_Rect.Visible = false;
					pnl_Properties_Line.Visible = false;
					pnl_Properties_Custom.Visible = true;

					com_Properties_Bitmap.Items.Clear();
					if (!(Canvas.selectedJoint == null))
						if (Canvas.selectedJoint.bitmaps.Count > 0)
						{
							foreach (string s in Canvas.selectedJoint.Bitmap_names)
								com_Properties_Bitmap.Items.Add(s);
							com_Properties_Bitmap.SelectedItem = Canvas.selectedJoint.Bitmap_names[Canvas.selectedJoint.Bitmap_CurrentID];
						}

					pnlOpen = panels[6];
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
			if (Program.TimelineForm.frm_selected == null || Timeline.layers[Timeline.layer_sel].type == 4)
				return;

			int[] argb = new int[4];

			argb[0] = value;
			argb[1] = Canvas.activeFigure.figColor.R;
			argb[2] = Canvas.activeFigure.figColor.G;
			argb[3] = Canvas.activeFigure.figColor.B;

			if (Timeline.layers[Timeline.layer_sel].type != 3)
			{
				List<StickJoint> sf = Canvas.activeFigure.Joints;

				foreach (StickJoint a in sf)
					a.color = Color.FromArgb(argb[0], argb[1], argb[2], argb[3]);
			}

			(Program.TimelineForm.frm_selected).figColor = Color.FromArgb(argb[0], argb[1], argb[2], argb[3]);

			if (Canvas.activeFigure.type == 3)
				Canvas.activeFigure.setFillColor(Color.FromArgb(argb[0], argb[1], argb[2], argb[3]));
			else
				Canvas.activeFigure.setColor(Color.FromArgb(argb[0], argb[1], argb[2], argb[3]));
			Program.CanvasForm.Refresh();
		}

		private void setOLAlpha(int value)
		{
			if (Program.TimelineForm.frm_selected == null | Program.TimelineForm.frm_selected.GetType() != typeof(RectFrame))
				return;

			int[] argb = new int[4];

			argb[0] = value;
			argb[1] = Canvas.activeFigure.Joints[0].color.R;
			argb[2] = Canvas.activeFigure.Joints[0].color.G;
			argb[3] = Canvas.activeFigure.Joints[0].color.B;

			List<StickJoint> sf = Program.TimelineForm.frm_selected.Joints;

			foreach (StickJoint a in sf)
				a.color = Color.FromArgb(argb[0], argb[1], argb[2], argb[3]);

			Canvas.activeFigure.setColor(Color.FromArgb(argb[0], argb[1], argb[2], argb[3]));
			Program.CanvasForm.Refresh();
		}

		public void setColor(Color thecolor)
		{
			pic_pnlLine_color.BackColor = thecolor;
			pic_pnlStick_color.BackColor = thecolor;

			tkb_alpha.Value = thecolor.A;
		}

		private void btn_BGButton_Click(object sender, EventArgs e)
		{
			if (!(dlg_Color.ShowDialog() == DialogResult.OK))
				return;
			Program.CanvasForm.setBackgroundColor(dlg_Color.Color);
		}

		private void AddObject(object sender, TreeNodeMouseClickEventArgs e)
		{
			TreeNode theSender = trv_addView.SelectedNode;
			int x = 1;
			switch (theSender.Tag.ToString())
			{
				case "0":
					foreach (Layer l in Timeline.layers) { if (l.GetType() == typeof(StickLayer)) { x++; } }
					Program.TimelineForm.addStickLayer("Stick Layer " + x);
					break;
				case "1":
					foreach (Layer l in Timeline.layers) { if (l.GetType() == typeof(CustomLayer)) { x++; } }
					Program.TimelineForm.addCustomLayer("Custom Layer " + x);
					Timeline.layer_sel = Timeline.layer_cnt - 1;
					StickEditor f = new StickEditor();
					f.ShowDialog(this);
					return;

				case "2":
					foreach (Layer l in Timeline.layers) { if (l.GetType() == typeof(LineLayer)) { x++; } }
					Program.TimelineForm.addLineLayer("Line Layer " + x);
					break;
				case "3":
					foreach (Layer l in Timeline.layers) { if (l.GetType() == typeof(RectLayer)) { x++; } }
					Program.TimelineForm.addRectLayer("Rect Layer " + x);
					break;
				case "4":
					foreach (Layer l in Timeline.layers) { if (l.GetType() == typeof(LightLayer)) { x++; } }
					Program.TimelineForm.addLightLayer("Light Layer " + x);
					return;

				case "5":
					//layerType = typeof(TextLayer);
					return;

				case "6":
					TISFAT_ZERO.Forms.Dialogs.PolyPrompt p = new Forms.Dialogs.PolyPrompt();

					if(p.ShowDialog() == DialogResult.OK)
					{
						foreach (Layer l in Timeline.layers) { if (l.GetType() == typeof(PolyLayer)) { x++; } }
						Program.TimelineForm.addPolyLayer("Poly Layer " + x, Convert.ToInt32(p.numericUpDown1.Value));
					}
					return;

				case "7":
					dlg_File.Filter = Functions.GetImageFilters();

					if(dlg_File.ShowDialog() == DialogResult.OK)
					{
						foreach (Layer l in Timeline.layers) { if (l.GetType() == typeof(ImageLayer)) { x++; } }
						Program.TimelineForm.addImageLayer("Image Layer " + x, (Bitmap)Image.FromFile(dlg_File.FileName));
					}
					return;

				default:
					return;
			}
			Program.TimelineForm.Refresh();

			pnl_addTools.Enabled = false;
			slideOutObject = pnl_addTools;
			animTimer.Start();
		}

		public void updateOpenPanel()
		{
			if (pnlOpen == "None")
				return;
			int type = Canvas.activeFigure.type;
			setColor(Program.TimelineForm.frm_selected.figColor);

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
					pnl_Properties_Custom.Visible = false;

					slideOutObject = pnl_Properties;
					reOpen = true;
					reOpenPanel = pnl_Properties;

					pnlOpen = panels[1];

					animTimer.Start();
				}
			}
			else if (type == 2)
			{
				pic_pnlLine_color.BackColor = Canvas.activeFigure.Joints[0].color;
				num_pnlLine_thickness.Value = Canvas.activeFigure.Joints[0].thickness;
				if (pnlOpen != panels[2])
				{
					pnl_Properties_Stick.Visible = false;
					pnl_Properties_Rect.Visible = false;
					pnl_Properties_Line.Visible = true;
					pnl_Properties_Custom.Visible = false;

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
				pic_rectOLColor.BackColor = Canvas.activeFigure.Joints[0].color;
				chk_rectFilled.Checked = ((StickRect)Canvas.activeFigure).filled;

				num_rectFillAlpha.Value = Canvas.activeFigure.figColor.A;
				num_rectOLAlpha.Value = Canvas.activeFigure.Joints[0].color.A;
				num_rectOLThickness.Value = Canvas.activeFigure.Joints[0].thickness;
				if (pnlOpen != panels[3])
				{
					pnl_Properties_Stick.Visible = false;
					pnl_Properties_Rect.Visible = true;
					pnl_Properties_Line.Visible = false;
					pnl_Properties_Custom.Visible = false;

					slideOutObject = pnl_Properties;
					reOpen = true;
					reOpenPanel = pnl_Properties;

					pnlOpen = panels[3];

					animTimer.Start();
				}
			}
			else if (type == 4)
			{
				com_Properties_Bitmap.Items.Clear();
				if (!(Canvas.selectedJoint == null))
					if (Canvas.selectedJoint.bitmaps.Count > 0)
					{
						foreach (string s in Canvas.selectedJoint.Bitmap_names)
							com_Properties_Bitmap.Items.Add(s);
						com_Properties_Bitmap.SelectedItem = Canvas.selectedJoint.Bitmap_names[Canvas.selectedJoint.Bitmap_CurrentID];
					}
				if (pnlOpen != panels[6])
				{
					pnl_Properties_Stick.Visible = false;
					pnl_Properties_Rect.Visible = false;
					pnl_Properties_Line.Visible = false;
					pnl_Properties_Custom.Visible = true;

					slideOutObject = pnl_Properties;
					reOpen = true;
					reOpenPanel = pnl_Properties;

					pnlOpen = panels[3];

					animTimer.Start();
				}
			}
		}

		public void updateBitmapList()
		{
			com_Properties_Bitmap.Items.Clear();
			if (!(Canvas.selectedJoint == null))
				if (Canvas.selectedJoint.bitmaps.Count > 0)
				{
					foreach (string s in Canvas.selectedJoint.Bitmap_names)
						com_Properties_Bitmap.Items.Add(s);
					if (Canvas.selectedJoint.Bitmap_CurrentID != -1)
						com_Properties_Bitmap.SelectedItem = Canvas.selectedJoint.Bitmap_names[Canvas.selectedJoint.Bitmap_CurrentID];
				}
		}

		private void pic_rectFillColor_Click(object sender, EventArgs e)
		{
			if (!(dlg_Color.ShowDialog() == DialogResult.OK))
				return;
			pic_rectFillColor.BackColor = dlg_Color.Color;
			Canvas.activeFigure.setFillColor(dlg_Color.Color);
			Program.TimelineForm.frm_selected.figColor = dlg_Color.Color;

			Program.CanvasForm.Refresh();
		}

		private void chk_rectFilled_CheckedChanged(object sender, EventArgs e)
		{
			((StickRect)Canvas.activeFigure).filled = chk_rectFilled.Checked;

			Program.CanvasForm.Refresh();
		}

		private void pic_rectOLColor_Click(object sender, EventArgs e)
		{
			if (!(dlg_Color.ShowDialog() == DialogResult.OK))
				return;
			pic_rectOLColor.BackColor = dlg_Color.Color;
			Canvas.activeFigure.setColor(dlg_Color.Color);
			for (int i = 0;i < Program.TimelineForm.frm_selected.Joints.Count;i++)
				Program.TimelineForm.frm_selected.Joints[i].color = dlg_Color.Color;

			Program.CanvasForm.Refresh();
		}

		private void num_rectFillAlpha_ValueChanged(object sender, EventArgs e)
		{
			setFigureAlpha((int)num_rectFillAlpha.Value);
			Program.CanvasForm.Refresh();
		}

		private void num_rectOLAlpha_ValueChanged(object sender, EventArgs e)
		{
			setOLAlpha((int)num_rectOLAlpha.Value);
			Program.CanvasForm.Refresh();
		}

		private void ckb_renderShadows_CheckedChanged(object sender, EventArgs e)
		{
			Canvas.renderShadows = ckb_renderShadows.Checked;
			Program.CanvasForm.Refresh();
		}

		private void com_Properties_Bitmap_SelectionChangeCommitted(object sender, EventArgs e)
		{
			Program.TimelineForm.frm_selected.Joints[Canvas.selectedJoint.ParentFigure.Joints.IndexOf(Canvas.selectedJoint)].Bitmap_CurrentID = com_Properties_Bitmap.SelectedIndex;
			//Canvas.selectedJoint.Bitmap_CurrentID = com_Properties_Bitmap.SelectedIndex;
			((StickCustom)Timeline.layers[Timeline.layer_sel].tweenFig).Joints[Canvas.selectedJoint.ParentFigure.Joints.IndexOf(Canvas.selectedJoint)].Bitmap_CurrentID = com_Properties_Bitmap.SelectedIndex;
			Program.CanvasForm.Refresh();
		}

		private void btn_bitmapUseNone_Click(object sender, EventArgs e)
		{
			Program.TimelineForm.frm_selected.Joints[Canvas.selectedJoint.ParentFigure.Joints.IndexOf(Canvas.selectedJoint)].Bitmap_CurrentID = -1;
			((StickCustom)Timeline.layers[Timeline.layer_sel].tweenFig).Joints[Canvas.selectedJoint.ParentFigure.Joints.IndexOf(Canvas.selectedJoint)].Bitmap_CurrentID = -1;
			updateBitmapList();
			Program.CanvasForm.Refresh();
		}

		private void btn_Properties_Edit_Click(object sender, EventArgs e)
		{
			string tempfile = System.IO.Path.GetTempFileName();
			CustomFigSaver.saveFigure(tempfile, (StickCustom)Canvas.activeFigure);

			StickEditor sticked = new StickEditor(tempfile);
			sticked.ShowDialog();
		}

		private void ckb_LoopAnim_CheckedChanged(object sender, EventArgs e)
		{
			Program.TimelineForm.looping = ckb_LoopAnim.Checked;
		}

		private void btn_scaleButton_Click(object sender, EventArgs e)
		{
			ScalePrompt s = new ScalePrompt();
			s.ShowDialog();
		}
	}
}