using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TISFAT_ZERO.Scripts
{
	public class ContextNotDefinedException : Exception
	{
		public ContextNotDefinedException(string message)
			: base(message)
		{ }
	}

	static class Drawing
	{
		static int GL_WIDTH, GL_HEIGHT;
		static bool MadeCurrent = false;

		/// <summary>
		/// Sets the graphics context to the current context, and assigns GL_WIDTH and GL_HEIGHT for later.
		/// </summary>
		/// <param name="context">The graphics context to be made current to draw to.</param>
		public static void ReadyDraw(OpenTK.GLControl context)
		{
			//Call makecurrent on the context.
			context.MakeCurrent();

			//Set GL_WIDTH and GL_HEIGHT variables for later use in the subsequent draw call.
			GL_WIDTH = context.Width;
			GL_HEIGHT = context.Height;

			//Set a boolean to be sure that this function was called before a draw call; otherwise the draw call will throw an exception.
			MadeCurrent = true;
		}


		/// <summary>
		/// Draws the graphics.
		/// </summary>
		/// <param name="type">What we're drawing. 1 = Line, 1 = Circle, 2 = Handle, 3 = Hollow Handle</param>
		/// <param name="color">The <see cref="Color">color</see> of what we're drawing.</param>
		/// <param name="one">The origin point.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <param name="two">The end point. (only used in line type)</param>
		public static void DrawGraphics(int type, Color color, Point one, int width, int height, Point two, int textureID = 0, float rotation = 0)
		{
			//if (!MadeCurrent)
			//throw new ContextNotDefinedException("Call made to DrawGraphics without the correct context set; Be sure to call ReadyDraw(GLControl); before calling DrawGraphics!");

			//Invert the y so OpenGL can draw it right-side up
			one.Y = GL_HEIGHT - one.Y;
			two.Y = GL_HEIGHT - two.Y;

			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

			switch (type)
			{
				//Line
				case (0):
					//since some opengl cards don't support line widths past 1.0, we need to draw quads
					GL.Color4(color);

					//step 1: spam floats
					float x1 = one.X;
					float x2 = two.X;
					float y1 = one.Y;
					float y2 = two.Y;

					//step 2: get slope/delta
					float vecX = x1 - x2;
					float vecY = y1 - y2;

					//step 3: calculate distance
					float dist = (float)Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));

					//step 4: normalize
					float norm1X = (vecX / dist);
					float norm1Y = (vecY / dist);

					GL.Begin(BeginMode.Quads);

					//step 5: get the perpindicular line to norm1, and scale it based on our width
					float normX = norm1Y * width / 2;
					float normY = -norm1X * width / 2;

					//step 6: draw the quad from the points using the normal as the offset
					GL.Vertex2((one.X - normX), (one.Y - normY));
					GL.Vertex2((one.X + normX), (one.Y + normY));

					GL.Vertex2((two.X + normX), (two.Y + normY));
					GL.Vertex2((two.X - normX), (two.Y - normY));

					GL.End();

					DrawCircle(one.X, one.Y, width / 2);
					DrawCircle(two.X, two.Y, width / 2);
					break;

				//Circle
				case (1):
					GL.Color4(color);
					DrawCircle(one.X, one.Y, width);
					break;

				//Handle
				case (2):
					GL.Disable(EnableCap.Multisample);

					GL.Color4(color);
					GL.Begin(BeginMode.Quads);

					GL.Vertex2(one.X - 2.5, one.Y - 2.5);
					GL.Vertex2(one.X + 2.5, one.Y - 2.5);
					GL.Vertex2(one.X + 2.5, one.Y + 2.5);
					GL.Vertex2(one.X - 2.5, one.Y + 2.5);

					GL.End();

					GL.Enable(EnableCap.Multisample);
					break;

				//Hollow Handle
				case (3):
					GL.Disable(EnableCap.Multisample);

					GL.Color4(color);
					GL.Begin(BeginMode.LineLoop);

					GL.Vertex2(one.X - 2.5, one.Y - 2.5);
					GL.Vertex2(one.X + 2.5, one.Y - 2.5);
					GL.Vertex2(one.X + 2.5, one.Y + 2.5);
					GL.Vertex2(one.X - 2.5, one.Y + 2.5);

					GL.End();

					GL.Enable(EnableCap.Multisample);
					break;

				//Selection Rect
				case (4):
					GL.Disable(EnableCap.Multisample);

					GL.Color4(color);
					GL.Begin(BeginMode.LineLoop);

					GL.Vertex2(one.X, one.Y);
					GL.Vertex2(two.X, one.Y);
					GL.Vertex2(two.X, two.Y);
					GL.Vertex2(one.X, two.Y);

					GL.End();

					color = Color.FromArgb(color.A - 200, color);
					GL.Color4(color);

					GL.Begin(BeginMode.Quads);

					GL.Vertex2(one.X, one.Y);
					GL.Vertex2(two.X, one.Y);
					GL.Vertex2(two.X, two.Y);
					GL.Vertex2(one.X, two.Y);

					GL.End();

					GL.Enable(EnableCap.Multisample);
					break;

				//Rect Fill
				case (5):
					GL.Color4(color);
					GL.Begin(BeginMode.Quads);

					GL.Vertex2(one.X, one.Y);
					GL.Vertex2(two.X, one.Y);
					GL.Vertex2(two.X, two.Y);
					GL.Vertex2(one.X, two.Y);

					GL.End();
					break;

				//Texture
				case (6):
					GL.Color4(color);

					GL.Enable(EnableCap.Texture2D);
					GL.Enable(EnableCap.Blend);
					GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

					GL.BindTexture(TextureTarget.Texture2D, textureID);

					GL.PushMatrix();

					GL.Translate(one.X, one.Y, 0);
					GL.Rotate(rotation, 0, 0, 1);

					GL.Begin(BeginMode.Quads);

					GL.TexCoord2(0.0, 1.0);
					GL.Vertex2(0, 0);

					GL.TexCoord2(0.0, 0.0);
					GL.Vertex2(0, -height);

					GL.TexCoord2(1.0, 0.0);
					GL.Vertex2(width, -height);

					GL.TexCoord2(1.0, 1.0);
					GL.Vertex2(width, 0);

					GL.End();

					GL.PopMatrix();

					GL.Disable(EnableCap.Blend);
					GL.Disable(EnableCap.Texture2D);
					break;

				//Polys
				case (7):
					GL.Color4(color);

					GL.Begin(BeginMode.TriangleFan);

					GL.End();

					break;
			}

			GL.Disable(EnableCap.Blend);

			MadeCurrent = false;
		}

		public static void DrawPoly(Point[] positions, Color color)
		{
			//Draw it once with Always
			GL.Enable(EnableCap.StencilTest);
			GL.ColorMask(false, false, false, false);
			//GL.StencilMask(0xFFFFFF);
			GL.StencilMask(0xFFFF);
			GL.StencilFunc(StencilFunction.Always, 1, 1);
			GL.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Incr);

			GL.Begin(BeginMode.TriangleFan);

			foreach (Point x in positions)
			{
				GL.Color4(color);

				//Invert the Y axis so it draws right
				int i;
				i = GL_HEIGHT - x.Y;

				GL.Vertex2(x.X, i);
			}

			GL.End();

			//Draw it once more with equal
			GL.ColorMask(true, true, true, true);
			GL.StencilFunc(StencilFunction.Equal, 1, 1);

			GL.Begin(BeginMode.TriangleFan);

			foreach (Point x in positions.Reverse())
			{
				GL.Color4(color);

				//Invert the Y axis so it draws right
				int i;
				i = GL_HEIGHT - x.Y;

				GL.Vertex2(x.X, i);
			}

			GL.End();
		}

		private static void DrawCircle(float cx, float cy, float r)
		{
			int num_segments = 5 * (int)Math.Sqrt(r);

			float theta = 6.28271f / num_segments;
			float tangetial_factor = (float)Math.Tan(theta);

			float radial_factor = (float)Math.Cos(theta);

			float y = 0;

			GL.Begin(BeginMode.TriangleFan);

			for (int ii = 0;ii < num_segments;ii++)
			{
				GL.Vertex2(r + cx, y + cy);

				float ty = r;

				r = (r + -y * tangetial_factor) * radial_factor;
				y = (y + ty * tangetial_factor) * radial_factor;
			}

			GL.End();
		}
	}
}
