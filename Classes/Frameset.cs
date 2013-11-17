using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Reflection;

namespace NewKeyFrames
{
	class Frameset : IEnumerable<KeyFrame>
	{
		public List<KeyFrame> KeyFrames = new List<KeyFrame>();
		private int startPos, endPos, frameCount = 2;

		public int StartingPosition
		{
			get { return startPos; }
		}

		public int EndingPosition
		{
			get { return endPos; }
		}

		public int FrameCount
		{
			get { return frameCount; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Frameset" /> class.
		/// </summary>
		/// <param name="SetType">The type of keyframes in the set.</param>
		/// <param name="startingPosition">The starting position of the frameset.</param>
		/// <param name="extent">The length of the set.</param>
		/// <exception cref="System.ArgumentException">SetType must be a derived type of KeyFrame;SetType</exception>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// startingPosition argument must be >= 0
		/// or
		/// extent argument must be >= 2
		/// </exception>
		public Frameset(Type SetType, int startingPosition = 0, int extent = 20)
		{
			if (SetType.BaseType != typeof(KeyFrame))
				throw new ArgumentException("SetType must be a derived type of KeyFrame", "SetType");

			if (startingPosition < 0)
				throw new ArgumentOutOfRangeException("startingPosition", "Argument must be >= 0");
			else if (extent < 2)
				throw new ArgumentOutOfRangeException("extent", "Argument must be >= 2");

			ConstructorInfo KFConstructor = SetType.GetConstructor(new Type[] { typeof(int)});

			startPos = startingPosition;
			endPos = startPos + extent;

			KeyFrames.Add((KeyFrame)KFConstructor.Invoke(new object[] { startPos }));
			KeyFrames.Add((KeyFrame)KFConstructor.Invoke(new object[] { endPos }));
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Frameset" /> class from two existing keyframes.
		/// </summary>
		/// <param name="First">The first keyframe in the set.</param>
		/// <param name="Last">The last keyframe in the set.</param>
		public Frameset(KeyFrame First, KeyFrame Last) : this(new KeyFrame[] { First, Last })
		{ }

		/// <summary>
		/// Initializes a new instance of the <see cref="Frameset" /> class from a set of keyframes sorted by timeline position.
		/// </summary>
		/// <param name="Frames">The set of frames to use in the frameset.</param>
		public Frameset(List<KeyFrame> Frames) : this(Frames.ToArray())
		{ }

		/// <summary>
		/// Initializes a new instance of the <see cref="Frameset" /> class from a set of keyframes sorted by timeline position.
		/// </summary>
		/// <param name="Frames">The set of frames to use in the frameset.</param>
		/// <exception cref="System.ArgumentException">Frames parameter must have at least 2 elements</exception>
		public Frameset(KeyFrame[] Frames)
		{
			if (Frames.Length < 2)
				throw new ArgumentException("Parameter must have at least 2 elements", "Frames");

			KeyFrames.AddRange(Frames);

			frameCount = Frames.Length;

			startPos = Frames[0].Position;
			endPos = Frames[Frames.Length - 1].Position;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Frameset" /> class from a single keyframe.
		/// </summary>
		/// <param name="Base">The base.</param>
		/// <param name="startingPosition">The starting position.</param>
		/// <param name="extent">The extent.</param>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// startingPosition argument must be >= 0
		/// or
		/// extent argument must be >= 2
		/// </exception>
		public Frameset(KeyFrame Base, int startingPosition = 0, int extent = 20)
		{
			if (startingPosition < 0)
				throw new ArgumentOutOfRangeException("startingPosition", "Argument must be >= 0");
			else if (extent < 2)
				throw new ArgumentOutOfRangeException("extent", "Argument must be >= 2");

			startPos = startingPosition;
			endPos = startingPosition + extent;

			KeyFrame start = Base.createClone();
			start.Position = startPos;

			KeyFrame end = Base.createClone();
			end.Position = endPos;

			KeyFrames.AddRange(new KeyFrame[] { start, end });
		}

		/// <summary>
		/// Gets or sets the <see cref="KeyFrame"/> at the specified index.
		/// </summary>
		/// <value>
		/// The <see cref="KeyFrame"/>.
		/// </value>
		/// <param name="index">The index of the keyframe to get/set.</param>
		/// <returns></returns>
		public KeyFrame this[int index]
		{
			get { return KeyFrames[index]; }
			
			set { KeyFrames[index] = value; }
		}

		/// <summary>
		/// Inserts a keyframe into the frameset.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="copyBeforeInsert">if set to <c>true</c>, insert a copy of the keyframe so that instance is not used by the frameset.</param>
		/// <returns>A boolean indicating whether or not the operation succeeded.</returns>
		/// <exception cref="System.ArgumentOutOfRangeException">Item's Position must be >= 0</exception>
		public bool InsertKeyFrame(KeyFrame item, bool copyBeforeInsert = false)
		{
			if(item.Position < 0)
				throw new ArgumentOutOfRangeException("Item", "Item's Position must be >= 0");

			int insertPosition = -BinarySearch(item.Position);

			if(copyBeforeInsert)
				item = item.createClone();

			//If the variable is below 0 that means that there's a keyframe with that position already in the frameset. 
			if(insertPosition < 0)
				return false;

			KeyFrames.Insert(insertPosition, item);
			frameCount++;

			int Position = item.Position;

			if(Position < startPos)
				startPos = Position;
			else if(Position > endPos)
				endPos = Position;

			return true;
		}

		/// <summary>
		/// Inserts a keyframe into the frameset.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="position">The position in the timeline that the new keyframe will have.</param>
		/// <param name="copyBeforeInsert">if set to <c>true</c>, insert a copy of the keyframe so that instance is not used by the frameset.</param>
		/// <returns>A boolean indicating whether or not the operation succeeded.</returns>
		/// <exception cref="System.ArgumentOutOfRangeException">Item's Position must be >= 0</exception>
		public bool InsertKeyFrameAt(KeyFrame item, int position, bool copyBeforeInsert = false)
		{
			if (position < 0)
				throw new ArgumentOutOfRangeException("position", "Argument must be >= 0");

			if(copyBeforeInsert)
				item = item.createClone();

			item.Position = position;

			return InsertKeyFrame(item, false);
		}

		/// <summary>
		/// Attempts to remove the keyframe that has the specified position in the timeline.
		/// </summary>
		/// <param name="position">The position at which to remove the keyframe from.</param>
		/// <returns>A boolean indicating whether or not the operation was a success.</returns>
		/// <exception cref="System.ArgumentOutOfRangeException">Position argument must be >= 0</exception>
		public bool RemoveKeyFrameAt(int position)
		{
			if(position < 0)
				throw new ArgumentOutOfRangeException("position", "Argument must be >= 0");

			int Index = BinarySearch(position);

			if(Index >= 0)
			{
				KeyFrames.RemoveAt(Index);
				frameCount--;

				return true;
			}

			return false;
		}

		/// <summary>
		/// Removes the keyframe at the specified index inside the frameset.
		/// </summary>
		/// <param name="index">The index at which to remove.</param>
		/// <returns>A boolean indicating whether or not the operation was a success.</returns>
		/// <exception cref="System.ArgumentOutOfRangeException">Index argument must be >= 0</exception>
		public bool RemoveKeyFrame (int index)
		{
			if (index < startPos)
			{
				if (index < 0)
					throw new ArgumentOutOfRangeException ("Index", "Argument must be >= 0");

				return false;
			}
			else if (index > endPos || frameCount < 2)
				return false;

			KeyFrame x = KeyFrames [index];

			if (x.Position == startPos)
				startPos = KeyFrames [index + 1].Position;
			else if (x.Position == endPos)
				endPos = KeyFrames [index - 1].Position;

			KeyFrames.RemoveAt(index);
			frameCount--;

			return true;
		}

		/// <summary>
		/// Removes the given keyframe from the frameset.
		/// </summary>
		/// <param name="item">The keyframe to remove from the set.</param>
		/// <returns>A boolean indicating whether or not the operation was a success.</returns>
		public bool RemoveKeyFrame(KeyFrame item)
		{
			try { return RemoveKeyFrame(KeyFrames.IndexOf(item)); }
			catch { return false; }
		}

		/// <summary>
		/// Does a binary search through the frameset for a keyframe with the given position.
		/// </summary>
		/// <param name="position">The position to search for.</param>
		/// <returns>A positive number giving the index of the keyframe inside the frameset if it is found, and a negative number if one was not found.</returns>
		public int BinarySearch(int position)
		{
			if (position < 0)
				throw new ArgumentOutOfRangeException("position", "Argument must be >= 0");

			if(position > endPos)
				return -frameCount;

			int bottom = 0;
			int top = frameCount;
			int middle = top >> 1;

			//I had to make this binary search algorithm custom because I need it to store the middle index if the target is not found.
			while (top >= bottom)
			{
				int x = KeyFrames[middle].Position;

				if (x > position)
					top = middle - 1;
				else if (x < position)
					bottom = middle + 1;
				else
					return middle;

				middle = (bottom + top) >> 1;
			}

			return -middle - 1;
		}

		/// <summary>
		/// Gets the keyframe that has the specified position in the timeline.
		/// </summary>
		/// <param name="position">The position.</param>
		/// <returns>The keyframe that is at the specified position.</returns>
		/// <exception cref="System.ArgumentOutOfRangeException">Position argument must be >= 0</exception>
		public KeyFrame GetKeyFrameAt(int position)
		{
			if (position < 0)
				throw new ArgumentOutOfRangeException("Position", "Argument must be >= 0");

			if (position > endPos || position < startPos)
				return null;

			int SearchPos = BinarySearch(position);

			if (SearchPos < 0)
				return null;

			return KeyFrames[SearchPos];
		}

		/// <summary>
		/// Moves the specified keyframe to a new position inside the timeline.
		/// </summary>
		/// <param name="item">The keyframe to move.</param>
		/// <param name="position">The position to move the keyframe to.</param>
		/// <returns>A boolean indicating whether or not the operation was a success.</returns>
		/// <exception cref="System.ArgumentOutOfRangeException">Position argument must be >= 0</exception>
		public bool MoveKeyFrameTo(KeyFrame item, int position)
		{
			if(position < 0)
				throw new ArgumentOutOfRangeException("position", "Argument must be >= 0");

			try { return MoveKeyFrameTo(KeyFrames.IndexOf(item), position); }
			catch { return false; }
		}

		/// <summary>
		/// Moves the keyframe at the specified index inside the set to a new position.
		/// </summary>
		/// <param name="index">The index of the keyframe to move.</param>
		/// <param name="position">The position to move the keyframe to.</param>
		/// <returns>A boolean indicating whether or not the operation was a success.</returns>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// Position argument must be >= 0
		/// or
		/// Index argument must be [0, this.FrameCount)
		/// </exception>
		public bool MoveKeyFrameTo(int index, int position)
		{
			if (position < 0)
				throw new ArgumentOutOfRangeException("position", "Argument must be >= 0");
			if(index < 0 || index > frameCount)
				throw new ArgumentOutOfRangeException("index", "Argument must be >= 0 and < " + frameCount);

			int insertPosition = -BinarySearch(position);

			if(insertPosition < 0)
				return false;

			if (insertPosition > index)
				insertPosition--;

			KeyFrame item = KeyFrames[index];
			item.Position = position;

			if(position < startPos)
				startPos = position;
			else if(position > endPos)
				endPos = position;

			KeyFrames.RemoveAt(index);
			KeyFrames.Insert(insertPosition, item);

			return true;
		}

		/// <summary>
		/// Moves the keyframe that is at the specified position in the timeline to a new location.
		/// </summary>
		/// <param name="oldPosition">The position of the keyframe in the timeline.</param>
		/// <param name="newPosition">The position to move the keyframe to.</param>
		/// <returns>A boolean indicating whether or not the operation was a success.</returns>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// oldPosition argument must be >= 0
		/// or
		/// newPosition argument must be >= 0
		/// </exception>
		public bool MoveKeyFrameAtTo(int oldPosition, int newPosition)
		{
			if (oldPosition < 0)
				throw new ArgumentOutOfRangeException("oldPosition", "Argument must be >= 0");

			if (newPosition < 0)
				throw new ArgumentOutOfRangeException("newPosition", "Argument must be >= 0");

			if (oldPosition < startPos || oldPosition > endPos)
				return false;
			else if (oldPosition == newPosition)
				return true; //Technically it DOES succeed in moving the keyframe by doing absolutely nothing.

			int index = BinarySearch(oldPosition);

			if (index < 0)
				return false;

			return MoveKeyFrameTo(index, newPosition);
		}

		public bool shiftFrames(int amount)
		{
			if (startPos + amount < 0)
				return false;

			startPos += amount; endPos += amount;

			foreach (KeyFrame f in KeyFrames)
				f.Position += amount;

			return true;
		}

		public IEnumerator<KeyFrame> GetEnumerator()
		{
			foreach (KeyFrame f in KeyFrames)
				yield return f;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public override string ToString ()
		{
			string x = "";
			foreach(KeyFrame f in KeyFrames)
				x += "\n" + f.Position;
			return x;
		}
	}
}