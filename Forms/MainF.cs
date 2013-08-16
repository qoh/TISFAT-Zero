using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace TISFAT_ZERO
{
	public partial class MainF : Form
	{
		private Toolbox theToolbox;
		private Canvas theCanvas;
        bool bChanged;
        bool bOld;

		private int lastScroll = 0, lastWidth = 650;
		private int sX, sY;
        

		public MainF()
		{
			InitializeComponent();
			this.IsMdiContainer = true;
		}

		private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
		{
		}

		private void exitTISFATToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void Main_Load(object sender, EventArgs e)
		{
			Toolbox t = new Toolbox(this);
			t.TopLevel = false;
			t.Parent = this.splitContainer1.Panel2;
			this.splitContainer1.Panel2.Controls.Add(t);
			t.Show();

			Canvas f = new Canvas(this, t);
			f.TopLevel = false;
			f.Parent = this.splitContainer1.Panel2;
			f.StartPosition = FormStartPosition.Manual;
			f.Location = new Point(175, 10);
			this.splitContainer1.Panel2.Controls.Add(f);

			Timeline ti = new Timeline(this);
			ti.TopLevel = false;
			ti.Parent = this.splitContainer1.Panel1;
			ti.StartPosition = FormStartPosition.Manual;
			ti.Location = new Point(0, 0);
			this.splitContainer1.Panel1.Controls.Add(ti);

			ti.Show();
			
			f.Show();
			//this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
			theToolbox = t;
			theCanvas = f;
		}

		private void panel4_Paint(object sender, PaintEventArgs e)
		{
		}

		private void editToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//Preferences f = new Preferences();
			//f.ShowDialog();
		}

		private void Main_ResizeEnd(object sender, EventArgs e)
		{
			//label2.Location = new Point(label2.Parent.Location.X / 2, label2.Parent.Location.Y / 2);
		}

		private void drawStickToolStripMenuItem_Click(object sender, EventArgs e)
		{
			StickFigure f = new StickFigure();
			f.Draw(true);
		}

		private void timelinePanel_Paint(object sender, PaintEventArgs e)
		{
			/*
			Pen lp = new Pen(Color.Black);
			//Calculate how many frames need to be drawn
			int frames = (int)Math.Min(Math.Ceiling(timelinePanel.Width / 9d), Math.Ceiling(this.Width / 9d));

			int p1Hscroll = panel1.HorizontalScroll.Value;
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

			switch (type)
			{
				case 0:
					frames = (int)Math.Ceiling((p1Hscroll - lastScroll) / 9d) + 1;
					offset += (int)Math.Ceiling(panel1.Width / 9d) - frames;

					break;
				case 1:
					frames = (int)Math.Ceiling((lastScroll - p1Hscroll) / 9d) + 1;

					break;
				case 2:


					break;
				case 3:
					return;

					break;
				default:
					return;
			}
			
			lastWidth = this.Width;
			lastScroll = p1Hscroll;

			Graphics g = timelinePanel.CreateGraphics();

			Font fo = SystemFonts.DefaultFont;
			for (int a = offset; a-offset < frames; a++)
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

					g.DrawString(((a + 1) % 10).ToString(), fo, Brushes.Black, new PointF(xx-1, 1));
				}
			}
			//
			g.Dispose();
			lp.Dispose();
			 */
		}

		private void drawFrame(Graphics g, Pen lp, Color C, int x, int y) //x, y makes up top corner of pixel
		{
			g.DrawLines(lp, new Point[] { new Point(x + 8, y), new Point(x + 8, y + 15), new Point(x, y + 15) });
			g.FillRectangle(new SolidBrush(C), x, y, 8, 15);
		}

		private void drawFrame(Graphics g, Pen lp, Color C, int x, int y, char c) //x, y makes up top corner of pixel
		{
			g.DrawLines(lp, new Point[] { new Point(x + 8, y), new Point(x + 8, y + 15), new Point(x, y + 15) });
			g.FillRectangle(new SolidBrush(C), x, y, 8, 15);
			Font fo = SystemFonts.DefaultFont;

			g.DrawString("" + c, fo, Brushes.Black, new PointF(x - 1, y + 1));
		}

		private void timelinePanel_MouseMove(object sender, MouseEventArgs e)
		{
		}

		private void layersPanel_Paint(object sender, PaintEventArgs e)
		{
			/*
			Pen lp = new Pen(Color.DarkGray), blk = new Pen(Color.Black);
			Graphics g = layersPanel.CreateGraphics();
			g.DrawLines(blk, new Point[] { new Point(79, 0), new Point(79, 31), new Point(0, 31) });
			g.DrawLine(blk, new Point(0, 15), new Point(79, 15));
			g.FillRectangles(new SolidBrush(Color.CornflowerBlue), new Rectangle[] { new Rectangle(0, 0, 79, 15), new Rectangle(0, 16, 79, 15) });
			g.DrawString("T I M E L I N E", SystemFonts.DefaultFont, new SolidBrush(Color.Black), 1, 1.5f);
			g.DrawString("V I D E O", SystemFonts.DefaultFont, new SolidBrush(Color.Black), 13, 17);
			 */
		}

		private void timelinePanel_MouseDown(object sender, MouseEventArgs e)
		{
			/*
			int sX = e.X / 9, sY = e.Y / 16;
			Graphics g = timelinePanel.CreateGraphics();
			Pen lp = new Pen(Color.Black);
			drawFrame(g, lp, Color.Red, sX * 9, sY * 16);
			g.Dispose();
			lp.Dispose();

			onFrameSelected();
			 */
		}

		private void onFrameSelected()
		{
			//Do stuff here
		}



        //Outline for loading files.
        public void LoadFile(string strFileName)
        {
            int f, g, h, i, nFrameSetCount, nFramesCount, x, y, nWide, nHigh, nType, nActionCount, misc, nSkip;
            Layer pLayer;
            //SingleFrame pFrameSet
            Frame pFrame;
            String[] strInfo, strLayerName = new string[255];
            FileStream fs;
            bool bRead, bLoadNew, bTrans, bFirstLayer, bMore, bNewFormat;
            //ActionObj pAction;

            Bitmap bitty;
            MemoryStream ms;

            fs = new FileStream(strFileName, FileMode.Open);

        }
	}
}