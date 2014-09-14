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
	public partial class CanvasSizePrompt : Form
	{
		public CanvasSizePrompt()
		{
			InitializeComponent();
		}

		private void btn_OK_Click(object sender, EventArgs e)
		{
			Program.CanvasForm.Size = new Size((int)num_Width.Value, (int)num_Height.Value);
			this.Close();
		}

		private void CanvasSizePrompt_Load(object sender, EventArgs e)
		{
			num_Width.Value = Program.CanvasForm.Size.Width;
			num_Height.Value = Program.CanvasForm.Size.Height;
		}

		private void btn_Cancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
