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
