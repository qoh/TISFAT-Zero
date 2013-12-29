using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace TISFAT_Zero
{
	static class Functions
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
			for (int i = 0; i < figure.FigureJoints.Count; i++)
			{
				if (figure.FigureJoints[i].parentJoint != null)
				{
					figure.FigureJoints[i].CalcLength(null);
				}
			}

			for (int i = 0; i < figure.FigureJoints.Count; i++)
			{
				if (figure.FigureJoints[i].parentJoint != null)
				{
					if (!(figure.FigureJoints[i].parentJoint.childJoints.IndexOf(figure.FigureJoints[i]) >= 0))
						figure.FigureJoints[i].parentJoint.childJoints.Add(figure.FigureJoints[i]);
				}
				figure.FigureJoints[i].parentFigure = figure;
			}
		}

		public static string GetImageFilters()
		{
			StringBuilder allImageExtensions = new StringBuilder();
			string separator = "";
			ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
			Dictionary<string, string> images = new Dictionary<string, string>();
			foreach (ImageCodecInfo codec in codecs)
			{
				allImageExtensions.Append(separator);
				allImageExtensions.Append(codec.FilenameExtension);
				separator = ";";
				images.Add(string.Format("{0} Files: ({1})", codec.FormatDescription, codec.FilenameExtension),
						   codec.FilenameExtension);
			}
			StringBuilder sb = new StringBuilder();
			if (allImageExtensions.Length > 0)
			{
				sb.AppendFormat("{0}|{1}", "All Images", allImageExtensions.ToString());
			}
			images.Add("All Files", "*.*");
			foreach (KeyValuePair<string, string> image in images)
			{
				sb.AppendFormat("|{0}|{1}", image.Key, image.Value);
			}
			return sb.ToString();
		}
	}
}