using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TISFAT.Util;

namespace TISFAT.Entities
{
	public partial class TextObject
	{
		public class State : IEntityState, IManipulatable
		{
			public RectangleF Bounds;

			public string Text;
			public Font TextFont;
			public Color TextColor;
			public StringAlignment TextAlignment;

			public State() { }

			public IEntityState Copy()
			{
				State state = new State();
				state.Bounds = new RectangleF(Bounds.Location, Bounds.Size);

				state.Text = Text;
				state.TextFont = new Font(TextFont, TextFont.Style);
				state.TextColor = Color.FromArgb(TextColor.A, TextColor);
				state.TextAlignment = TextAlignment;
				return state;
			}

			public IEntityState Interpolate(IEntityState target, float interpolationAmount)
			{
				return TextObject._Interpolate(interpolationAmount, this, target, EntityInterpolationMode.Linear);
			}

			public int HandleAtLocation(PointF location)
			{
				float size = 6; // Dis is handle size

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

				writer.Write(Text);
				writer.Write(TextFont.FontFamily.ToString());
				writer.Write((double)TextFont.Size);
				writer.Write((int)TextFont.Style);
			}

			public void Read(BinaryReader reader, UInt16 version)
			{
				Bounds = new RectangleF();

				Bounds.X = (float)reader.ReadDouble();
				Bounds.Y = (float)reader.ReadDouble();
				Bounds.Width = (float)reader.ReadDouble();
				Bounds.Height = (float)reader.ReadDouble();

				Text = reader.ReadString();
				string fontname = reader.ReadString();
				float fontsize = (float)reader.ReadDouble();
				FontStyle style = (FontStyle)Enum.Parse(typeof(FontStyle), reader.ReadInt32().ToString());

				TextFont = new Font(fontname, fontsize, style);
			}
		}
	}
}
