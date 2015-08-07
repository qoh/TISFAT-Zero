using System;
using System.IO;

namespace TISFAT.Entities
{
    partial class StickFigure
    {
        public class State : IEntityState, ISaveable
        {
            public Joint.State Root;

            public State() { }

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
