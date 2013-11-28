using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OpenTK.Graphics;
using OpenTK;

namespace TISFAT_Zero
{
	interface ICanDraw
	{
		GLControl GLGraphics
		{
			get;
		}

		void drawGraphics(int type, Color color, Point one, int width, int height, Point two);
	}

	interface IGLDrawable
	{
		void Draw(ICanDraw Canvas, Point position = new Point());
	}
}