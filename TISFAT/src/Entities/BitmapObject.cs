using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TISFAT.Util;

namespace TISFAT.Entities
{
	public partial class BitmapObject : IEntity
	{
		public Bitmap Texture;
		public int TextureID;

		public BitmapObject() { }

		public IEntityState Interpolate(float t, IEntityState _current, IEntityState _target, EntityInterpolationMode mode)
		{
			return _Interpolate(t, _current, _target, mode);
		}

		private static IEntityState _Interpolate(float t, IEntityState _current, IEntityState _target, EntityInterpolationMode mode)
		{
			State current = _current as State;
			State target = _target as State;
			State state = new State();

			state.Bounds = Interpolation.Interpolate(t, current.Bounds, target.Bounds, mode);
			state.Rotation = Interpolation.Interpolate(t, current.Rotation, target.Rotation, mode);
			state.BitmapAlpha = (int)Math.Max(Interpolation.Interpolate(t, current.BitmapAlpha, target.BitmapAlpha, mode), 0);

			return state;
		}

		public void Draw(IEntityState _state)
		{
			State state = _state as State;
			Drawing.Bitmap(state.Bounds.Location, state.Bounds.Size, state.Rotation, state.BitmapAlpha, TextureID);
		}

		private void DrawHandle(State state, Color c)
		{
			float size = Math.Min(12, Texture.Width / 2);

			Drawing.RectangleLine(state.Bounds.Location, state.Bounds.Size, 1, c);
			Drawing.Rectangle(new PointF(state.Bounds.Left, state.Bounds.Top), new SizeF(size, size), c);
			Drawing.Rectangle(new PointF(state.Bounds.Right - size, state.Bounds.Top), new SizeF(size, size), c);
			Drawing.Rectangle(new PointF(state.Bounds.Left, state.Bounds.Bottom - size), new SizeF(size, size), c);
			Drawing.Rectangle(new PointF(state.Bounds.Right - size, state.Bounds.Bottom - size), new SizeF(size, size), c);
		}

		public void DrawEditable(IEntityState _state)
		{
			State state = _state as State;

			Drawing.Bitmap(state.Bounds.Location, state.Bounds.Size, state.Rotation, state.BitmapAlpha,TextureID);
			DrawHandle(state, Color.Cyan);
		}

		public class ManipulateParams : IManipulatableParams
		{
			public bool AbsoluteDrag;
			public PointF AbsoluteOffset;

			public int CornerGrabbed;

			public bool KeepAspectRatio;
		}

		public ManipulateResult TryManipulate(IEntityState _state, Point location, System.Windows.Forms.MouseButtons button, System.Windows.Forms.Keys modifiers)
		{
			State state = _state as State;

			if (state == null)
				return null;

			ManipulateResult result = new ManipulateResult();
			ManipulateParams mparams = new ManipulateParams();
			result.Params = mparams;

			if (modifiers == Keys.Shift)
				mparams.KeepAspectRatio = true;

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
			Texture = (Bitmap)Bitmap.FromFile(e.Arguments, true);
			TextureID = Drawing.GenerateTexID(Texture);

			if (!Program.ActiveProject.LayerCount.ContainsKey(typeof(BitmapObject)))
				Program.ActiveProject.LayerCount.Add(typeof(BitmapObject), 0);

			int CreatedLayerCount = ++Program.ActiveProject.LayerCount[typeof(BitmapObject)];

			Layer layer = new Layer(this);
			layer.Name = "Bitmap " + CreatedLayerCount;

			layer.Framesets.Add(new Frameset());
			layer.Framesets[0].Keyframes.Add(new Keyframe(StartTime, CreateRefState(), EntityInterpolationMode.Linear));
			layer.Framesets[0].Keyframes.Add(new Keyframe(EndTime, CreateRefState(), EntityInterpolationMode.Linear));

			return layer;
		}

		public IEntityState CreateRefState()
		{
			return new State() { Bounds = new RectangleF(10, 10, Texture.Width, Texture.Height), BitmapAlpha = 255, TexWidth = Texture.Width, TexHeight = Texture.Height };
		}

		public void Write(BinaryWriter writer)
		{
			FileFormat.WriteBitmap(Texture, writer);
		}

		public void Read(BinaryReader reader, UInt16 version)
		{
			Texture = FileFormat.ReadBitmap(reader);
			TextureID = Drawing.GenerateTexID(Texture);
		}
	}
}
