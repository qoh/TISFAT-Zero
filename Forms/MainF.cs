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
	partial class MainF : Form
	{
		public Timeline timeline;

		public MainF()
		{
			InitializeComponent();
		}

		private void MainF_Load(object sender, EventArgs e)
		{
			timeline = new Timeline(this);

			timeline.TopLevel = false;
			timeline.Parent = splitContainer1.Panel1;
			timeline.Size = new Size(splitContainer1.Panel1.Width - 2, splitContainer1.Panel1.Height - 2);
			timeline.StartPosition = FormStartPosition.Manual;
			timeline.Location = new Point(0, 0);

			splitContainer1.Panel1.Controls.Add(timeline);

			timeline.Show();
			timeline.Invalidate();

            timeline.Width = splitContainer1.Panel1.Width - 2;
            timeline.Height = splitContainer1.Panel1.Height - 2;
		}

        private void splitContainer1_Panel1_Resize(object sender, EventArgs e)
        {
            if (timeline == null)
            {
                return;
            }

            timeline.Width = splitContainer1.Panel1.Width - 2;
			timeline.Height = splitContainer1.Panel1.Height - 3;
			timeline.Height++;
        }

		private void splitContainer1_SplitterMoving(object sender, SplitterCancelEventArgs e)
		{
			timeline.Height = splitContainer1.Panel1.Height - 3;
			timeline.Invalidate();
		}
	}
}
