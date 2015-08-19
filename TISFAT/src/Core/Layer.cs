using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using TISFAT.Util;

namespace TISFAT
{
	public class Layer : ISaveable
	{
		private static int ShouldntBeStatic = 0;

		public string Name;
		public bool Visible;
		public Color TimelineColor;
		public IEntity Data;
		public List<Frameset> Framesets;

		#region Constructors
		public Layer()
		{
			Name = "Layer " + (++ShouldntBeStatic);
			Visible = true;
			TimelineColor = Color.AliceBlue;
			Framesets = new List<Frameset>();
		}

		public Layer(IEntity data)
		{
			Name = "Layer " + (++ShouldntBeStatic);
			Visible = true;
			TimelineColor = Color.DodgerBlue;
			Data = data;
			Framesets = new List<Frameset>();
		} 
		#endregion

		public Frameset FindFrameset(float time)
		{
			for (int i = 0; i < Framesets.Count; i++)
			{
				Frameset currentSet = Framesets[i];

				if (time >= currentSet.StartTime && time <= currentSet.EndTime)
					return currentSet;
			}

			return null;
		}

		public IEntityState FindCurrentState(float time)
		{
			if (!Visible || Data == null)
			{
				// die;
				return null;
			}

			Frameset frameset = FindFrameset(time);

			if (frameset == null)
				return null;

			int nextIndex;

			for (nextIndex = 1; nextIndex < frameset.Keyframes.Count; nextIndex++)
			{
				if (frameset.Keyframes[nextIndex].Time >= time)
				{
					break;
				}
			}

			Keyframe current = frameset.Keyframes[nextIndex - 1];
			Keyframe target = frameset.Keyframes[nextIndex];
			float t = (time - current.Time) / (target.Time - current.Time);

			return Data.Interpolate(t, current.State, target.State);
		}

		public void Draw(float time)
		{
			IEntityState state = FindCurrentState(time);

			if (state == null)
				return;

			Data.Draw(state);
		}

		public void DrawEditable(float time)
		{
			IEntityState state = FindCurrentState(time);

			if (state == null || Program.Form.MainTimeline.SelectedKeyframe == null)
				return;

			Data.DrawEditable(state);
		}
		
		#region File Saving / Loading
		public void Write(BinaryWriter writer)
		{
			if (Data == null)
				throw new NullReferenceException("Attempting to serialize Layer with null data");

			writer.Write(Name);
			writer.Write(Visible);
			writer.Write(TimelineColor.A);
			writer.Write(TimelineColor.R);
			writer.Write(TimelineColor.G);
			writer.Write(TimelineColor.B);
			writer.Write(FileFormat.GetEntityID(Data.GetType()));
			Data.Write(writer);
			FileFormat.WriteList(writer, Framesets);
		}

		public void Read(BinaryReader reader, UInt16 version)
		{
			Name = reader.ReadString();
			Visible = reader.ReadBoolean();
			byte a = reader.ReadByte();
			byte r = reader.ReadByte();
			byte g = reader.ReadByte();
			byte b = reader.ReadByte();
			TimelineColor = Color.FromArgb(a, r, g, b);
			Type type = FileFormat.ResolveEntityID(reader.ReadUInt16());
			Type[] args = { };
			object[] values = { };
			Data = (IEntity)type.GetConstructor(args).Invoke(values);
			Data.Read(reader, version);
			Framesets = FileFormat.ReadList<Frameset>(reader, version);
		} 
		#endregion
	}
}
