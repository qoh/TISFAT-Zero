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
	}
}