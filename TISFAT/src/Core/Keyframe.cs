using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TISFAT.Interfaces;
using TISFAT.Util;

namespace TISFAT
{
	public class KeyframeAddAction : IAction
	{
		uint Time;
		IEntityState State;

		int LayerIndex;
		int FramesetIndex;

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

			AddedFrame = new Keyframe(Time, State);

			PrevSelectedBlankFrame = Program.Form.Form_Timeline.MainTimeline.SelectedBlankFrame;
			PrevSelectedNullFrame = Program.Form.Form_Timeline.MainTimeline.SelectedNullFrame;

			Program.Form.Form_Timeline.MainTimeline.SelectedNullFrame = -1;
			Program.Form.Form_Timeline.MainTimeline.SelectedBlankFrame = -1;
			Program.Form.Form_Timeline.MainTimeline.SelectedKeyframe = AddedFrame;

			TargetFrameset.Keyframes.Add(AddedFrame);
			TargetFrameset.Keyframes = TargetFrameset.Keyframes.OrderBy(o => o.Time).ToList();

			Program.Form.Form_Timeline.MainTimeline.GLContext.Invalidate();
		}

		public void Undo()
		{
			Layer TargetLayer = Program.Form.ActiveProject.Layers[LayerIndex];
			Frameset TargetFrameset = TargetLayer.Framesets[FramesetIndex];

			TargetFrameset.Keyframes.Remove(AddedFrame);

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
		public List<Keyframe> OriginalKeyframes;
		public uint OriginalTime;
		public uint NewTime;

		public Frameset TargetFrameset;
		public Keyframe TargetKeyframe;

		public KeyframeMoveAction(Frameset frameset, Keyframe frame, uint time)
		{
			TargetFrameset = frameset;
			TargetKeyframe = frame;
			OriginalTime = time;
			NewTime = frame.Time;
			OriginalKeyframes = new List<Keyframe>(frameset.Keyframes);
		}

		public void Do()
		{
			TargetKeyframe.Time = NewTime;
			TargetFrameset.Keyframes = TargetFrameset.Keyframes.OrderBy(o => o.Time).ToList();

			Program.Form.Form_Timeline.MainTimeline.SelectedKeyframe = TargetKeyframe;
			Program.Form.Form_Timeline.MainTimeline.GLContext.Invalidate();
		}

		public void Undo()
		{
			TargetFrameset.Keyframes = OriginalKeyframes;

			Program.Form.Form_Timeline.MainTimeline.GLContext.Invalidate();
		}
	}

	public class KeyframeRemoveAction : IAction
	{
		Frameset TargetFrameset;

		List<Keyframe> OriginalKeyframes;
		Keyframe RemovedKeyframe;

		public KeyframeRemoveAction(Frameset frameset, Keyframe frame)
		{
			TargetFrameset = frameset;
			RemovedKeyframe = frame;

			OriginalKeyframes = new List<Keyframe>(frameset.Keyframes);
		}

		public void Do()
		{
			TargetFrameset.Keyframes.Remove(RemovedKeyframe);
			TargetFrameset.Keyframes = TargetFrameset.Keyframes.OrderBy(o => o.Time).ToList();

			Program.Form.Form_Timeline.MainTimeline.SelectedNullFrame = -1;
			Program.Form.Form_Timeline.MainTimeline.SelectedBlankFrame = (int)RemovedKeyframe.Time;
			Program.Form.Form_Timeline.MainTimeline.SelectedKeyframe = null;

			Program.Form.Form_Timeline.MainTimeline.GLContext.Invalidate();
		}

		public void Undo()
		{
			TargetFrameset.Keyframes = OriginalKeyframes;

			Program.Form.Form_Timeline.MainTimeline.SelectedNullFrame = -1;
			Program.Form.Form_Timeline.MainTimeline.SelectedBlankFrame = -1;
			Program.Form.Form_Timeline.MainTimeline.SelectedKeyframe = RemovedKeyframe;

			Program.Form.Form_Timeline.MainTimeline.GLContext.Invalidate();
		}
	}

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
