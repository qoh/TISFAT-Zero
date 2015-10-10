using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TISFAT.Util;

namespace TISFAT
{
	public partial class Camera
	{
		public class State : IEntityState, IManipulatable 
		{
			public float Scale;
			public PointF Location;
			public float Angle;

			public State() { }

			public IEntityState Copy()
			{
				State state = new State();
				
				state.Scale = Scale;
				state.Location = Location;
				state.Angle = Angle;

				return state;
			}

			public int HandleAtLocation(PointF location)
			{
				float size = 6; // Dis is handle size

				RectangleF Bounds = new RectangleF(Location, new SizeF(Program.ActiveProject.Width * Scale, Program.ActiveProject.Height * Scale));

				RectangleF TopLeft = new RectangleF(new PointF(Bounds.Left, Bounds.Top), new SizeF(size, size));
				RectangleF TopRight = new RectangleF(new PointF(Bounds.Right - size, Bounds.Top), new SizeF(size, size));
				RectangleF BottomLeft = new RectangleF(new PointF(Bounds.Left, Bounds.Bottom - size), new SizeF(size, size));
				RectangleF BottomRight = new RectangleF(new PointF(Bounds.Right - size, Bounds.Bottom - size), new SizeF(size, size));

				if (MathUtil.PointInRect(location, TopLeft))
					return 0;
				if (MathUtil.PointInRect(location, TopRight))
					return 1;
				if (MathUtil.PointInRect(location, BottomRight))
					return 2;
				if (MathUtil.PointInRect(location, BottomLeft))
					return 3;

				return -1;
			}

			public void Move(PointF target, ManipulateParams mparams)
			{
				var x1 = Location.X;
				var x2 = Location.X + Program.ActiveProject.Width * Scale;
				var y1 = Location.Y;
				var y2 = Location.Y + Program.ActiveProject.Height * Scale;

				RectangleF Bounds = new RectangleF(Location, new SizeF(Program.ActiveProject.Width * Scale, Program.ActiveProject.Height * Scale));

				if (!mparams.AbsoluteDrag)
				{
					switch (mparams.CornerGrabbed)
					{
						case 0:
							x1 = target.X;
							y1 = target.Y;
							break;
						case 1:
							x2 = target.X;
							y1 = target.Y;
							break;
						case 2:
							x2 = target.X;
							y2 = target.Y;
							break;
						case 3:
							x1 = target.X;
							y2 = target.Y;
							break;
					}
				}
				else
				{
					target = new PointF(
							   target.X - mparams.AbsoluteOffset.X,
							   target.Y - mparams.AbsoluteOffset.Y);

					x1 = target.X;
					y1 = target.Y;
				}

				Location.X = x1;
				Location.Y = y1;
				
				if (!mparams.AbsoluteDrag)
				{
					Bounds.Width = x2 - x1;
					Bounds.Height = y2 - y1;
				}

				Scale = Math.Min(Bounds.Width / Program.ActiveProject.Width, Bounds.Height / Program.ActiveProject.Height);
			}

			public void Write(BinaryWriter writer)
			{
				writer.Write((double)Location.X);
				writer.Write((double)Location.Y);
				writer.Write((double)Scale);
				writer.Write((double)Angle);
			}

			public void Read(BinaryReader reader, UInt16 version)
			{
				float x, y;
				x = (float)reader.ReadDouble();
				y = (float)reader.ReadDouble();

				Location = new PointF(x, y);
				Scale = (float)reader.ReadDouble();
				Angle = (float)reader.ReadDouble();
			}
		}
	}

	public partial class Camera : IEntity
	{
		public Camera() { }

		public IEntityState Interpolate(float t, IEntityState _current, IEntityState _target, EntityInterpolationMode mode)
		{
			State state = new State();
			State current = _current as State;
			State target = _target as State;

			state.Angle = Interpolation.Interpolate(t, current.Angle, target.Angle, mode);
			state.Location = Interpolation.Interpolate(t, current.Location, target.Location, mode);
			state.Scale = Interpolation.Interpolate(t, current.Scale, target.Scale, mode);

			return state;
		}
		
		public void Draw(IEntityState _state)
		{
			State state = _state as State;

			if (Program.Form_Main.PreviewCamera)
				return;

			Drawing.RectangleLine(state.Location, new SizeF(Program.ActiveProject.Width * state.Scale, Program.ActiveProject.Height * state.Scale), 2, Color.Red);
		}

		public void DrawEditable(IEntityState _state)
		{
			State state = _state as State;

			Drawing.RectangleLine(state.Location, new SizeF(Program.ActiveProject.Width * state.Scale, Program.ActiveProject.Height * state.Scale), 2, Color.Red);

			float offset = 15.0f;
			float x1, x2;
			float y1, y2;
			float dx, dy;

			x1 = state.Location.X;
			y1 = state.Location.Y;
			x2 = state.Location.X + Program.ActiveProject.Width * state.Scale;
			y2 = state.Location.Y + Program.ActiveProject.Height * state.Scale;

			dx = Program.ActiveProject.Width * state.Scale / 8.0f;
			dy = Program.ActiveProject.Height * state.Scale / 8.0f;

			Drawing.Line(new PointF(x1 + offset, y1 + offset), new PointF(x1 + offset, y1 + offset + dy), 2, Color.Red);
			Drawing.Line(new PointF(x1 + offset, y1 + offset), new PointF(x1 + offset + dx, y1 + offset), 2, Color.Red);

			Drawing.Line(new PointF(x2 - offset, y1 + offset), new PointF(x2 - offset, y1 + offset + dy), 2, Color.Red);
			Drawing.Line(new PointF(x2 - offset, y1 + offset), new PointF(x2 - offset - dx, y1 + offset), 2, Color.Red);

			Drawing.Line(new PointF(x1 + offset, y2 - offset), new PointF(x1 + offset, y2 - offset - dy), 2, Color.Red);
			Drawing.Line(new PointF(x1 + offset, y2 - offset), new PointF(x1 + offset + dx, y2 - offset), 2, Color.Red);

			Drawing.Line(new PointF(x2 - offset, y2 - offset), new PointF(x2 - offset, y2 - offset - dy), 2, Color.Red);
			Drawing.Line(new PointF(x2 - offset, y2 - offset), new PointF(x2 - offset - dx, y2 - offset), 2, Color.Red);
		}

		public class ManipulateParams : IManipulatableParams
		{
			public bool AbsoluteDrag;
			public PointF AbsoluteOffset;

			public int CornerGrabbed;
		}

		public ManipulateResult TryManipulate(IEntityState _state, Point location, System.Windows.Forms.MouseButtons button, System.Windows.Forms.Keys modifiers)
		{
			State state = _state as State;

			if (Program.Form_Main.PreviewCamera)
				return null;

			if (state == null)
				return null;

			ManipulateResult result = new ManipulateResult();
			ManipulateParams mparams = new ManipulateParams();
			result.Params = mparams;

			if (button == System.Windows.Forms.MouseButtons.Right)
			{
				result.Target = state;
				mparams.AbsoluteDrag = true;
				mparams.AbsoluteOffset = new PointF(location.X - state.Location.X, location.Y - state.Location.Y);
			}
			else
			{
				mparams.CornerGrabbed = state.HandleAtLocation(location);

				if (mparams.CornerGrabbed == -1)
					return null;

				result.Target = state;
				mparams.AbsoluteDrag = false;
			}

			return result;
		}

		public ManipulateResult TryManipulate(IEntityState state, Point location, System.Windows.Forms.MouseButtons button, System.Windows.Forms.Keys modifiers, bool fromEditor)
		{
			// This does nothing. Editors have no camera.
			return null;
		}

		public void ManipulateStart(IManipulatable target, IManipulatableParams mparams, Point location)
		{

		}

		public void ManipulateUpdate(IManipulatable _target, IManipulatableParams mparams, Point location)
		{
			State target = _target as State;
			target.Move(location, (ManipulateParams)mparams);
		}

		public void ManipulateEnd(IManipulatable target, IManipulatableParams mparams, Point location)
		{

		}

		public IEntityState CreateRefState()
		{
			State state = new State();

			state.Location = new PointF(0, 0);
			state.Scale = 1.0f;
			state.Angle = 0.0f;

			return state;
		}

		public Layer CreateDefaultLayer(uint StartTime, uint EndTime, LayerCreationArgs e)
		{
			Layer layer = new Layer(this);
			layer.Name = "Camera";
			layer.TimelineColor = Color.FromArgb(70, 120, 255);

			layer.Framesets.Add(new Frameset());
			layer.Framesets[0].Keyframes.Add(new Keyframe(StartTime, CreateRefState(), EntityInterpolationMode.Linear));
			layer.Framesets[0].Keyframes.Add(new Keyframe(EndTime, CreateRefState(), EntityInterpolationMode.Linear));

			return layer;
		}

		public void Write(BinaryWriter writer)
		{
			// Nothing to see here
		}

		public void Read(BinaryReader reader, UInt16 version)
		{
			// Move along
		}
	}
}