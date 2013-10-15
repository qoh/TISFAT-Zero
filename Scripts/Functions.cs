using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TISFAT_ZERO
{
	public static class Functions
	{
		//Method for centering handles.
		//TODO: Make this method more accurate.
		public static Point Center(this Rectangle rect)
		{
			return new Point(rect.Left - rect.Width / 2,
								rect.Top - rect.Height / 2);
		}

		public static double DegToRads(double d)
		{
			return (Math.PI * d) / 180;
		}

        public static int compareDrawOrder(StickJoint x, StickJoint y)
        {
            return x.drawOrder - y.drawOrder;
        }

		public static Point calcFigureDiff(StickJoint a, StickJoint b)
		{
			int x1 = a.location.X;
			int y1 = a.location.Y;

			int x2 = b.location.X;
			int y2 = b.location.Y;

			return new Point(x2 - x1, y2 - y1);
		}

		public static Point calcFigureDiff(Point a, StickJoint b)
		{
			int x1 = a.X;
			int y1 = a.Y;

			int x2 = b.location.X;
			int y2 = b.location.Y;

			return new Point(x2 - x1, y2 - y1);
		}

		public static void recalcFigureJoints(StickObject figure)
		{
			for (int i = 0; i < figure.Joints.Count; i++)
			{
				if (figure.Joints[i].parent != null)
				{
					figure.Joints[i].CalcLength(null);
				}
			}

			for (int i = 0; i < figure.Joints.Count; i++)
			{
				if (figure.Joints[i].parent != null)
				{
					if (!(figure.Joints[i].parent.children.IndexOf(figure.Joints[i]) >= 0))
						figure.Joints[i].parent.children.Add(figure.Joints[i]);
				}
				figure.Joints[i].ParentFigure = figure;
			}
		}
	}
}
