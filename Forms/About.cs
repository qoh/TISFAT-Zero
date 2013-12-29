using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TISFAT_Zero
{
	public partial class About : Form
	{
		public About()
		{
			InitializeComponent();
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listBox1.SelectedIndex == 0)
			{
				pnl_About.Visible = true;
				pnl_Developers.Visible = false;
				pnl_Thanks.Visible = false;
			}
			else if (listBox1.SelectedIndex == 1)
			{
				pnl_About.Visible = false;
				pnl_Developers.Visible = true;
				pnl_Thanks.Visible = false;
			}
			else if (listBox1.SelectedIndex == 2)
			{
				pnl_About.Visible = false;
				pnl_Developers.Visible = false;
				pnl_Thanks.Visible = true;
			}
		}

		private void About_Load(object sender, EventArgs e)
		{
			pnl_About.BringToFront();
			listBox1.SelectedIndex = 0;
		}
	}
}
