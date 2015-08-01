using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TISFAT.Util;

namespace TISFAT
{
    public class Frameset : ISaveable
    {
        public List<Keyframe> Keyframes;

        public float StartTime
        {
            get { return Keyframes[0].Time; }
        }

        public float EndTime
        {
            get { return Keyframes[Keyframes.Count - 1].Time; }
        }

        public float Duration
        {
            get { return EndTime - StartTime; }
        }

        public Frameset()
        {
            Keyframes = new List<Keyframe>();
        }

        public void Write(BinaryWriter writer)
        {
            FileFormat.WriteList(writer, Keyframes);
        }

        public void Read(BinaryReader reader, UInt16 version)
        {
            Keyframes = FileFormat.ReadList<Keyframe>(reader, version);
        }
    }
}
