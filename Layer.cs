using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Text;
using System.Linq;

namespace NewKeyFrames
{
	abstract class Layer
	{
		public List<Frameset> Framesets = new List<Frameset>();
		public string LayerName;
		protected ushort layerType;
		public int SelectedKeyframe_Loc = -1; //The position of the selected keyframe in it's respective frameset
		public int SelectedKeyframe_Pos = -1; //The position in the timeline of the selected keyframe.
		public int SelectedFrameset_Loc = -1; //The index of the selected frameset in the list of framesets.

		public StickObject LayerFigure, LayerTweenFigure;

		//Dynamic properties that can be used if need be. Generally, use this if it isn't a shared property between layer types.
		public Attributes Properties = new Attributes();

		public ushort LayerType
		{
			get { return layerType; }
		}

		//Properties that must be overridden by derived classes
		public static Type FrameType
		{
			get { return null; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Layer"/> class.
		/// </summary>
		/// <param name="layerName">The name of the layer.</param>
		/// <param name="layerType">The type of keyframes the layer will contain. Must be a type derived from the KeyFrame type.</param>
		/// <param name="type">The type number associated with the layerType parameter.</param>
		/// <param name="startingOffset">Optionally offset the position of all the frames by this amount.</param>
		protected Layer(string layerName, Type frameType, ushort type, int startingOffset = 0)
		{
			LayerName = layerName;
			layerType = type;

			Framesets.Add(new Frameset(frameType, startingOffset));

			//I use reflection here to fetch and invoke the constructor for the specific type of StickObject that we want to create for out figs.
			ConstructorInfo constructor = ((Type)((PropertyInfo)(frameType.GetMember("ObjectType").GetValue(0))).GetValue(this, null)).GetConstructor(new Type[] { typeof(bool) });

			LayerFigure = (StickObject)constructor.Invoke(new object[] { false });
			LayerTweenFigure = (StickObject)constructor.Invoke(new object[] { false });
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Layer"/> class using the given frameset.
		/// </summary>
		/// <param name="layerName">The name of the layer.</param>
		/// <param name="frames">The frameset to use in the</param>
		/// <param name="startingOffset">Optionally offset the position of all the frames by this amount.</param>
		protected Layer(string layerName, Frameset frames, int startingOffset = 0)
		{
			LayerName = layerName;

			if (startingOffset > 0)
				for (int a = frames.FrameCount - 1; a >= 0; a--)
					frames.MoveKeyFrameTo(a, frames[a].Position + startingOffset);
			else
				foreach (KeyFrame f in frames)
					frames.MoveKeyFrameTo(f, f.Position + startingOffset);

			Type frameType = frames[0].GetType();

			ConstructorInfo constructor = frameType.GetConstructor(new Type[] { typeof(bool) });

			LayerFigure = (StickObject)constructor.Invoke(new object[] { false });
			LayerTweenFigure = (StickObject)constructor.Invoke(new object[] { false });

			Framesets.Add(frames);
		}

		//Searches for a frameset that encases the given position in the timeline.
		public int BinarySearch(int position)
		{
			int bottom = 0;
			int top = Framesets.Count - 1;
			int middle = top >> 1;

			//I had to make this binary search algorithm custom because I need it to store the middle index if the target is not found.
			while (top >= bottom)
			{
				int low = Framesets[middle].StartingPosition;
				int high = Framesets[middle].EndingPosition;

				if (low > position)
					top = middle - 1;
				else if (high < position)
					bottom = middle + 1;
				else
					return middle;

				middle = (bottom + top) >> 1;
			}

			return -middle - 1;
		}

		public int[] BinarySearchDeep(int position)
		{
			int[] result = { -1, -1 };

			result[0] = BinarySearch(position);

			if (result[0] < 0)
				return result;

			result[1] = Framesets[result[0]].BinarySearch(position);

			return result;
		}
	}

	class StickLayer : Layer
	{
		new public static Type FrameType
		{
			get { return typeof(StickFigure); }
		}

		public StickLayer(string layerName, int startingOffset = 0) : base(layerName, typeof(StickFrame), 0, startingOffset)
		{ }
	}

	class LineLayer : Layer
	{
		new public static Type FrameType
		{
			get { return typeof(StickLine); }
		}

		public LineLayer(string layerName, int startingOffset = 0) : base(layerName, typeof(LineFrame), 1, startingOffset)
		{ }
	}

	class RectLayer : Layer
	{
		new public static Type FrameType
		{
			get { return typeof(StickRect); }
		}

		public RectLayer(string layerName, int startingOffset = 0) : base(layerName, typeof(RectFrame), 2, startingOffset)
		{ }
	}

	class CustomLayer : Layer
	{
		new public static Type FrameType
		{
			get { return typeof(StickCustom); }
		}

		public CustomLayer(string layerName, int startingOffset = 0) : base(layerName, typeof(StickCustom), 3, startingOffset)
		{ }
	}
}