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

		public Toolbox(MainF f)
		{
			mainForm = f;
			InitializeComponent();
		}

		private void panel3_Paint(object sender, PaintEventArgs e)
		{

		}

		private void button1_MouseClick(object sender, MouseEventArgs e)
		{
			lbl_selectionDummy.Focus();
		}

		private void btn_playPause_Click(object sender, System.EventArgs e)
		{
			if(isPlaying)
				mainForm.tline.stopTimer();
			else
				mainForm.tline.startTimer(frameRate);
			isPlaying = !isPlaying;
			btn_playPause.Text = isPlaying ? "Pause" : "Play";
		}
	}

	public class NumericTextBox : TextBox
	{
		bool allowSpace = false;

		// Restricts the entry of characters to digits (including hex), the negative sign, 
		// the decimal point, and editing keystrokes (backspace). 
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);

			NumberFormatInfo numberFormatInfo = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
			string decimalSeparator = numberFormatInfo.NumberDecimalSeparator;
			string groupSeparator = numberFormatInfo.NumberGroupSeparator;
			string negativeSign = numberFormatInfo.NegativeSign;

			// Workaround for groupSeparator equal to non-breaking space 
			if (groupSeparator == ((char)160).ToString())
			{
				groupSeparator = " ";
			}

			string keyInput = e.KeyChar.ToString();

			if (char.IsDigit(e.KeyChar))
			{
				// Digits are OK
			}
			else if (keyInput.Equals(decimalSeparator) || keyInput.Equals(groupSeparator) ||
			 keyInput.Equals(negativeSign))
			{
				// Decimal separator is OK
			}
			else if (e.KeyChar == '\b')
			{
				// Backspace key is OK
			}
			//    else if ((ModifierKeys & (Keys.Control | Keys.Alt)) != 0) 
			//    { 
			//     // Let the edit control handle control and alt key combinations 
			//    } 
			else if (this.allowSpace && e.KeyChar == ' ')
			{

			}
			else
			{
				// Consume this invalid key and beep
				e.Handled = true;
				//    MessageBeep();
			}
		}

		public int IntValue
		{
			get
			{
				return Int32.Parse(this.Text);
			}
		}

		public decimal DecimalValue
		{
			get
			{
				return Decimal.Parse(this.Text);
			}
		}

		public bool AllowSpace
		{
			set
			{
				this.allowSpace = value;
			}

			get
			{
				return this.allowSpace;
			}
		}
	}
}