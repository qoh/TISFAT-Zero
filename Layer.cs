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
		//This property must derived from the KeyFrame type
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

		/// <summary>
		/// Gets the frameset that contains the given position on the timeline.
		/// </summary>
		/// <param name="position">The position at which to search for a frameset.</param>
		/// <returns>The frameset that contains the given timeline position.</returns>
		/// <exception cref="System.ArgumentOutOfRangeException">Position argument must be >=0</exception>
		public Frameset GetFramesetAt(int position)
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

			int result = -BinarySearch(position);

			if (result < 0)
				return -1;

			return Framesets[result].StartingPosition - position - 1;
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

		/// <summary>
		/// Inserts the given frameset into the layer
		/// </summary>
		/// <param name="item">The frameset to insert.</param>
		/// <returns>A boolean indicating whether or not the operation was a success.</returns>
		/// <exception cref="System.ArgumentNullException">Item</exception>
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

		/// <summary>
		/// Inserts the given frameset at the given position.
		/// </summary>
		/// <param name="item">The frameset to insert into the layer.</param>
		/// <param name="position">The position at which to insert the frameset.</param>
		/// <returns>A boolean indicating whether or not the operation was a success.</returns>
		/// <exception cref="System.ArgumentNullException">Item</exception>
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
		/// <returns>True if the given frameset can be inserted into the layer with no problems and false if it won't fit. That's what she told me. Go ahead, ask her.</returns>
		public bool canBeInserted(Frameset item)
		{
			return canBeInserted(item.StartingPosition, item.EndingPosition - item.StartingPosition);
		}

		/// <summary>
		/// Determines whether a frameset with the given starting position and extent can be successfully inserted into the layer.
		/// </summary>
		/// <param name="position">The starting position of the frameset.</param>
		/// <param name="extent">The extent of the frameset.</param>
		/// <returns>A boolean indicating whether or not a frameset with the given position and extent can be inserted into the layer.</returns>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// Position argument must be >= 0
		/// or
		/// Extent argument must be >= 2
		/// </exception>
		private bool canBeInserted(int position, int extent)
		{
			if(position < 0)
				throw new ArgumentOutOfRangeException("position", "Argument must be >= 0");
			if(extent < 2)
				throw new ArgumentOutOfRangeException("extent", "Argument must be >= 2");

			if (Framesets.Count == 0)
				return true;

			int result = -BinarySearch(position);

			if (result < 0)
				return false;
			else if (result >= Framesets.Count)
				return true;

			return Framesets[result].StartingPosition - position - extent > 0;
		}

		/// <summary>
		/// Removes the given frameset from the layer
		/// </summary>
		/// <param name="item">The frameset to remove.</param>
		/// <returns>A boolean indicating whether or not the operation was a success.</returns>
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

		/// <summary>
		/// Removes the frameset at the given position.
		/// </summary>
		/// <param name="position">The position at which to search for a frameset.</param>
		/// <returns>A boolean indicating whether or not the operation was a success.</returns>
		/// <exception cref="System.ArgumentOutOfRangeException">Position argument must be >= 0</exception>
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

		/// <summary>
		/// Attempts to insert a new frameset based on the layer type at the given position.
		/// </summary>
		/// <param name="position">The position to insert the frameset at.</param>
		/// <returns>A boolean indicating whether or not the operation was a success.</returns>
		/// <exception cref="System.ArgumentOutOfRangeException">Position argument must be >= 0</exception>
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
			else if (space == 2)
				space = 3;
			else if (space < 2)
				return false;

			int extent = Math.Min(space - 1, 20);

			Framesets.Insert(result, new Frameset(Framesets[0][0].GetType(), position, extent));

			return true;
		}

		/// <summary>
		/// Attempts to move the given frameset to a new position.
		/// </summary>
		/// <param name="item">The frameset to move.</param>
		/// <param name="position">The position to move the frameset to.</param>
		/// <returns>A boolean indicating whether or not the operation was a success.</returns>
		/// <exception cref="System.ArgumentNullException">Item</exception>
		/// <exception cref="System.ArgumentOutOfRangeException">Position argument must be >= 0</exception>
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

		/// <summary>
		/// Attempts to insert a new keyframe based on the type of the layer at the given position
		/// </summary>
		/// <param name="position">The position at which to insert the keyframe..</param>
		/// <returns>A boolean indicating whether or not the operation was a success.</returns>
		/// <exception cref="System.ArgumentOutOfRangeException">Position argument must be >= 0</exception>
		public bool insertNewKeyFrameAt(int position)
		{
			if (position < 0)
				throw new ArgumentOutOfRangeException("position", "Argument must be >= 0");

			int[] result = BinarySearchDeep(position);

			//This equates to "If there's no frameset at the given position or if there's a keyframe at the given spot, return false"
			if(result[0] < 0 || result[1] >= 0)
				return false;

			KeyFrame item = (KeyFrame)(getFrameType().GetConstructor(new Type[0]).Invoke(new object[0]));
			Framesets[result[0]].InsertKeyFrameAt(item, position);
			return true;
		}

		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override string ToString()
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

		/// <summary>
		/// Gets the type of the frames used in the current layer.
		/// </summary>
		/// <returns>The type of frames used in the current layer instance.</returns>
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

		/// <summary>
		/// Initializes a new instance of the <see cref="StickLayer"/> class.
		/// </summary>
		/// <param name="layerName">The name to be given to the layer.</param>
		/// <param name="startingOffset">The starting position of the first frameset. Defaults to 0.</param>
		public StickLayer(string layerName, int startingOffset = 0) : base(layerName, typeof(StickFrame), 0, startingOffset)
		{ }
	}

	class LineLayer : Layer
	{
		new public static Type FrameType
		{
			get { return typeof(LineFrame); }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LineLayer"/> class.
		/// </summary>
		/// <param name="layerName">The name to be given to the layer.</param>
		/// <param name="startingOffset">The starting position of the first frameset. Defaults to 0.</param>
		public LineLayer(string layerName, int startingOffset = 0) : base(layerName, typeof(LineFrame), 1, startingOffset)
		{ }
	}

	class RectLayer : Layer
	{
		new public static Type FrameType
		{
			get { return typeof(RectFrame); }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RectLayer"/> class.
		/// </summary>
		/// <param name="layerName">The name to be given to the layer.</param>
		/// <param name="startingOffset">The starting position of the first frameset. Defaults to 0.</param>
		public RectLayer(string layerName, int startingOffset = 0) : base(layerName, typeof(RectFrame), 2, startingOffset)
		{ }
	}

	class CustomLayer : Layer
	{
		new public static Type FrameType
		{
			get { return typeof(CustomFrame); }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CustomLayer"/> class.
		/// </summary>
		/// <param name="layerName">The name to be given to the layer.</param>
		/// <param name="startingOffset">The starting position of the first frameset. Defaults to 0.</param>
		public CustomLayer(string layerName, int startingOffset = 0) : base(layerName, typeof(StickCustom), 3, startingOffset)
		{ }
	}
}