using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TISFAT.Entities;

namespace TISFAT.Util
{
	public static class Drawing
	{
		#region CACHE MONSTERS INSIDE PLEASE NO STAY IN THE CAGE
		private static Dictionary<Font, Dictionary<Color, Dictionary<SizeF, Dictionary<StringAlignment, Dictionary<string, int>>>>> TextRectCache = new Dictionary<Font, Dictionary<Color, Dictionary<SizeF, Dictionary<StringAlignment, Dictionary<string, int>>>>>();
		private static Dictionary<string, int> BitmapCache = new Dictionary<string, int>();
		#endregion

		private static bool ShadowsInit = false;

		private static int ShadowsFBO = 0;
		private static int ShadowsTexture = 0;
		private static int LightProgram = 0;

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

			GL.BindTexture(TextureTarget.Texture2D, 0);
		}

		public static int MakeShader(string filename, ShaderType type)
		{
			string src = File.ReadAllText(filename);

			int shader = GL.CreateShader(type);
			GL.ShaderSource(shader, src);
			GL.CompileShader(shader);

			int compiled;
			GL.GetShader(shader, ShaderParameter.CompileStatus, out compiled);

			if (compiled == 0)
			{
				string infoLog = GL.GetShaderInfoLog(shader);
				MessageBox.Show(infoLog, "Shader compilation failed (" + filename + ")");
				GL.DeleteShader(shader);
				return 0;
			}

			return shader;
		}

		public static int MakeProgram(string vertFileName, string fragFileName)
		{
			int vertShader = MakeShader(vertFileName, ShaderType.VertexShader);
			int fragShader = MakeShader(fragFileName, ShaderType.FragmentShader);

			int program = GL.CreateProgram();

			GL.AttachShader(program, vertShader);
			GL.AttachShader(program, fragShader);

			GL.LinkProgram(program);

			GL.DetachShader(program, vertShader);
			GL.DetachShader(program, fragShader);

			int status;
			GL.GetProgram(program, GetProgramParameterName.LinkStatus, out status);

			if (status == 0)
			{
				string infoLog = GL.GetProgramInfoLog(program);
				MessageBox.Show(infoLog, "Failed to link shader program");
				GL.DeleteProgram(program);
				return 0;
			}

			return program;
		}

		private static int NextPowerOf2(int v)
		{
			v--;
			v |= v >> 1;
			v |= v >> 2;
			v |= v >> 4;
			v |= v >> 8;
			v |= v >> 16;
			v++;
			return v;
		}

		private static void InitShadows()
		{
			GL.GenFramebuffers(1, out ShadowsFBO);

			GL.GenTextures(1, out ShadowsTexture);
			GL.BindTexture(TextureTarget.Texture2D, ShadowsTexture);

			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

			int size = Math.Max(NextPowerOf2(Program.ActiveProject.Width), NextPowerOf2(Program.ActiveProject.Height));

			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, size, size, 0, OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);

			GL.BindTexture(TextureTarget.Texture2D, 0);

			GL.GenFramebuffers(1, out ShadowsFBO);

			GL.BindFramebuffer(FramebufferTarget.Framebuffer, ShadowsFBO);
			GL.FramebufferTexture(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, ShadowsTexture, 0);

			DrawBuffersEnum[] bufferEnum = new DrawBuffersEnum[1] { (DrawBuffersEnum)FramebufferAttachment.ColorAttachment0 };
			GL.DrawBuffers(bufferEnum.Length, bufferEnum);

			GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

			ShadowsInit = true;
		}

		public static int GetCachedTextRect(string text, SizeF area, Font font, Color color, StringAlignment alignment)
		{
			if (!TextRectCache.ContainsKey(font))
				TextRectCache[font] = new Dictionary<Color, Dictionary<SizeF, Dictionary<StringAlignment, Dictionary<string, int>>>>();

			if (!TextRectCache[font].ContainsKey(color))
				TextRectCache[font][color] = new Dictionary<SizeF, Dictionary<StringAlignment, Dictionary<string, int>>>();

			if (!TextRectCache[font][color].ContainsKey(area))
				TextRectCache[font][color][area] = new Dictionary<StringAlignment, Dictionary<string, int>>();

			if (!TextRectCache[font][color][area].ContainsKey(alignment))
				TextRectCache[font][color][area][alignment] = new Dictionary<string, int>();

			if (!TextRectCache[font][color][area][alignment].ContainsKey(text))
			{
				Bitmap bmp = new Bitmap(Math.Max(1, Math.Abs((int)area.Width)), Math.Max(1, Math.Abs((int)area.Height)));

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

					g.DrawString(text, font, new SolidBrush(color), new RectangleF(new Point(0, 0), area), format);
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
			GL.Enable(EnableCap.Blend);

			GL.Begin(PrimitiveType.Lines);
			GL.Color4(color);
			GL.Vertex2(Math.Floor(start.X) + 0.5, Math.Floor(start.Y) + 0.5);
			GL.Vertex2(Math.Floor(end.X) + 0.5, Math.Floor(end.Y) + 0.5);
			GL.End();

			GL.Disable(EnableCap.Blend);
		}

		public static void QuadLine(PointF start, PointF end, float thickness, Color color)
		{
			GL.Enable(EnableCap.Blend);

			float dx = end.X - start.X;
			float dy = end.Y - start.Y;
			float dm = (float)Math.Sqrt(dx * dx + dy * dy);

			float px = (dy / dm) * thickness;
			float py = -(dx / dm) * thickness;

			GL.Begin(PrimitiveType.Quads);
			GL.Color4(color);
			GL.Vertex2(new Vector2(start.X - px, start.Y - py));
			GL.Vertex2(new Vector2(end.X - px, end.Y - py));
			GL.Vertex2(new Vector2(end.X + px, end.Y + py));
			GL.Vertex2(new Vector2(start.X + px, start.Y + py));
			GL.End();

			GL.Disable(EnableCap.Blend);
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
			GL.Enable(EnableCap.Blend);

			GL.Begin(PrimitiveType.TriangleFan);
			GL.Color4(color);
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

			GL.Disable(EnableCap.Blend);
		}

		public static void Rectangle(PointF position, SizeF size, Color color)
		{
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

			GL.Begin(PrimitiveType.Quads);
			GL.Color4(color);
			GL.Vertex2(PointToVector(position));
			GL.Vertex2(position.X + size.Width, position.Y);
			GL.Vertex2(position.X + size.Width, position.Y + size.Height);
			GL.Vertex2(position.X, position.Y + size.Height);
			GL.End();

			GL.Disable(EnableCap.Blend);
		}

		public static void RectangleLine(PointF position, SizeF size, Color color)
		{
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

			GL.Begin(PrimitiveType.LineLoop);
			GL.Color4(color);
			GL.Vertex2(Math.Floor(position.X) + 0.5, Math.Floor(position.Y) + 0.5);
			GL.Vertex2(Math.Floor(position.X + size.Width) + 0.5, Math.Floor(position.Y) + 0.5);
			GL.Vertex2(Math.Floor(position.X + size.Width) + 0.5, Math.Floor(position.Y + size.Height) + 0.5);
			GL.Vertex2(Math.Floor(position.X) + 0.5, Math.Floor(position.Y + size.Height) + 0.5);
			GL.End();

			GL.Disable(EnableCap.Blend);
		}

		public static void DrawPoly(PolyObject.Joint[] positions, Color color)
		{
			GL.Enable(EnableCap.Blend);

			//Draw it once with Always
			GL.Enable(EnableCap.StencilTest);
			GL.ColorMask(false, false, false, false);
			//GL.StencilMask(0xFFFFFF);
			GL.StencilMask(0xFFFF);
			GL.StencilFunc(StencilFunction.Always, 1, 1);
			GL.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Incr);

			GL.Begin(PrimitiveType.TriangleFan);

			foreach (PointF x in positions)
			{
				GL.Color4(color);

				GL.Vertex2(x.X, x.Y);
			}

			GL.End();

			//Draw it once more with equal
			GL.ColorMask(true, true, true, true);
			GL.StencilFunc(StencilFunction.Equal, 1, 1);

			GL.Begin(PrimitiveType.TriangleFan);

			foreach (PointF x in positions.Reverse())
			{
				GL.Color4(color);

				GL.Vertex2(x.X, x.Y);
			}

			GL.End();

			GL.Disable(EnableCap.StencilTest);
			GL.Disable(EnableCap.Blend);
		}

		public static void BitmapOriginRotation(PointF position, SizeF size, float rotation, int alpha, int texID)
		{
			GL.BindTexture(TextureTarget.Texture2D, texID);

			GL.Enable(EnableCap.Texture2D);
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

			GL.PushMatrix();
			GL.Translate(position.X, position.Y, 0);
			GL.Rotate(rotation, 0, 0, 1);

			GL.Color4(Color.FromArgb(alpha, Color.White));

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

		public static void Bitmap(PointF position, SizeF size, float rotation, int alpha, int texID)
		{
			GL.BindTexture(TextureTarget.Texture2D, texID);

			GL.Enable(EnableCap.Texture2D);
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

			GL.PushMatrix();
			GL.Translate(position.X + size.Width / 2, position.Y + size.Height / 2, 0);
			GL.Rotate(rotation, 0, 0, 1);
			GL.Translate(-size.Width / 2, -size.Height / 2, 0);

			GL.Color4(Color.FromArgb(alpha, Color.White));

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

		public static void Bitmap(PointF position, SizeF size, float rotation, string path)
		{
			Bitmap(position, size, rotation, 255, GetCachedBitmap(path));
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

			Bitmap(position, size, 0, 255, id);

			GL.DeleteTexture(id);
		}

		public static void TextRect(string Text, PointF position, SizeF area, Font font, Color color, StringAlignment alignment)
		{
			int id = GetCachedTextRect(Text, area, font, color, alignment);

			Bitmap(position, new SizeF(area.Width, area.Height), 0, 255, id);
		}

		public static void PointLight(PointF position, Color color, Vector3 attenuation, float radius)
		{
			if (LightProgram == 0)
				LightProgram = MakeProgram("Shaders\\PointLight.vert", "Shaders\\PointLight.frag");
			if (!ShadowsInit)
				InitShadows();

			int size = NextPowerOf2(Math.Max(Program.ActiveProject.Width, Program.ActiveProject.Height));

			GL.BindFramebuffer(FramebufferTarget.Framebuffer, ShadowsFBO);

			GL.ClearColor(Color.FromArgb(0, Program.ActiveProject.BackColor));
			GL.Clear(ClearBufferMask.ColorBufferBit);

			GL.PushMatrix();

			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Viewport(0, 0, size, size);
			GL.Ortho(0, size, size, 0, -1, 1);

			foreach (Layer l in Program.ActiveProject.Layers)
				if (l.Data.GetType() != typeof(PointLight))
					l.Draw(Program.MainTimeline.GetCurrentFrame());

			GL.PopMatrix();

			GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

			GL.ClearColor(Program.ActiveProject.BackColor);

			GL.Clear(ClearBufferMask.ColorBufferBit);

			GL.UseProgram(LightProgram);

			GL.Uniform1(GL.GetUniformLocation(LightProgram, "s_Texture"), 0);
			GL.BindTexture(TextureTarget.Texture2D, ShadowsTexture);

			GL.Uniform2(GL.GetUniformLocation(LightProgram, "s_Res"), new Vector2(size, -size));

			GL.Uniform2(GL.GetUniformLocation(LightProgram, "lightPos"), PointToVector(position));
			GL.Uniform3(GL.GetUniformLocation(LightProgram, "lightColor"), new Vector3(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f));
			GL.Uniform3(GL.GetUniformLocation(LightProgram, "lightAttenuation"), attenuation);
			GL.Uniform1(GL.GetUniformLocation(LightProgram, "lightRadius"), radius);

			Rectangle(new PointF(0, 0), new SizeF(Program.ActiveProject.Width, Program.ActiveProject.Height), color);

			GL.UseProgram(0);
		}
	}
}
