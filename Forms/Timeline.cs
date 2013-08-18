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

		public Timeline(MainF m)
		{
			InitializeComponent();
			mainForm = m;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
		}

		private void Timeline_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.White);
			Pen lp = new Pen(Color.Black);
			//Calculate how many frames need to be drawn
			int frames = (mainForm.Width-80) / 9;
            int scroll = mainForm.splitContainer1.Panel1.HorizontalScroll.Value;
			int offset = (int)Math.Max(scroll - 80d, 0) / 9;


            Pen blk = new Pen(Color.Black);
            g.DrawLines(blk, new Point[] { new Point(79, 0), new Point(79, 15), new Point(0, 15) });
            g.FillRectangle(new SolidBrush(Color.CornflowerBlue), new Rectangle(0, 0, 79, 15));
            g.DrawString("T I M E L I N E", SystemFonts.DefaultFont, new SolidBrush(Color.Black), 1, 1.5f);
			Font fo = SystemFonts.DefaultFont;
			for (int a = offset; a - offset < frames; a++)
			{
				int xx = (a-offset) * 9 + 80;

                if ((a + 1) % 10 > 0)
                {
                    g.DrawLines(lp, new Point[] { new Point(xx + 8, 0), new Point(xx + 8, 15), new Point(xx, 15) });
                    g.FillRectangle(new SolidBrush(Color.LightGray), xx, 0, 8, 15);

                    g.DrawString(((a + 1) % 10).ToString(), fo, Brushes.Black, new PointF(xx - 1, 1));
                }
                else if ((a + 1) % 100 == 0)
                {
                    g.DrawLines(lp, new Point[] { new Point(xx + 8, 0), new Point(xx + 8, 15), new Point(xx, 15) });
                    g.FillRectangle(new SolidBrush(Color.Pink), xx, 0, 8, 15);

                    g.DrawString(((a + 1) % 10).ToString(), fo, Brushes.Black, new PointF(xx - 1, 1));
                }
				else
				{
					g.DrawLines(lp, new Point[] { new Point(xx + 8, 0), new Point(xx + 8, 15), new Point(xx, 15) });
					g.FillRectangle(new SolidBrush(Color.Cyan), xx, 0, 8, 15);

					g.DrawString(((a + 1) % 10).ToString(), fo, Brushes.Black, new PointF(xx - 1, 1));
				}
			}

			lp.Dispose();
		}
	}
}
