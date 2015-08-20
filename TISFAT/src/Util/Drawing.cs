using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace TISFAT.Util
{
	public static class Drawing
	{
		#region CACHE MONSTERS INSIDE PLEASE NO STAY IN THE CAGE
		private static Dictionary<Font, Dictionary<Color, Dictionary<Size, Dictionary<StringAlignment, Dictionary<string, int>>>>> TextRectCache = new Dictionary<Font, Dictionary<Color, Dictionary<Size, Dictionary<StringAlignment, Dictionary<string, int>>>>>();
		private static Dictionary<string, int> BitmapCache = new Dictionary<string, int>();
		#endregion

		private static Vector2 PointToVector(PointF point)
		{
			return new Vector2(point.X, point.Y);
		}

		public static int GenerateTexID(Bitmap img)
		{
			int ind;
			GenerateTexID(img, out ind);

			return ind;
		}

		public static void GenerateTexID(Bitmap img, out int ID)
		{
			BitmapData raw;
			try
			{
				raw = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			}
			catch (Exception e)
			{
				ID = -1;
				Console.WriteLine("Couldn't generate texture ID: " + e.Message);
				return;
			}

			ID = GL.GenTexture();

			GL.BindTexture(TextureTarget.Texture2D, ID);

			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, raw.Width, raw.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, raw.Scan0);

			img.UnlockBits(raw);

			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
		}

		public static int GetCachedTextRect(string text, Size area, Font font, Color color, StringAlignment alignment)
		{
			if (!TextRectCache.ContainsKey(font))
				TextRectCache[font] = new Dictionary<Color, Dictionary<Size, Dictionary<StringAlignment, Dictionary<string, int>>>>();

			if (!TextRectCache[font].ContainsKey(color))
				TextRectCache[font][color] = new Dictionary<Size, Dictionary<StringAlignment, Dictionary<string, int>>>();

			if (!TextRectCache[font][color].ContainsKey(area))
				TextRectCache[font][color][area] = new Dictionary<StringAlignment, Dictionary<string, int>>();

			if (!TextRectCache[font][color][area].ContainsKey(alignment))
				TextRectCache[font][color][area][alignment] = new Dictionary<string, int>();

			if (!TextRectCache[font][color][area][alignment].ContainsKey(text))
			{
				Bitmap bmp = new Bitmap(area.Width, area.Height);

				using (Graphics g = Graphics.FromImage(bmp))
				{
					g.Clear(Color.Transparent);
					g.SmoothingMode = SmoothingMode.AntiAlias;
					g.InterpolationMode = InterpolationMode.HighQualityBicubic;
					g.PixelOffsetMode = PixelOffsetMode.HighQuality;
					g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

					StringFormat format = new StringFormat();
					format.Alignment = alignment;
					format.LineAlignment = alignment;

					g.DrawString(text, font, new SolidBrush(color), new Rectangle(new Point(0, 0), area), format);
				}

				TextRectCache[font][color][area][alignment][text] = GenerateTexID(bmp);
			}

			return TextRectCache[font][color][area][alignment][text];
		}

		public static int GetCachedBitmap(string path)
		{
			if (!BitmapCache.ContainsKey(path))
			{
				Bitmap img = new Bitmap(path);

				BitmapCache[path] = GenerateTexID(img);
			}

			return BitmapCache[path];
		}

		public static void Line(PointF start, PointF end, Color color)
		{
			GL.Begin(PrimitiveType.Lines);
			GL.Color3(color);
			//GL.Vertex2(PointToVector(start));
			//GL.Vertex2(PointToVector(end));
			GL.Vertex2(Math.Floor(start.X) + 0.5, Math.Floor(start.Y) + 0.5);
			GL.Vertex2(Math.Floor(end.X) + 0.5, Math.Floor(end.Y) + 0.5);
			GL.End();
		}

		public static void QuadLine(PointF start, PointF end, float thickness, Color color)
		{
			float dx = end.X - start.X;
			float dy = end.Y - start.Y;
			float dm = (float)Math.Sqrt(dx * dx + dy * dy);
			
			float px = (dy / dm) * thickness;
			float py = -(dx / dm) * thickness;

			GL.Begin(PrimitiveType.Quads);
			GL.Color3(color);
			GL.Vertex2(new Vector2(start.X - px, start.Y - py));
			GL.Vertex2(new Vector2(end.X - px, end.Y - py));
			GL.Vertex2(new Vector2(end.X + px, end.Y + py));
			GL.Vertex2(new Vector2(start.X + px, start.Y + py));
			GL.End();
		}

		public static void CappedLine(PointF start, PointF end, float thickness, Color color)
		{
			QuadLine(start, end, thickness, color);
			Circle(start, thickness, color);
			Circle(end, thickness, color);
		}

		public static void Circle(PointF center, float radius, Color color)
		{
			Circle(center, radius, color, (int)Math.Ceiling(radius * 2));
		}

		public static void Circle(PointF center, float radius, Color color, int segments)
		{
			GL.Begin(PrimitiveType.TriangleFan);
			GL.Color3(color);
			GL.Vertex2(PointToVector(center));

			for (int i = 0; i < segments; i++)
			{
				float theta = ((float)i / ((float)segments - 1)) * ((float)Math.PI * 2);

				GL.Vertex2(new Vector2(
					center.X + (float)Math.Cos(theta) * radius,
					center.Y + (float)Math.Sin(theta) * radius
				));
			}

			GL.End();
		}

		public static void Rectangle(PointF position, SizeF size, Color color)
		{
			GL.Begin(PrimitiveType.Quads);
			GL.Color3(color);
			GL.Vertex2(PointToVector(position));
			GL.Vertex2(position.X + size.Width, position.Y);
			GL.Vertex2(position.X + size.Width, position.Y + size.Height);
			GL.Vertex2(position.X, position.Y + size.Height);
			GL.End();
		}

		public static void RectangleLine(PointF position, SizeF size, Color color)
		{
			GL.Begin(PrimitiveType.LineLoop);
			GL.Color3(color);
			GL.Vertex2(Math.Floor(position.X) + 0.5, Math.Floor(position.Y) + 0.5);
			GL.Vertex2(Math.Floor(position.X + size.Width) + 0.5, Math.Floor(position.Y) + 0.5);
			GL.Vertex2(Math.Floor(position.X + size.Width) + 0.5, Math.Floor(position.Y + size.Height) + 0.5);
			GL.Vertex2(Math.Floor(position.X) + 0.5, Math.Floor(position.Y + size.Height) + 0.5);
			GL.End();
		}

		public static void Bitmap(PointF position, SizeF size, int texID)
		{
			GL.BindTexture(TextureTarget.Texture2D, texID);

			GL.Enable(EnableCap.Texture2D);
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.OneMinusSrcAlpha);

			GL.PushMatrix();
			GL.Translate(position.X, position.Y, 0);

			GL.Color3(Color.White);

			GL.Begin(PrimitiveType.Quads);
			GL.TexCoord2(0, 0); GL.Vertex2(0, 0);
			GL.TexCoord2(0, 1); GL.Vertex2(0, size.Height);
			GL.TexCoord2(1, 1); GL.Vertex2(size.Width, size.Height);
			GL.TexCoord2(1, 0); GL.Vertex2(size.Width, 0);
			GL.End();

			GL.PopMatrix();

			GL.Disable(EnableCap.Blend);
			GL.Disable(EnableCap.Texture2D);
		}

		public static void Bitmap(PointF position, SizeF size, string path)
		{
			Bitmap(position, size, GetCachedBitmap(path));
		}

		public static void Text(string Text, PointF position, Font font, Color color)
		{
			Size size = TextRenderer.MeasureText(Text, font);

			Bitmap bmp = new Bitmap(size.Width, size.Height);

			using (Graphics g = Graphics.FromImage(bmp))
			{
				g.Clear(Color.Transparent);
				g.SmoothingMode = SmoothingMode.AntiAlias;
				g.InterpolationMode = InterpolationMode.HighQualityBicubic;
				g.PixelOffsetMode = PixelOffsetMode.HighQuality;
				g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

				g.DrawString(Text, font, new SolidBrush(color), new Point(0, 0));
			}

			int id = GenerateTexID(bmp);

			Bitmap(position, size, id);

			GL.DeleteTexture(id);
		}

		public static void TextRect(string Text, PointF position, Size area, Font font, Color color, StringAlignment alignment)
		{
			int id = GetCachedTextRect(Text, area, font, color, alignment);

			Bitmap(position, new SizeF(area.Width, area.Height), id);
		}
	}
}
