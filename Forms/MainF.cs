using System;
using System.Drawing;
using System.Windows.Forms;

namespace TISFAT_Zero
{
	partial class MainF : Form
	{
		public MainF()
		{
			InitializeComponent();
		}

		private void MainF_Load(object sender, EventArgs e)
		{
			Program.TheTimeline = new Timeline(this);

			Program.TheTimeline.TopLevel = false;
			Program.TheTimeline.Parent = splitContainer1.Panel1;
			Program.TheTimeline.Size = new Size(splitContainer1.Panel1.Width - 2, splitContainer1.Panel1.Height - 2);
			Program.TheTimeline.StartPosition = FormStartPosition.Manual;
			Program.TheTimeline.Location = new Point(0, 0);

			splitContainer1.Panel1.Controls.Add(Program.TheTimeline);

			Program.TheTimeline.Show();
			Program.TheTimeline.Invalidate();

			Program.TheTimeline.Width = splitContainer1.Panel1.Width - 2;
			Program.TheTimeline.Height = splitContainer1.Panel1.Height - 2;

			Program.TheCanvas = new Canvas();
			Program.TheCanvas.TopLevel = false;
			Program.TheCanvas.Size = new Size(460, 360);
			Program.TheCanvas.StartPosition = FormStartPosition.Manual;
			Program.TheCanvas.Location = new Point(200, 10);

			splitContainer1.Panel2.Controls.Add(Program.TheCanvas);

			Program.TheToolbox = new Toolbox();
			Program.TheToolbox.TopLevel = false;
			Program.TheToolbox.Parent = this.splitContainer1.Panel2;

			splitContainer1.Panel2.Controls.Add(Program.TheToolbox);

			Program.TheToolbox.Show();
			Program.TheCanvas.Show();

			Timeline.doRender();
		}

		private void splitContainer1_Panel1_Resize(object sender, EventArgs e)
		{
			if (Program.TheTimeline == null)
				return;

			Program.TheTimeline.Width = splitContainer1.Panel1.Width - 2;
			Program.TheTimeline.Height = splitContainer1.Panel1.Height - 2;
			Program.TheCanvas.GL_GRAPHICS.Location = new System.Drawing.Point(0, 0);
			Timeline.doRender();
		}

		private void splitContainer1_SplitterMoving(object sender, SplitterCancelEventArgs e)
		{
			Program.TheTimeline.Invalidate();
		}
	}
}