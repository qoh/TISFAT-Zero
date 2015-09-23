using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.Windows.Forms;
using TISFAT.Util;

namespace TISFAT
{
	public partial class GLColorPickerDialog : Form
	{
		int HueBar;
		int shaderProgram;
		float Hue;
		Vector4 LatestColor;

		public GLColorPickerDialog()
		{
			InitializeComponent();
		}

		private void ColorPickerDialog_Load(object sender, EventArgs e)
		{
			GLContext_Init();
			Hue = 0.0f;
			LatestColor = new Vector4(1f, 1f, 1f, 1f);
			HueBar = Drawing.GenerateTexID(Properties.Resources.hue_picker);
		}

		private void GLContext_Init()
		{
			GLContext.MakeCurrent();

			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Viewport(0, 0, GLContext.Width, GLContext.Height);
			GL.Ortho(0, GLContext.Width, GLContext.Height, 0, -1, 1);
			GL.Disable(EnableCap.DepthTest);

			shaderProgram = Drawing.MakeProgram("Shaders\\ColorPicker.vert", "Shaders\\ColorPicker.frag");
		}

		private void GLContext_Paint(object sender, PaintEventArgs e)
		{
			GLContext.MakeCurrent();

			GL.ClearColor(LatestColor.X, LatestColor.Y, LatestColor.Z, LatestColor.W);
			GL.Clear(ClearBufferMask.ColorBufferBit);

			GL.UseProgram(shaderProgram);
			int location = GL.GetUniformLocation(shaderProgram, "Hue");
			GL.Uniform1(location, Hue);

			GL.Begin(PrimitiveType.Quads);

			GL.Vertex2(0f, 0f);
			GL.Vertex2(258f, 0f);
			GL.Vertex2(258f, 258f);
			GL.Vertex2(0f, 258f);

			GL.End();

			GL.UseProgram(0);

			Drawing.Bitmap(new PointF(265.0f, 0.0f), new SizeF(20.0f, 258.0f), 0, HueBar);

			GLContext.SwapBuffers();
		}

		private void GLContext_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && e.X >= 258f)
			{
				Hue = (float)e.Y / 258f * 6f;
			}
			else if (e.X < 258f)
			{
				float gray = 1.0f - (float)e.X / 258f;
				float red = 1.0f - e.Y / 258f;
				Vector4 color = new Vector4(red, gray * red, gray * red, 1.0f);
				LatestColor = adjustHue(color, Hue);
			}

			GLContext.Invalidate();
		}

		static Vector4 kRGBToYPrime = new Vector4(0.299f, 0.587f, 0.114f, 0.0f);
		static Vector4 kRGBToI = new Vector4(0.596f, -0.275f, -0.321f, 0.0f);
		static Vector4 kRGBToQ = new Vector4(0.212f, -0.523f, 0.311f, 0.0f);

		static Vector4 kYIQToR = new Vector4(1.0f, 0.956f, 0.621f, 0.0f);
		static Vector4 kYIQToG = new Vector4(1.0f, -0.272f, -0.647f, 0.0f);
		static Vector4 kYIQToB = new Vector4(1.0f, -1.107f, 1.704f, 0.0f);

		static Vector4 adjustHue(Vector4 color, float hueAdjust)
		{
			// Convert to YIQ
			float YPrime = Vector4.Dot(color, kRGBToYPrime);
			float I = Vector4.Dot(color, kRGBToI);
			float Q = Vector4.Dot(color, kRGBToQ);

			// Calculate the hue and chroma
			float hue = (float)Math.Atan2(Q, I);
			float chroma = (float)Math.Sqrt(I * I + Q * Q);

			// Make the user's adjustments
			hue += hueAdjust;

			// Convert back to YIQ
			Q = chroma * (float)Math.Sin(hue);
			I = chroma * (float)Math.Cos(hue);

			// Convert back to RGB
			Vector4 yIQ = new Vector4(YPrime, I, Q, 0.0f);
			return new Vector4(Vector4.Dot(yIQ, kYIQToR), Vector4.Dot(yIQ, kYIQToG), Vector4.Dot(yIQ, kYIQToB), color.Z);
		}
	}
}
