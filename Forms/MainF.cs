using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using TISFAT_ZERO.Forms;

namespace TISFAT_ZERO
{
	public partial class MainF : Form
	{
		private Toolbox theToolbox;
		private Canvas theCanvas;
        private Timeline tline;
        bool bChanged;
        bool bOld;

		private int lastScroll = 0, lastWidth = 650;
		private int sX, sY;
        public int layersCount = 2;

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

			tline = new Timeline(this);
            tline.TopLevel = false;
			tline.Parent = this.splitContainer1.Panel1;
            tline.Size = new Size(this.splitContainer1.Width - 2, splitContainer1.Panel1.Height);
			tline.StartPosition = FormStartPosition.Manual;
			tline.Location = new Point(0, 0);
			this.splitContainer1.Panel1.Controls.Add(tline);

            this.framesPanel.Location = new Point(9080, 0);

            tline.Show();
			
			f.Show();
			
			theToolbox = t;
			theCanvas = f;
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

        private void MainF_Resize(object sender, EventArgs e)
        {
            int height = layersCount * 16;

            tline.Size = new Size(this.splitContainer1.Width - 2, height);
        }

        private void splitContainer1_Panel1_Resize(object sender, EventArgs e)
        {
            int height = layersCount * 16;
            if (splitContainer1.Panel1.Height < 32)
                splitContainer1.SplitterDistance = 32;
            try
            {
                tline.Size = new Size(this.Width - 2 - (height > splitContainer1.Panel1.Height ? 18 : 0), height);
            }
            catch
            {
                return;
            }
        }

        private void splitContainer1_Panel1_Scroll(object sender, ScrollEventArgs e)
        {
            tline.Location = new Point(0, 0);
            tline.Refresh();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About f = new About();
            f.ShowDialog();
        }
	}
}