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
	public partial class LineObject
	{
		public class State : IEntityState, IManipulatable
		{
			public PointF Handle1;
			public PointF Handle2;

			public float Thickness;
			public Color Color;

			public State() { }

			public IEntityState Copy()
			{
				State state = new State();
				state.Handle1 = new PointF(Handle1.X, Handle1.Y);
				state.Handle2 = new PointF(Handle2.X, Handle2.Y);
				state.Thickness = Thickness;
				state.Color = Color.FromArgb(Color.A, Color);
				return state;
			}

			public IEntityState Interpolate(IEntityState target, float interpolationAmount)
			{
				return LineObject._Interpolate(interpolationAmount, this, target, EntityInterpolationMode.Linear);
			}

			public int HandleAtLocation(PointF location)
			{
				if (MathUtil.IsPointInPoint(location, Handle1, 4))
					return 0;
				if (MathUtil.IsPointInPoint(location, Handle2, 4))
					return 1;

				return -1;
			}

			public void Move(PointF target, ManipulateParams mparams)
			{
				if (mparams.AbsoluteDrag)
				{
					Handle1 = new PointF(target.X - mparams.Handle1Offset.X, target.Y - mparams.Handle1Offset.Y);
					Handle2 = new PointF(target.X - mparams.Handle2Offset.X, target.Y - mparams.Handle2Offset.Y);
				}
				else
				{
					if (mparams.HandleGrabbed == 0)
						Handle1 = target;
					else
						Handle2 = target;
				}
			}

			public void Write(BinaryWriter writer)
			{
				writer.Write((double)Handle1.X);
				writer.Write((double)Handle1.Y);
				writer.Write((double)Handle2.X);
				writer.Write((double)Handle2.Y);

				FileFormat.WriteColor(Color, writer);
				writer.Write((double)Thickness);
			}

			public void Read(BinaryReader reader, UInt16 version)
			{
				float x1, y1;
				float x2, y2;

				x1 = (float)reader.ReadDouble();
				y1 = (float)reader.ReadDouble();
				x2 = (float)reader.ReadDouble();
				y2 = (float)reader.ReadDouble();

				Handle1 = new PointF(x1, y1);
				Handle2 = new PointF(x2, y2);

				Color = FileFormat.ReadColor(reader);
				Thickness = (float)reader.ReadDouble();
			}
		}
	}
}
