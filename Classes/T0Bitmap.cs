using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;

namespace TISFAT_Zero
{
	class T0Bitmap : IGLDrawable
	{
		public int texID;
		public Size texSize;

		//Construct a new T0Bitmap from a bitmap object
		public T0Bitmap(Bitmap original)
		{
			BitmapData rawData = original.LockBits(new Rectangle(0, 0, original.Width, original.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			texSize = original.Size;
			texID = GL.GenTexture();

			GL.BindTexture(TextureTarget.Texture2D, texID);

			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, rawData.Width, rawData.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.UnsignedByte, rawData.Scan0);
			original.UnlockBits(rawData);

			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

			GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
		}

		public void Draw(ICanDraw Canvas, Point position = new Point())
		{
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

			GL.Enable(EnableCap.Texture2D);

			GL.BindTexture(TextureTarget.Texture2D, texID);
			GL.Begin(BeginMode.Quads);

			GL.TexCoord2(0.0, 0.0);
			GL.Vertex2(position.X, position.Y);

			GL.TexCoord2(0.0, 1.0);
			GL.Vertex2(position.X, position.Y + texSize.Height);

			GL.TexCoord2(1.0, 1.0);
			GL.Vertex2(position.X + texSize.Width, position.Y + texSize.Height);

			GL.TexCoord2(1.0, 0.0);
			GL.Vertex2(position.X + texSize.Width, position.Y);

			GL.End();

			GL.Disable(EnableCap.Texture2D);
			GL.Disable(EnableCap.Blend);
		}
	}
}
