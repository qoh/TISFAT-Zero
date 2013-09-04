using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace TISFAT_ZERO
{
	public partial class MainF : Form
	{
		#region Variables
		public Toolbox theToolbox;
		private Canvas theCanvas;
		public Timeline tline;
		bool bChanged;
		bool bOld;

		private int lastScroll = 0, lastWidth = 650;
		private int sX, sY;
		public int layersCount = 5; 
		#endregion

		#region Form/Class Events
		public MainF()
		{
			InitializeComponent();
			this.IsMdiContainer = true;
		}

		private void Main_Load(object sender, EventArgs e)
		{
			Toolbox t = new Toolbox(this);
			t.TopLevel = false;
			t.Parent = this.splitContainer1.Panel2;
			this.splitContainer1.Panel2.Controls.Add(t);
			t.Show();

			Canvas f = new Canvas(this, t);
			f.Size = Properties.User.Default.CanvasSize;
			f.BackColor = Properties.User.Default.CanvasColor;

			f.TopLevel = false;
			f.Parent = this.splitContainer1.Panel2;
			f.StartPosition = FormStartPosition.Manual;
			f.Location = new Point(175, 10);
			this.splitContainer1.Panel2.Controls.Add(f);

			tline = new Timeline(this, f);
			tline.TopLevel = false;
			tline.Parent = this.splitContainer1.Panel1;
			tline.Size = new Size(this.splitContainer1.Width - 2, splitContainer1.Panel1.Height);
			tline.StartPosition = FormStartPosition.Manual;
			tline.Location = new Point(0, 0);
			this.splitContainer1.Panel1.Controls.Add(tline);

			uint timelineLength = 1024;

			this.framesPanel.Location = new Point((int)timelineLength * 9, 0);

			tline.Show();

			f.Show();

			theToolbox = t;
			theCanvas = f;
		} 
		#endregion

		#region Functions
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
			KeyFrame pFrame;
			String[] strInfo, strLayerName = new string[255];
			FileStream fs;
			bool bRead, bLoadNew, bTrans, bFirstLayer, bMore, bNewFormat;
			//ActionObj pAction;

			Bitmap bitty;
			MemoryStream ms;

			fs = new FileStream(strFileName, FileMode.Open);

		} 
		#endregion

		#region Form Controls
		private void MainF_Resize(object sender, EventArgs e)
		{
			int height = layersCount * 16;

			tline.Size = new Size(this.splitContainer1.Width - 2, height);
		}

		private void splitContainer1_Panel1_Resize(object sender, EventArgs e)
		{
			//int height = layersCount * 16 + 1;
			if (splitContainer1.Panel1.Height < 51)
				splitContainer1.SplitterDistance = 51;
			try
			{
				tline.Size = new Size(this.Width - 2 - (tline.Size.Height > splitContainer1.Panel1.Height ? 18 : 0), tline.Size.Height);
			}
			catch
			{
				return;
			}
		}

		private void splitContainer1_Panel1_Scroll(object sender, ScrollEventArgs e)
		{
			tline.Location = new Point(0, tline.Location.Y);
			tline.Refresh();
		}

		private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
		{
		}
		#endregion

		#region Menu Bar
		private void editToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Preferences f = new Preferences();
			f.ShowDialog();
		}

		private void drawStickToolStripMenuItem_Click(object sender, EventArgs e)
		{
			tline.addStickLayer("LAYER NAME");
			tline.Refresh();
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			About f = new About();
			f.ShowDialog();
		}

		private void exitTISFATToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		#endregion

		private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
		{
			lbl_selectionDummy.Focus();
		}
	}
}