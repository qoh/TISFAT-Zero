using System;
using System.Collections.Generic;
using System.IO;
using TISFAT.Util;

namespace TISFAT
{
    public class Layer : ISaveable
    {
        public IEntity Data;
        public List<Keyframe> Keyframes;

        public Layer()
        {
            Keyframes = new List<Keyframe>();
        }

        public Layer(IEntity data)
        {
            Data = data;
            Keyframes = new List<Keyframe>();
        }

        public void Draw(float time)
        {
            if (Data == null || Keyframes.Count < 2)
            {
                // die;
                return;
            }

            UInt32 start = Keyframes[0].Time;
            UInt32 end = Keyframes[Keyframes.Count - 1].Time;

            if (time < start || time > end)
            {
                return;
            }

            int nextIndex;

            for (nextIndex = 1; nextIndex < Keyframes.Count; nextIndex++)
            {
                if (Keyframes[nextIndex].Time >= time)
                {
                    break;
                }
            }

            Keyframe current = Keyframes[nextIndex - 1];
            Keyframe target = Keyframes[nextIndex];
            float t = (time - current.Time) / (target.Time - current.Time);

            Data.Draw(Data.Interpolate(t, current.State, target.State));
        }

        public void Write(BinaryWriter writer)
        {
            if (Data == null)
                throw new NullReferenceException("Attempting to serialize Layer with null data");

            writer.Write(FileFormat.GetEntityID(Data.GetType()));
            Data.Write(writer);
            FileFormat.WriteList(writer, Keyframes);
        }

        public void Read(BinaryReader reader, UInt16 version)
        {
            Type type = FileFormat.ResolveEntityID(reader.ReadUInt16());
            Type[] args = { };
            object[] values = { };
            Data = (IEntity)type.GetConstructor(args).Invoke(values);
            Data.Read(reader, version);
            Keyframes = FileFormat.ReadList<Keyframe>(reader, version);
        }
    }
}
