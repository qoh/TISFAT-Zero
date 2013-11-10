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

		public Frameset(Type SetType, int startingPosition = 0, int extent = 20)
		{
			if (SetType.BaseType != typeof(KeyFrame))
				throw new ArgumentException("SetType must be a derived type of KeyFrame", "SetType");

			ConstructorInfo KFConstructor = SetType.GetConstructor(new Type[] { typeof(int)});

			startPos = startingPosition;
			endPos = startPos + extent;

			KeyFrames.Add((KeyFrame)KFConstructor.Invoke(new object[] { startPos }));
			KeyFrames.Add((KeyFrame)KFConstructor.Invoke(new object[] { endPos }));
		}

		public Frameset(KeyFrame First, KeyFrame Last)
		{
			KeyFrames.AddRange(new KeyFrame[] { First, Last });
		}

		public Frameset(KeyFrame Base, int startingPosition = 0, int extent = 20)
		{
			KeyFrame start = Base.createClone();
			start.Position = startingPosition;

			KeyFrame end = Base.createClone();
			end.Position = startingPosition + extent;

			KeyFrames.AddRange(new KeyFrame[] { start, end });
		}

		public KeyFrame this[int index]
		{
			get { return KeyFrames[index]; }
			
			set { KeyFrames[index] = value; }
		}

		public bool InsertKeyFrame(KeyFrame item, bool copyBeforeInsert = false)
		{
			if(item.Position < 0)
				throw new ArgumentException("Item's Position must be >= 0", "Item");

			int insertPosition = -BinarySearch(item.Position) - 1;

			if(copyBeforeInsert)
				item = item.createClone();

			//If the variable is below 0 that means that there's a keyframe with that position already in the frameset. 
			if(insertPosition < 0)
				return false;

			KeyFrames.Insert(insertPosition, item);

			int Position = item.Position;

			if(Position < startPos)
				startPos = Position;
			else if(Position > endPos)
				endPos = Position;

			return true;
		}

		public bool InsertKeyFrameAt(KeyFrame item, int position, bool copyBeforeInsert = false)
		{
			if (position < 0)
				throw new ArgumentOutOfRangeException("position", "Argument must be >= 0");

			if(copyBeforeInsert)
				item = item.createClone();

			item.Position = position;

			return InsertKeyFrame(item, false);
		}

		//Attempts to remove the keyframe from the set that has the specified position.
		public bool RemoveKeyFrameAt(int position)
		{
			if(position < 0)
				throw new ArgumentOutOfRangeException("position", "Argument must be >= 0");

			int Index = BinarySearch(position);

			if(Index >= 0)
			{
				KeyFrames.RemoveAt(Index);
				return true;
			}

			return false;
		}

		public bool RemoveKeyFrame(int index)
		{
			if(index < startPos)
			{
				if(index < 0)
					throw new ArgumentOutOfRangeException("Index", "Argument must be >= 0");

				return false;
			}
			else if(index > endPos)
				return false;

			KeyFrames.RemoveAt(index);
			return true;
		}

		public bool RemoveKeyFrame(KeyFrame item)
		{
			try
			{
				return RemoveKeyFrame(KeyFrames.IndexOf(item));
			}
			catch
			{
				return false;
			}
		}

		//Returns a negative value if not found, and positive if found.
		private int BinarySearch(int position)
		{
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

		public bool MoveKeyFrameTo(KeyFrame item, int position)
		{
			if(position < 0)
				throw new ArgumentOutOfRangeException("position", "Argument must be >= 0");

			try
			{
				return MoveKeyFrameTo(KeyFrames.IndexOf(item), position);
			}
			catch
			{
				return false;
			}
		}

		public bool MoveKeyFrameTo(int index, int position)
		{
			if (position < 0)
				throw new ArgumentOutOfRangeException("position", "Argument must be >= 0");
			if(index < 0 || index > frameCount)
				throw new ArgumentOutOfRangeException("index", "Argument must be >= 0 and < " + frameCount);

			int insertPosition = -BinarySearch(position) - 1;

			if(insertPosition < 0)
				return false;

			if (insertPosition > index)
				insertPosition--;

			KeyFrame item = KeyFrames[index];

			KeyFrames.RemoveAt(index);
			KeyFrames.Insert(insertPosition, item);

			return true;
		}

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

		public IEnumerator<KeyFrame> GetEnumerator()
		{
			foreach (KeyFrame f in KeyFrames)
				yield return f;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}