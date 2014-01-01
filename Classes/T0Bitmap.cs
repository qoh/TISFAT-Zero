using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Drawing.Imaging;

namespace TISFAT_Zero
{
	struct T0Bitmap : IGLDrawable
	{
		public int texID;
		public Size texSize;
		public Point texPos;

		//Construct a new T0Bitmap from a bitmap object
		public T0Bitmap(Bitmap original, Point position = new Point())
		{
			BitmapData rawData = original.LockBits(new Rectangle(0, 0, original.Width, original.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

			texSize = original.Size;
			texID = GL.GenTexture();

			GL.BindTexture(TextureTarget.Texture2D, texID);
			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, rawData.Width, rawData.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, rawData.Scan0);
			original.UnlockBits(rawData);

			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

			GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

			texPos = position;
		}

		public void Draw(ICanDraw Canvas, Point position = new Point())
		{
			if (texSize == new Size())
				return;

			//Yes I know, if texPos isn't at 0,0 then it's impossible to draw the bitmap at 0,0, it's unavoidable
			if (position == new Point())
				position = texPos;

			Canvas.GLGraphics.MakeCurrent();

			GL.Color4(Color.White);
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

			GL.Enable(EnableCap.Texture2D);

			GL.BindTexture(TextureTarget.Texture2D, texID);
			GL.Begin(PrimitiveType.Quads);

			GL.TexCoord2(0, 0);
			GL.Vertex2(position.X, position.Y);

			GL.TexCoord2(0, 1);
			GL.Vertex2(position.X, position.Y + texSize.Height);

			GL.TexCoord2(1, 1);
			GL.Vertex2(position.X + texSize.Width, position.Y + texSize.Height);

			GL.TexCoord2(1, 0);
			GL.Vertex2(position.X + texSize.Width, position.Y);

			GL.End();

			GL.Disable(EnableCap.Texture2D);
			GL.Disable(EnableCap.Blend);
		}
	}
}