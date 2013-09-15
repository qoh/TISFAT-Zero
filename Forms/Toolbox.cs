using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Globalization;

namespace TISFAT_ZERO
{
	public partial class Toolbox : Form
	{
		public static MainF mainForm;
		public bool isPlaying = false;
		public byte frameRate = 30;
		public bool inMenu = false;

		public Panel slideOutObject;

		public Toolbox(MainF f)
		{
			mainForm = f;
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
				mainForm.tline.stopTimer();
			else
				mainForm.tline.startTimer(frameRate);

			isPlaying = !isPlaying;
			btn_playPause.Text = isPlaying ? "Pause" : "Play";
		}

		private void drawButton_MouseClick(object sender, MouseEventArgs e)
		{
			lbl_selectionDummy.Focus();
		}

		private void addButton_Click(object sender, EventArgs e)
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
				}
			}
		}

		private void Toolbox_Load(object sender, EventArgs e)
		{
			Size = new Size(179, 375);
		}

		private void btn_addStick_Click(object sender, EventArgs e)
		{
			int i = 1;
			foreach(StickLayer k in Timeline.layers)
			{
				i++;
			}
			mainForm.tline.addStickLayer("Stick Figure " + i.ToString());
			mainForm.tline.Refresh();

			pnl_addTools.Enabled = false;
			slideOutObject = pnl_addTools;
			animTimer.Start();
		}

		private void maskedTextBox1_ValueChanged(object sender, EventArgs e)
		{
			frameRate = (byte)maskedTextBox1.Value;
		}

		private void btn_addLine_Click(object sender, EventArgs e)
		{
			int i = 1;
			foreach (Layer k in Timeline.layers)
			{
				i++;
			}
			mainForm.tline.addLineLayer("Line " + i.ToString());
			mainForm.tline.Refresh();

			pnl_addTools.Enabled = false;
			slideOutObject = pnl_addTools;
			animTimer.Start();
		}

		private void btn_addRectangle_Click(object sender, EventArgs e)
		{
			int i = 1;
			foreach (Layer k in Timeline.layers)
			{
				i++;
			}
			mainForm.tline.addRectLayer("Rectangle " + i.ToString());
			mainForm.tline.Refresh();

			pnl_addTools.Enabled = false;
			slideOutObject = pnl_addTools;
			animTimer.Start();
		}

		private void pic_pnlLine_color_Click(object sender, EventArgs e)
		{
			dlg_Color.ShowDialog();
			pic_pnlLine_color.BackColor = dlg_Color.Color;
			Canvas.activeFigure.setColor(dlg_Color.Color);
			Canvas.theCanvas.Refresh();
		}

		private void num_pnlLine_thickness_ValueChanged(object sender, EventArgs e)
		{
			StickLine line = (StickLine)Canvas.activeFigure;
			if (!(num_pnlLine_thickness.Value == -1))
			{
				line.setThickness((int)num_pnlLine_thickness.Value);
				mainForm.tline.theCanvas.Refresh();
			}
		}

		private void pic_pnlStick_color_Click(object sender, EventArgs e)
		{
			dlg_Color.ShowDialog();
			pic_pnlStick_color.BackColor = dlg_Color.Color;
			Canvas.activeFigure.setColor(dlg_Color.Color);
			Canvas.theCanvas.Refresh();
		}

		private void fPropButton_Click_1(object sender, EventArgs e)
		{
			if (Canvas.activeFigure.type == 1)
			{
				pnl_Properties_Stick.Visible = true;
				pnl_Properties_Line.Visible = false;
				pic_pnlStick_color.BackColor = Canvas.activeFigure.figColor;
				tkb_alpha.Value = Canvas.activeFigure.figColor.A;
				num_alpha.Value = Canvas.activeFigure.figColor.A;
			}
			else if (Canvas.activeFigure.type == 2)
			{
				pnl_Properties_Stick.Visible = false;
				pnl_Properties_Line.Visible = true;
				pic_pnlLine_color.BackColor = Canvas.activeFigure.figColor;
				num_pnlLine_thickness.Value = Canvas.activeFigure.Joints[0].thickness;
			}
				

			pnl_mainTools.Enabled = false;
			slideOutObject = pnl_Properties;
			animTimer.Start();
		}

		private void btn_pnlLine_Cancel_Click(object sender, EventArgs e)
		{
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
			 if (mainForm.tline.selectedKeyFrame == null)
				 return;

			int[] argb = new int[4];

			argb[0] = value;
			argb[1] = Canvas.activeFigure.figColor.R;
			argb[2] = Canvas.activeFigure.figColor.G;
			argb[3] = Canvas.activeFigure.figColor.B;

			List<StickJoint> sf = ((StickFrame)mainForm.tline.selectedKeyFrame).Joints;

			foreach(StickJoint a in sf)
				a.color = Color.FromArgb(argb[0], argb[1], argb[2], argb[3]);

			Canvas.activeFigure.setColor(Color.FromArgb(argb[0], argb[1], argb[2], argb[3]));
			Canvas.theCanvas.Refresh();
		}
	}
}