namespace TISFAT
{
	public class ManipulatableUpdateAction : IAction
	{
		int LayerIndex;
		int FramesetIndex;
		int KeyframeIndex;

		IEntityState OldState;
		IEntityState NewState;

		public ManipulatableUpdateAction(Layer l, Frameset f, Keyframe k, IEntityState prevState, IEntityState newState)
		{
			LayerIndex = Program.ActiveProject.Layers.IndexOf(l);
			FramesetIndex = l.Framesets.IndexOf(f);
			KeyframeIndex = f.Keyframes.IndexOf(k);

			OldState = prevState.Copy();
			NewState = newState.Copy();
		}

		public bool Do()
		{
			Keyframe keyframe = Program.ActiveProject.Layers[LayerIndex].Framesets[FramesetIndex].Keyframes[KeyframeIndex];

			keyframe.State = NewState.Copy();

			Program.MainTimeline.ClearSelection();
			Program.MainTimeline.selectedItems.Select(Program.ActiveProject.Layers[LayerIndex], keyframe,
													  Program.ActiveProject.Layers[LayerIndex].Framesets[FramesetIndex]);

			Program.MainTimeline.GLContext.Invalidate();

			return true;
		}

		public bool Undo()
		{
			Keyframe keyframe = Program.ActiveProject.Layers[LayerIndex].Framesets[FramesetIndex].Keyframes[KeyframeIndex];

			keyframe.State = OldState.Copy();

			Program.MainTimeline.ClearSelection();
			Program.MainTimeline.selectedItems.Select(Program.ActiveProject.Layers[LayerIndex], keyframe,
													  Program.ActiveProject.Layers[LayerIndex].Framesets[FramesetIndex]);

			Program.MainTimeline.GLContext.Invalidate();

			return true;
		}
	}

	public class ManipulateResult
	{
		public IManipulatable Target;
		public IManipulatableParams Params;
	}

	public interface IManipulatable
	{
	}

	public interface IManipulatableParams
	{
	}
}
