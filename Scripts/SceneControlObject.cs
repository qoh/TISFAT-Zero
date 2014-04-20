using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TISFAT_ZERO.UserControls
{
	public partial class SceneControlObject : UserControl
	{
		public SceneControlObject()
		{
			InitializeComponent();
		}

		private void pnl_mainPanel_MouseEnter(object sender, EventArgs e)
		{
			this.OnMouseEnter(e);
		}

		private void pnl_mainPanel_MouseLeave(object sender, EventArgs e)
		{
			this.OnMouseLeave(e);
		}

		private void label1_MouseEnter(object sender, EventArgs e)
		{
			this.OnMouseEnter(e);
		}
	}
}
