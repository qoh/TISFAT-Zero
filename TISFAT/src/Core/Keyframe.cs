using System;
using System.IO;
using TISFAT.Util;

namespace TISFAT
{
    public class Keyframe : ISaveable
    {
        public UInt32 Time;
        public IEntityState State;

        public Keyframe()
        {
            Time = 0;
            State = null;
        }

        public Keyframe(UInt32 time, IEntityState state)
        {
            Time = time;
            State = state;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Time);
            writer.Write(FileFormat.GetEntityStateID(State.GetType()));
            State.Write(writer);
        }

        public void Read(BinaryReader reader, UInt16 version)
        {
            Time = reader.ReadUInt32();
        }
    }
}
