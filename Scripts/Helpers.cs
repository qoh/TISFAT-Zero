using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text;
using System.Drawing.Imaging;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using TISFAT_ZERO.Scripts;

namespace TISFAT_ZERO
{
	public struct DoublePoint
	{
		public double X, Y;

		public DoublePoint(double a, double b)
		{
			X = a;
			Y = b;
		}
	}

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

		public static PointF getFigureCenter(StickObject fig)
		{
			float? x1 = null, y1 = null;
			float? x2 = null, y2 = null;

			foreach(StickJoint joint in fig.Joints)
			{
				x1 = x1 != null ? Math.Min(x1.Value, joint.location.X) : joint.location.X;
				y1 = y1 != null ? Math.Min(y1.Value, joint.location.Y) : joint.location.Y;
				x2 = x2 != null ? Math.Max(x2.Value, joint.location.X) : joint.location.X;
				y2 = y2 != null ? Math.Max(y2.Value, joint.location.Y) : joint.location.Y;
			}

			// Drawing.DrawGraphics(0, Color.Green, new Point((int)Math.Round(x1.Value), (int)Math.Round(y1.Value)), 2, 0, new Point((int)Math.Round(x1.Value), (int)Math.Round(y2.Value)));
			// Drawing.DrawGraphics(0, Color.Green, new Point((int)Math.Round(x1.Value), (int)Math.Round(y2.Value)), 2, 0, new Point((int)Math.Round(x2.Value), (int)Math.Round(y2.Value)));
			// Drawing.DrawGraphics(0, Color.Green, new Point((int)Math.Round(x2.Value), (int)Math.Round(y2.Value)), 2, 0, new Point((int)Math.Round(x2.Value), (int)Math.Round(y1.Value)));
			// Drawing.DrawGraphics(0, Color.Green, new Point((int)Math.Round(x2.Value), (int)Math.Round(y1.Value)), 2, 0, new Point((int)Math.Round(x1.Value), (int)Math.Round(y1.Value)));

			return new PointF((x2.Value + x1.Value) / 2, (y2.Value + y1.Value) / 2);
		}

		public static Point getFigureCenter(StickObject fig, int derp)
		{
			PointF center = getFigureCenter(fig);
			return new Point((int)Math.Round(center.X), (int)Math.Round(center.Y));
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

		public static void AssignGlid(StickJoint joint, int i)
		{
			BitmapData raw;
			try
			{
				raw = joint.bitmaps[i].LockBits(new Rectangle(0, 0, joint.bitmaps[i].Width, joint.bitmaps[i].Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			}
			catch(Exception e)
			{
				Console.WriteLine("Adding texture ID Failed! Reason: " + e.Message);
				return;
			}

			joint.textureIDs.Add(GL.GenTexture());
			//Console.WriteLine("Texture ID Added");

			GL.BindTexture(TextureTarget.Texture2D, joint.textureIDs[i]);

			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, raw.Width, raw.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, raw.Scan0);

			joint.bitmaps[i].UnlockBits(raw);

			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
		}
		public static byte[] GetBytes(string str)
		{
			byte[] bytes = new byte[str.Length * sizeof(char)];
			System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
			return bytes;
		}

		public static int GetByteCount(string str)
		{
			byte[] bytes = new byte[str.Length * sizeof(char)];
			System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
			return bytes.Length;
		}

		public static string GetString(byte[] bytes)
		{
			char[] chars = new char[bytes.Length / sizeof(char)];
			System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
			return new string(chars);
		}

		public static double lerp(double t, double a, double b)
		{
			return a + (b - a) * t;
		}

		public static double sine(double t, double a, double b)
		{
			return lerp(Math.Sin(t * (Math.PI / 2)), a, b);
		}
	}
}

namespace TISFAT_ZERO.ExtensionMethods
{
	public static class Extensions
	{
		public static byte[] ToByteArray(this Bitmap bitty)
		{
			ImageConverter converter = new ImageConverter();
			return (byte[])converter.ConvertTo(bitty, typeof(byte[]));
		}

	}
}