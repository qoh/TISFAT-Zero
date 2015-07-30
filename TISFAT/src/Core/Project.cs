using System;
using System.Collections.Generic;
using System.IO;
using TISFAT.Util;

namespace TISFAT
{
    public class Project : ISaveable
    {
        public List<Layer> Layers;

        public Project()
        {
            Layers = new List<Layer>();
        }

        public void Draw(float time)
        {
            foreach (Layer layer in Layers)
            {
                layer.Draw(time);
            }
        }

        public void Write(BinaryWriter writer)
        {
            FileFormat.WriteList(writer, Layers);
        }

        public void Read(BinaryReader reader, UInt16 version)
        {
            Layers = FileFormat.ReadList<Layer>(reader, version);
        }
    }
}
