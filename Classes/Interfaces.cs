using OpenTK;
using System.Drawing;
using System.IO;

namespace TISFAT_Zero
{
	internal interface ICanDraw
	{
		GLControl GLGraphics
		{
			get;
		}

		void drawGraphics(int type, Color color, Point one, int width, int height, Point two);
	}

	internal interface IGLDrawable
	{
		void Draw(ICanDraw Canvas, Point position = new Point());
	}

	internal interface ISavable
	{
		void saveObjectToStream(Stream saveTo);
	}
}