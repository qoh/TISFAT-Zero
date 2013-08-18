using System;
using System.Collections.Generic;
using System.Text;

namespace TISFAT_ZERO
{
    public abstract class Layer
    {
		//Define variables
        public uint firstKF, lastKF;
        public List<KeyFrame> keyFrames;
		public string name;

		//Insert a keyframe at position pos in the timeline
		public bool insertKeyFrame(uint pos)
		{
			//If inserting before the first, then make the new keyframe the first and re-arrange list
			if (pos < firstKF)
			{
				firstKF = pos;
				KeyFrame x = keyFrames[0];
				x.pos = keyFrames[0].pos - 1;
				
				keyFrames.Insert(0, x);

				return true;
			}
			else if (pos > lastKF) //Do the same if it's more than the last
			{
				lastKF = pos;

				KeyFrame x = keyFrames[keyFrames.Count - 1];
				x.pos = keyFrames[keyFrames.Count - 1].pos + 1;

				keyFrames.Add(x);

				return true;
			}

			KeyFrame n = null;
			int c = 0;

			//Look through the list for the nearest keyframe (as we want to retain all it's properties except for the position in the timeline)
			foreach (KeyFrame k in keyFrames)
			{
				if (pos < k.pos)
					n = keyFrames[c - 1];
				else if (pos == k.pos) // We can't insert a frame in the same spot as another!
					return false;

				c++;
			}

			keyFrames.Insert(c - 1, n);

			return true;
		}

		public bool removeKeyFrame(uint pos)
		{
			foreach (KeyFrame k in keyFrames)
				if (pos == k.pos)
				{
					keyFrames.Remove(k);
					return true;
				}

			return false;
		}
    }

	public class StickLayer : Layer
	{
		private StickFigure fig;

		public StickLayer(string nom)
		{
			firstKF = 0;
			lastKF = 4;
			keyFrames = new List<KeyFrame>();
			keyFrames.Add(new StickFrame(new StickFigure(false).Joints, 0));
			keyFrames.Add(new StickFrame(new StickFigure(false).Joints, 4));

			name = nom; //nomnomnom
		}
	}
}
