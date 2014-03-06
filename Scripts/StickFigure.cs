using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using OpenTK.Graphics.OpenGL;

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

		public Double AngleToParent;

		public Color color;
		public Color handleColor;
		public Color defaultHandleColor;


		public List<Bitmap> bitmaps = new List<Bitmap>();

		public List<int> Bitmap_IDs = new List<int>();
		public List<string> Bitmap_names = new List<string>();
		public List<int> Bitmap_Rotations = new List<int>();
		public List<Point> Bitmap_Offsets = new List<Point>();

		public int Bitmap_CurrentID = -1;

		public List<int> textureIDs = new List<int>();
		#endregion

		#region Functions
		public StickJoint(string newname, Point newLocation, int newThickness, Color newColor, Color newHandleColor, int newState = 0, int newDrawState = 0, bool newFill = false, StickJoint newParent = null, bool newHandleDrawn = true, Bitmap bitty = null, String bName = null)
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
			this.AngleToParent = obj.AngleToParent;
			this.bitmaps = obj.bitmaps;
			this.Bitmap_Rotations = obj.Bitmap_Rotations;
			this.Bitmap_Offsets = obj.Bitmap_Offsets;
			this.Bitmap_names = obj.Bitmap_names;
			this.Bitmap_IDs = obj.Bitmap_IDs;
			this.Bitmap_CurrentID = obj.Bitmap_CurrentID;
			this.color = obj.color;
			this.defaultHandleColor = obj.defaultHandleColor;
			this.drawOrder = obj.drawOrder;
			this.drawState = obj.drawState;
			this.fill = obj.fill;
			this.handleColor = obj.handleColor;
			this.handleDrawn = obj.handleDrawn;
			this.length = obj.length;
			this.location = obj.location;
			this.name = obj.name;
			this.state = obj.state;
			this.textureIDs = obj.textureIDs;
			this.thickness = obj.thickness;
			this.visible = obj.visible;
			parent = newParent;
		}

		public StickJoint(StickJoint obj)
		{
			this.AngleToParent = obj.AngleToParent;
			this.bitmaps = obj.bitmaps;
			this.Bitmap_Rotations = obj.Bitmap_Rotations;
			this.Bitmap_Offsets = obj.Bitmap_Offsets;
			this.Bitmap_names = obj.Bitmap_names;
			this.Bitmap_IDs = obj.Bitmap_IDs;
			this.Bitmap_CurrentID = obj.Bitmap_CurrentID;
			this.color = obj.color;
			this.defaultHandleColor = obj.defaultHandleColor;
			this.drawOrder = obj.drawOrder;
			this.drawState = obj.drawState;
			this.fill = obj.fill;
			this.handleColor = obj.handleColor;
			this.handleDrawn = obj.handleDrawn;
			this.length = obj.length;
			this.location = obj.location;
			this.name = obj.name;
			this.state = obj.state;
			this.textureIDs = obj.textureIDs;
			this.thickness = obj.thickness;
			this.visible = obj.visible;
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

		public void Tween(StickJoint pStart, StickJoint pEnd, Single sPercent)
		{
			//StickJoint pNew;
			int[] r = new int[3];
			int[] g = new int[3];
			int[] b = new int[3];
			int[] a = new int[3];

			location.X = (int)Math.Round(pStart.location.X + ((pEnd.location.X - pStart.location.X) * sPercent));
			location.Y = (int)Math.Round(pStart.location.Y + ((pEnd.location.Y - pStart.location.Y) * sPercent));

			length = (int)Math.Round(pStart.length + ((pEnd.length - pStart.length) * sPercent));
			thickness = (int)Math.Round(pStart.thickness + ((pEnd.thickness - pStart.thickness) * sPercent));
			AngleToParent = (int)Math.Round(pStart.AngleToParent + ((pEnd.AngleToParent - pStart.AngleToParent) * sPercent));

			if (pStart.ParentFigure != null)
				if (pStart.ParentFigure.type == 3)
					if (((StickRect)ParentFigure).filled)
					{
						int[] r2 = new int[3];
						int[] g2 = new int[3];
						int[] b2 = new int[3];
						int[] a2 = new int[3];

						Color tmp1 = pStart.ParentFigure.parentLayer.adjacentBack.figColor;
						Color tmp2 = pEnd.ParentFigure.parentLayer.adjacentFront.figColor;

						r2[0] = tmp1.R;
						g2[0] = tmp1.G;
						b2[0] = tmp1.B;
						a2[0] = tmp1.A;

						r2[1] = tmp2.R;
						g2[1] = tmp2.G;
						b2[1] = tmp2.B;
						a2[1] = tmp2.A;

						r2[2] = r2[0] + (int)Math.Round(sPercent * (r2[1] - r2[0]));
						g2[2] = g2[0] + (int)Math.Round(sPercent * (g2[1] - g2[0]));
						b2[2] = b2[0] + (int)Math.Round(sPercent * (b2[1] - b2[0]));
						a2[2] = a2[0] + (int)Math.Round(sPercent * (a2[1] - a2[0]));

						if (a2[2] > 255)
							a2[2] = 255;

						((StickRect)ParentFigure).figColor = Color.FromArgb(a2[2], r2[2], g2[2], b2[2]);
					}

			r[0] = pStart.color.R;
			g[0] = pStart.color.G;
			b[0] = pStart.color.B;
			a[0] = pStart.color.A;

			r[1] = pEnd.color.R;
			g[1] = pEnd.color.G;
			b[1] = pEnd.color.B;
			a[1] = pEnd.color.A;

			r[2] = r[0] + (int)Math.Round(sPercent * (r[1] - r[0]));
			g[2] = g[0] + (int)Math.Round(sPercent * (g[1] - g[0]));
			b[2] = b[0] + (int)Math.Round(sPercent * (b[1] - b[0]));
			a[2] = a[0] + (int)Math.Round(sPercent * (a[1] - a[0]));

			if (a[2] > 255)
				a[2] = 255;

			color = Color.FromArgb(a[2], r[2], g[2], b[2]);
		}

		public void removeChildren()
		{
			for (int i = children.Count;i > 0;i--)
			{
				children[i - 1].removeChildren();
				ParentFigure.Joints.Remove(children[i - 1]);
				children.RemoveAt(i - 1);
			}
		}
		#endregion

		#region Positioning
		public void SetPosAbs(double vx, double vy)
		{
			location.X = Convert.ToInt32(vx);
			location.Y = Convert.ToInt32(vy);

			recalcAngleToParent();
		}

		public void Recalc(StickJoint pStart = null)
		{
			if (pStart == null || pStart.children.Count == 0)
				return;

			foreach (StickJoint pJoint in pStart.children)
			{
				int xDiff = pStart.location.X - pJoint.location.X;
				int yDiff = pStart.location.Y - pJoint.location.Y;

				double dAngle = 180 * (1 + Math.Atan2(yDiff, xDiff) / Math.PI);
				double dRads = Functions.DegToRads(dAngle);

				if (pStart.parent != null)
				{
					xDiff = pStart.parent.location.X - pStart.location.X;
					yDiff = pStart.parent.location.Y - pStart.location.Y;
					pJoint.AngleToParent = (180 * (1 + Math.Atan2(yDiff, xDiff) / Math.PI)) - dAngle;
				}

				double cx = Math.Round(pJoint.length * Math.Cos(dRads));
				double cy = Math.Round(pJoint.length * Math.Sin(dRads));

				pJoint.AngleToParent = -dAngle;

				pJoint.SetPosAbs(Math.Round(pStart.location.X + cx), Math.Round(pStart.location.Y + cy));

				Recalc(pJoint);
			}
		}

		public void recalcAngleToParent()
		{
			if (parent == null)
				return;

			StickJoint pStart = this;
			StickJoint pJoint = this.parent;
			/*
			int xDiff = pStart.location.X - pJoint.location.X;
			int yDiff = pStart.location.Y - pJoint.location.Y;

			double dAngle = 180 * (1 + Math.Atan2(yDiff, xDiff) / Math.PI);
			AngleToParent = (180 * (1 + Math.Atan2(yDiff, xDiff) / Math.PI)) - dAngle;
			*/
			int xDiff = pStart.location.X - pJoint.location.X;
			int yDiff = pStart.location.Y - pJoint.location.Y;

			AngleToParent = -(180 * (1 + Math.Atan2(yDiff, xDiff) / Math.PI));
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
					pThis.AngleToParent = -dAngle;

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

	public abstract class StickObject
	{

		#region Properties

		public List<StickJoint> Joints;
		public bool isActiveFig, drawFig, drawHandles, isTweenFig, fromSticked;
		public Color figColor = Color.Black;
		public Layer parentLayer;

		public bool isDrawn
		{
			get
			{
				return drawFig;
			}
			set
			{
				if (value)
				{
					drawFig = true;
					drawHandles = true;
				}
				else
				{
					drawFig = false;
					drawHandles = false;
				}
			}
		}
		public byte type;

		#endregion Properties

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

		public void drawWholeFigure(bool fromCanvas = true)
		{
			if (!fromCanvas)
			{
				Canvas.theCanvas.Refresh();
				return;
			}

			if (!drawFig)
				return;

			drawJoints(fromCanvas);

			if (!isTweenFig && drawHandles)
				drawFigHandles();
		}

		private void drawJoints(bool fromCanvas = true)
		{
			if (!fromCanvas)
			{
				Canvas.theCanvas.Refresh();
				return;
			}

			bool useStencil = false;
			//there's probably a better way instead of looping to determine if any parts have transparency...
			foreach (StickJoint i in Joints)
			{
				if (i.parent != null)
				{
					if (i.color.A != 255)
					{
						useStencil = true;
						break;
					}
				}
			}

			GL.Disable(EnableCap.StencilTest);

			if (useStencil)
			{
				GL.Clear(ClearBufferMask.StencilBufferBit);
				GL.Enable(EnableCap.StencilTest);
				GL.StencilMask(0xFFFFFF);
				GL.StencilFunc(StencilFunction.Equal, 0, 0xFFFFFF);
				GL.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Incr);
			}

			if (type == 3)
				if (((StickRect)this).filled)
					Canvas.drawGraphics(5, figColor, Joints[0].location, Joints[0].thickness, Joints[0].thickness, Joints[2].location);
			foreach (StickJoint i in Joints)
			{
				if (i.parent != null)
				{
					Canvas.drawGraphics(i.drawState, i.color, new Point(i.location.X, i.location.Y), i.thickness, i.thickness, new Point(i.parent.location.X, i.parent.location.Y));
				}
				else if (i.drawState != 0)
				{
					Canvas.drawGraphics(i.drawState, i.color, new Point(i.location.X, i.location.Y), i.thickness, i.thickness, new Point(i.location.X, i.location.Y));
				}

				if (i.bitmaps.Count != 0)
					if (i.textureIDs[i.Bitmap_CurrentID] != 0)
					{
						Point offset = rotate(i.Bitmap_Offsets[i.Bitmap_CurrentID], i.AngleToParent);
						Canvas.drawGraphics(6, Color.White, new Point(i.location.X - offset.X, i.location.Y - offset.Y), i.bitmaps[i.Bitmap_CurrentID].Width, i.bitmaps[i.Bitmap_CurrentID].Height, new Point(0, 0), i.textureIDs[i.Bitmap_CurrentID], (float)i.AngleToParent - i.Bitmap_Rotations[i.Bitmap_CurrentID]);
					}
			}

			GL.Disable(EnableCap.StencilTest);
		}

		private Point rotate(Point offset, double rotation)
		{
			double length = Math.Sqrt((offset.X * offset.X) + (offset.Y * offset.Y));
			double angle = Math.Atan2(offset.Y, offset.X) + Functions.DegToRads(rotation);

			Point result = new Point((int)(Math.Sin(angle) * length), (int)(Math.Cos(angle) * length));

			return result;
		}

		private void drawJoints(int x, bool fromCanvas = true)
		{
			if (!fromCanvas)
			{
				StickEditor.theSticked.Refresh();
				return;
			}

			bool useStencil = false;
			//there's probably a better way instead of looping to determine if any parts have transparency...
			foreach (StickJoint i in Joints)
			{
				if (i.parent != null)
				{
					if (i.color.A != 255)
					{
						useStencil = true;
						break;
					}
				}
			}

			GL.Disable(EnableCap.StencilTest);

			if (useStencil)
			{
				GL.Clear(ClearBufferMask.StencilBufferBit);
				GL.Enable(EnableCap.StencilTest);
				GL.StencilMask(0xFFFFFF);
				GL.StencilFunc(StencilFunction.Equal, 0, 0xFFFFFF);
				GL.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Incr);
			}

			foreach (StickJoint i in Joints)
			{
				if (i.parent != null)
				{
					StickEditor.theSticked.drawGraphics(i.drawState, i.color, new Point(i.location.X, i.location.Y), i.thickness, i.thickness, new Point(i.parent.location.X, i.parent.location.Y));
				}
				else if (i.drawState != 0)
				{
					StickEditor.theSticked.drawGraphics(i.drawState, i.color, new Point(i.location.X, i.location.Y), i.thickness, i.thickness, new Point(i.location.X, i.location.Y));
				}

				if (i.bitmaps.Count != 0)
					if (i.textureIDs[i.Bitmap_CurrentID] != 0)
					{
						Point offset = rotate(i.Bitmap_Offsets[i.Bitmap_CurrentID], i.AngleToParent);
						StickEditor.theSticked.drawGraphics(6, Color.White, new Point(i.location.X - offset.X, i.location.Y - offset.Y), i.bitmaps[i.Bitmap_CurrentID].Width, i.bitmaps[i.Bitmap_CurrentID].Height, new Point(0, 0), i.textureIDs[i.Bitmap_CurrentID], (float)i.AngleToParent - i.Bitmap_Rotations[i.Bitmap_CurrentID]);
					}
			}

			GL.Disable(EnableCap.StencilTest);
		}

		public void drawFigHandles(bool fromCanvas = true)
		{
			if (!fromCanvas)
			{
				Canvas.theCanvas.Refresh();
				return;
			}

			foreach (StickJoint i in Joints)
			{
				if (drawHandles)
				{
					if (!isActiveFig)
					{
						Canvas.drawGraphics(2, Color.DimGray, new Point(i.location.X, i.location.Y), 4, 4, new Point(0, 0));
						continue;
					}

					if (i.handleDrawn & isActiveFig)
						Canvas.drawGraphics(2, i.handleColor, new Point(i.location.X, i.location.Y), 4, 4, new Point(0, 0));

					if (i.state == 1 | i.state == 3 | i.state == 4)
						Canvas.drawGraphics(3, Color.WhiteSmoke, new Point(i.location.X - 1, i.location.Y - 1), 6, 6, new Point(0, 0));
				}
			}
		}

		public void drawFigHandles(int x, bool fromCanvas = true)
		{
			if (!fromCanvas)
			{
				StickEditor.theSticked.Refresh();
				return;
			}

			foreach (StickJoint i in Joints)
			{
				if (drawHandles)
				{
					if (!isActiveFig)
					{
						StickEditor.theSticked.drawGraphics(2, Color.DimGray, new Point(i.location.X, i.location.Y), 4, 4, new Point(0, 0));
						continue;
					}

					if (i.handleDrawn & isActiveFig)
						StickEditor.theSticked.drawGraphics(2, i.handleColor, new Point(i.location.X, i.location.Y), 4, 4, new Point(0, 0));
					else if (!i.handleDrawn & isActiveFig)
						StickEditor.theSticked.drawGraphics(3, i.handleColor, new Point(i.location.X, i.location.Y), 4, 4, new Point(0, 0));

					if (i.state == 1 | i.state == 3 | i.state == 4)
						StickEditor.theSticked.drawGraphics(3, Color.WhiteSmoke, new Point(i.location.X - 1, i.location.Y - 1), 6, 6, new Point(0, 0));
				}
			}
		}

		#endregion Drawing

		public void setAsActiveFigure()
		{
		}

		public void setColor(Color color)
		{
			for (int i = 0;i < Joints.Count;i++)
			{
				Joints[i].color = color;
			}
			if (type != 3)
				figColor = color;
		}

		public void setFillColor(Color color)
		{
			figColor = color;
		}

		public int getPointAt(Point coords, int tolerance)
		{
			if (!(Joints.Count() > 0))
				return -1;
			List<StickJoint> resultIndex = new List<StickJoint>();
			double minimum = short.MaxValue; //ITS OVER 9000!!!!
			int index = new int();

			for (int i = 0;i < Joints.Count();i++)
			{
				if (Joints[i].handleDrawn & !fromSticked)
				{
					double itr_result = Math.Sqrt(Math.Pow(coords.X - Joints[i].location.X, 2) + Math.Pow(coords.Y - Joints[i].location.Y, 2));
					if (itr_result < minimum)
					{
						minimum = itr_result;
						index = i;
					}
				}
				else if (fromSticked)
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
				return null;

			if (index == -1)
			{
				for (int i = 0;i < Joints.Count();i++)
				{
					Joints[i].handleColor = Joints[i].defaultHandleColor;
				}
				return null;
			}

			for (int i = 0;i < Joints.Count();i++)
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

		public void activate()
		{
			for (int i = 0;i < Canvas.figureList.Count();i++)
			{
				Canvas.figureList[i].isActiveFig = false;
			}
			Canvas.activateFigure(this);
			isActiveFig = true;
		}

		public void onJointMoved()
		{
			Layer x = Timeline.layers[Timeline.layer_sel];
			x.keyFrames[x.selectedFrame].Joints = this.Joints;
		}

		//Base as in it has no parent... I know the comparison is iffy at best. Just deal with it.
		public void setAsBase(StickJoint centre)
		{
			if (centre.parent == null)
				return;

			sunIter(centre.parent, centre);

			centre.parent = null;

			for (int i = 0;i < Joints.Count();i++)
			{
				if (Joints[i].parent != null)
				{
					Joints[i].CalcLength(null);
				}
			}
		}

		private void sunIter(StickJoint next, StickJoint prev)
		{
			if (next.parent != null)
				sunIter(next.parent, next);

			next.children.Remove(prev);
			next.parent = prev;
			prev.children.Add(next);
		}

		public void reSortJoints()
		{
			Joints.Sort(Functions.compareDrawOrder);
		}
	}

	public class StickFigure : StickObject
	{

		#region Variables

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

			//Joints[1]
			//Joints[0]

			base.type = 1;
			base.Joints = new List<StickJoint>(12);
			Joints.Add(new StickJoint("Neck", new Point(222, 158), 12, Color.Black, Color.Blue, 0, 0, true, null, false));
			Joints.Add(new StickJoint("Shoulder", new Point(222, 155), 12, Color.Black, Color.Yellow, 0, 0, false, null));
			Joints[0].parent = Joints[1];
			Joints.Add(new StickJoint("RElbow", new Point(238, 166), 12, Color.Black, Color.Red, 0, 0, false, Joints[1]));
			Joints.Add(new StickJoint("RHand", new Point(246, 184), 12, Color.Black, Color.Red, 0, 0, false, Joints[2]));
			Joints.Add(new StickJoint("LElbow", new Point(206, 167), 12, Color.Black, Color.Blue, 0, 0, false, Joints[1]));
			Joints.Add(new StickJoint("LHand", new Point(199, 186), 12, Color.Black, Color.Blue, 0, 0, false, Joints[4]));
			Joints.Add(new StickJoint("Hip", new Point(222, 195), 12, Color.Black, Color.Yellow, 0, 0, false, Joints[1]));
			Joints.Add(new StickJoint("LKnee", new Point(211, 218), 12, Color.Black, Color.Blue, 0, 0, false, Joints[6]));
			Joints.Add(new StickJoint("LFoot", new Point(202, 241), 12, Color.Black, Color.Blue, 0, 0, false, Joints[7]));
			Joints.Add(new StickJoint("RKnee", new Point(234, 217), 12, Color.Black, Color.Red, 0, 0, false, Joints[6]));
			Joints.Add(new StickJoint("RFoot", new Point(243, 240), 12, Color.Black, Color.Red, 0, 0, false, Joints[9]));
			Joints.Add(new StickJoint("Head", new Point(222, 150), 13, Color.Black, Color.Yellow, 0, 1, true, Joints[0]));

			foreach (StickJoint j in Joints)
				j.ParentFigure = this;

			#endregion

			#region Calculate joint Lengths/Add Children to Parents | Draw Order defined
			for (int i = 0;i < Joints.Count();i++)
			{
				if (Joints[i].parent != null)
					Joints[i].CalcLength(null);

				Joints[i].drawOrder = i;
			}

			for (int i = 0;i < Joints.Count();i++)
				if (Joints[i].parent != null)
					Joints[i].parent.children.Add(Joints[i]);

			reSortJoints();
			#endregion

			Canvas.addFigure(this);
			base.drawFig = activate;
			this.drawHandles = activate;
			this.isTweenFig = false;
			if (activate)
				this.activate();
		}

		public StickFigure(bool isTweenFigure, bool stuff)
		{
			#region Define Joints/Position
			base.Joints = new List<StickJoint>(12);
			Joints.Add(new StickJoint("Neck", new Point(222, 158), 12, Color.Black, Color.Blue, 0, 0, true, null, false));
			Joints.Add(new StickJoint("Shoulder", new Point(222, 155), 12, Color.Black, Color.Yellow, 0, 0, false, null));
			Joints[0].parent = Joints[1];
			Joints.Add(new StickJoint("RElbow", new Point(238, 166), 12, Color.Black, Color.Red, 0, 0, false, Joints[1]));
			Joints.Add(new StickJoint("RHand", new Point(246, 184), 12, Color.Black, Color.Red, 0, 0, false, Joints[2]));
			Joints.Add(new StickJoint("LElbow", new Point(206, 167), 12, Color.Black, Color.Blue, 0, 0, false, Joints[1]));
			Joints.Add(new StickJoint("LHand", new Point(199, 186), 12, Color.Black, Color.Blue, 0, 0, false, Joints[4]));
			Joints.Add(new StickJoint("Hip", new Point(222, 195), 12, Color.Black, Color.Yellow, 0, 0, false, Joints[1]));
			Joints.Add(new StickJoint("LKnee", new Point(211, 218), 12, Color.Black, Color.Blue, 0, 0, false, Joints[6]));
			Joints.Add(new StickJoint("LFoot", new Point(202, 241), 12, Color.Black, Color.Blue, 0, 0, false, Joints[7]));
			Joints.Add(new StickJoint("RKnee", new Point(234, 217), 12, Color.Black, Color.Red, 0, 0, false, Joints[6]));
			Joints.Add(new StickJoint("RFoot", new Point(243, 240), 12, Color.Black, Color.Red, 0, 0, false, Joints[9]));
			Joints.Add(new StickJoint("Head", new Point(222, 155), 13, Color.Black, Color.Yellow, 0, 1, true, Joints[0]));

			foreach (StickJoint j in Joints)
				j.ParentFigure = this;

			#endregion

			#region Calculate joint Lengths/Add Children to Parents | Draw Order defined
			for (int i = 0;i < Joints.Count();i++)
			{
				if (Joints[i].parent != null)
				{
					Joints[i].CalcLength(null);
				}
				Joints[i].drawOrder = i;
			}

			for (int i = 0;i < Joints.Count();i++)
			{
				if (Joints[i].parent != null)
				{
					Joints[i].parent.children.Add(Joints[i]);
				}
			}

			reSortJoints();
			#endregion

			Canvas.addTweenFigure(this);
			this.drawHandles = false;
		}

		#region Figure Manipulation
		//go aheadoo
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
		}
		#endregion

	}

	public class StickLine : StickObject
	{
		public StickLine(bool isTweenFigure = false)
		{
			type = 2;
			Joints = new List<StickJoint>(2);

			//Somewhere between a rock and a hardplace is a line. :) -Evar678
			//I don't get it. -Ipquarx

			Joints.Add(new StickJoint("Rock", new Point(30, 30), 12, Color.Black, Color.Green, 0, 0, false, null));
			Joints.Add(new StickJoint("Hard Place", new Point(45, 30), 12, Color.Black, Color.Yellow, 0, 0, false, Joints[0]));

			foreach (StickJoint j in Joints)
				j.ParentFigure = this;

			if (!isTweenFigure)
				Canvas.addFigure(this);
			else
				Canvas.addTweenFigure(this);
		}

		public void setThickness(int thickness)
		{
			Joints[0].thickness = thickness;
			Joints[1].thickness = thickness;
		}
	}

	public class StickRect : StickObject
	{
		public bool filled = true;

		public StickRect(bool isTweenFigure = false)
		{
			type = 3;
			Joints = new List<StickJoint>(4);

			Joints.Add(new StickJoint("CornerTL", new Point(30, 30), 3, Color.Black, Color.LimeGreen, 0, 0, false, null));
			Joints.Add(new StickJoint("CornerLL", new Point(30, 70), 3, Color.Black, Color.Yellow, 0, 0, false, Joints[0]));
			Joints.Add(new StickJoint("CornerLR", new Point(150, 70), 3, Color.Black, Color.Red, 0, 0, false, Joints[1]));
			Joints.Add(new StickJoint("CornerTR", new Point(150, 30), 3, Color.Black, Color.Blue, 0, 0, false, Joints[2]));

			foreach (StickJoint j in Joints)
				j.ParentFigure = this;

			Joints[0].parent = Joints[3];
			Joints[3].children.Add(Joints[0]);

			if (!isTweenFigure)
				Canvas.addFigure(this);
			else
				Canvas.addTweenFigure(this);
		}

		public void onRectJointMoved(StickJoint j)
		{
			if (j.name == "CornerTL")
			{
				Joints[1].location.X = j.location.X;
				Joints[3].location.Y = j.location.Y;
			}
			else if (j.name == "CornerLL")
			{
				Joints[0].location.X = j.location.X;
				Joints[2].location.Y = j.location.Y;
			}
			else if (j.name == "CornerLR")
			{
				Joints[3].location.X = j.location.X;
				Joints[1].location.Y = j.location.Y;
			}
			else if (j.name == "CornerTR")
			{
				Joints[2].location.X = j.location.X;
				Joints[0].location.Y = j.location.Y;
			}
		}
	}

	public class StickCustom : StickObject
	{
		public StickCustom(StickCustom obj, bool isTweenFigure= false)
		{
			type = 4;
			Joints = obj.Joints;

			if (!isTweenFigure)
				Canvas.addFigure(this);
			else
				Canvas.addTweenFigure(this);
		}

		public StickCustom(bool isTweenFigure = false)
		{
			type = 4;
			Joints = new List<StickJoint>();

			if (!isTweenFigure)
				Canvas.addFigure(this);
			else
				Canvas.addTweenFigure(this);
		}

		public StickCustom(int x, bool isTweenFigure = false)
		{
			type = 4;
			Joints = new List<StickJoint>();

			fromSticked = true;
		}

		public int getBitmapCount()
		{
			int bitmaps = 0;
			foreach (StickJoint j in Joints)
				bitmaps = bitmaps + j.bitmaps.Count;

			return bitmaps;
		}
	}

	public class LightObject : StickObject
	{
		int Size;
		Color Color;

		public LightObject(bool isTweenFigure = false)
		{
			type = 5;
			Joints = new List<StickJoint>();

			Joints.Add(new StickJoint("Light Source", new Point(30, 30), 1, Color.Black, Color.Green));

			if (!isTweenFigure)
				Canvas.addFigure(this);
			else
				Canvas.addTweenFigure(this);

			Canvas.lights.Add(this);
		}
	}
}