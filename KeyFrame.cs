using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Reflection;

namespace NewKeyFrames
{
	public abstract class KeyFrame
	{
		//Default attributes
		protected byte type;
		protected int position;
		protected List<StickJoint> joints;
		protected Color figColor;

		public static string x = "hai";

		//Dynamic properties
		protected Attributes Properties;

		public int Position
		{
			get
			{
				return position;
			}
		}

		public KeyFrame(KeyFrame original)
		{
			KeyFrame New = original.copyKeyFrame();

			type = New.type;
			position = New.position;
			joints = New.joints;
			figColor = New.figColor;
			Properties = New.Properties;
		}

		public KeyFrame(int Position)
		{
		}

		public KeyFrame()
		{
		}

		public KeyFrame copyKeyFrame()
		{
			var method = typeof(KeyFrame).GetMethod("createNewInstanceStep1", BindingFlags.NonPublic | BindingFlags.Instance).MakeGenericMethod(this.GetType());
			var value = method.Invoke(this, null);
			return (KeyFrame)value;
		}

		protected virtual void copyKeyFrameStep2(Attributes oldAttributes)
		{
			//Do nothing by default. If there are special properties to be copied over, each inherited class will do that on it's own.
		}

		protected T copyKeyFrameStep1<T>() where T : KeyFrame, new()
		{
			T newKeyFrame = new T();

			newKeyFrame.type = type;
			newKeyFrame.position = position;
			newKeyFrame.joints = StickObject.copyJoints(joints);
			newKeyFrame.figColor = figColor;

			//The keyframe class only handles copying the default paramaters, then after that it will be handed off to the appropriate class
			//to handle copying everything in properties.
			
			//Put our current result in the properties under a name that isn't used anywhere else.
			newKeyFrame.copyKeyFrameStep2(Properties);
			return newKeyFrame;
		}
	}

	public class StickFrame : KeyFrame
	{
		public StickFrame(List<StickJoint> OriginalJoints, int Position)
		{
			position = Position; type = 0;

			//I can do it like this because it actually saves space in the code. For the other types however, they just don't have enough joints to make it worth the extra typing.
			int[] parentPositions = new int[] { -1, 1, 1, 2, 1, 4, 1, 6, 7, 6, 9, 0 };

			int x = 0;
			foreach (StickJoint j in OriginalJoints)
				joints.Add(new StickJoint(OriginalJoints[x++], null));

			for (int i = 0; i < joints.Count; i++)
			{
				joints[x].parent = parentPositions[x] != -1 ? joints[parentPositions[x]] : null;
				if (joints[i].parent != null)
				{
					joints[i].CalcLength(null);
					joints[i].parent.children.Add(joints[i]);
				}
			}

			if(OriginalJoints[0].ParentFigure != null)
				figColor = OriginalJoints[0].ParentFigure.figColor;
		}

		public StickFrame(int Position)
		{
			joints.Add(new StickJoint("Neck", new Point(222, 158), 12, Color.Black, Color.Blue, 0, 0, true, null, false));
			joints.Add(new StickJoint("Shoulder", new Point(222, 155), 12, Color.Black, Color.Yellow, 0, 0, false, null));
			joints[0].parent = joints[1];
			joints.Add(new StickJoint("RElbow", new Point(238, 166), 12, Color.Black, Color.Red, 0, 0, false, joints[1]));
			joints.Add(new StickJoint("RHand", new Point(246, 184), 12, Color.Black, Color.Red, 0, 0, false, joints[2]));
			joints.Add(new StickJoint("LElbow", new Point(206, 167), 12, Color.Black, Color.Blue, 0, 0, false, joints[1]));
			joints.Add(new StickJoint("LHand", new Point(199, 186), 12, Color.Black, Color.Blue, 0, 0, false, joints[4]));
			joints.Add(new StickJoint("Hip", new Point(222, 195), 12, Color.Black, Color.Yellow, 0, 0, false, joints[1]));
			joints.Add(new StickJoint("LKnee", new Point(211, 218), 12, Color.Black, Color.Blue, 0, 0, false, joints[6]));
			joints.Add(new StickJoint("LFoot", new Point(202, 241), 12, Color.Black, Color.Blue, 0, 0, false, joints[7]));
			joints.Add(new StickJoint("RKnee", new Point(234, 217), 12, Color.Black, Color.Red, 0, 0, false, joints[6]));
			joints.Add(new StickJoint("RFoot", new Point(243, 240), 12, Color.Black, Color.Red, 0, 0, false, joints[9]));
			joints.Add(new StickJoint("Head", new Point(222, 147), 13, Color.Black, Color.Yellow, 0, 1, true, joints[0]));

			for (int i = 0; i < joints.Count; i++)
			{
				if (joints[i].parent != null)
				{
					joints[i].CalcLength(null);
					joints[i].parent.children.Add(joints[i]);
				}
			}

			position = Position;
			type = 0;
		}
	}

	public class LineFrame : KeyFrame
	{
		public LineFrame(List<StickJoint> ps, int Position)
		{
			type = 2; position = Position;

			joints.Add(new StickJoint(ps[0], null));
			joints.Add(new StickJoint(ps[1], joints[0]));
		}

		public LineFrame(int po)
		{
			type = 2; position = po;
			joints.Add(new StickJoint("Rock", new Point(30, 30), 12, Color.Black, Color.Green, 0, 0, false, null));
			joints.Add(new StickJoint("Hard Place", new Point(45, 30), 12, Color.Black, Color.Yellow, 0, 0, false, joints[0]));
		}
	}

	public class RectFrame : KeyFrame
	{
		public bool filled = true;

		public RectFrame(List<StickJoint> ps, int po)
		{
			type = 3; position = po;

			joints.Add(new StickJoint(ps[0], null));
			joints.Add(new StickJoint(ps[1], joints[0]));
			joints.Add(new StickJoint(ps[2], joints[1]));
			joints.Add(new StickJoint(ps[3], joints[2]));
			joints[0].parent = joints[3];
		}

		public RectFrame(int po)
		{
			type = 3; position = po;

			joints.Add(new StickJoint("CornerTL", new Point(30, 30), 3, Color.Black, Color.LimeGreen, 0, 0, false, null));
			joints.Add(new StickJoint("CornerLL", new Point(30, 70), 3, Color.Black, Color.Yellow, 0, 0, false, joints[0]));
			joints.Add(new StickJoint("CornerLR", new Point(150, 70), 3, Color.Black, Color.Red, 0, 0, false, joints[1]));
			joints.Add(new StickJoint("CornerTR", new Point(150, 30), 3, Color.Black, Color.Blue, 0, 0, false, joints[2]));
			joints[0].parent = joints[3];
		}
	}

	public class custObjectFrame : KeyFrame
	{
		public custObjectFrame(List<StickJoint> ps, int po)
		{
			type = 4; position = po;

			joints = StickObject.copyJoints(ps);
		}

		public custObjectFrame(int po)
		{
			position = po;
			joints = new List<StickJoint>();
		}
	}
}
