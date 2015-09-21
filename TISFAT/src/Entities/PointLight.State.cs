using OpenTK;
using System;
using System.Drawing;
using System.IO;
using TISFAT.Util;

namespace TISFAT.Entities
{
	public partial class PointLight
	{
		public class State : IEntityState, IManipulatable
		{
			public PointF Location;

			public Color LightColor;
			public float LightRadius;
			public Vector3 LightAttenuation;

			public State() { }

			public IEntityState Copy()
			{
				State state = new State();
				state.Location = new PointF(Location.X, Location.Y);
				state.LightColor = Color.FromArgb(LightColor.A, LightColor);
				state.LightRadius = LightRadius;
				state.LightAttenuation = new Vector3(LightAttenuation.X, LightAttenuation.Y, LightAttenuation.Z);
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

				FileFormat.WriteColor(LightColor, writer);
				writer.Write((double)LightRadius);

				FileFormat.WriteVec3(LightAttenuation, writer);
			}

			public void Read(BinaryReader reader, UInt16 version)
			{
				float x, y;

				x = (float)reader.ReadDouble();
				y = (float)reader.ReadDouble();

				LightColor = FileFormat.ReadColor(reader);
				LightRadius = (float)reader.ReadDouble();

				LightAttenuation = FileFormat.ReadVec3(reader);
			}
		}
	}
}
