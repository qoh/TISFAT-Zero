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
	public partial class PolyObject
	{
		public class State : IEntityState, IManipulatable
		{
			public List<Joint> Points;
			public Color Color;

			public State() { }

			public IEntityState Copy()
			{
				State state = new State();
				state.Points = new List<Joint>();

				foreach (Joint j in Points)
					state.Points.Add(j.Copy());

				state.Color = Color.FromArgb(Color.A, Color);
				return state;
			}

			public void Move(PointF target, ManipulateParams mparams)
			{
				if(mparams.AbsoluteDrag)
				{
					for (int i = 0; i < Points.Count; i++)
					{
						Points[i] = new PointF(
							target.X - mparams.AbsoluteOffset[i].X,
                            target.Y - mparams.AbsoluteOffset[i].Y);
					}
				}
				else
				{
					Points[mparams.PointIndex] = target;
				}

			}

			public void Write(BinaryWriter writer)
			{
				FileFormat.WriteList<Joint>(writer, Points);
				FileFormat.WriteColor(Color, writer);
			}

			public void Read(BinaryReader reader, UInt16 version)
			{
				Points = FileFormat.ReadList<Joint>(reader, version);
				Color = FileFormat.ReadColor(reader);
			}
		}
	}
}
