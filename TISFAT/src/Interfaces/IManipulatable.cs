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
			LayerIndex = Program.Form.ActiveProject.Layers.IndexOf(l);
			FramesetIndex = l.Framesets.IndexOf(f);
			KeyframeIndex = f.Keyframes.IndexOf(k);

			OldState = prevState;
			NewState = newState;
		}

		public void Do()
		{
			Keyframe keyframe = Program.Form.ActiveProject.Layers[LayerIndex].Framesets[FramesetIndex].Keyframes[KeyframeIndex];

			keyframe.State = NewState;

			Program.Form.Form_Timeline.MainTimeline.SelectedKeyframe = keyframe;
			Program.Form.Form_Timeline.MainTimeline.SelectedNullFrame = -1;
			Program.Form.Form_Timeline.MainTimeline.SelectedBlankFrame = -1;

			Program.Form.Form_Timeline.MainTimeline.GLContext.Invalidate();
		}

		public void Undo()
		{
			Keyframe keyframe = Program.Form.ActiveProject.Layers[LayerIndex].Framesets[FramesetIndex].Keyframes[KeyframeIndex];

			keyframe.State = OldState;

			Program.Form.Form_Timeline.MainTimeline.SelectedKeyframe = keyframe;
			Program.Form.Form_Timeline.MainTimeline.SelectedNullFrame = -1;
			Program.Form.Form_Timeline.MainTimeline.SelectedBlankFrame = -1;

			Program.Form.Form_Timeline.MainTimeline.GLContext.Invalidate();
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
