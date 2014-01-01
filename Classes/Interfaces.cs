using OpenTK;
using System.Drawing;
using System.IO;

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

	interface ISavable
	{
		void saveObjectToStream(Stream saveTo);
		//void loadFromStream(Stream loadFrom);
	}
}