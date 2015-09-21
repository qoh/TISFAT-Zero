using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TISFAT.Util;

namespace TISFAT.Entities
{
	public partial class TextObject : IEntity
	{
		public TextObject() { }

		public IEntityState Interpolate(float t, IEntityState _current, IEntityState _target, EntityInterpolationMode mode)
		{
			State current = _current as State;
			State target = _target as State;
			State state = new State();

			state.Bounds = Interpolation.Interpolate(t, current.Bounds, target.Bounds, mode);

			state.Text = current.Text;
			state.TextAlignment = current.TextAlignment;
			state.TextFont = current.TextFont;
			state.TextColor = Interpolation.Interpolate(t, current.TextColor, target.TextColor, mode);

			return state;
		}

		public void Draw(IEntityState _state)
		{
			State state = _state as State;
			Drawing.TextRect(state.Text, state.Bounds.Location, state.Bounds.Size, state.TextFont, state.TextColor, state.TextAlignment);
		}

		private void DrawHandle(State state)
		{
			float size = 6;

			Drawing.RectangleLine(state.Bounds.Location, state.Bounds.Size, Color.Black);

			Drawing.Rectangle(new PointF(state.Bounds.Left, state.Bounds.Top), new SizeF(size, size), Color.Red);
			Drawing.RectangleLine(new PointF(state.Bounds.Left, state.Bounds.Top), new SizeF(size, size), Color.Black);

			Drawing.Rectangle(new PointF(state.Bounds.Right - size, state.Bounds.Top), new SizeF(size, size), Color.Blue);
			Drawing.RectangleLine(new PointF(state.Bounds.Right - size, state.Bounds.Top), new SizeF(size, size), Color.Black);

			Drawing.Rectangle(new PointF(state.Bounds.Left, state.Bounds.Bottom - size), new SizeF(size, size), Color.Green);
			Drawing.RectangleLine(new PointF(state.Bounds.Left, state.Bounds.Bottom - size), new SizeF(size, size), Color.Black);

			Drawing.Rectangle(new PointF(state.Bounds.Right - size, state.Bounds.Bottom - size), new SizeF(size, size), Color.Yellow);
			Drawing.RectangleLine(new PointF(state.Bounds.Right - size, state.Bounds.Bottom - size), new SizeF(size, size), Color.Black);
		}

		public void DrawEditable(IEntityState _state)
		{
			State state = _state as State;

			Drawing.TextRect(state.Text, state.Bounds.Location, state.Bounds.Size, state.TextFont, state.TextColor, state.TextAlignment);
			DrawHandle(state);
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

			if (state == null)
				return null;

			ManipulateResult result = new ManipulateResult();
			ManipulateParams mparams = new ManipulateParams();
			result.Params = mparams;

			if (button == System.Windows.Forms.MouseButtons.Right)
			{
				result.Target = state;
				mparams.AbsoluteDrag = true;
				mparams.AbsoluteOffset = new PointF(location.X - state.Bounds.X, location.Y - state.Bounds.Y);
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

		public ManipulateResult TryManipulate(IEntityState _state, Point location, System.Windows.Forms.MouseButtons button, System.Windows.Forms.Keys modifiers, bool fromEditor)
		{
			return TryManipulate(_state, location, button, modifiers);
		}

		public void ManipulateStart(IManipulatable _target, IManipulatableParams mparams, Point location)
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

		public Layer CreateDefaultLayer(uint StartTime, uint EndTime, LayerCreationArgs e)
		{
			if (!Program.ActiveProject.LayerCount.ContainsKey(typeof(TextObject)))
				Program.ActiveProject.LayerCount.Add(typeof(TextObject), 0);

			int CreatedLayerCount = ++Program.ActiveProject.LayerCount[typeof(TextObject)];

			Layer layer = new Layer(this);
			layer.Name = "Text " + CreatedLayerCount;

			layer.Framesets.Add(new Frameset());
			layer.Framesets[0].Keyframes.Add(new Keyframe(StartTime, CreateRefState(), EntityInterpolationMode.Linear));
			layer.Framesets[0].Keyframes.Add(new Keyframe(EndTime, CreateRefState(), EntityInterpolationMode.Linear));

			return layer;
		}

		public IEntityState CreateRefState()
		{
			return new State()
			{
				Bounds = new RectangleF(10, 10, 50, 50),
				Text = "Hello World",
				TextFont = new Font("Segoe UI", 9, FontStyle.Regular),
				TextColor = Color.Black,
				TextAlignment = StringAlignment.Near
			};
		}

		public void Write(BinaryWriter writer)
		{
			// Move along..
		}

		public void Read(BinaryReader reader, UInt16 version)
		{
			// Nothing to see here
		}
	}
}
