using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TISFAT_ZERO
{
	public abstract class Layer
	{
		//Define variables
		public int firstKF, lastKF;
		public int selectedFrame = -1;
		public List<KeyFrame> keyFrames;
		public string name;
		public byte type;
		public StickObject fig, tweenFig;
		protected Canvas theCanvas;

		public KeyFrame adjacentBack, adjacentFront;

		public bool removeKeyFrame(int pos)
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

		public void doDisplay(int pos, bool current = true)
		{
			bool render = false; int imid = -1;
			if (keyFrames == null)
				return;

			//Binary search for the frame that has the specified position
			//Binary searches only work on sorted lists, and since the keyframes are always sorted based on position, this works nicely.
			int end = keyFrames.Count, start = 0, npos = -1;

			if (pos > lastKF || pos < firstKF)
			{
				tweenFig.isDrawn = false;
			}
			else
			{
				while (end >= start)
				{
					imid = (end + start) >> 1; //equivilent to / 2

					try
					{
						npos = keyFrames[imid].pos;
					}
					catch
					{
						selectedFrame = -1;
						break;
					}

					if (npos < pos)
					{
						start = imid + 1;
					}
					else if (npos > pos)
						end = imid - 1;
					else
					{
						render = true;
						fig.Joints = keyFrames[imid].Joints;
						if (fig.type == 3)
						{
							fig.figColor = keyFrames[imid].figColor;
						}

						if (!(tweenFig == null))
							tweenFig.drawFig = false;

						break;
					}
				}
			}

			if (!render)
			{
				if (pos > firstKF && pos < lastKF)
				{
					KeyFrame s = keyFrames[start], e = keyFrames[end];

					adjacentBack = s; adjacentFront = e;

					float percent = (float)(pos - s.pos) / (e.pos - s.pos);
					List<StickJoint> ps = e.Joints;
					tweenFig.drawFig = true;

					for (int a = 0; a < tweenFig.Joints.Count; a++)
					{
						tweenFig.Joints[a].location = ps[a].location;
						tweenFig.Joints[a].Tween(s.Joints[a], ps[a], percent);
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
			selectedFrame = render ? imid : -1;

			if (current)
			{
				Timeline.frm_selInd = selectedFrame;
				if(fig.type == 3)
					Canvas.activeFigure.figColor = keyFrames[imid].figColor;
			}
		}

		public abstract int insertKeyFrame(int pos);
	}

	public class StickLayer : Layer
	{
		public StickLayer(string nom, StickFigure figure, Canvas aTheCanvas)
		{
			fig = figure;
			fig.parentLayer = this;

			//These are the default positions for keyframes.
			firstKF = 0;
			lastKF = 19;
			type = 1;

			keyFrames = new List<KeyFrame>();

			StickFrame first = new StickFrame(firstKF), last = new StickFrame(lastKF);

			foreach (StickJoint j in first.Joints)
				j.ParentFigure = fig;

			foreach (StickJoint j in last.Joints)
				j.ParentFigure = fig;

			keyFrames.Add(first);
			keyFrames.Add(last);

			theCanvas = aTheCanvas;

			tweenFig = new StickFigure(true, true);

			name = nom; //nomnomnom
		}

		//Insert a keyframe at position pos in the timeline
		public override int insertKeyFrame(int pos)
		{
			//If inserting before the first, then make the new keyframe the first and re-arrange list
			if (pos < firstKF)
			{
				firstKF = pos;
				StickFrame x = new StickFrame(((StickFrame)keyFrames[0]).Joints, pos);

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
			for (int a = 0; a < keyFrames.Count; a++)
			{
				StickFrame k = (StickFrame)keyFrames[a];
				if (pos < k.pos)
				{
					n = new StickFrame(((StickFrame)keyFrames[c - 1]).Joints, pos);
					n.pos = pos;
					break;
				}
				else if (pos > k.pos)
				{
					c++;
				}
				else if (pos == k.pos) // We can't insert a frame in the same spot as another!
					return -1;

			}

			foreach (StickJoint j in n.Joints)
				j.ParentFigure = fig;

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
			fig.parentLayer = this;

			theCanvas = _Canvas;
			tweenFig = new StickLine(true);
			type = 2;

			firstKF = 0;
			lastKF = 19;

			keyFrames = new List<KeyFrame>();

			LineFrame first = new LineFrame(firstKF), last = new LineFrame(lastKF);

			foreach (StickJoint j in first.Joints)
				j.ParentFigure = fig;

			foreach (StickJoint j in last.Joints)
				j.ParentFigure = fig;

			keyFrames.Add(first);
			keyFrames.Add(last);
		}

		public override int insertKeyFrame(int pos)
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

			foreach (StickJoint j in n.Joints)
				j.ParentFigure = fig;

			keyFrames.Insert(c, n);

			return c;
		}
	}

	public class RectLayer : Layer
	{
		public RectLayer(string Name, StickRect rect, Canvas _Canvas)
		{
			name = Name;

			fig = rect;
			fig.parentLayer = this;

			theCanvas = _Canvas;
			tweenFig = new StickRect(true);
			type = 3;

			firstKF = 0;
			lastKF = 19;

			keyFrames = new List<KeyFrame>();

			RectFrame first = new RectFrame(firstKF), last = new RectFrame(lastKF);

			foreach (StickJoint j in first.Joints)
				j.ParentFigure = fig;

			foreach (StickJoint j in last.Joints)
				j.ParentFigure = fig;

			keyFrames.Add(first);
			keyFrames.Add(last);
		}

		public override int insertKeyFrame(int pos)
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

			foreach (StickJoint j in n.Joints)
				j.ParentFigure = fig;

			keyFrames.Insert(c, n);

			return c;
		}
	}

	public class CustomLayer : Layer
	{
		public CustomLayer(string Name, StickCustom custom, Canvas _Canvas)
		{
			name = Name;

			fig = custom;
			fig.parentLayer = this;

			theCanvas = _Canvas;
			tweenFig = new StickCustom(true);

			type = 4;

			firstKF = 0;
			lastKF = 19;

			keyFrames = new List<KeyFrame>();

			custObjectFrame first = new custObjectFrame(firstKF), last = new custObjectFrame(lastKF);

			foreach (StickJoint j in first.Joints)
				j.ParentFigure = fig;

			foreach (StickJoint j in last.Joints)
				j.ParentFigure = fig;

			keyFrames.Add(first);
			keyFrames.Add(last);
		}

		public override int insertKeyFrame(int pos)
		{
			//If inserting before the first, then make the new keyframe the first and re-arrange list
			if (pos < firstKF)
			{
				firstKF = pos;
				custObjectFrame x = new custObjectFrame(keyFrames[0].Joints, pos);

				keyFrames.Insert(0, x);

				return 0;
			}
			else if (pos > lastKF) //Do the same if it's more than the last
			{
				lastKF = pos;

				custObjectFrame x = new custObjectFrame(keyFrames[keyFrames.Count - 1].Joints, pos);
				x.pos = pos;

				keyFrames.Add(x);

				return keyFrames.Count - 1;
			}

			custObjectFrame n = null;
			int c = 0;

			//Look through the list for the nearest keyframe (as we want to retain all it's properties except for the position in the timeline)
			for (int a = 0; a < keyFrames.Count; a++)
			{
				custObjectFrame k = (custObjectFrame)keyFrames[a];
				if (pos < k.pos)
				{
					n = new custObjectFrame(keyFrames[c - 1].Joints, pos);
					n.pos = pos;
					break;
				}
				else if (pos > k.pos)
					c++;
				else if (pos == k.pos)
					return -1;

			}

			foreach (StickJoint j in n.Joints)
				j.ParentFigure = fig;

			keyFrames.Insert(c, n);

			return c;
		}
	}

	public class PolyLayer : Layer
	{
		int jointCount;

		public PolyLayer(string Name, StickPoly custom, Canvas _Canvas, int jointCount)
		{
			this.jointCount = jointCount;

			name = Name;

			fig = custom;
			fig.parentLayer = this;

			theCanvas = _Canvas;
			tweenFig = new StickPoly(true, jointCount);

			type = 10;

			firstKF = 0;
			lastKF = 19;

			keyFrames = new List<KeyFrame>();

			PolyFrame first = new PolyFrame(firstKF, false, jointCount), last = new PolyFrame(lastKF, false, jointCount);

			foreach (StickJoint j in first.Joints)
				j.ParentFigure = fig;

			foreach (StickJoint j in last.Joints)
				j.ParentFigure = fig;

			keyFrames.Add(first);
			keyFrames.Add(last);
		}

		public override int insertKeyFrame(int pos)
		{
			//If inserting before the first, then make the new keyframe the first and re-arrange list
			if (pos < firstKF)
			{
				firstKF = pos;
				PolyFrame x = new PolyFrame(pos, true, jointCount);

				keyFrames.Insert(0, x);

				return 0;
			}
			else if (pos > lastKF) //Do the same if it's more than the last
			{
				lastKF = pos;

				PolyFrame x = new PolyFrame(keyFrames[keyFrames.Count - 1].Joints, pos);
				x.pos = pos;

				keyFrames.Add(x);

				return keyFrames.Count - 1;
			}

			PolyFrame n = null;
			int c = 0;

			//Look through the list for the nearest keyframe (as we want to retain all it's properties except for the position in the timeline)
			for (int a = 0;a < keyFrames.Count;a++)
			{
				PolyFrame k = (PolyFrame)keyFrames[a];
				if (pos < k.pos)
				{
					n = new PolyFrame(keyFrames[c - 1].Joints, pos);
					n.pos = pos;
					break;
				}
				else if (pos > k.pos)
					c++;
				else if (pos == k.pos)
					return -1;

			}

			foreach (StickJoint j in n.Joints)
				j.ParentFigure = fig;

			keyFrames.Insert(c, n);

			return c;
		}
	}

	public class LightLayer : Layer
	{
		public LightLayer(string Name, LightObject custom, Canvas _Canvas)
		{
			name = Name;

			fig = custom;
			fig.parentLayer = this;

			theCanvas = _Canvas;
			tweenFig = new LightObject(true);

			type = 5;

			firstKF = 0;
			lastKF = 19;

			keyFrames = new List<KeyFrame>();

			LightFrame first = new LightFrame(firstKF), last = new LightFrame(lastKF);

			foreach (StickJoint j in first.Joints)
				j.ParentFigure = fig;

			foreach (StickJoint j in last.Joints)
				j.ParentFigure = fig;

			keyFrames.Add(first);
			keyFrames.Add(last);
		}

		public override int insertKeyFrame(int pos)
		{
			//If inserting before the first, then make the new keyframe the first and re-arrange list
			if (pos < firstKF)
			{
				firstKF = pos;
				LightFrame x = new LightFrame(pos);

				keyFrames.Insert(0, x);

				return 0;
			}
			else if (pos > lastKF) //Do the same if it's more than the last
			{
				lastKF = pos;

				LightFrame x = new LightFrame(keyFrames[keyFrames.Count - 1].Joints, pos);
				x.pos = pos;

				keyFrames.Add(x);

				return keyFrames.Count - 1;
			}

			LightFrame n = null;
			int c = 0;

			//Look through the list for the nearest keyframe (as we want to retain all it's properties except for the position in the timeline)
			for (int a = 0;a < keyFrames.Count;a++)
			{
				LightFrame k = (LightFrame)keyFrames[a];
				if (pos < k.pos)
				{
					n = new LightFrame(keyFrames[c - 1].Joints, pos);
					n.pos = pos;
					break;
				}
				else if (pos > k.pos)
					c++;
				else if (pos == k.pos)
					return -1;

			}

			foreach (StickJoint j in n.Joints)
				j.ParentFigure = fig;

			keyFrames.Insert(c, n);

			return c;
		}
	}

	public class ImageLayer : Layer
	{
		public Bitmap img;

		public ImageLayer(string Name, StickImage rect, Canvas _Canvas, Bitmap img)
		{
			this.img = img;

			name = Name;

			fig = rect;
			fig.parentLayer = this;

			theCanvas = _Canvas;
			tweenFig = new StickImage(img, true);
			type = 7;

			firstKF = 0;
			lastKF = 19;

			keyFrames = new List<KeyFrame>();

			ImageFrame first = new ImageFrame(firstKF, img), last = new ImageFrame(lastKF, img);

			foreach (StickJoint j in first.Joints)
				j.ParentFigure = fig;

			foreach (StickJoint j in last.Joints)
				j.ParentFigure = fig;

			keyFrames.Add(first);
			keyFrames.Add(last);
		}

		public override int insertKeyFrame(int pos)
		{
			//If inserting before the first, then make the new keyframe the first and re-arrange list
			if (pos < firstKF)
			{
				firstKF = pos;
				ImageFrame x = new ImageFrame(keyFrames[0].Joints, pos, img);

				keyFrames.Insert(0, x);

				return 0;
			}
			else if (pos > lastKF) //Do the same if it's more than the last
			{
				lastKF = pos;

				ImageFrame x = new ImageFrame(keyFrames[keyFrames.Count - 1].Joints, pos, img);
				x.pos = pos;

				keyFrames.Add(x);

				return keyFrames.Count - 1;
			}

			ImageFrame n = null;
			int c = 0;

			//Look through the list for the nearest keyframe (as we want to retain all it's properties except for the position in the timeline)
			for (int a = 0;a < keyFrames.Count;a++)
			{
				ImageFrame k = (ImageFrame)keyFrames[a];
				if (pos < k.pos)
				{
					n = new ImageFrame(keyFrames[c - 1].Joints, pos, img);
					n.pos = pos;
					break;
				}
				else if (pos > k.pos)
					c++;
				else if (pos == k.pos)
					return -1;

			}

			foreach (StickJoint j in n.Joints)
				j.ParentFigure = fig;

			keyFrames.Insert(c, n);

			return c;
		}
	}
}
