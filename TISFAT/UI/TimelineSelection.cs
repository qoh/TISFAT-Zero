using System;

namespace TISFAT
{
	[Flags]
	public enum SelectionType
    {
		None = 1,
        Layer = 2,
        Frameset = 4,
        Keyframe = 8,
        BlankFrame = 16,
        NullFrame = 32
    }
       
	public class TimelineSelection
	{
        public SelectionType Current;

		// Returns either the SelectedKeyframe's time or the selected blank / null frame.
		public int Time {
			get
			{
				if ((SelectionType.Keyframe & Current) != 0)
					return (int)SelectedKeyframe.Time;

				return SelectedFrameTime;
			}
		}

		private Layer SelectedLayer;
		private Frameset SelectedFrameset;
		private Keyframe SelectedKeyframe;
		private int SelectedFrameTime = -1;

		public TimelineSelection() { }

		public void Select(params ISaveable[] objs)
		{
			foreach(ISaveable obj in objs)
			{
				SelectionType type = SelectionType.None;

				if (obj.GetType() == typeof(Layer))
				{
					SelectedLayer = obj as Layer;
					type = SelectionType.Layer;
				}
				else if (obj.GetType() == typeof(Frameset))
				{
					SelectedFrameset = obj as Frameset;
					type = SelectionType.Frameset;
				}
				else if (obj.GetType() == typeof(Keyframe))
				{
					SelectedKeyframe = obj as Keyframe;
					type = SelectionType.Keyframe;

					if ((SelectionType.BlankFrame & Current) != 0)
						Current &= ~SelectionType.BlankFrame;
					if ((SelectionType.NullFrame & Current) != 0)
						Current &= ~SelectionType.NullFrame;
				}
				else
					throw new ArgumentException("Attempting to select an unknown type!");

				if ((type & Current) == 0)
					Current |= type;
			}
		}

		public void Select(SelectionType type, int time)
		{
			if ((type & Current) == 0)
				Current |= type;

			if ((SelectionType.Keyframe & Current) != 0)
			{
				SelectedKeyframe = null;
				Current &= ~SelectionType.Keyframe;
			}

			if (type == SelectionType.BlankFrame)
			{
				if ((SelectionType.NullFrame & Current) != 0)
					Current &= ~SelectionType.NullFrame;
			}
			else
			{
				if ((SelectionType.BlankFrame & Current) != 0)
					Current &= ~SelectionType.BlankFrame;
			}

			SelectedFrameTime = time;
		}

		public void Clear()
		{
			Current = SelectionType.None;

			SelectedLayer = null;
			SelectedFrameset = null;
			SelectedKeyframe = null;
			SelectedFrameTime = -1;
		}

		public void Clear(SelectionType type)
		{
			switch(type)
			{
				case SelectionType.Layer:
					SelectedLayer = null;
					break;
				case SelectionType.Frameset:
					SelectedFrameset = null;
					break;
				case SelectionType.Keyframe:
					SelectedKeyframe = null;
					break;
				case SelectionType.BlankFrame:
					SelectedFrameTime = -1;
					break;
				case SelectionType.NullFrame:
					SelectedFrameTime = -1;
					break;

				default:
					throw new ArgumentException("Unrecognized enum type!");
			}

			Current &= ~type;
		}

		public bool Contains(SelectionType type)
		{
			return (Current & type) != 0;
		}

		public ISaveable GetSelected(SelectionType type)
		{
			if ((type & Current) == 0)
				return null;

			switch (type)
			{
				case SelectionType.Layer:
					return SelectedLayer;
				case SelectionType.Frameset:
					return SelectedFrameset;
				case SelectionType.Keyframe:
					return SelectedKeyframe;
				case SelectionType.BlankFrame:
					throw new ArgumentException("Can't return a blank frame, use the Time attribute");
				case SelectionType.NullFrame:
					throw new ArgumentException("Can't return a null frame, use the Time attribute");

				default:
					throw new ArgumentException("Unrecognized enum type!");
			}
		}
	}
}
