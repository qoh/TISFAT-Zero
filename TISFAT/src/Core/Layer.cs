using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using TISFAT.Util;

namespace TISFAT
{
	public class LayerCreationArgs
	{
		public int Variant;
		public string Arguments;

		public object ArgObject;

		public LayerCreationArgs(int v, string args)
		{
			Variant = v;
			Arguments = args;
		}

		public LayerCreationArgs(int v, object argObj)
		{
			Variant = v;
			ArgObject = argObj;
		}
	}

	#region Undo/Redo Actions
	public class LayerAddAction : IAction
	{
		Type figureType;
		uint Start;
		uint End;
		LayerCreationArgs args;

		int LayerIndex;

		public LayerAddAction(Type figType, uint start, uint end, LayerCreationArgs e)
		{
			figureType = figType;
			Start = start;
			End = end;
			args = e;
		}

		public bool Do()
		{
			IEntity figure = (IEntity)figureType.GetConstructor(new Type[0]).Invoke(new object[0]);
			Layer layer = figure.CreateDefaultLayer(Start, End, args);

			Program.ActiveProject.Layers.Add(layer);
			LayerIndex = Program.ActiveProject.Layers.IndexOf(layer);

			Program.MainTimeline.ClearSelection();
			Program.MainTimeline.selectedItems.Select(layer, layer.Framesets[0], layer.Framesets[0].Keyframes[0]);

			Program.MainTimeline.GLContext.Invalidate();

			return true;
		}

		public bool Undo()
		{
			Program.ActiveProject.Layers.RemoveAt(LayerIndex);
			Program.ActiveProject.LayerCount[figureType]--;
			Program.MainTimeline.ClearSelection();

			Program.MainTimeline.GLContext.Invalidate();
			return true;
		}
	}

	public class LayerRemoveAction : IAction
	{
		Layer RemovedLayer;
		int RemovedLayerIndex;

		public LayerRemoveAction(Layer layer)
		{
			RemovedLayer = layer;
			RemovedLayerIndex = Program.ActiveProject.Layers.IndexOf(layer);
		}

		public bool Do()
		{
			Program.ActiveProject.Layers.RemoveAt(RemovedLayerIndex);

			if(Program.ActiveProject.LayerCount.ContainsKey(RemovedLayer.Data.GetType()))
				Program.ActiveProject.LayerCount[RemovedLayer.Data.GetType()]--;

			Program.MainTimeline.ClearSelection();
			Program.MainTimeline.selectedItems.Select(RemovedLayer);

			Program.MainTimeline.GLContext.Invalidate();
			return true;
		}

		public bool Undo()
		{
			Program.ActiveProject.Layers.Insert(RemovedLayerIndex, RemovedLayer);
			Program.ActiveProject.LayerCount[RemovedLayer.Data.GetType()]++;
			Program.MainTimeline.ClearSelection();

			Program.MainTimeline.GLContext.Invalidate();
			return true;
		}
	}

	public class LayerMoveUpAction : IAction
	{
		int TargetLayerIndex;
		int PrevIndex;

		public LayerMoveUpAction(Layer layer)
		{
			TargetLayerIndex = Program.ActiveProject.Layers.IndexOf(layer);
			PrevIndex = Program.ActiveProject.Layers.IndexOf(layer);
		}

		public bool Do()
		{
			if (TargetLayerIndex - 1 < 0)
				return false;

			Layer L = Program.ActiveProject.Layers[TargetLayerIndex];

			Program.ActiveProject.Layers.RemoveAt(PrevIndex);
			Program.ActiveProject.Layers.Insert(TargetLayerIndex - 1, L);

			Program.MainTimeline.GLContext.Invalidate();

			return true;
		}

		public bool Undo()
		{
			Program.ActiveProject.Layers.RemoveAt(PrevIndex - 1);
			Program.ActiveProject.Layers.Insert(PrevIndex, Program.ActiveProject.Layers[TargetLayerIndex]);

			Program.MainTimeline.GLContext.Invalidate();

			return true;
		}
	}

	public class LayerMoveDownAction : IAction
	{
		int TargetLayerIndex;
		int PrevIndex;

		public LayerMoveDownAction(Layer layer)
		{
			TargetLayerIndex = Program.ActiveProject.Layers.IndexOf(layer);
			PrevIndex = Program.ActiveProject.Layers.IndexOf(layer);
		}

		public bool Do()
		{
			if (PrevIndex + 1 > Program.ActiveProject.Layers.Count - 1)
				return false;

			Program.ActiveProject.Layers.RemoveAt(PrevIndex);
			Program.ActiveProject.Layers.Insert(PrevIndex + 1, Program.ActiveProject.Layers[TargetLayerIndex]);

			Program.MainTimeline.GLContext.Invalidate();

			return true;
		}

		public bool Undo()
		{
			Program.ActiveProject.Layers.RemoveAt(PrevIndex + 1);
			Program.ActiveProject.Layers.Insert(PrevIndex, Program.ActiveProject.Layers[TargetLayerIndex]);

			Program.MainTimeline.GLContext.Invalidate();

			return true;
		}
	}

	#endregion


	public class Layer : ISaveable
	{
		public string Name;
		public bool Visible;
		public Color TimelineColor;
		public IEntity Data;
		public List<Frameset> Framesets;

		#region Constructors
		public Layer()
		{
			Name = "Layer";
			Visible = true;
			TimelineColor = Color.AliceBlue;
			Framesets = new List<Frameset>();
		}

		public Layer(IEntity data)
		{
			Name = "Layer";
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

		public Keyframe FindPrevKeyframe(float time)
		{
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

			return frameset.Keyframes[nextIndex - 1];
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

			return Data.Interpolate(t, current.State, target.State, current.InterpMode);
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

			if (state == null || Program.MainTimeline.SelectedKeyframe == null)
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
