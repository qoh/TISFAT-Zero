using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TISFAT.Util;

namespace TISFAT
{
	#region Undo/Redo Actions
	public class FramesetAddAction : IAction
	{
		public int LayerIndex;
		public int FramesetIndex;

		public int PrevSelectedIndex;
		public int PrevSelectedKeyframe;

		public FramesetAddAction(Layer l)
		{
			LayerIndex = Program.ActiveProject.Layers.IndexOf(l);
			PrevSelectedIndex = Program.MainTimeline.SelectedNullFrame;
		}

		public bool Do()
		{
			Layer SelectedLayer = Program.ActiveProject.Layers[LayerIndex];

			Frameset fs = new Frameset();
			fs.Keyframes.Add(new Keyframe((uint)PrevSelectedIndex, SelectedLayer.Data.CreateRefState(), EntityInterpolationMode.Linear));
			fs.Keyframes.Add(new Keyframe((uint)PrevSelectedIndex + 1, SelectedLayer.Data.CreateRefState(), EntityInterpolationMode.Linear));

			SelectedLayer.Framesets.Add(fs);
			SelectedLayer.Framesets = SelectedLayer.Framesets.OrderBy(o => o.EndTime).ToList();

			FramesetIndex = SelectedLayer.Framesets.IndexOf(fs);

			Program.MainTimeline.ClearSelection();
			Program.MainTimeline.SelectedLayer = Program.ActiveProject.Layers[LayerIndex];
			Program.MainTimeline.SelectedFrameset = Program.ActiveProject.Layers[LayerIndex].Framesets[FramesetIndex];
			Program.MainTimeline.SelectedKeyframe = Program.ActiveProject.Layers[LayerIndex].Framesets[FramesetIndex].Keyframes[0];

			Program.MainTimeline.GLContext.Invalidate();
			return true;
		}

		public bool Undo()
		{
			Layer SelectedLayer = Program.ActiveProject.Layers[LayerIndex];

			SelectedLayer.Framesets.RemoveAt(FramesetIndex);
			SelectedLayer.Framesets = SelectedLayer.Framesets.OrderBy(o => o.EndTime).ToList();

			Program.MainTimeline.GLContext.Invalidate();
			return true;
		}
	}

	public class FramesetMoveAction : IAction
	{
		public int LayerIndex;
		public int FramesetIndex;

		public int startMoveLocation;
		public int endMoveLocation;

		public FramesetMoveAction(Layer l, Frameset f, int startLocation, int endLocation)
		{
			LayerIndex = Program.ActiveProject.Layers.IndexOf(l);
			FramesetIndex = l.Framesets.IndexOf(f);

			startMoveLocation = startLocation;
			endMoveLocation = endLocation;
		}

		public bool Do()
		{
			Layer currentLayer = Program.ActiveProject.Layers[LayerIndex];
			Frameset currentFrameset = currentLayer.Framesets[FramesetIndex];

			// Move by MoveAmount only if it hasn't already been moved there
			if (currentFrameset.Keyframes[0].Time == startMoveLocation)
			{
				int MoveAmount = endMoveLocation - startMoveLocation;

				foreach (Keyframe frame in currentFrameset.Keyframes)
					frame.Time = (uint)(frame.Time + MoveAmount);

				currentLayer.Framesets = currentLayer.Framesets.OrderBy(o => o.EndTime).ToList();

				Program.MainTimeline.ClearSelection();
				Program.MainTimeline.SelectedLayer = currentLayer;
				Program.MainTimeline.SelectedFrameset = currentFrameset;
				Program.MainTimeline.SelectedKeyframe = currentFrameset.Keyframes[0];

				Program.MainTimeline.GLContext.Invalidate();
			}

			return true;
		}

		public bool Undo()
		{
			Layer currentLayer = Program.ActiveProject.Layers[LayerIndex];
			Frameset currentFrameset = currentLayer.Framesets[FramesetIndex];

			// Move by -MoveAmount only if it hasn't already been moved there
			if (currentFrameset.Keyframes[0].Time == endMoveLocation)
			{
				int MoveAmount = endMoveLocation - startMoveLocation;

				foreach (Keyframe frame in currentFrameset.Keyframes)
					frame.Time = (uint)(frame.Time - MoveAmount);

				currentLayer.Framesets = currentLayer.Framesets.OrderBy(o => o.EndTime).ToList();

				Program.MainTimeline.ClearSelection();
				Program.MainTimeline.SelectedLayer = currentLayer;
				Program.MainTimeline.SelectedFrameset = currentFrameset;
				Program.MainTimeline.SelectedKeyframe = currentFrameset.Keyframes[0];

				Program.MainTimeline.GLContext.Invalidate();
			}

			return true;
		}
	}

	public class FramesetRemoveAction : IAction
	{
		public int LayerIndex;
		public int FramesetIndex;

		public Frameset RemovedFrameset;
		public int PrevSelectedIndex;
		public int PrevSelectedKeyframe;

		public FramesetRemoveAction(Layer l, Frameset f)
		{
			LayerIndex = Program.ActiveProject.Layers.IndexOf(l);
			FramesetIndex = l.Framesets.IndexOf(f);

			RemovedFrameset = f;
		}

		public bool Do()
		{
			PrevSelectedIndex = Program.MainTimeline.SelectedBlankFrame;
			PrevSelectedKeyframe = Program.MainTimeline.SelectedKeyframe == null ? -1 : RemovedFrameset.Keyframes.IndexOf(Program.MainTimeline.SelectedKeyframe);

			Layer SelectedLayer = Program.ActiveProject.Layers[LayerIndex];
			Frameset SelectedFrameset = SelectedLayer.Framesets[FramesetIndex];

			SelectedLayer.Framesets.RemoveAt(FramesetIndex);
			SelectedLayer.Framesets = SelectedLayer.Framesets.OrderBy(o => o.EndTime).ToList();

			Program.MainTimeline.SelectedNullFrame = -1;
			Program.MainTimeline.SelectedBlankFrame = -1;
			Program.MainTimeline.SelectedFrameset = null;
			Program.MainTimeline.SelectedKeyframe = null;

			Program.MainTimeline.GLContext.Invalidate();

			return true;
		}

		public bool Undo()
		{
			Layer SelectedLayer = Program.ActiveProject.Layers[LayerIndex];

			SelectedLayer.Framesets.Insert(FramesetIndex, RemovedFrameset);
			SelectedLayer.Framesets = SelectedLayer.Framesets.OrderBy(o => o.EndTime).ToList();

			Frameset SelectedFrameset = SelectedLayer.Framesets[FramesetIndex];

			Program.MainTimeline.SelectedNullFrame = -1;
			Program.MainTimeline.SelectedBlankFrame = PrevSelectedIndex;
			Program.MainTimeline.SelectedFrameset = SelectedFrameset;
			Program.MainTimeline.SelectedLayer = SelectedLayer;
			Program.MainTimeline.SelectedKeyframe = PrevSelectedKeyframe == -1 ? null : SelectedFrameset.Keyframes[PrevSelectedKeyframe];

			Program.MainTimeline.GLContext.Invalidate();

			return true;
		}
	} 
	#endregion

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
