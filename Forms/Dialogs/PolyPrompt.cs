using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TISFAT_ZERO.Forms.Dialogs
{
	public partial class PolyPrompt : Form
	{
		public PolyPrompt()
		{
			InitializeComponent();
			numericUpDown1.Maximum = int.MaxValue;
		}

		private void btn_OK_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btn_Cancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
