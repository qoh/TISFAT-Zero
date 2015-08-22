using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TISFAT.UI.Controls
{
	public partial class SeparatorControl : UserControl
	{
		public SeparatorControl()
		{
			InitializeComponent();
		}

		private void SeparatorControl_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			e.Graphics.Clear(BackColor);
			e.Graphics.DrawLine(new Pen(new SolidBrush(ForeColor)), new Point(Width / 2, 0), new Point(Width / 2, Height));
		}
	}
}
