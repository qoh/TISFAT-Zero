using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Reflection;

namespace NewKeyFrames
{
	//KEYFRAME CLASSES ARE DONE, BASE CLASS NEEDS A BIT OF WORK FOR WHEN FRAMESETS AND LAYERS ARE DONE
	abstract class KeyFrame
	{

		#region Properties

		//Default attributes
		protected byte type;
		protected int position;
		public List<StickJoint> frameJoints;
		protected Color figColor;
		//public Layer parentLayer;

		//Dynamic properties
		protected Attributes Properties;

		public int Position
		{
			get { return position; }
		}

		//This is used to specify what type of stickobject it is. ie StickFigure, StickLine, etc.
		public static Type FrameType
		{
			get { return null; }
		}

		//TO-DO: MAKE SET POSITION METHOD

		#endregion Properties

		#region Constructors

		public KeyFrame(KeyFrame original)
		{
			KeyFrame New = original.createClone();

			type = New.type;
			position = New.position;
			frameJoints = New.frameJoints;
			figColor = New.figColor;
			Properties = New.Properties;
		}

		public KeyFrame(int Position)
		{
			frameJoints = (List<StickJoint>)(((Type)(this.GetType().GetProperty("FrameType").GetValue(this, null))).GetProperty("DefaultPose").GetValue(this, null));

			Properties = new Attributes();

			for (int i = 0; i < frameJoints.Count; i++)
			{
				if (frameJoints[i].parentJoint != null)
				{
					frameJoints[i].CalcLength(null);
					frameJoints[i].parentJoint.childJoints.Add(frameJoints[i]);
				}
			}

			position = Position;
		}

		public KeyFrame(){}

		#endregion Constructors

		#region Methods

		public KeyFrame createClone()
		{
			//Honestly not sure how to use all this, it was given to me in a StackOverflow question that I asked.
			var method = typeof(KeyFrame).GetMethod("copyKeyFrameStep1", BindingFlags.NonPublic | BindingFlags.Instance).MakeGenericMethod(this.GetType());
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

			//Copy all default properties
			newKeyFrame.type = type;
			newKeyFrame.position = position;
			newKeyFrame.frameJoints = StickObject.copyJoints(frameJoints);
			newKeyFrame.figColor = figColor;

			//The copyKeyFrameStep2 method is overridden by each keyframe class which has custom properties, and will make a new instance of the attributes class with values equivalent to the original.
			newKeyFrame.copyKeyFrameStep2(Properties);

			return newKeyFrame;
		}

		#endregion Methods
	}

	class StickFrame : KeyFrame
	{
		new public static Type FrameType
		{
			get { return typeof(StickFigure); }
		}

		public StickFrame(int Position) : base(Position)
		{
			type = 0;
		}

		public StickFrame() { }
	}

	class LineFrame : KeyFrame
	{
		new public static Type FrameType
		{
			get { return typeof(StickLine); }
		}

		public LineFrame(int Position) : base(Position)
		{
			type = 2;
		}

		public LineFrame() { }
	}

	class RectFrame : KeyFrame
	{
		new public static Type FrameType
		{
			get { return typeof(StickRect); }
		}

		public bool isFilled
		{
			get { return Properties["isFilled"]; }

			set { Properties["isFilled"] = value; }
		}

		public RectFrame(int Position) : base(Position)
		{
			type = 3;

			Properties.addAttribute(true, "isFilled");
		}

		public RectFrame() { }

		protected override void copyKeyFrameStep2(Attributes oldAttributes)
		{
			Properties = new Attributes();
			Properties.addAttribute(oldAttributes["isFilled"], "isFilled");
		}
	}

	class CustomFrame : KeyFrame
	{
		new public static Type FrameType
		{
			get { return typeof(StickCustom); }
		}

		public CustomFrame(int Position) : base(Position)
		{
			type = 4;
		}

		public CustomFrame() { }
	}
}
