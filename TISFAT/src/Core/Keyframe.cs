using System;
using System.Collections.Generic;
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
		int PrevSelectedNullFrame;
		int PrevSelectedBlankFrame;

		public KeyframeAddAction(Layer l, Frameset f, uint targ, IEntityState state)
		{
			LayerIndex = Program.Form.ActiveProject.Layers.IndexOf(l);
			FramesetIndex = l.Framesets.IndexOf(f);

			Time = targ;
			State = state;
		}

		public void Do()
		{
			Layer TargetLayer = Program.Form.ActiveProject.Layers[LayerIndex];
			Frameset TargetFrameset = TargetLayer.Framesets[FramesetIndex];

			AddedFrame = new Keyframe(Time, State.Copy());

			PrevSelectedBlankFrame = Program.Form.Form_Timeline.MainTimeline.SelectedBlankFrame;
			PrevSelectedNullFrame = Program.Form.Form_Timeline.MainTimeline.SelectedNullFrame;

			Program.Form.Form_Timeline.MainTimeline.SelectedNullFrame = -1;
			Program.Form.Form_Timeline.MainTimeline.SelectedBlankFrame = -1;
			Program.Form.Form_Timeline.MainTimeline.SelectedKeyframe = AddedFrame;

			TargetFrameset.Keyframes.Add(AddedFrame);
			TargetFrameset.Keyframes = TargetFrameset.Keyframes.OrderBy(o => o.Time).ToList();
			AddedFrameIndex = TargetFrameset.Keyframes.IndexOf(AddedFrame);

			Program.Form.Form_Timeline.MainTimeline.GLContext.Invalidate();
		}

		public void Undo()
		{
			Layer TargetLayer = Program.Form.ActiveProject.Layers[LayerIndex];
			Frameset TargetFrameset = TargetLayer.Framesets[FramesetIndex];

			TargetFrameset.Keyframes.RemoveAt(AddedFrameIndex);

			Program.Form.Form_Timeline.MainTimeline.SelectedNullFrame = PrevSelectedNullFrame;
			Program.Form.Form_Timeline.MainTimeline.SelectedBlankFrame = PrevSelectedBlankFrame;
			Program.Form.Form_Timeline.MainTimeline.SelectedKeyframe = null;

			AddedFrame = null;
			PrevSelectedNullFrame = -1;
			PrevSelectedBlankFrame = -1;

			Program.Form.Form_Timeline.MainTimeline.GLContext.Invalidate();
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
			LayerIndex = Program.Form.ActiveProject.Layers.IndexOf(layer);
			FramesetIndex = layer.Framesets.IndexOf(frameset);
			KeyframeIndex = frameset.Keyframes.IndexOf(frame);

			TargetKeyframe = frame;
			OriginalTime = time;
			NewTime = frame.Time;
		}

		public void Do()
		{
			Frameset TargetFrameset = Program.Form.ActiveProject.Layers[LayerIndex].Framesets[FramesetIndex];

			if (TargetFrameset.Keyframes.IndexOf(TargetKeyframe) == -1)
				TargetFrameset.Keyframes.Insert(KeyframeIndex, TargetKeyframe);

			TargetKeyframe.Time = NewTime;
			TargetFrameset.Keyframes = TargetFrameset.Keyframes.OrderBy(o => o.Time).ToList();

			Program.Form.Form_Timeline.MainTimeline.SelectedKeyframe = TargetKeyframe;
			Program.Form.Form_Timeline.MainTimeline.GLContext.Invalidate();
		}

		public void Undo()
		{
			Frameset TargetFrameset = Program.Form.ActiveProject.Layers[LayerIndex].Framesets[FramesetIndex];

			TargetKeyframe.Time = OriginalTime;
			TargetFrameset.Keyframes = TargetFrameset.Keyframes.OrderBy(o => o.Time).ToList();

			Program.Form.Form_Timeline.MainTimeline.SelectedKeyframe = TargetKeyframe;
			Program.Form.Form_Timeline.MainTimeline.GLContext.Invalidate();
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
			LayerIndex = Program.Form.ActiveProject.Layers.IndexOf(layer);
			FramesetIndex = layer.Framesets.IndexOf(frameset);

			RemovedKeyframe = frame;
			RemovedKeyframeIndex = frameset.Keyframes.IndexOf(RemovedKeyframe);
		}

		public void Do()
		{
			Frameset TargetFrameset = Program.Form.ActiveProject.Layers[LayerIndex].Framesets[FramesetIndex];

			TargetFrameset.Keyframes.RemoveAt(RemovedKeyframeIndex);
			TargetFrameset.Keyframes = TargetFrameset.Keyframes.OrderBy(o => o.Time).ToList();

			Program.Form.Form_Timeline.MainTimeline.SelectedNullFrame = -1;
			Program.Form.Form_Timeline.MainTimeline.SelectedBlankFrame = (int)RemovedKeyframe.Time;
			Program.Form.Form_Timeline.MainTimeline.SelectedKeyframe = null;

			Program.Form.Form_Timeline.MainTimeline.GLContext.Invalidate();
		}

		public void Undo()
		{
			Frameset TargetFrameset = Program.Form.ActiveProject.Layers[LayerIndex].Framesets[FramesetIndex];

			TargetFrameset.Keyframes.Insert(RemovedKeyframeIndex, RemovedKeyframe);
			TargetFrameset.Keyframes = TargetFrameset.Keyframes.OrderBy(o => o.Time).ToList();

			Program.Form.Form_Timeline.MainTimeline.SelectedNullFrame = -1;
			Program.Form.Form_Timeline.MainTimeline.SelectedBlankFrame = -1;
			Program.Form.Form_Timeline.MainTimeline.SelectedKeyframe = RemovedKeyframe;

			Program.Form.Form_Timeline.MainTimeline.GLContext.Invalidate();
		}
	} 
	#endregion

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

		#region File Saving / Loading
		public void Write(BinaryWriter writer)
		{
			writer.Write(Time);
			writer.Write(FileFormat.GetEntityStateID(State.GetType()));
			State.Write(writer);
		}

		public void Read(BinaryReader reader, UInt16 version)
		{
			Time = reader.ReadUInt32();
			Type type = FileFormat.ResolveEntityStateID(reader.ReadUInt16());
			Type[] args = { };
			object[] values = { };
			State = (IEntityState)type.GetConstructor(args).Invoke(values);
			State.Read(reader, version);
		} 
		#endregion
	}
}
