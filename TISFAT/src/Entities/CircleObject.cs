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
	public partial class CircleObject : IEntity
	{
		public CircleObject() { }

		public IEntityState Interpolate(float t, IEntityState _current, IEntityState _target, EntityInterpolationMode mode)
		{
			return _Interpolate(t, _current, _target, mode);
		}

		private static IEntityState _Interpolate(float t, IEntityState _current, IEntityState _target, EntityInterpolationMode mode)
		{
			State current = _current as State;
			State target = _target as State;
			State state = new State();

			state.Location = Interpolation.Interpolate(t, current.Location, target.Location, mode);
            state.Size = Interpolation.Interpolate(t, current.Size, target.Size, mode);
			state.Color = Interpolation.Interpolate(t, current.Color, target.Color, mode);

			return state;
		}

		public void Draw(IEntityState _state)
		{
			State state = _state as State;
			Drawing.Circle(state.Location, state.Size, state.Color);
		}

		public void DrawEditable(IEntityState _state)
		{
			State state = _state as State;

			PointF loc = new PointF(state.Location.X - 2, state.Location.Y - 2);
			Drawing.Rectangle(loc, new Size(4, 4), Color.Red);
			Drawing.RectangleLine(loc, new Size(4, 4), Color.White);
		}

		public class ManipulateParams : IManipulatableParams
		{
			public bool AbsoluteDrag;
			public PointF AbsoluteOffset;
		}

		public void SetColor(IEntityState _state, Color color)
		{
			State state = _state as State;

			state.Color = color;
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
				mparams.AbsoluteOffset = new PointF(location.X - state.Location.X, location.Y - state.Location.Y);
			}
			else
			{
				if (!MathUtil.IsPointInPoint(location, state.Location, 4))
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
			if (!Program.ActiveProject.LayerCount.ContainsKey(typeof(CircleObject)))
				Program.ActiveProject.LayerCount.Add(typeof(CircleObject), 0);

			int CreatedLayerCount = ++Program.ActiveProject.LayerCount[typeof(CircleObject)];

			Layer layer = new Layer(this);
			layer.Name = "Circle " + CreatedLayerCount;

			layer.Framesets.Add(new Frameset());
			layer.Framesets[0].Keyframes.Add(new Keyframe(StartTime, CreateRefState(), EntityInterpolationMode.Linear));
			layer.Framesets[0].Keyframes.Add(new Keyframe(EndTime, CreateRefState(), EntityInterpolationMode.Linear));

			return layer;
		}

		public IEntityState CreateRefState()
		{
			return new State()
			{
				Location = new PointF(200, 200),
				Size = 32,
				Color = Color.Black
			};
		}

		public void Write(BinaryWriter writer)
		{
			// Nothing to see here, folks
		}

		public void Read(BinaryReader reader, UInt16 version)
		{
			// Keep it moving
		}
	}
}
