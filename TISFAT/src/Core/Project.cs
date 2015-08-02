using System;
using System.Collections.Generic;
using System.Drawing;
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
            Layer selectedLayer = null;

            foreach (Layer layer in Layers)
            {
                if (layer == Program.Form.MainTimeline.SelectedLayer)
                    selectedLayer = layer;

                layer.Draw(time);
            }

            if (selectedLayer != null)
                selectedLayer.DrawEditable(time);
        }

        public bool TryManipulate(float time, Point location)
        {
            if (Program.Form.MainTimeline.SelectedLayer == null)
                return false;

            foreach (Layer layer in Layers)
            {
                if (Program.Form.MainTimeline.SelectedLayer == layer)
                    return layer.TryManipulate(time, location);
            }

            return false;
        }

        #region File Saving / Loading
        public void Write(BinaryWriter writer)
        {
            FileFormat.WriteList(writer, Layers);
        }

        public void Read(BinaryReader reader, UInt16 version)
        {
            Layers = FileFormat.ReadList<Layer>(reader, version);
        } 
        #endregion
    }
}
