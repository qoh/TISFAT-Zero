using System;
using System.IO;
using System.Linq;
using TISFAT.Util;

namespace TISFAT
{
	#region Undo/Redo Actions
	public class KeyframeAddAction : IAction
	{
		uint Time;
		IEntityState State;

		int LayerIndex;
		int FramesetIndex;
		int AddedFrameIndex;

		Keyframe AddedFrame;
		int PrevSelectedFrame;

		public KeyframeAddAction(Layer l, Frameset f, uint targ, IEntityState start, IEntityState end, float interpolation)
		{
			LayerIndex = Program.ActiveProject.Layers.IndexOf(l);
			FramesetIndex = l.Framesets.IndexOf(f);

			Time = targ;
			State = start.Interpolate(end, interpolation);
		}

		public bool Do()
		{
			Layer TargetLayer = Program.ActiveProject.Layers[LayerIndex];
			Frameset TargetFrameset = TargetLayer.Framesets[FramesetIndex];

			AddedFrame = new Keyframe(Time, State.Copy(), EntityInterpolationMode.Linear);

			PrevSelectedFrame = Program.MainTimeline.selectedTime;

			Program.MainTimeline.ClearSelection();
			Program.MainTimeline.selectedItems.Select(TargetLayer, TargetFrameset, AddedFrame);

			TargetFrameset.Keyframes.Add(AddedFrame);
			TargetFrameset.Keyframes = TargetFrameset.Keyframes.OrderBy(o => o.Time).ToList();
			AddedFrameIndex = TargetFrameset.Keyframes.IndexOf(AddedFrame);

			Program.MainTimeline.GLContext.Invalidate();

			return true;
		}

		public bool Undo()
		{
			Layer TargetLayer = Program.ActiveProject.Layers[LayerIndex];
			Frameset TargetFrameset = TargetLayer.Framesets[FramesetIndex];

			TargetFrameset.Keyframes.RemoveAt(AddedFrameIndex);

			Program.MainTimeline.ClearSelection();
			Program.MainTimeline.selectedItems.Select(SelectionType.BlankFrame, PrevSelectedFrame);

			AddedFrame = null;
			PrevSelectedFrame = -1;

			Program.MainTimeline.GLContext.Invalidate();

			return true;
		}
	}

	public class KeyframeMoveAction : IAction
	{
		public int LayerIndex;
		public int FramesetIndex;
		public int KeyframeIndex;

		public Keyframe TargetKeyframe;
		public uint OriginalTime;
		public uint NewTime;

		public KeyframeMoveAction(Layer layer, Frameset frameset, Keyframe frame, uint time)
		{
			LayerIndex = Program.ActiveProject.Layers.IndexOf(layer);
			FramesetIndex = layer.Framesets.IndexOf(frameset);
			KeyframeIndex = frameset.Keyframes.IndexOf(frame);

			TargetKeyframe = frame;
			OriginalTime = time;
			NewTime = frame.Time;
		}

		public bool Do()
		{
			Frameset TargetFrameset = Program.ActiveProject.Layers[LayerIndex].Framesets[FramesetIndex];

			if (TargetFrameset.Keyframes.IndexOf(TargetKeyframe) == -1)
				TargetFrameset.Keyframes.Insert(KeyframeIndex, TargetKeyframe);

			TargetKeyframe.Time = NewTime;
			TargetFrameset.Keyframes = TargetFrameset.Keyframes.OrderBy(o => o.Time).ToList();

			Program.MainTimeline.ClearSelection();
			Program.MainTimeline.selectedItems.Select(Program.ActiveProject.Layers[LayerIndex], TargetFrameset, TargetKeyframe);

			Program.MainTimeline.GLContext.Invalidate();
			return true;
		}

		public bool Undo()
		{
			Frameset TargetFrameset = Program.ActiveProject.Layers[LayerIndex].Framesets[FramesetIndex];

			TargetKeyframe.Time = OriginalTime;
			TargetFrameset.Keyframes = TargetFrameset.Keyframes.OrderBy(o => o.Time).ToList();

			Program.MainTimeline.ClearSelection();
			Program.MainTimeline.selectedItems.Select(Program.ActiveProject.Layers[LayerIndex], TargetFrameset, TargetKeyframe);

			Program.MainTimeline.GLContext.Invalidate();
			return true;
		}
	}

	public class KeyframeRemoveAction : IAction
	{
		int LayerIndex;
		int FramesetIndex;
		int RemovedKeyframeIndex;

		Keyframe RemovedKeyframe;

		public KeyframeRemoveAction(Layer layer, Frameset frameset, Keyframe frame)
		{
			LayerIndex = Program.ActiveProject.Layers.IndexOf(layer);
			FramesetIndex = layer.Framesets.IndexOf(frameset);

			RemovedKeyframe = frame;
			RemovedKeyframeIndex = frameset.Keyframes.IndexOf(RemovedKeyframe);
		}

		public bool Do()
		{
			Frameset TargetFrameset = Program.ActiveProject.Layers[LayerIndex].Framesets[FramesetIndex];

			TargetFrameset.Keyframes.RemoveAt(RemovedKeyframeIndex);
			TargetFrameset.Keyframes = TargetFrameset.Keyframes.OrderBy(o => o.Time).ToList();

			Program.MainTimeline.selectedItems.Clear(SelectionType.Keyframe);
			Program.MainTimeline.selectedItems.Select(SelectionType.BlankFrame, (int)RemovedKeyframe.Time);

			Program.MainTimeline.GLContext.Invalidate();

			return true;
		}

		public bool Undo()
		{
			Frameset TargetFrameset = Program.ActiveProject.Layers[LayerIndex].Framesets[FramesetIndex];

			TargetFrameset.Keyframes.Insert(RemovedKeyframeIndex, RemovedKeyframe);
			TargetFrameset.Keyframes = TargetFrameset.Keyframes.OrderBy(o => o.Time).ToList();

			Program.MainTimeline.ClearSelection();
			Program.MainTimeline.selectedItems.Select(Program.ActiveProject.Layers[LayerIndex], TargetFrameset, RemovedKeyframe);

			Program.MainTimeline.GLContext.Invalidate();

			return true;
		}
	}

	public class KeyframeChangeInterpModeAction : IAction
	{
		int LayerIndex;
		int FramesetIndex;
		int KeyframeIndex;

		EntityInterpolationMode PrevMode;
		EntityInterpolationMode TargetMode;

		public KeyframeChangeInterpModeAction(Layer l, Frameset f, Keyframe frame, EntityInterpolationMode target)
		{
			LayerIndex = Program.ActiveProject.Layers.IndexOf(l);
			FramesetIndex = l.Framesets.IndexOf(f);
			KeyframeIndex = f.Keyframes.IndexOf(frame);
			TargetMode = target;
		}

		public bool Do()
		{
			PrevMode = Program.ActiveProject.Layers[LayerIndex].Framesets[FramesetIndex].Keyframes[KeyframeIndex].InterpMode;

			Program.ActiveProject.Layers[LayerIndex].Framesets[FramesetIndex].Keyframes[KeyframeIndex].InterpMode = TargetMode;

			Program.MainTimeline.GLContext.Invalidate();
			return true;
		}

		public bool Undo()
		{
			Program.ActiveProject.Layers[LayerIndex].Framesets[FramesetIndex].Keyframes[KeyframeIndex].InterpMode = PrevMode;

			Program.MainTimeline.GLContext.Invalidate();
			return true;
		}
	}
	#endregion

	public class Keyframe : ISaveable
	{
		public UInt32 Time;
		public IEntityState State;
		public EntityInterpolationMode InterpMode;

		public Keyframe()
		{
			Time = 0;
			State = null;
			InterpMode = EntityInterpolationMode.Linear;
		}

		public Keyframe(UInt32 time, IEntityState state, EntityInterpolationMode interpMode)
		{
			Time = time;
			State = state;
			InterpMode = interpMode;
		}

		#region File Saving / Loading
		public void Write(BinaryWriter writer)
		{
			writer.Write(Time);
			writer.Write(FileFormat.GetEntityStateID(State.GetType()));
			writer.Write(InterpMode.ToString());
			State.Write(writer);
		}

		public void Read(BinaryReader reader, UInt16 version)
		{
			Time = reader.ReadUInt32();
			Type type = FileFormat.ResolveEntityStateID(reader.ReadUInt16());
			InterpMode = version >= 2 ? (EntityInterpolationMode)Enum.Parse(typeof(EntityInterpolationMode), reader.ReadString()) : EntityInterpolationMode.Linear;
			Type[] args = { };
			object[] values = { };
			State = (IEntityState)type.GetConstructor(args).Invoke(values);
			State.Read(reader, version);
		}
		#endregion
	}
}
