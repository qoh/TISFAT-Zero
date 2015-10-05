using System;
using System.Drawing;
using System.IO;
using TISFAT.Util;

namespace TISFAT.Entities
{
	public partial class PointLight : IEntity
	{
		public PointLight() { }

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
			state.LightRadius = Interpolation.Interpolate(t, current.LightRadius, target.LightRadius, mode);
			state.LightColor = Interpolation.Interpolate(t, current.LightColor, target.LightColor, mode);
			state.LightAttenuation = Interpolation.Interpolate(t, current.LightAttenuation, target.LightAttenuation, mode);

			return state;
		}

		public void Draw(IEntityState _state)
		{
			State state = _state as State;
			Drawing.PointLight(state.Location, state.LightColor, state.LightAttenuation, state.LightRadius);
		}

		private void DrawHandle(State state, Color c)
		{

		}

		public void DrawEditable(IEntityState _state)
		{

		}

		public class ManipulateParams : IManipulatableParams
		{
			public bool AbsoluteDrag;
			public PointF AbsoluteOffset;
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
			if (!Program.ActiveProject.LayerCount.ContainsKey(typeof(PointLight)))
				Program.ActiveProject.LayerCount.Add(typeof(PointLight), 0);

			int CreatedLayerCount = ++Program.ActiveProject.LayerCount[typeof(PointLight)];

			Layer layer = new Layer(this);
			layer.Name = "Point Light " + CreatedLayerCount;

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
				LightRadius = 50.0f,
				LightAttenuation = new OpenTK.Vector3(1, 1, 1),
				LightColor = Color.Aqua
			};
		}

		public void Write(BinaryWriter writer)
		{

		}

		public void Read(BinaryReader reader, UInt16 version)
		{

		}

	}
}
