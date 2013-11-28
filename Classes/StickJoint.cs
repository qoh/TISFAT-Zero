using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Drawing;
using OpenTK;

namespace TISFAT_Zero
{
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

		public int bitmapID;

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
			if (pStart == null || pStart.childJoints.Count == 0)
				return;

			foreach (StickJoint pJoint in pStart.childJoints)
			{
				int xDiff = pStart.location.X - pJoint.location.X;
				int yDiff = pStart.location.Y - pJoint.location.Y;

				double dAngle = 180 * (1 + Math.Atan2(yDiff, xDiff) / Math.PI);
				double dRads = (Math.PI * dAngle) / 180;

				if (pStart.parentJoint != null)
				{
					xDiff = pStart.parentJoint.location.X - pStart.location.X;
					yDiff = pStart.parentJoint.location.Y - pStart.location.Y;
					pJoint.AngleToParent = (180 * (1 + Math.Atan2(yDiff, xDiff) / Math.PI)) - dAngle;
				}

				double cx = Math.Round(pJoint.length * Math.Cos(dRads));
				double cy = Math.Round(pJoint.length * Math.Sin(dRads));

				pJoint.SetPosAbs(Math.Round(pStart.location.X + cx), Math.Round(pStart.location.Y + cy));
				Recalc(pJoint);
			}
		}

		public void setPos(int vx, int vy)
		{
			int xDiff, yDiff;
			StickJoint pParent, pThis;
			double dAngle, dRads, dParentAngle, cx, cy;
			bool bContinue;

			location.X = vx;
			location.Y = vy;
			pThis = this;
			pParent = parentJoint;
			bContinue = true;
			while (pParent != null)
			{
				if ((pParent.jointState != 0) & !(pParent.isSelectedJoint))
					bContinue = false;
				if (bContinue)
				{
					xDiff = pThis.location.X - pParent.location.X;
					yDiff = pThis.location.Y - pParent.location.Y;
					dAngle = 180 * (1 + Math.Atan2(yDiff, xDiff) / Math.PI);
					dRads = (Math.PI * dAngle) / 180;
					cx = Math.Round(pThis.length * Math.Cos(dRads));
					cy = Math.Round(pThis.length * Math.Sin(dRads));
					pParent.SetPosAbs(Math.Round(pThis.location.X + cx), Math.Round(pThis.location.Y + cy));

					if (pParent.parentJoint != null)
					{
						xDiff = pParent.location.X - pParent.parentJoint.location.X;
						yDiff = pParent.location.Y - pParent.parentJoint.location.Y;
						dParentAngle = 180 * (1 + Math.Atan2(yDiff, xDiff) / Math.PI);
						pThis.AngleToParent = 360 - (dAngle - dParentAngle);
					}
				}

				pThis = pParent;
				pParent = pParent.parentJoint;
			}

			Recalc(pThis);

			//Dunno if we even need this
			//this.parentFigure.onJointMoved();
		}

		#endregion

	}
}
