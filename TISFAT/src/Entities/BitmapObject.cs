using System;
using System.Drawing;
using System.IO;
using TISFAT.Util;

namespace TISFAT.Entities
{
	public class BitmapObject : IEntity
	{
		public Bitmap Texture;
		public int TextureID;

		public class State : IEntityState
		{
			public RectangleF Bounds;

			public State() { }

			public IEntityState Copy()
			{
				State state = new State();
				state.Bounds = new RectangleF(Bounds.Location, Bounds.Size);
				return state;
			}

			public void Write(BinaryWriter writer)
			{

			}

			public void Read(BinaryReader reader, UInt16 version)
			{

			}
		}

		public BitmapObject() { }

		public IEntityState Interpolate(float t, IEntityState _current, IEntityState _target)
		{
			State current = _current as State;
			State target = _target as State;
			State state = new State();

			state.Bounds = Interpolation.Linear(t, current.Bounds, target.Bounds);

			return state;
		}

		public void Draw(IEntityState _state)
		{
			State state = _state as State;
			Drawing.Bitmap(state.Bounds.Location, state.Bounds.Size, TextureID);
		}

		private void DrawHandle(State state, Color c)
		{
			float size = Math.Min(12, Texture.Width / 2);

			Drawing.RectangleLine(state.Bounds.Location, state.Bounds.Size, c);
			Drawing.Rectangle(new PointF(state.Bounds.Left, state.Bounds.Top), new SizeF(size, size), c);
			Drawing.Rectangle(new PointF(state.Bounds.Right - size, state.Bounds.Top), new SizeF(size, size), c);
			Drawing.Rectangle(new PointF(state.Bounds.Left, state.Bounds.Bottom - size), new SizeF(size, size), c);
			Drawing.Rectangle(new PointF(state.Bounds.Right - size, state.Bounds.Bottom - size), new SizeF(size, size), c);
		}

		public void DrawEditable(IEntityState _state)
		{
			State state = _state as State;

			Drawing.Bitmap(state.Bounds.Location, state.Bounds.Size, TextureID);
			DrawHandle(state, Color.Cyan);
		}

		public ManipulateResult TryManipulate(IEntityState state, Point location, System.Windows.Forms.MouseButtons button, System.Windows.Forms.Keys modifiers)
		{
			return null;
		}

		public void ManipulateStart(IManipulatable target, IManipulatableParams mparams, Point location)
		{

		}

		public void ManipulateUpdate(IManipulatable target, IManipulatableParams mparams, Point location)
		{

		}

		public void ManipulateEnd(IManipulatable target, IManipulatableParams mparams, Point location)
		{

		}

		public IEntityState CreateRefState()
		{
			return new State() { Bounds = new RectangleF(10, 10, Texture.Width, Texture.Height) };
		}

		public void Write(BinaryWriter writer)
		{

		}

		public void Read(BinaryReader reader, UInt16 version)
		{

		}
	}
}
