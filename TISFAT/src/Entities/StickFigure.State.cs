using System;
using System.IO;
using TISFAT.Util;

namespace TISFAT.Entities
{
	partial class StickFigure
	{
		public class State : IEntityState, ISaveable
		{
			public Joint.State Root;

			public State() { }

			public IEntityState Copy()
			{
				return new State { Root = this.Root.Clone() };
			}

            public IEntityState Interpolate(IEntityState target, float interpolationAmount)
            {
                return StickFigure._Interpolate(interpolationAmount, this, target, EntityInterpolationMode.Linear);
            }

            public void Write(BinaryWriter writer)
			{
				Root.Write(writer);
			}

			public void Read(BinaryReader reader, UInt16 version)
			{
				Root = new Joint.State();
				Root.Read(reader, version);
			}
		}
	}
}
