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
		//This is the set of points used for drawing the black outline of the timeline layer
		private Point[] p1 = new Point[] { new Point(79, 0), new Point(79, 15), new Point(0, 15) };

		public MainF mainForm;

		//List of layers
		public List<Layer> layers;

		public Timeline(MainF m)
		{
			InitializeComponent(); 
			mainForm = m;

			layers = new List<Layer>();
			Layer x = new StickLayer("Layer1"), y = new StickLayer("asdf"), X = new StickLayer("2346"), Y = new StickLayer("nope");
			layers.Add(x); layers.Add(X); layers.Add(y); layers.Add(Y);
			this.Refresh();
		}

		private void Timeline_Paint(object sender, PaintEventArgs e)
        {
			//Create the graphics object then clear to the background colour of the majortiy of frames (light gray)
            Graphics g = e.Graphics;
            g.Clear(Color.FromArgb(220, 220, 220));

			//Create a black pen
			Pen blk = new Pen(Color.SlateGray), bblk = new Pen(Color.Black);

			//Calculate how many frames need to be drawn and what the offset is
			int frames = (mainForm.Width-80) / 9;
            int scroll = mainForm.splitContainer1.Panel1.HorizontalScroll.Value;
			int offset = (int)Math.Max(scroll - 80d, 0) / 9;

			//Grab the font we need to use to draw strings
			Font fo = SystemFonts.DefaultFont;

			//draw the timeline layer
			g.FillRectangle(new SolidBrush(Color.CornflowerBlue), new Rectangle(0, 0, 79, layers.Count * 16 + 15));
			g.DrawLines(bblk, p1);
            g.DrawString("T I M E L I N E", fo, new SolidBrush(Color.Black), 1, 1.5f);

			//Draw each layer
			for (int a = 1; a-1 < layers.Count; a++)
			{
				g.DrawLines(bblk, new Point[] { new Point(79, 16 * a - 1), new Point(79, 16 * a + 15), new Point(0, 16 * a + 15) });
				g.DrawString(layers[a-1].name, fo, new SolidBrush(Color.Black), 1, 16 * a + 0.4f);
			}

			//Draw the timeline frames
			for (int a = offset; a - offset < frames; a++)
			{
				//Calculate where on the timeline we need to draw the frame
				int xx = (a-offset) * 9 + 80;

				//Default to cyan colour (10th frame colour)
				Color x = Color.Cyan;

				//If frame number is divisble by 100, set colour to red
				if ((a + 1) % 100 == 0)
					x = Color.FromArgb(255, 200, 255);

				//If the frame is not a special colour, don't fill it in (as it's already filled in with that colour)
				if ((a + 1) % 10 == 0)
					g.FillRectangle(new SolidBrush(x), xx, 0, 8, 16 * layers.Count - 1);

				//Write in the number
				g.DrawString(((a + 1) % 10).ToString(), fo, Brushes.Black, new PointF(xx - 1, 1));
			}

			Height = layers.Count * 16;

			//Draw all the frame outlines
			for (int a = 0, b = 88; a < frames; a++, b = 88 + 9 * a)
				g.DrawLine(blk, new Point(b, 0), new Point(b, Height));
			for (int a = 0, b = 15; a < layers.Count; a++, b = 16 * a + 15)
				g.DrawLine(blk, new Point(80, b), new Point(frames * 9 + 80, b));


			//Dispose of the pens (because apparently this is necessary)
			blk.Dispose(); bblk.Dispose();
		}
	}
}
