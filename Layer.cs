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
		//This list of framesets are always sorted, which makes searching through them much faster and easier to manage in general.
		public List<Frameset> Framesets = new List<Frameset>();
		public string LayerName;
		protected ushort layerType;
		public int SelectedKeyframe_Loc = -1; //The position of the selected keyframe in it's respective frameset
		public int SelectedKeyframe_Pos = -1; //The position in the timeline of the selected keyframe.
		public int SelectedFrameset_Loc = -1; //The index of the selected frameset in the list of framesets.

		public StickObject LayerFigure;

		//Dynamic properties that can be used if need be. Generally, use this if it isn't a shared property between layer types.
		public Attributes Properties = new Attributes();

		public ushort LayerType
		{
			get { return layerType; }
		}

		//Properties that must be overridden by derived classes
		//This property must 
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
		public Layer(string layerName, Type frameType, ushort type, int startingOffset = 0) : this(layerName, new Frameset(frameType, startingOffset), type, startingOffset)
		{ }

		/// <summary>
		/// Initializes a new instance of the <see cref="Layer"/> class using the given frameset.
		/// </summary>
		/// <param name="layerName">The name of the layer.</param>
		/// <param name="frames">The frameset to use in the</param>
		/// <param name="startingOffset">Optionally offset the position of all the frames by this amount.</param>
		public Layer(string layerName, Frameset frames, ushort type, int startingOffset = 0)
		{
			LayerName = layerName;
			layerType = type;
			Type fType = frames[0].GetType();

			frames.shiftFrames(startingOffset);

			ConstructorInfo constructor = ((Type)((PropertyInfo)(fType.GetMember("ObjectType").GetValue(0))).GetValue(this, null)).GetConstructor(new Type[] { typeof(bool) });

			LayerFigure = (StickObject)constructor.Invoke(new object[] { false });

			Framesets.Add(frames);
		}

		/// <summary>
		/// Performs a binary search to find the frameset that contains the given position.   ... and binaries the search according to ghostdoc
		/// </summary>
		/// <param name="position">The position at which to search for a frameset.</param>
		/// <returns>
		/// The index of the frameset that does or is closest to containing the given timeline position.
		/// If the given position is not contained by any frameset it will give a negative result indicating the frameset that is closest to containing the given position.
		/// </returns>
		/// <exception cref="System.ArgumentOutOfRangeException">Position argument must be >= 0</exception>
		public int BinarySearch(int position)
		{
			if (position < 0)
				throw new ArgumentOutOfRangeException("position", "Argument must be >= 0");

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

		/// <summary>
		/// Searches for a frameset at the given position, then searches for a keyframe inside that frameset if one is found.   ... and binaries the search DEEP according to ghostdoc.
		/// </summary>
		/// <param name="position">The position at which to search for a frameset/keyframe.</param>
		/// <returns>The arguments needed to retrive a keyframe directly from the list of framesets</returns>
		/// <exception cref="System.ArgumentOutOfRangeException">Position argument must be >= 0</exception>
		public int[] BinarySearchDeep(int position)
		{
			if (position < 0)
				throw new ArgumentOutOfRangeException("position", "Argument must be >= 0");

			int[] result = { -1, -1 };

			result[0] = BinarySearch(position);

			if (result[0] < 0)
				return result;

			result[1] = Framesets[result[0]].BinarySearch(position);

			return result;
		}

		public Frameset GetFramesetAt (int position)
		{
			if(position < 0)
				throw new ArgumentOutOfRangeException("position", "Argument must be >=0");

			int result = BinarySearch(position);

			return result >= 0 ? Framesets[result] : null;
		}

		/// <summary>
		/// Gets the keyframe that has the specified position in the timeline.
		/// </summary>
		/// <param name="position">The position to search for a keyframe at.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentOutOfRangeException">Position argument must be >= 0</exception>
		public KeyFrame GetKeyframeAt(int position)
		{
			if (position < 0)
				throw new ArgumentOutOfRangeException("position", "Argument must be >= 0");

			int[] framePosition = BinarySearchDeep(position);

			if (framePosition[1] == -1)
				return null;

			return Framesets[framePosition[0]][framePosition[1]];
		}


		/// <summary>
		/// Gets the number of empty frames starting from the given position to the position of the next frameset.
		/// </summary>
		/// <param name="position">The position.</param>
		/// <returns>
		/// -1 if given position is inside a frameset, -2 if given position is past the last frameset, a positive number if there is space from the given position to the nearest frameset.
		/// </returns>
		/// <exception cref="System.ArgumentOutOfRangeException">Position argument must be >= 0</exception>
		public int getEmptyFramesCount(int position)
		{
			if (position < 0)
				throw new ArgumentOutOfRangeException("position", "Argument must be >= 0");

			if (position > Framesets[Framesets.Count - 1].EndingPosition)
				return -2;

			int result = BinarySearch(position);

			if (result > 0)
				return -1;

			return Framesets[result].StartingPosition - position;
		}

		/// <summary>
		/// Gets the type of the frame at the given position in the timeline.
		/// </summary>
		/// <param name="position">The inputted position.</param>
		/// <returns>
		/// The type of frame at the given position.
		/// 0: No frame
		/// 1: Middle Keyframe
		/// 2: First Keyframe of Frameset
		/// 3: Last Keyframe of Frameset
		/// 4: Tween frame
		/// </returns>
		/// <exception cref="System.ArgumentOutOfRangeException">Position argument must be >= 0</exception>
		public int getFrameTypeAt(int position)
		{
			if (position < 0)
				throw new ArgumentOutOfRangeException("position", "Argument must be >= 0");

			if (position > Framesets[Framesets.Count - 1].EndingPosition || position < Framesets[0].StartingPosition)
				return 0;

			int[] result = BinarySearchDeep(position);

			if (result[0] < 0)
				return 0;

			Frameset set = Framesets[result[0]];

			if (result[1] < 0)
				return 4;
			else if (result[1] == 0)
				return 2;
			else if (result[1] == set.FrameCount - 1)
				return 3;

			return 1;
		}

		public bool insertFrameset(Frameset item)
		{
			if (item == null)
				throw new ArgumentNullException("item");

			//Check if the frameset will actually fit in it's current spot
			if (!canBeInserted(item))
				return false;

			//Insert the frameset in the correct spot
			Framesets.Insert(-BinarySearch(item.StartingPosition), item);

			return true;
		}

		public bool insertFramesetAt(Frameset item, int position)
		{
			if (item == null)
				throw new ArgumentNullException("item");

			bool result = item.shiftFrames(position - item.StartingPosition);

			return result ? insertFrameset(item) : false;
		}

		/// <summary>
		/// Determines whether the given frameset can be inserted without causing problems.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <returns>True if it can be inserted into the layer with no problems and false if it won't fit. That's what she told me. Go ahead, ask her.</returns>
		public bool canBeInserted(Frameset item)
		{
			return canBeInserted(item.StartingPosition, item.EndingPosition - item.StartingPosition);
		}

		private bool canBeInserted(int position, int extent)
		{
			if(position < 0)
				throw new ArgumentOutOfRangeException("position", "Argument must be >= 0");
			if(extent < 2)
				throw new ArgumentOutOfRangeException("extent", "Argument must be >= 2");

			int result = -BinarySearch(position);

			if(result < 0)
				return false;

			return Framesets[result].StartingPosition - position - extent > 0;
		}

		public bool removeFrameset(Frameset item)
		{
			if(item == null)
				return false;

			int index = Framesets.IndexOf(item);

			if (index == -1)
				return false;

			Framesets.RemoveAt(index);
			return true;
		}

		public bool removeFramesetAt(int position)
		{
			if (position < 0)
				throw new ArgumentOutOfRangeException("position", "Argument must be >= 0");

			if (position > Framesets[Framesets.Count - 1].EndingPosition || position < Framesets[0].StartingPosition)
				return false;

			int result = BinarySearch(position);

			if (result < 0)
				return false;

			Framesets.RemoveAt(result);
			return true;
		}

		public bool insertNewFramesetAt(int position)
		{
			if (position < 0)
				throw new ArgumentOutOfRangeException("position", "Argument must be >= 0");

			int result = -BinarySearch(position);

			if (result < 0)
				return false;

			int space = getEmptyFramesCount(position);

			if (space == -2)
				space = 21;
			else if (space == 1)
				return false;

			int extent = Math.Min(space - 1, 20);

			Framesets.Insert(result, new Frameset(Framesets[0][0].GetType(), position, extent));

			return true;
		}

		public bool moveFramesetTo(Frameset item, int position)
		{
			if(item == null)
				throw new ArgumentNullException("item");

			if(position < 0)
				throw new ArgumentOutOfRangeException("position", "Argument must be >= 0");

			int index = BinarySearch(item.StartingPosition);

			if(index < 0)
				return false;

			Framesets.RemoveAt(index);

			if(!canBeInserted(position, item.EndingPosition - item.StartingPosition))
				return false;

			item.shiftFrames(position - item.StartingPosition);

			index = BinarySearch(position);

			Framesets.Insert(index, item);

			return true;
		}

		public bool insertNewKeyFrameAt(int position)
		{
			int[] result = BinarySearchDeep(position);

			if(result[0] < 0 || result[1] >= 0)
				return false;

			KeyFrame item = (KeyFrame)(getFrameType().GetConstructor(new Type[0]).Invoke(new object[0]));
			Framesets[result[0]].InsertKeyFrameAt(item, position);
			return true;
		}

		public override string ToString ()
		{
			string result = "Layer Name: " + this.LayerName + "\tFrameset Count: " + this.Framesets.Count;

			int max = this.Framesets.Count;
			for(int a = 0; a < max; a++)
			{
				Frameset s = this.Framesets[a];
				result += "\nFrameset #" + (a+1) + ":\tS:" + s.StartingPosition + "\tE:" + s.EndingPosition + "\tL:" + (s.EndingPosition - s.StartingPosition);
				result += s.ToString();
			}

			return result;
		}

		public Type getFrameType()
		{
			return (Type)(this.GetType().GetProperty("FrameType").GetValue(this, null));
		}
	}

	class StickLayer : Layer
	{
		new public static Type FrameType
		{
			get { return typeof(StickFrame); }
		}

		public StickLayer(string layerName, int startingOffset = 0) : base(layerName, typeof(StickFrame), 0, startingOffset)
		{ }
	}

	class LineLayer : Layer
	{
		new public static Type FrameType
		{
			get { return typeof(LineFrame); }
		}

		public LineLayer(string layerName, int startingOffset = 0) : base(layerName, typeof(LineFrame), 1, startingOffset)
		{ }
	}

	class RectLayer : Layer
	{
		new public static Type FrameType
		{
			get { return typeof(RectFrame); }
		}

		public RectLayer(string layerName, int startingOffset = 0) : base(layerName, typeof(RectFrame), 2, startingOffset)
		{ }
	}

	class CustomLayer : Layer
	{
		new public static Type FrameType
		{
			get { return typeof(CustomFrame); }
		}

		public CustomLayer(string layerName, int startingOffset = 0) : base(layerName, typeof(StickCustom), 3, startingOffset)
		{ }
	}
}