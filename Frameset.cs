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

		public Frameset(Type SetType, int StartingPosition = 0, int Extent = 20)
		{
			ConstructorInfo KFConstructor = SetType.GetConstructor(new Type[] { typeof(int)});

			startPos = StartingPosition;
			endPos = startPos + Extent;

			KeyFrames.Add((KeyFrame)KFConstructor.Invoke(new object[] { startPos }));
			KeyFrames.Add((KeyFrame)KFConstructor.Invoke(new object[] { endPos }));
		}

		public Frameset(KeyFrame First, KeyFrame Last)
		{
			KeyFrames.AddRange(new KeyFrame[] { First, Last });
		}

		public Frameset(KeyFrame Base, int StartingPosition = 0, int Extent = 20)
		{
			KeyFrame start = Base.createClone();
			start.Position = StartingPosition;

			KeyFrame end = Base.createClone();
			end.Position = StartingPosition + Extent;

			KeyFrames.AddRange(new KeyFrame[] { start, end });
		}

		public KeyFrame this[int index]
		{
			get { return KeyFrames[index]; }
			
			set { KeyFrames[index] = value; }
		}

		public bool insertKeyFrame(KeyFrame Item, bool copyBeforeInsert = false)
		{
			if(Item.Position < 0)
				throw new ArgumentException("Item's Position must be >= 0", "Item");

			int insertPosition = -BinarySearch(Item.Position) - 1;

			if(copyBeforeInsert)
				Item = Item.createClone();

			//If the variable is below 0 that means that there's a keyframe with that position already in the frameset. 
			if(insertPosition < 0)
				return false;

			KeyFrames.Insert(insertPosition, Item);

			int Position = Item.Position;

			if(Position < startPos)
				startPos = Position;
			else if(Position > endPos)
				endPos = Position;

			return true;
		}

		public bool insertKeyFrameAt(KeyFrame Item, int Position, bool copyBeforeInsert = false)
		{
			if(Position < 0)
				throw new ArgumentException("Parameter must be >= 0", "Position");

			if(copyBeforeInsert)
				Item = Item.createClone();

			Item.Position = Position;

			return insertKeyFrame(Item, false);
		}

		//Attempts to remove the keyframe from the set that has the specified position.

		public bool removeKeyFrameAt(int Position)
		{
			int Index = BinarySearch(Position);

			if(Index >= 0)
			{
				KeyFrames.RemoveAt(Index);
				return true;
			}

			return false;
		}

		public bool removeKeyFrame(int Index)
		{
			if(Index < startPos)
			{
				if(Index < 0)
					throw new ArgumentOutOfRangeException("Index", "Argument must be >= 0");

				return false;
			}
			else if(Index > endPos)
				return false;

			KeyFrames.RemoveAt(Index);

			return true;
		}

		public bool removeKeyFrame(KeyFrame Item)
		{
			int Index = KeyFrames.IndexOf(Item);

			if(Index < 0)
				return false;

			KeyFrames.RemoveAt(Index);
			return true;
		}

		//Returns a negative value if not found, and positive if found.
		private int BinarySearch(int Position)
		{
			int bottom = 0;
			int top = frameCount;
			int middle = top >> 1;

			//I had to make this binary search algorithm custom because I need it to store the middle index if the target is not found.
			while (top >= bottom)
			{
				int x = KeyFrames[middle].Position;

				if (x > Position)
					top = middle - 1;
				else if (x < Position)
					bottom = middle + 1;
				else
					return middle;

				middle = (bottom + top) >> 1;
			}

			return -middle - 1;
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