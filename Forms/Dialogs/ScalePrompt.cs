using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TISFAT_ZERO.Forms.Dialogs
{
	public partial class ScalePrompt : Form
	{
		List<Point> location;
		List<int> thickness;
		List<double> length;

		public ScalePrompt()
		{
			InitializeComponent();
		}

		private void tkb_Scale_Scroll(object sender, EventArgs e)
		{
			num_Scale.Value = tkb_Scale.Value;
		}

		private void num_Scale_ValueChanged(object sender, EventArgs e)
		{
			tkb_Scale.Value = (int)num_Scale.Value;

			PointF center = Functions.getFigureCenter(Canvas.activeFigure);

			for (int i = 0;i < Canvas.activeFigure.Joints.Count;i++)
			{
				Canvas.activeFigure.Joints[i].location = location[i];
				Canvas.activeFigure.Joints[i].thickness = thickness[i];
				Canvas.activeFigure.Joints[i].length = length[i];

				Canvas.activeFigure.Joints[i].Scale((float)(tkb_Scale.Value / 100.0), center);
			}

			Program.CanvasForm.Refresh();
		}

		private void btn_OK_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void ScalePrompt_Load(object sender, EventArgs e)
		{
			location = new List<Point>();
			thickness = new List<int>();
			length = new List<double>();

			foreach (StickJoint j in Canvas.activeFigure.Joints)
			{
				location.Add(j.location);
				thickness.Add(j.thickness);
				length.Add(j.length);
			}
		}

		private void btn_Cancel_Click(object sender, EventArgs e)
		{
			for (int i = 0;i < Canvas.activeFigure.Joints.Count; i++)
			{
				Canvas.activeFigure.Joints[i].location = location[i];
				Canvas.activeFigure.Joints[i].thickness = thickness[i];
				Canvas.activeFigure.Joints[i].length = length[i];
			}
			Program.CanvasForm.Refresh();
			this.Close();
		}
	}
}
