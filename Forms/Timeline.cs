using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TISFAT_ZERO
{
	public partial class Timeline : Form
	{
		public MainF mainForm;
		private int lastScroll = 0, lastWidth = 1932;

		public Timeline(MainF m)
		{
			InitializeComponent();
			mainForm = m;
		}

		private void Timeline_Paint(object sender, PaintEventArgs e)
		{
			Pen lp = new Pen(Color.Black);
			//Calculate how many frames need to be drawn
			int frames = (int)Math.Min(Math.Ceiling(this.Width / 9d), Math.Ceiling(mainForm.Width / 9d));

			int p1Hscroll = mainForm.splitContainer1.Panel1.HorizontalScroll.Value;
			byte type = 0; // 0: end draw, 1: start draw, 2: full redraw, 3: no redraw
			if (p1Hscroll > lastScroll)
				type = 0;
			else if (p1Hscroll < lastScroll)
				type = 1;
			else if (this.Width > lastWidth)
				type = 2;
			else if (this.Width < lastWidth)
				type = 3;
			else //Otherwise it's safe to say we should redraw the entire thing.
				type = 2;


			int offset = p1Hscroll / 9;

			/*switch (type)
			{
				case 0:
					frames = (int)Math.Ceiling((p1Hscroll - lastScroll) / 9d) + 2;
					offset += (int)Math.Ceiling(mainForm.Width / 9d) - frames - 1;

					break;
				case 1:
					frames = (int)Math.Ceiling((lastScroll - p1Hscroll) / 9d) + 2;

					break;
				case 2:


					break;
				case 3:
					return;

					break;
				default:
					return;
			}*/

			lastWidth = mainForm.Width;
			lastScroll = p1Hscroll;

            Graphics g = e.Graphics;

			Font fo = SystemFonts.DefaultFont;
			for (int a = offset; a - offset < frames; a++)
			{
				int xx = a * 9;
				if ((a + 1) % 100 == 0)
				{
					g.DrawLines(lp, new Point[] { new Point(xx + 8, 0), new Point(xx + 8, 15), new Point(xx, 15) });
					g.FillRectangle(new SolidBrush(Color.Pink), a * 9, 0, 8, 15);

					g.DrawString(((a + 1) % 10).ToString(), fo, Brushes.Black, new PointF(xx - 1, 1));
				}
				else if ((a + 1) % 10 == 0)
				{
					g.DrawLines(lp, new Point[] { new Point(xx + 8, 0), new Point(xx + 8, 15), new Point(xx, 15) });
					g.FillRectangle(new SolidBrush(Color.Cyan), a * 9, 0, 8, 15);

					g.DrawString(((a + 1) % 10).ToString(), fo, Brushes.Black, new PointF(xx - 1, 1));
				}
				else
				{
					g.DrawLines(lp, new Point[] { new Point(xx + 8, 0), new Point(xx + 8, 15), new Point(xx, 15) });
					g.FillRectangle(new SolidBrush(Color.LightGray), a * 9, 0, 8, 15);

					g.DrawString(((a + 1) % 10).ToString(), fo, Brushes.Black, new PointF(xx - 1, 1));
				}
			}
			
			//g.Dispose();
			lp.Dispose();
		}
	}
}
