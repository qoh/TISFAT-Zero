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
	public partial class CircleObject
	{
		public class State : IEntityState, IManipulatable
		{
			public PointF Location;
			public float Size;
			public Color Color;

			public State() { }

			public IEntityState Copy()
			{
				State state = new State();
				state.Location = new PointF(Location.X, Location.Y);
				state.Size = Size;
				state.Color = Color.FromArgb(Color.A, Color);
				return state;
			}

			public void Move(PointF target, ManipulateParams mparams)
			{
				if (mparams.AbsoluteDrag)
				{
					Location = new PointF(target.X - mparams.AbsoluteOffset.X, target.Y - mparams.AbsoluteOffset.Y);
				}
				else
				{
					Location = target;
				}
			}

			public void Write(BinaryWriter writer)
			{
				writer.Write((double)Location.X);
				writer.Write((double)Location.Y);
				writer.Write((double)Size);
				FileFormat.WriteColor(Color, writer);
			}

			public void Read(BinaryReader reader, UInt16 version)
			{
				float x, y;
				x = (float)reader.ReadDouble();
				y = (float)reader.ReadDouble();
				Location = new PointF(x, y);
				Size = (float)reader.ReadDouble();
				Color = FileFormat.ReadColor(reader);
			}
		}
	}
}
