using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TISFAT_ZERO
{
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
        public StickFigure ParentFigure;
        public List<StickJoint> children = new List<StickJoint>();

        public bool selected;
        public bool handleDrawn;

        public int bitmap; //The index of the bitmap in the image array. Possibly used later?
        public Double AngleToParent;

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

        public double CalcLength(StickJoint start)
        {
            if (start == null)
            {
                start = this;
            }

            if (!(start.parent == null))
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
                pJoint.AngleToParent = (dAngle2 - dAngle1);
            }

            children.Add(pJoint);

            return pJoint;
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
            if (pStart == null)
                return;

            int xDiff, yDiff, f;
            double dAngle, dRads, cx, cy;
            StickJoint pJoint;

            for (f = 0; f < pStart.children.Count(); f++)
            {
                pJoint = pStart.children[f];

                xDiff = pStart.location.X - pJoint.location.X;
                yDiff = pStart.location.Y - pJoint.location.Y;
                dAngle = 180 * (1 + Math.Atan2(yDiff, xDiff) / Math.PI);

                if ((pJoint.state == 3) | (pJoint.state == 4))
                {
                    if (!(pStart.parent == null))
                    {
                        xDiff = pStart.parent.location.X - pStart.location.X;
                        yDiff = pStart.parent.location.Y - pStart.location.Y;
                        dAngle = 180 * (1 + Math.Atan2(yDiff, xDiff) / Math.PI);
                        dAngle = (dAngle - pJoint.AngleToParent);
                    }
                }
                else
                {
                    if (!(pStart.parent == null))
                    {
                        xDiff = pStart.parent.location.X - pStart.location.X;
                        yDiff = pStart.parent.location.Y - pStart.location.Y;
                        pJoint.AngleToParent = (180 * (1 + Math.Atan2(yDiff, xDiff) / Math.PI)) - dAngle;
                    }
                }
                dRads = Functions.DegToRads(dAngle);
                cx = Math.Round(pJoint.length * Math.Cos(dRads));
                cy = Math.Round(pJoint.length * Math.Sin(dRads));
                pJoint.SetPosAbs(Math.Round(pStart.location.X + cx), Math.Round(pStart.location.Y + cy));
                Recalc(pJoint);
			}
			this.ParentFigure.onJointMoved();
        }

        public void SetPos(int vx, int vy)
        {
            int xDiff, yDiff;
            StickJoint pParent, pThis;
            double dAngle, dRads, dParentAngle, cx, cy;
            bool bContinue;

            location.X = vx;
            location.Y = vy;
            pThis = this;
            pParent = parent;
            bContinue = true;
            while (pParent != null)
            {
                if ((pParent.state != 0) & !(pParent.selected))
                    bContinue = false;
                if (bContinue)
                {
                    xDiff = pThis.location.X - pParent.location.X;
                    yDiff = pThis.location.Y - pParent.location.Y;
                    dAngle = 180 * (1 + Math.Atan2(yDiff, xDiff) / Math.PI);
                    dRads = Functions.DegToRads(dAngle);
                    cx = Math.Round(pThis.length * Math.Cos(dRads));
                    cy = Math.Round(pThis.length * Math.Sin(dRads));
                    pParent.SetPosAbs(Math.Round(pThis.location.X + cx), Math.Round(pThis.location.Y + cy));

                    if (pParent.parent != null)
                    {
                        xDiff = pParent.location.X - pParent.parent.location.X;
                        yDiff = pParent.location.Y - pParent.parent.location.Y;
                        dParentAngle = 180 * (1 + Math.Atan2(yDiff, xDiff) / Math.PI);
                        pThis.AngleToParent = 360 - (dAngle - dParentAngle);
                    }
                }
                pThis = pParent;
                pParent = pParent.parent;
            }

            Recalc(pThis);

			this.ParentFigure.onJointMoved();
        } 
        #endregion
    }

    public class StickFigure
    {
        #region Variables
        public bool isActiveFigure;
        public bool drawHandles = true;
        public bool drawFigure = true;
		public int int1 = -1, int2 = -1;

        public StickJoint[] Joints = new StickJoint[12];
        /*
         * 0 = Head
         * 1 = Neck
         * 2 = Right Elbow
         * 3 = Right Hand
         * 4 = Left Elbow
         * 5 = Left Hand
         * 6 = Hip
         * 7 = Left Knee
         * 8 = Left Foot
         * 9 = Right Knee
         * 10 = Right Foot
         */

        #endregion

        public StickFigure(bool activate = true)
        {
            #region Define Joints/Position
            Joints[0] = new StickJoint("Neck", new Point(222, 158), 60, Color.Black, Color.Blue, 0, 0, true, Joints[1], false);
			
            Joints[1] = new StickJoint("Shoulder", new Point(222, 155), 12, Color.Black, Color.Yellow, 0, 0, false, Joints[0]);
            Joints[2] = new StickJoint("RElbow", new Point(238, 166), 12, Color.Black, Color.Red, 0, 0, false, Joints[1]);
            Joints[3] = new StickJoint("RHand", new Point(246, 184), 12, Color.Black, Color.Red, 0, 0, false, Joints[2]);
            Joints[4] = new StickJoint("LElbow", new Point(206, 167), 12, Color.Black, Color.Blue, 0, 0, false, Joints[1]);
            Joints[5] = new StickJoint("LHand", new Point(199, 186), 12, Color.Black, Color.Blue, 0, 0, false, Joints[4]);
            Joints[6] = new StickJoint("Hip", new Point(222, 195), 12, Color.Black, Color.Yellow, 0, 0, false, Joints[1]);
            Joints[7] = new StickJoint("LKnee", new Point(211, 218), 12, Color.Black, Color.Blue, 0, 0, false, Joints[6]);
            Joints[8] = new StickJoint("LFoot", new Point(202, 241), 12, Color.Black, Color.Blue, 0, 0, false, Joints[7]);
            Joints[9] = new StickJoint("RKnee", new Point(234, 217), 12, Color.Black, Color.Red, 0, 0, false, Joints[6]);
            Joints[10] = new StickJoint("RFoot", new Point(243, 240), 12, Color.Black, Color.Red, 0, 0, false, Joints[9]);
            Joints[11] = new StickJoint("Head", new Point(222, 150), 13, Color.Black, Color.Yellow, 0, 1, true, Joints[0]);

			for (int a = 0; a < 12; a++)
				Joints[a].ParentFigure = this;

            #endregion

            #region Calculate joint Lengths/Add Children to Parents
            for (int i = 0; i < Joints.Count(); i++)
            {
                if (Joints[i].parent != null)
                {
                    Joints[i].CalcLength(null);
                }
            }

            for (int i = 0; i < Joints.Count(); i++)
            {
                if (Joints[i].parent != null)
                {
                    Joints[i].parent.children.Add(Joints[i]);
                }
            } 
            #endregion

			Canvas.addStickFigure(this);
			this.drawFigure = activate;
			this.drawHandles = activate;
			if(activate)
				this.activate();
        }

		public void activate()
		{
            for (int i = 0; i < Canvas.stickFigureList.Count(); i++)
            {
                Canvas.stickFigureList[i].isActiveFigure = false;
            }
			Canvas.activateFigure(this);
            isActiveFigure = true;
		}

        #region Figure Manipulation
		public void onJointMoved()
		{
			if (int1 < 0)
				return;

			StickLayer currLayer = (StickLayer)Timeline.layers[int1];
			((StickFrame)(currLayer.keyFrames[currLayer.selectedFrame])).Joints = this.Joints.ToArray();
			Canvas.theCanvas.Invalidate();
			int asdf = 523;
		}

        public void flipArms()
        {
            Point rElbow = Joints[2].location;
            Point rHand = Joints[3].location;
            Point lElbow = Joints[4].location;
            Point lHand = Joints[5].location;

            Joints[2].location = lElbow;
            Joints[3].location = lHand;
            Joints[4].location = rElbow;
            Joints[5].location = rHand;

            Draw(false);
        }

        public void flipLegs()
        {
            Point lKnee = Joints[7].location;
            Point lFoot = Joints[8].location;
            Point rKnee = Joints[9].location;
            Point rFoot = Joints[10].location;

            Joints[7].location = rKnee;
            Joints[8].location = rFoot;
            Joints[9].location = lKnee;
            Joints[10].location = lFoot;

            Draw(true);
        } 
        #endregion

        #region Graphics/Drawing
        public void Draw(bool fromthingy)
        {
            if (fromthingy)
            {
                Canvas.theCanvas.Refresh();
                return;
            }
            foreach (StickJoint i in Joints)
            {
                if (i.parent != null)
                {
                    Canvas.drawGraphics(i.drawState, new Pen(i.color, i.thickness), new Point(i.location.X, i.location.Y), i.thickness, i.thickness, new Point(i.parent.location.X, i.parent.location.Y));
                }
            }
        }

        public void DrawHandles()
        {
            foreach (StickJoint i in Joints)
            {
                if (drawHandles)
                {
                    if (!isActiveFigure)
                    {
                        Canvas.drawGraphics(2, new Pen(Color.DimGray, 1), new Point(i.location.X, i.location.Y), 4, 4, new Point(0, 0));
                        continue;
                    }

                    if (i.handleDrawn & isActiveFigure)
                    {
                        Canvas.drawGraphics(2, new Pen(i.handleColor, 1), new Point(i.location.X, i.location.Y), 4, 4, new Point(0, 0));
                    }
                    if (i.state == 1 | i.state == 3 | i.state == 4)
                    {
                        Canvas.drawGraphics(3, new Pen(Color.WhiteSmoke, 1), new Point(i.location.X - 1, i.location.Y - 1), 6, 6, new Point(0, 0));
                    }
                }
            }
        } 
        #endregion

        #region Point Selection
        public int getPointAt(Point coords, int tolerance)
        {
            if (!(Joints.Count() > 0))
                return -1;
            List<StickJoint> resultIndex = new List<StickJoint>();
            double minimum = 9000001; //ITS OVER 9000!!!!
            int index = new int();

            for (int i = 0; i < Joints.Count(); i++)
            {
                if (Joints[i].handleDrawn)
                {
                    double itr_result = Math.Sqrt(Math.Pow(coords.X - Joints[i].location.X, 2) + Math.Pow(coords.Y - Joints[i].location.Y, 2));
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

        public StickJoint selectPoint(Point coords, int tolerance)
        {
            int index = getPointAt(coords, tolerance);
            if (!drawHandles)
                return new StickJoint("null", new Point(0, 0), 0, Color.Transparent, Color.Transparent);

            if (index == -1)
            {
                for (int i = 0; i < Joints.Count(); i++)
                {
                    Joints[i].handleColor = Joints[i].defaultHandleColor;
                }
                return new StickJoint("null", new Point(0, 0), 0, Color.Transparent, Color.Transparent);
            }

            for (int i = 0; i < Joints.Count(); i++)
            {
                if (i == index)
                {
                    //Joints[i].handleColor = Color.Orange;
                }
                else
                {
                    Joints[i].handleColor = Joints[i].defaultHandleColor;
                }
            }

            return Joints[index];
        } 
        #endregion
    }
}