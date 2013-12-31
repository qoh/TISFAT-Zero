using System;
using System.Collections.Generic;
using System.Drawing;

namespace TISFAT_Zero
{
	internal class StickJoint
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

		public int bitmapID;

		private Double AngleToParent; //Used in IK posing, I'm just going to leave this here.

		public Color jointColor;
		public Color handleColor;
		public Color defaultHandleColor;

		#endregion Variables

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

			if (pEnd.parentFigure.figureType == 3 && ((StickRect)parentFigure).isFilled)
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

		public void setPos(int x, int y)
		{
			StickJoint thisJoint = this, parent = parentJoint;

			thisJoint.location = new Point(x, y);

			for (; parent != null; thisJoint = parent, parent = thisJoint.parentJoint)
			{
				if (parent.jointState != 0)
				{
					adjustTo(parent, thisJoint, false);
					break;
				}

				adjustTo(thisJoint, parent, false);
			}

			adjustThroughChildren(thisJoint);
		}

		public void adjustThroughChildren(StickJoint pStart = null)
		{
			if (pStart == null)
				pStart = this;

			foreach (StickJoint pJoint in pStart.childJoints)
			{
				adjustTo(pStart, pJoint, true);
				adjustThroughChildren(pJoint);
			}
		}

		public void adjustTo(StickJoint start, StickJoint end, bool endischild, bool adjustend = true)
		{
			int odx = end.location.X - start.location.X, ody = end.location.Y - start.location.Y;
			double angle = Math.Atan2(ody, odx);
			double len = endischild ? end.length : start.length;

			double ndy = len * Math.Sin(angle);

			double ndx = len * Math.Cos(angle);

			if (adjustend)
				end.location = new Point((int)Math.Round(start.location.X + ndx), (int)Math.Round(start.location.Y + ndy));
			else
				start.location = new Point((int)Math.Round(end.location.X + ndx), (int)Math.Round(end.location.Y + ndy));
		}

		#endregion Positioning
	}
}