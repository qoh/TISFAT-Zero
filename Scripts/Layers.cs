using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TISFAT_ZERO
{
    public abstract class Layer
    {
		//Define variables
		public uint firstKF, lastKF;
		public int selectedFrame = -1;
        public List<KeyFrame> keyFrames;
		public string name;
		public byte type;
		public StickObject fig, tweenFig;
		protected Canvas theCanvas;

		public bool removeKeyFrame(uint pos)
		{
			if (pos == firstKF || pos == lastKF)
				return false;

			foreach (KeyFrame k in keyFrames)
				if (pos == k.pos)
				{
					keyFrames.Remove(k);
					return true;
				}

			return false;
		}
		
		public void doDisplay(uint pos, bool current = true)
		{
			bool render = false; int x = -1, start = -1, end = -1;
			if (keyFrames == null)
				return;

			if (selectedFrame >= 0)
			{
				KeyFrame frm = keyFrames[selectedFrame];
				for (int a = 0; a < frm.Joints.Count; a++)
				{
					frm.Joints[a].ParentFigure = null;
				}
			}


			for (int a = 0; a < keyFrames.Count; a++)
			{
				if (pos == keyFrames[a].pos)
				{
					x = a;
					render = true;
					fig.Joints = keyFrames[a].Joints;
					if (!(tweenFig == null))
					{
						tweenFig.drawFig = false;
					}
					break;
				}
				if (pos < keyFrames[a].pos)
				{
					end = a;
					break;
				}
				else if (pos > keyFrames[a].pos)
					start = a;
			}


			if (!render)
			{
				if (pos > firstKF && pos < lastKF)
				{
					KeyFrame s = keyFrames[start], e = keyFrames[end];
					float percent = (float)(pos - s.pos) / (e.pos - s.pos);

					tweenFig.drawFig = true;

					for (int a = 0; a < s.Joints.Count; a++)
					{
						tweenFig.Joints[a].location = s.Joints[a].location;
						tweenFig.Joints[a].Tween(s.Joints[a], e.Joints[a], percent);
					}
				}
				else
				{
					tweenFig.isDrawn = false;
				}
			}

			fig.drawFig = render;
			fig.drawHandles = render & current;
			fig.isActiveFig = true;
			selectedFrame = x;

			if (selectedFrame >= 0)
			{
				KeyFrame frm = keyFrames[selectedFrame];
				for (int a = 0; a < frm.Joints.Count; a++)
				{
					frm.Joints[a].ParentFigure = fig;
				}
			}
		}

		public abstract int insertKeyFrame(uint pos);
    }

	public class StickLayer : Layer
	{
		public StickLayer(string nom, StickFigure figure, Canvas aTheCanvas)
		{
			fig = figure;

			//These are the default positions for keyframes.
			firstKF = 0;
			lastKF = 19;
			type = 1;

			keyFrames = new List<KeyFrame>();
			keyFrames.Add(new StickFrame(firstKF));
			keyFrames.Add(new StickFrame(lastKF));

            theCanvas = aTheCanvas;

            tweenFig = new StickFigure(true, true);

			name = nom; //nomnomnom
		}

		//Insert a keyframe at position pos in the timeline
		public override int insertKeyFrame(uint pos)
		{
			//If inserting before the first, then make the new keyframe the first and re-arrange list
			if (pos < firstKF)
			{
				firstKF = pos;
				StickFrame x = new StickFrame(((StickFrame)keyFrames[0]).Joints,pos);

				keyFrames.Insert(0, x);

				return 0;
			}
			else if (pos > lastKF) //Do the same if it's more than the last
			{
				lastKF = pos;

				KeyFrame x = keyFrames[keyFrames.Count - 1];
				x.pos = pos;

				keyFrames.Add(x);

				return keyFrames.Count - 1;
			}

			StickFrame n = null;
			int c = 0;

			//Look through the list for the nearest keyframe (as we want to retain all it's properties except for the position in the timeline)
			for(int a = 0; a < keyFrames.Count; a++)
			{
                StickFrame k = (StickFrame)keyFrames[a];
				if (pos < k.pos)
                {
                    n = new StickFrame(((StickFrame)keyFrames[c - 1]).Joints, pos);
                    n.pos = pos;
                    break;
				}
                else if(pos > k.pos)
                {
                    c++;
                }
				else if (pos == k.pos) // We can't insert a frame in the same spot as another!
					return -1;

			}

			keyFrames.Insert(c, n);

			return c;
		}
	}

	public class LineLayer : Layer
	{
		public LineLayer(string Name, StickLine Line, Canvas _Canvas)
		{
			name = Name;
			fig = Line;
			theCanvas = _Canvas;
			tweenFig = new StickLine(true);
			type = 2;

			firstKF = 0;
			lastKF = 19;
			type = 1;

			keyFrames = new List<KeyFrame>();
			keyFrames.Add(new LineFrame(firstKF));
			keyFrames.Add(new LineFrame(lastKF));
		}

		public override int insertKeyFrame(uint pos)
		{
			//If inserting before the first, then make the new keyframe the first and re-arrange list
			if (pos < firstKF)
			{
				firstKF = pos;
				LineFrame x = new LineFrame(keyFrames[0].Joints, pos);

				keyFrames.Insert(0, x);

				return 0;
			}
			else if (pos > lastKF) //Do the same if it's more than the last
			{
				lastKF = pos;

				LineFrame x = new LineFrame(keyFrames[keyFrames.Count - 1].Joints, pos);
				x.pos = pos;

				keyFrames.Add(x);

				return keyFrames.Count - 1;
			}

			LineFrame n = null;
			int c = 0;

			//Look through the list for the nearest keyframe (as we want to retain all it's properties except for the position in the timeline)
			for (int a = 0; a < keyFrames.Count; a++)
			{
				LineFrame k = (LineFrame)keyFrames[a];
				if (pos < k.pos)
				{
					n = new LineFrame(keyFrames[c - 1].Joints, pos);
					n.pos = pos;
					break;
				}
				else if (pos > k.pos)
					c++;
				else if (pos == k.pos)
					return -1;

			}

			keyFrames.Insert(c, n);

			return c;
		}
	}

	public class RectLayer : Layer
	{
		public RectLayer(string Name, StickRect Line, Canvas _Canvas)
		{
			name = Name;
			fig = Line;
			theCanvas = _Canvas;
			tweenFig = new StickRect(true);
			type = 2;

			firstKF = 0;
			lastKF = 19;
			type = 1;

			keyFrames = new List<KeyFrame>();
			keyFrames.Add(new RectFrame(firstKF));
			keyFrames.Add(new RectFrame(lastKF));
		}

		public override int insertKeyFrame(uint pos)
		{
			//If inserting before the first, then make the new keyframe the first and re-arrange list
			if (pos < firstKF)
			{
				firstKF = pos;
				RectFrame x = new RectFrame(keyFrames[0].Joints, pos);

				keyFrames.Insert(0, x);

				return 0;
			}
			else if (pos > lastKF) //Do the same if it's more than the last
			{
				lastKF = pos;

				RectFrame x = new RectFrame(keyFrames[keyFrames.Count - 1].Joints, pos);
				x.pos = pos;

				keyFrames.Add(x);

				return keyFrames.Count - 1;
			}

			RectFrame n = null;
			int c = 0;

			//Look through the list for the nearest keyframe (as we want to retain all it's properties except for the position in the timeline)
			for (int a = 0; a < keyFrames.Count; a++)
			{
				RectFrame k = (RectFrame)keyFrames[a];
				if (pos < k.pos)
				{
					n = new RectFrame(keyFrames[c - 1].Joints, pos);
					n.pos = pos;
					break;
				}
				else if (pos > k.pos)
					c++;
				else if (pos == k.pos)
					return -1;

			}

			keyFrames.Insert(c, n);

			return c;
		}
	}
}
