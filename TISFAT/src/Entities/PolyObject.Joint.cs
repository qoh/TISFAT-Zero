using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TISFAT.Entities
{
	public partial class PolyObject
	{
		public class Joint : IManipulatable, ISaveable
		{
			public PointF Location;

			public float X { get { return Location.X; } }
			public float Y { get { return Location.Y; } }

			public Joint() { }

			public Joint(PointF p)
			{
				Location = p;
			}

			public Joint Copy()
			{
				return new Joint { Location = new PointF(Location.X, Location.Y) };
			}

			public static implicit operator PointF(Joint point)
			{
				return point.Location;
			}

			public static implicit operator Joint(PointF point)
			{
				return new Joint(point);
			}

			public void Write(BinaryWriter writer)
			{
				writer.Write((double)Location.X);
				writer.Write((double)Location.Y);
			}

			public void Read(BinaryReader reader, UInt16 version)
			{
				float x, y;
				x = (float)reader.ReadDouble();
				y = (float)reader.ReadDouble();

				Location = new PointF(x, y);
			}
		}
	}
}
