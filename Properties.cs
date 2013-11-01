using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NewKeyFrames
{

	public class Attributes
	{
		private List<dynamic> attributes;
		private List<string> attributeNames;

		public Attributes()
		{
			attributes = new List<dynamic>();
			attributeNames = new List<string>();
		}

		public void addAttribute(dynamic value, string name)
		{
			if (attributeCount == 0)
			{
				attributes.Add(value);
				attributeNames.Add(name);
				return;
			}

			int bottom = 0;
			int top = attributeCount;
			int middle = top >> 1;

			//I had to make this binary search algorithm custom because I need it to store the middle index if the target is not found.
			while (top >= bottom)
			{
				int x = attributeNames[middle].CompareTo(name);

				if (x > 0)
					top = middle - 1;
				else if (x < 0)
					bottom = middle + 1;
				else
					throw new ArgumentException("Attribute already exists");

				middle = (bottom + top) >> 1;
			}

			attributes.Insert(middle, value);
			attributeNames.Insert(middle, name);
		}

		public void removeAttribute(string name)
		{
			int index = attributeNames.BinarySearch(name);

			if (index == -1)
				throw new ArgumentException("Attribute with specified name not found.");

			attributes.RemoveAt(index);
			attributeNames.RemoveAt(index);
		}

		public void removeAttribute(dynamic value)
		{
			int index = attributes.IndexOf(value);

			if (index == -1)
				throw new ArgumentException("Attribute with specified value not found.");

			attributes.RemoveAt(index);
			attributeNames.RemoveAt(index);
		}

		public int attributeCount
		{
			get
			{
				return attributes.Count;
			}
		}

		public dynamic this[string name]
		{
			get
			{
				return attributes[attributeNames.BinarySearch(name)];
			}
			set
			{
				attributes[attributeNames.BinarySearch(name)] = value;
			}
		}
	}

	public class StickJoint
	{

		#region Variables

		//Defining Public Varaibles
		public string name;

		public int state;
		/*
			* 0 = Normal
			* 1 = Locked
			* 2 = Nuffin' to see here
			* 3 = Adjust to parent
			* 4 = Adjust to parent locked
			*/

		public int drawOrder;
		public int drawState;
		
		/*
			* 0 = Line
			* 1 = Circle
			* 2 = Bitmap
			*/

		public Point location;
		public double length;
		public int thickness;

		public bool visible;
		public bool fill = false;

		public StickJoint parent;
		public StickObject ParentFigure;
		public List<StickJoint> children = new List<StickJoint>();

		public bool selected;
		public bool handleDrawn;

		//public int bitmap; //The index of the bitmap in the image array. Possibly used later?

		private Double AngleToParent;

		public Color color;
		public Color handleColor;
		public Color defaultHandleColor;

		#endregion

		#region Functions
		public StickJoint(string newname, Point newLocation, int newThickness, Color newColor, Color newHandleColor, int newState = 0, int newDrawState = 0, bool newFill = false, StickJoint newParent = null, bool newHandleDrawn = true)
		{
			name = newname;
			location = newLocation;
			thickness = newThickness;
			color = newColor;
			handleColor = newHandleColor;
			defaultHandleColor = newHandleColor;
			state = newState;
			drawState = newDrawState;
			fill = newFill;
			parent = newParent;
			handleDrawn = newHandleDrawn;
		}

		public StickJoint(StickJoint obj, StickJoint newParent)
		{
			name = obj.name;
			location = obj.location;
			thickness = obj.thickness;
			color = obj.color;
			handleColor = obj.handleColor;
			defaultHandleColor = obj.defaultHandleColor;
			state = obj.state;
			drawState = obj.drawState;
			fill = obj.fill;
			parent = newParent;
			ParentFigure = obj.ParentFigure;
			handleDrawn = obj.handleDrawn;
		}

		public double CalcLength(StickJoint start = null)
		{
			if (start == null)
				start = this;

			if (start.parent != null)
				start.length = Math.Round(Math.Sqrt(((start.parent.location.X - start.location.X) * (start.parent.location.X - start.location.X)) + ((start.parent.location.Y - start.location.Y) * (start.parent.location.Y - start.location.Y))));

			return start.length;
		}

		public StickJoint AddChild(int vx, int vy)
		{
			StickJoint pJoint;
			int xDiff, yDiff;
			double dAngle1, dAngle2;

			pJoint = new StickJoint("Child", new Point(0, 0), 10, Color.Black, Color.Blue);
			pJoint.SetPos(vx, vy);
			pJoint.parent = this;

			xDiff = pJoint.location.X - location.X;
			yDiff = pJoint.location.Y - location.Y;

			pJoint.length = Math.Round(Math.Sqrt((xDiff * xDiff) + (yDiff * yDiff)));

			pJoint.AngleToParent = 360;

			if (parent != null)
			{
				dAngle1 = 180 * (1 + Math.Atan2(yDiff, xDiff) / Math.PI);

				xDiff = location.X - parent.location.X;
				yDiff = location.Y - parent.location.Y;

				dAngle2 = 180 * (1 + Math.Atan2(yDiff, xDiff) / Math.PI);

				pJoint.AngleToParent = dAngle2 - dAngle1;
			}

			children.Add(pJoint);

			return pJoint;
		}

		public void Tween(StickJoint pStart, StickJoint pEnd, Single sPercent)
		{
		}

		public void removeChildren()
		{
			for (int i = children.Count; i > 0; i--)
			{
				children[i - 1].removeChildren();
				ParentFigure.FigureJoints.Remove(children[i - 1]);
				children.RemoveAt(i - 1);
			}
		}
		#endregion

		#region Positioning

		public void SetPosAbs(double vx, double vy)
		{
			location.X = Convert.ToInt32(vx);
			location.Y = Convert.ToInt32(vy);
		}

		public void Recalc(StickJoint pStart = null)
		{
		}

		public void SetPos(int vx, int vy)
		{
		}

		#endregion

	}

	public abstract class StickObject
	{

		#region Properties

		public List<StickJoint> FigureJoints;
		public bool isActiveFig, drawFig, drawHandles, isTweenFig, fromStickEditor;
		public Color figColor = Color.Black;

		public abstract List<StickJoint> DefaultPose
		{
			get;
		}

		public abstract int[] ParentPositions
		{
			get;
		}

		public bool isDrawn
		{
			get
			{
				return drawFig;
			}
			set
			{
				drawFig = value;
				drawHandles = value;
			}
		}
		public byte type;

		#endregion Properties

		public StickObject(bool isTweenFigure = false, bool setAsActive = true)
		{
			FigureJoints = DefaultPose;

			foreach (StickJoint j in FigureJoints)
				j.ParentFigure = this;

			for (int i = 0; i < FigureJoints.Count(); i++)
			{
				if (FigureJoints[i].parent != null)
					FigureJoints[i].CalcLength();

				FigureJoints[i].drawOrder = i;
			}

			for (int i = 0; i < FigureJoints.Count(); i++)
				if (FigureJoints[i].parent != null)
					FigureJoints[i].parent.children.Add(FigureJoints[i]);

			if (!isTweenFigure) ;
				//Canvas.addFigure(this);
			else ;
				//Canvas.addTweenFigure(this);

			isActiveFig = setAsActive;
			drawFig = setAsActive;

			isTweenFig = isTweenFigure;
		}

		#region Drawing

		public void drawFigure(bool fromCanvas = true)
		{
			if (!drawFig)
				return;

			drawJoints(fromCanvas);
		}

		public void drawFigure(int x, bool fromCanvas = true)
		{
			if (!drawFig)
				return;

			drawJoints(1, fromCanvas);
		}

		private void drawJoints(bool fromCanvas = true)
		{
		}

		private void drawJoints(int x, bool fromCanvas = true)
		{
		}

		public void drawFigHandles(bool fromCanvas = true)
		{
		}

		public void drawFigHandles(int x, bool fromCanvas = true)
		{
		}

		#endregion Drawing

		public void setAsActiveFigure()
		{
		}

		public void setColor(Color color)
		{
			for (int i = 0; i < FigureJoints.Count; i++)
				FigureJoints[i].color = color;

			if (type != 3)
				figColor = color;
		}

		public void setFillColor(Color color)
		{
			figColor = color;
		}

		public int getPointAt(Point coords, int tolerance)
		{
			if (FigureJoints.Count == 0)
				return -1;

			double minimum = short.MaxValue; //ITS OVER 9000!!!!
			int index = new int();

			for (int i = 0; i < FigureJoints.Count(); i++)
			{
				if (FigureJoints[i].handleDrawn & !fromStickEditor)
				{
					double itr_result = Math.Sqrt(Math.Pow(coords.X - FigureJoints[i].location.X, 2) + Math.Pow(coords.Y - FigureJoints[i].location.Y, 2));

					if (itr_result < minimum)
					{
						minimum = itr_result;
						index = i;
					}
				}
				else if (fromStickEditor)
				{
					double itr_result = Math.Sqrt(Math.Pow(coords.X - FigureJoints[i].location.X, 2) + Math.Pow(coords.Y - FigureJoints[i].location.Y, 2));

					if (itr_result < minimum)
					{
						minimum = itr_result;
						index = i;
					}
				}
			}

			index = (minimum < tolerance) ? index : -1;
			return index;
		}

		//Saved for when we *might* implement multiple stick figures per layer.
		public StickJoint selectPoint(Point Coords, int Tolerance)
		{
			if (!drawHandles)
				return null;

			int index = getPointAt(Coords, Tolerance);

			if (index == -1)
			{
				for (int i = 0; i < FigureJoints.Count(); i++)
					FigureJoints[i].handleColor = FigureJoints[i].defaultHandleColor;

				return null;
			}

			for (int i = 0; i < FigureJoints.Count(); i++)
				if (i != index)
					FigureJoints[i].handleColor = FigureJoints[i].defaultHandleColor;

			return FigureJoints[index];
		}

		public void onJointMoved()
		{
		}

		//Base as in it has no parent... I know the comparison is iffy at best. Just deal with it.
		public void setAsBase(StickJoint Centre)
		{
			if (Centre.parent == null)
				return;

			baseIter(Centre.parent, Centre);

			Centre.parent = null;

			for (int i = 0; i < FigureJoints.Count(); i++)
				if (FigureJoints[i].parent != null)
					FigureJoints[i].CalcLength(null);
		}

		private void baseIter(StickJoint next, StickJoint prev)
		{
			if (next.parent != null)
				baseIter(next.parent, next);

			next.children.Remove(prev);
			next.parent = prev;
			prev.children.Add(next);

			//Swap the color and draw order so they draw and color correctly. No idea if this works correctly.
			Color x = prev.color;
			prev.color = next.color;
			next.color = x;

			int y = prev.drawOrder;
			prev.drawOrder = next.drawOrder;
			next.drawOrder = y;
		}

		public void reSortJoints()
		{
			//Joints.Sort(Functions.compareDrawOrder);
		}

		public static List<StickJoint> copyJoints(List<StickJoint> original)
		{
			List<StickJoint> newList = new List<StickJoint>();

			int listCount = original.Count;

			int[] parentPositions = new int[listCount];

			for (int a = 0; a < listCount; a++)
			{
				StickJoint current = original[a];

				newList.Add(new StickJoint(current, null));

				int parentPosition = original.IndexOf(current.parent);

				if (parentPosition >= a || parentPosition == -1)
					parentPositions[a] = parentPosition;
				else if (parentPosition != -1)
					newList[a].parent = newList[parentPosition];
			}

			for (int a = 0; a < listCount; a++)
				if (parentPositions[a] != -1)
					newList[a].parent = newList[parentPositions[a]];

			for (int i = 0; i < listCount; i++)
			{
				if (newList[i].parent != null)
				{
					newList[i].CalcLength();
					newList[i].parent.children.Add(newList[i]);
				}
			}

			return newList;
		}

		public static List<StickJoint> copyJoints(List<StickJoint> original, int[] parentPositions)
		{
			List<StickJoint> newList = new List<StickJoint>();

			int listCount = original.Count;

			foreach(StickJoint j in original)
				newList.Add(new StickJoint(j, null));

			for (int a = 0; a < listCount; a++)
				if (parentPositions[a] != -1)
					newList[a].parent = newList[parentPositions[a]];

			foreach (StickJoint j in newList)
			{
				if (j != null)
				{
					j.CalcLength();
					j.parent.children.Add(j);
				}
			}

			return newList;
		}
	}

	public class StickFigure : StickObject
	{

		#region Variables

		/*	* 0 = Head
			* 1 = Neck
			* 2 = Right Elbow
			* 3 = Right Hand
			* 4 = Left Elbow
			* 5 = Left Hand
			* 6 = Hip
			* 7 = Left Knee
			* 8 = Left Foot
			* 9 = Right Knee
			* 10 = Right Foot	*/

		public override List<StickJoint> DefaultPose
		{
			get
			{
				List<StickJoint> Pose = new List<StickJoint>();
				Pose.Add(new StickJoint("Neck", new Point(222, 158), 12, Color.Black, Color.Blue, 0, 0, true, null, false));
				Pose.Add(new StickJoint("Shoulder", new Point(222, 155), 12, Color.Black, Color.Yellow, 0, 0, false, null));
				Pose[0].parent = Pose[1];
				Pose.Add(new StickJoint("RElbow", new Point(238, 166), 12, Color.Black, Color.Red, 0, 0, false, Pose[1]));
				Pose.Add(new StickJoint("RHand", new Point(246, 184), 12, Color.Black, Color.Red, 0, 0, false, Pose[2]));
				Pose.Add(new StickJoint("LElbow", new Point(206, 167), 12, Color.Black, Color.Blue, 0, 0, false, Pose[1]));
				Pose.Add(new StickJoint("LHand", new Point(199, 186), 12, Color.Black, Color.Blue, 0, 0, false, Pose[4]));
				Pose.Add(new StickJoint("Hip", new Point(222, 195), 12, Color.Black, Color.Yellow, 0, 0, false, Pose[1]));
				Pose.Add(new StickJoint("LKnee", new Point(211, 218), 12, Color.Black, Color.Blue, 0, 0, false, Pose[6]));
				Pose.Add(new StickJoint("LFoot", new Point(202, 241), 12, Color.Black, Color.Blue, 0, 0, false, Pose[7]));
				Pose.Add(new StickJoint("RKnee", new Point(234, 217), 12, Color.Black, Color.Red, 0, 0, false, Pose[6]));
				Pose.Add(new StickJoint("RFoot", new Point(243, 240), 12, Color.Black, Color.Red, 0, 0, false, Pose[9]));
				Pose.Add(new StickJoint("Head", new Point(222, 150), 13, Color.Black, Color.Yellow, 0, 1, true, Pose[0]));

				return Pose;
			}
		}

		public override int[] ParentPositions
		{
			get { return new int[] { 1, -1, 1, 2, 1, 4, 1, 6, 7, 6, 9, 0 }; }
		}

		#endregion

		//Yay for no redundant code!
		public StickFigure(bool isTweenFig = false, bool setAsActive = true) : base(isTweenFig, setAsActive)
		{
			type = 1;
		}

		#region Custom Methods

		public void flipArms()
		{
			Point rElbow = FigureJoints[2].location;
			Point rHand = FigureJoints[3].location;
			Point lElbow = FigureJoints[4].location;
			Point lHand = FigureJoints[5].location;

			FigureJoints[2].location = lElbow;
			FigureJoints[3].location = lHand;
			FigureJoints[4].location = rElbow;
			FigureJoints[5].location = rHand;
		}

		public void flipLegs()
		{
			Point lKnee = FigureJoints[7].location;
			Point lFoot = FigureJoints[8].location;
			Point rKnee = FigureJoints[9].location;
			Point rFoot = FigureJoints[10].location;

			FigureJoints[7].location = rKnee;
			FigureJoints[8].location = rFoot;
			FigureJoints[9].location = lKnee;
			FigureJoints[10].location = lFoot;
		}

		#endregion Custom Methods

	}

	public class StickLine : StickObject
	{

		#region Properties

		public override List<StickJoint> DefaultPose
		{
			get
			{
				List<StickJoint> Pose = new List<StickJoint>();
				Pose.Add(new StickJoint("Rock", new Point(30, 30), 12, Color.Black, Color.Green, 0, 0, false, null));
				Pose.Add(new StickJoint("Hard Place", new Point(45, 30), 12, Color.Black, Color.Yellow, 0, 0, false, Pose[0]));

				return Pose;
			}
		}

		public override int[] ParentPositions
		{
			get { return new int[] { -1, 0 }; }
		}

		#endregion Properties

		public StickLine(bool isTweenFig = false, bool setAsActive = true) : base(isTweenFig, setAsActive)
		{
			type = 2;
		}

		#region Custom Methods

		public void setThickness(int thickness)
		{
			FigureJoints[0].thickness = thickness;
			FigureJoints[1].thickness = thickness;
		}

		#endregion Custom Methods
	}

	public class StickRect : StickObject
	{

		#region Properties

		public bool isFilled;

		public override List<StickJoint> DefaultPose
		{
			get
			{
				List<StickJoint> Pose = new List<StickJoint>();

				Pose.Add(new StickJoint("CornerTL", new Point(30, 30), 3, Color.Black, Color.LimeGreen, 0, 0, false, null));
				Pose.Add(new StickJoint("CornerLL", new Point(30, 70), 3, Color.Black, Color.Yellow, 0, 0, false, Pose[0]));
				Pose.Add(new StickJoint("CornerLR", new Point(150, 70), 3, Color.Black, Color.Red, 0, 0, false, Pose[1]));
				Pose.Add(new StickJoint("CornerTR", new Point(150, 30), 3, Color.Black, Color.Blue, 0, 0, false, Pose[2]));

				return Pose;
			}
		}

		public override int[] ParentPositions
		{
			get { return new int[] { -1, 0, 1, 2 }; }
		}

		#endregion Properties

		public StickRect(bool isTweenFig = false, bool setAsActive = true) : base(isTweenFig, setAsActive)
		{
			type = 3;

			isFilled = true;
		}

		#region Custom Methods

		public void onRectJointMoved(StickJoint j)
		{
			if (j.name == "CornerTL")
			{
				FigureJoints[1].location.X = j.location.X;
				FigureJoints[3].location.Y = j.location.Y;
			}
			else if (j.name == "CornerLL")
			{
				FigureJoints[0].location.X = j.location.X;
				FigureJoints[2].location.Y = j.location.Y;
			}
			else if (j.name == "CornerLR")
			{
				FigureJoints[3].location.X = j.location.X;
				FigureJoints[1].location.Y = j.location.Y;
			}
			else if (j.name == "CornerTR")
			{
				FigureJoints[2].location.X = j.location.X;
				FigureJoints[0].location.Y = j.location.Y;
			}
		}

		#endregion Custom Methods
	}

	public class StickCustom : StickObject
	{

		#region Properties

		public override List<StickJoint> DefaultPose
		{
			get { return new List<StickJoint>(); }
		}

		public override int[] ParentPositions
		{
			get { return new int[0]; }
		}

		#endregion Properties

		public StickCustom(bool isTweenFig = false, bool setAsActive = true) : base(isTweenFig, setAsActive)
		{
			type = 4;
		}
	}
}