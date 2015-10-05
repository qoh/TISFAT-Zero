using System;
using System.Drawing;
using System.IO;
using TISFAT.Util;

namespace TISFAT.Entities
{
	public partial class BitmapObject
	{
		public class State : IEntityState, ISaveable, IManipulatable
		{
			public RectangleF Bounds;
			public float TexWidth;
			public float TexHeight;
			public float Rotation;
			public int BitmapAlpha;

			public State() { }

			public IEntityState Copy()
			{
				State state = new State();
				state.Bounds = new RectangleF(Bounds.Location, Bounds.Size);
				state.TexHeight = this.TexHeight;
				state.TexWidth = this.TexWidth;
				state.Rotation = this.Rotation;
				state.BitmapAlpha = this.BitmapAlpha;
				return state;
			}

            public IEntityState Interpolate(IEntityState target, float interpolationAmount)
            {
                return BitmapObject._Interpolate(interpolationAmount, this, target, EntityInterpolationMode.Linear);
            }

            public int HandleAtLocation(PointF location)
			{
				float size = Math.Min(12, TexWidth / 2);

				RectangleF TopLeft = new RectangleF(new PointF(Bounds.Left, Bounds.Top), new SizeF(size, size));
				RectangleF TopRight = new RectangleF(new PointF(Bounds.Right - size, Bounds.Top), new SizeF(size, size));
				RectangleF BottomLeft = new RectangleF(new PointF(Bounds.Left, Bounds.Bottom - size), new SizeF(size, size));
				RectangleF BottomRight = new RectangleF(new PointF(Bounds.Right - size, Bounds.Bottom - size), new SizeF(size, size));

				if (MathUtil.PointInRect(location, TopLeft))
					return 0;
				if (MathUtil.PointInRect(location, TopRight))
					return 1;
				if (MathUtil.PointInRect(location, BottomRight))
					return 2;
				if (MathUtil.PointInRect(location, BottomLeft))
					return 3;

				return -1;
			}

			public void Move(PointF target, ManipulateParams mparams)
			{
				var x1 = Bounds.X;
				var x2 = Bounds.X + Bounds.Width;
				var y1 = Bounds.Y;
				var y2 = Bounds.Y + Bounds.Height;

				if (!mparams.AbsoluteDrag)
				{
					switch (mparams.CornerGrabbed)
					{
						case 0:
							x1 = target.X;
							y1 = target.Y;
							break;
						case 1:
							x2 = target.X;
							y1 = target.Y;
							break;
						case 2:
							x2 = target.X;
							y2 = target.Y;
							break;
						case 3:
							x1 = target.X;
							y2 = target.Y;
							break;
					}
				}
				else
				{
					target = new PointF(
							   target.X - mparams.AbsoluteOffset.X,
							   target.Y - mparams.AbsoluteOffset.Y);

					x1 = target.X;
					y1 = target.Y;
				}

				Bounds.X = x1;
				Bounds.Y = y1;
				if (!mparams.AbsoluteDrag)
				{
					Bounds.Width = x2 - x1;
					Bounds.Height = y2 - y1;

					if (mparams.KeepAspectRatio)
						Bounds.Size = fitToSize(TexWidth, TexHeight, Bounds.Width, Bounds.Height);
				}
			}

			public SizeF fitToSize(float srcWidth, float srcHeight, float maxWidth, float maxHeight)
			{
				var ratio = Math.Min(maxWidth / srcWidth, maxHeight / srcHeight);

				return new SizeF(srcWidth * ratio, srcHeight * ratio);
			}

			public void Write(BinaryWriter writer)
			{
				writer.Write((double)Bounds.X);
				writer.Write((double)Bounds.Y);
				writer.Write((double)Bounds.Width);
				writer.Write((double)Bounds.Height);

				writer.Write((double)TexWidth);
				writer.Write((double)TexHeight);
				writer.Write((double)Rotation);
				writer.Write(BitmapAlpha);
			}

			public void Read(BinaryReader reader, UInt16 version)
			{
				Bounds = new RectangleF();

				Bounds.X = (float)reader.ReadDouble();
				Bounds.Y = (float)reader.ReadDouble();
				Bounds.Width = (float)reader.ReadDouble();
				Bounds.Height = (float)reader.ReadDouble();

				TexWidth = (float)reader.ReadDouble();
				TexHeight = (float)reader.ReadDouble();
				Rotation = (float)reader.ReadDouble();
				if (version >= 5)
					BitmapAlpha = reader.ReadInt32();
				else
					BitmapAlpha = 255;
			}
		}
	}
}
