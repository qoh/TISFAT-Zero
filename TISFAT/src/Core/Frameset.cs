using System;
using System.Collections.Generic;
using System.IO;
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

		#region File Saving / Loading
		public void Write(BinaryWriter writer)
		{
			FileFormat.WriteList(writer, Keyframes);
		}

		public void Read(BinaryReader reader, UInt16 version)
		{
			Keyframes = FileFormat.ReadList<Keyframe>(reader, version);
		} 
		#endregion
	}
}
