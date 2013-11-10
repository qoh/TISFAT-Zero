using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NewKeyFrames
{

	class Attributes
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
			get { return attributes.Count; }
		}

		public dynamic this[string name]
		{
			get { return attributes[attributeNames.BinarySearch(name)]; }

			set { attributes[attributeNames.BinarySearch(name)] = value; }
		}
	}

	//STICKJOINT CLASS IS DONE
	class StickJoint
	{

		#region Variables

		public string jointName;

		public int jointState; // 0 = Normal, 1 = Locked, 2 = Adjust to parent, 3 = Adjust to parent locked

		public int drawOrder;
		public int drawType; // 0 = Line, 1 = Circle, 2 = Bitmap
		
		public Point location;
		public double length;
		public int thickness;

		public bool isVisible;

		public StickJoint parentJoint;
		public StickObject parentFigure;
		public List<StickJoint> childJoints = new List<StickJoint>();

		public bool isSelectedJoint;
		public bool handleDrawn;

		public uint bitmapID;

		private Double AngleToParent; //Used in IK posing, I'm just going to leave this here.

		public Color jointColor;
		public Color handleColor;
		public Color defaultHandleColor;

		#endregion

		#region Constructors

		public StickJoint(string newname, Point newLocation, int newThickness, Color newColor, Color newHandleColor, int newState = 0, int newDrawState = 0, StickJoint newParent = null, bool newHandleDrawn = true)
		{
			jointName = newname;
			location = newLocation;
			thickness = newThickness;
			jointColor = newColor;
			handleColor = newHandleColor;
			defaultHandleColor = newHandleColor;
			jointState = newState;
			drawType = newDrawState;
			parentJoint = newParent;
			handleDrawn = newHandleDrawn;
		}

		public StickJoint(StickJoint obj, StickJoint newParent = null)
		{
			jointName = obj.jointName;
			location = obj.location;
			thickness = obj.thickness;
			jointColor = obj.jointColor;
			handleColor = obj.handleColor;
			defaultHandleColor = obj.defaultHandleColor;
			jointState = obj.jointState;
			drawType = obj.drawType;
			parentJoint = newParent;
			parentFigure = obj.parentFigure;
			handleDrawn = obj.handleDrawn;
		}

		#endregion Constructors

		#region Methods

		public double CalcLength(StickJoint start = null)
		{
			if (start == null)
				start = this;

			if (start.parentJoint != null)
				start.length = Math.Round(Math.Sqrt(((start.parentJoint.location.X - start.location.X) * (start.parentJoint.location.X - start.location.X)) + ((start.parentJoint.location.Y - start.location.Y) * (start.parentJoint.location.Y - start.location.Y))));

			return start.length;
		}

		public void Tween(StickJoint pStart, StickJoint pEnd, Single sPercent)
		{
			location.X = (int)Math.Round(pStart.location.X + ((pEnd.location.X - pStart.location.X) * sPercent));
			location.Y = (int)Math.Round(pStart.location.Y + ((pEnd.location.Y - pStart.location.Y) * sPercent));

			length = (int)Math.Round(pStart.length + ((pEnd.length - pStart.length) * sPercent));
			thickness = (int)Math.Round(pStart.thickness + ((pEnd.thickness - pStart.thickness) * sPercent));

			if (pStart.parentFigure.figureType == 3 && ((StickRect)parentFigure).isFilled)
			{
				/*Color tmp0 = pStart.parentFigure.parentLayer.adjacentBack.figColor;
				Color tmp1 = pEnd.parentFigure.parentLayer.adjacentFront.figColor;

				int r2 = tmp0.R + (int)Math.Round(sPercent * (tmp1.R - tmp0.R));
				int g2 = tmp0.G + (int)Math.Round(sPercent * (tmp1.G - tmp0.G));
				int b2 = tmp0.B + (int)Math.Round(sPercent * (tmp1.B - tmp0.B));
				int a2 = tmp0.A + (int)Math.Round(sPercent * (tmp1.A - tmp0.A));

				parentFigure.figColor = Color.FromArgb(a2, r2, g2, b2);*/
			}

			Color tmp2 = pStart.jointColor, tmp3 = pEnd.jointColor;

			int r = tmp2.R + (int)Math.Round(sPercent * (tmp3.R - tmp2.R));
			int g = tmp2.G + (int)Math.Round(sPercent * (tmp3.G - tmp2.G));
			int b = tmp2.B + (int)Math.Round(sPercent * (tmp3.B - tmp2.B));
			int a = tmp2.A + (int)Math.Round(sPercent * (tmp3.A - tmp2.A));

			jointColor = Color.FromArgb(a, r, g, b);
		}

		public void removeChildren()
		{
			for (int i = childJoints.Count; i > 0; i--)
			{
				childJoints[i - 1].removeChildren();
				parentFigure.FigureJoints.Remove(childJoints[i - 1]);
				childJoints.RemoveAt(i - 1);
			}
		}

		#endregion Methods

		#region Positioning

		public void SetPosAbs(double vx, double vy)
		{
			location.X = Convert.ToInt32(vx);
			location.Y = Convert.ToInt32(vy);
		}

		private void Recalc(StickJoint pStart = null)
		{
			//TO-DO: Copy in recalc method
		}

		public void setPos(int vx, int vy)
		{
			//TO-DO: Copy in setpos method
		}

		#endregion

	}

	abstract class StickObject : IEnumerable<StickJoint>
	{

		#region Properties

		public List<StickJoint> FigureJoints;
		public bool isActiveFig, drawFig, drawHandles;
		public Color figColor = Color.Black;
		public byte figureType; //This is used to identify what kind of figure it is. For example, 3 is a line, and 1 is a stickman.
		//public Layer parentLayer;

		public static List<StickJoint> DefaultPose
		{
			get { return new List<StickJoint>(); }
		}

		public static int[] ParentPositions
		{
			get { return new int[0]; }
		}

		public bool isDrawn
		{
			get { return drawFig; }

			set
			{
				drawFig = value;
				drawHandles = value;
			}
		}

		#endregion Properties

		public StickObject(bool setAsActive = true)
		{
			FigureJoints = DefaultPose;

			foreach (StickJoint j in FigureJoints)
				j.parentFigure = this;

			for (int i = 0; i < FigureJoints.Count(); i++)
			{
				if (FigureJoints[i].parentJoint != null)
					FigureJoints[i].CalcLength();

				FigureJoints[i].drawOrder = i;
			}

			for (int i = 0; i < FigureJoints.Count(); i++)
				if (FigureJoints[i].parentJoint != null)
					FigureJoints[i].parentJoint.childJoints.Add(FigureJoints[i]);

			isActiveFig = setAsActive;
			isDrawn = setAsActive;
		}

		#region Drawing

		public void drawFigure(IDrawable Canvas)
		{
			if (!drawFig)
				return;

			drawJoints(Canvas);
		}

		private void drawJoints(IDrawable Canvas)
		{
			bool useStencil = false;

			//there's probably a better way instead of looping to determine if any parts have transparency...
			if (figureType != 3)
			{
				foreach (StickJoint j in FigureJoints)
				{
					if (j.parentJoint != null && j.jointColor.A != 255)
					{
						useStencil = true;
						break;
					}
				}
			}

			//GL.Disable(EnableCap.StencilTest);

			if (useStencil)
			{
				/*GL.Clear(ClearBufferMask.StencilBufferBit);
				GL.Enable(EnableCap.StencilTest);
				GL.StencilMask(0xFFFFFF);
				GL.StencilFunc(StencilFunction.Equal, 0, 0xFFFFFF);
				GL.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Incr);*/
			}

			//If the figure is of the rectangle type and it's filled, then draw in the fill. (Thank goodness for short-circuiting!)
			if (figureType == 3 && ((StickRect)this).isFilled)
				Canvas.drawGraphics(5, figColor, FigureJoints[0].location, FigureJoints[0].thickness, FigureJoints[0].thickness, FigureJoints[2].location);

			foreach (StickJoint i in FigureJoints)
			{
				if (i.parentJoint != null)
				{
					Canvas.drawGraphics(i.drawType, i.jointColor, new Point(i.location.X, i.location.Y), i.thickness, i.thickness, new Point(i.parentJoint.location.X, i.parentJoint.location.Y));
				}
				else if (i.drawType != 0) //The only case we should draw when the joint doesn't have a parent is if it's draw type is NOT a standard line.
				{
					Canvas.drawGraphics(i.drawType, i.jointColor, new Point(i.location.X, i.location.Y), i.thickness, i.thickness, new Point(i.location.X, i.location.Y));
				}
			}

			//GL.Disable(EnableCap.StencilTest);
		}

		public void drawFigHandles(IDrawable Canvas)
		{
			if (!drawHandles)
				return;

			foreach (StickJoint j in FigureJoints)
			{
				if (!isActiveFig)
				{
					Canvas.drawGraphics(2, Color.DimGray, new Point(j.location.X, j.location.Y), 4, 4, new Point(0, 0));
					continue;
				}

				if (j.handleDrawn & isActiveFig)
					Canvas.drawGraphics(2, j.handleColor, new Point(j.location.X, j.location.Y), 4, 4, new Point(0, 0));

				if (j.jointState == 1 | j.jointState == 3 | j.jointState == 4)
					Canvas.drawGraphics(3, Color.WhiteSmoke, new Point(j.location.X - 1, j.location.Y - 1), 6, 6, new Point(0, 0));
			}
		}

		#endregion Drawing

		#region Methods

		public void setJointsColor(Color NewColor)
		{
			foreach(StickJoint j in FigureJoints)
				j.jointColor = NewColor;

			if (figureType != 3)
				figColor = NewColor;
		}

		public int getPointAt(Point Coords, int Tolerance)
		{
			if (FigureJoints.Count == 0)
				return -1;

			double minimum = short.MaxValue; //ITS OVER 9000!!!!
			int index = -1;

			for (int i = 0; i < FigureJoints.Count(); i++)
			{
				if (FigureJoints[i].handleDrawn)
				{
					double itr_result = Math.Sqrt(Math.Pow(Coords.X - FigureJoints[i].location.X, 2) + Math.Pow(Coords.Y - FigureJoints[i].location.Y, 2));

					if (itr_result < minimum && itr_result <= Tolerance)
					{
						minimum = itr_result;
						index = i;
					}
				}
			}

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
			//TO-DO: COPY IN ONJOINTMOVED CODE
		}

		//Base as in it has no parent... I know the comparison is iffy at best. Just deal with it.
		public void setAsBase(StickJoint Centre)
		{
			if (Centre.parentJoint == null)
				return;

			baseIter(Centre.parentJoint, Centre);

			Centre.parentJoint = null;

			for (int i = 0; i < FigureJoints.Count(); i++)
				if (FigureJoints[i].parentJoint != null)
					FigureJoints[i].CalcLength(null);
		}

		private void baseIter(StickJoint next, StickJoint prev)
		{
			if (next.parentJoint != null)
				baseIter(next.parentJoint, next);

			next.childJoints.Remove(prev);
			next.parentJoint = prev;
			prev.childJoints.Add(next);

			//Swap the color and draw order so they draw and color correctly. No idea if this works correctly.
			Color x = prev.jointColor;
			prev.jointColor = next.jointColor;
			next.jointColor = x;

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

				newList.Add(new StickJoint(current));
				parentPositions[a] = original.IndexOf(current.parentJoint);
			}

			for (int a = 0; a < listCount; a++)
				if (parentPositions[a] != -1)
					newList[a].parentJoint = newList[parentPositions[a]];

			for (int i = 0; i < listCount; i++)
			{
				if (newList[i].parentJoint != null)
				{
					newList[i].CalcLength();
					newList[i].parentJoint.childJoints.Add(newList[i]);
				}
			}

			return newList;
		}

		public static List<StickJoint> copyJoints(List<StickJoint> original, int[] parentPositions)
		{
			List<StickJoint> newList = new List<StickJoint>();

			int listCount = original.Count;

			foreach(StickJoint j in original)
				newList.Add(new StickJoint(j));

			for (int a = 0; a < listCount; a++)
				if (parentPositions[a] != -1)
					newList[a].parentJoint = newList[parentPositions[a]];

			foreach (StickJoint j in newList)
			{
				if (j != null)
				{
					j.CalcLength();
					j.parentJoint.childJoints.Add(j);
				}
			}

			return newList;
		}

		public List<StickJoint> copyJoints()
		{
			return StickObject.copyJoints(FigureJoints);
		}

		public IEnumerator<StickJoint> GetEnumerator()
		{
			foreach (StickJoint j in FigureJoints)
				yield return j;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion Methods
	}

	class StickFigure : StickObject
	{

		#region Variables

		new public static List<StickJoint> DefaultPose
		{
			get
			{
				List<StickJoint> Pose = new List<StickJoint>();
				Pose.Add(new StickJoint("Neck", new Point(222, 158), 12, Color.Black, Color.Blue, 0, 0, null, false));
				Pose.Add(new StickJoint("Shoulder", new Point(222, 155), 12, Color.Black, Color.Yellow, 0, 0, null));
				Pose[0].parentJoint = Pose[1];
				Pose.Add(new StickJoint("RElbow", new Point(238, 166), 12, Color.Black, Color.Red, 0, 0, Pose[1]));
				Pose.Add(new StickJoint("RHand", new Point(246, 184), 12, Color.Black, Color.Red, 0, 0, Pose[2]));
				Pose.Add(new StickJoint("LElbow", new Point(206, 167), 12, Color.Black, Color.Blue, 0, 0, Pose[1]));
				Pose.Add(new StickJoint("LHand", new Point(199, 186), 12, Color.Black, Color.Blue, 0, 0, Pose[4]));
				Pose.Add(new StickJoint("Hip", new Point(222, 195), 12, Color.Black, Color.Yellow, 0, 0, Pose[1]));
				Pose.Add(new StickJoint("LKnee", new Point(211, 218), 12, Color.Black, Color.Blue, 0, 0, Pose[6]));
				Pose.Add(new StickJoint("LFoot", new Point(202, 241), 12, Color.Black, Color.Blue, 0, 0, Pose[7]));
				Pose.Add(new StickJoint("RKnee", new Point(234, 217), 12, Color.Black, Color.Red, 0, 0, Pose[6]));
				Pose.Add(new StickJoint("RFoot", new Point(243, 240), 12, Color.Black, Color.Red, 0, 0, Pose[9]));
				Pose.Add(new StickJoint("Head", new Point(222, 150), 13, Color.Black, Color.Yellow, 0, 1, Pose[0]));

				return Pose;
			}
		}

		new public static int[] ParentPositions
		{
			get { return new int[] { 1, -1, 1, 2, 1, 4, 1, 6, 7, 6, 9, 0 }; }
		}

		#endregion

		public StickFigure( bool setAsActive = true) : base(setAsActive)
		{
			figureType = 1;
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

	class StickLine : StickObject
	{

		#region Properties

		new public static List<StickJoint> DefaultPose
		{
			get
			{
				List<StickJoint> Pose = new List<StickJoint>();
				Pose.Add(new StickJoint("Rock", new Point(30, 30), 12, Color.Black, Color.Green, 0, 0, null));
				Pose.Add(new StickJoint("Hard Place", new Point(45, 30), 12, Color.Black, Color.Yellow, 0, 0, Pose[0]));

				return Pose;
			}
		}

		new public static int[] ParentPositions
		{
			get { return new int[] { -1, 0 }; }
		}

		#endregion Properties

		public StickLine(bool setAsActive = true) : base(setAsActive)
		{
			figureType = 2;
		}

		#region Custom Methods

		public void setThickness(int thickness)
		{
			FigureJoints[0].thickness = thickness;
			FigureJoints[1].thickness = thickness;
		}

		#endregion Custom Methods
	}

	class StickRect : StickObject
	{

		#region Properties

		public bool isFilled;

		new public static List<StickJoint> DefaultPose
		{
			get
			{
				List<StickJoint> Pose = new List<StickJoint>();
				Pose.Add(new StickJoint("CornerTL", new Point(30, 30), 3, Color.Black, Color.LimeGreen, 0, 0, null));
				Pose.Add(new StickJoint("CornerLL", new Point(30, 70), 3, Color.Black, Color.Yellow, 0, 0, Pose[0]));
				Pose.Add(new StickJoint("CornerLR", new Point(150, 70), 3, Color.Black, Color.Red, 0, 0, Pose[1]));
				Pose.Add(new StickJoint("CornerTR", new Point(150, 30), 3, Color.Black, Color.Blue, 0, 0, Pose[2]));

				return Pose;
			}
		}

		new public static int[] ParentPositions
		{
			get { return new int[] { -1, 0, 1, 2 }; }
		}

		#endregion Properties

		public StickRect(bool setAsActive = true) : base(setAsActive)
		{
			figureType = 3;

			isFilled = true;
		}

		#region Custom Methods

		public void onRectJointMoved(StickJoint j)
		{
			if (j.jointName == "CornerTL")
			{
				FigureJoints[1].location.X = j.location.X;
				FigureJoints[3].location.Y = j.location.Y;
			}
			else if (j.jointName == "CornerLL")
			{
				FigureJoints[0].location.X = j.location.X;
				FigureJoints[2].location.Y = j.location.Y;
			}
			else if (j.jointName == "CornerLR")
			{
				FigureJoints[3].location.X = j.location.X;
				FigureJoints[1].location.Y = j.location.Y;
			}
			else if (j.jointName == "CornerTR")
			{
				FigureJoints[2].location.X = j.location.X;
				FigureJoints[0].location.Y = j.location.Y;
			}
		}

		#endregion Custom Methods
	}

	class StickCustom : StickObject
	{
		public StickCustom(bool setAsActive = true) : base(setAsActive)
		{
			figureType = 4;
		}
	}
}