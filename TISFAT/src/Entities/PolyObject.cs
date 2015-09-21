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
	public partial class PolyObject : IEntity
	{
		public int Sides;

		public PolyObject() { }

		public IEntityState Interpolate(float t, IEntityState _current, IEntityState _target, EntityInterpolationMode mode)
		{
			State current = _current as State;
			State target = _target as State;
			State state = new State();

			state.Points = new List<Joint>();

			for (int i = 0; i < current.Points.Count; i++)
			{
				state.Points.Add(current.Points[i].Copy());
				state.Points[i].Location = Interpolation.Interpolate(t, current.Points[i].Location, target.Points[i].Location, mode);
			}
				
			state.Color = Interpolation.Interpolate(t, current.Color, target.Color, mode);

			return state;
		}

		public void Draw(IEntityState _state)
		{
			State state = _state as State;
			Drawing.DrawPoly(state.Points.ToArray(), state.Color);
		}

		public void DrawEditable(IEntityState _state)
		{
			State state = _state as State;

			foreach (Joint point in state.Points)
			{
				PointF loc = new PointF(point.Location.X - 2, point.Location.Y - 2);
				Drawing.Rectangle(loc, new Size(4, 4), Color.Red);
				Drawing.RectangleLine(loc, new Size(4, 4), Color.White);
			}
		}

		public class ManipulateParams : IManipulatableParams
		{
			public bool AbsoluteDrag;
			public List<PointF> AbsoluteOffset;

			public int PointIndex;
		}

		public ManipulateResult TryManipulate(IEntityState _state, Point location, System.Windows.Forms.MouseButtons button, System.Windows.Forms.Keys modifiers)
		{
			State state = _state as State;

			if (state == null)
				return null;

			ManipulateResult result = new ManipulateResult();
			ManipulateParams mparams = new ManipulateParams();
			result.Params = mparams;

			mparams.AbsoluteOffset = new List<PointF>();

			if (button == System.Windows.Forms.MouseButtons.Right)
			{
				result.Target = state;
				mparams.AbsoluteDrag = true;

				for(int i = 0; i < state.Points.Count; i++)
					mparams.AbsoluteOffset.Add(new PointF(location.X - state.Points[i].X, location.Y - state.Points[i].Y));
			}
			else
			{
				for (int i = 0; i < state.Points.Count; i++)
				{
					if (MathUtil.IsPointInPoint(location, state.Points[i], 4))
					{
						result.Target = state.Points[i];
						mparams.PointIndex = i;
					}
				}

				if (result.Target == null)
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
			if (!Program.ActiveProject.LayerCount.ContainsKey(typeof(PolyObject)))
				Program.ActiveProject.LayerCount.Add(typeof(PolyObject), 0);

			int CreatedLayerCount = ++Program.ActiveProject.LayerCount[typeof(PolyObject)];

			Sides = e.Variant;

			Layer layer = new Layer(this);
			layer.Name = "Poly " + CreatedLayerCount;

			layer.Framesets.Add(new Frameset());
			layer.Framesets[0].Keyframes.Add(new Keyframe(StartTime, CreateRefState(), EntityInterpolationMode.Linear));
			layer.Framesets[0].Keyframes.Add(new Keyframe(EndTime, CreateRefState(), EntityInterpolationMode.Linear));

			return layer;
		}

		public IEntityState CreateRefState()
		{
			double max = 2 * Math.PI;
			double delta = 2 * Math.PI / Sides;

			List<Joint> Points = new List<Joint>();

			for (double i = 0.0f; i < max; i += delta)
			{
				float x = (float)Math.Sin(i) * 50;
				float y = (float)Math.Cos(i) * 50;

				Points.Add(new PointF(x, y));
			}

			PointF oldLocation = Points[0];
			Points[0] = new PointF(200, 200);

			for (int i = 1; i < Points.Count; i++)
				Points[i] = new PointF(
					Points[0].X + (Points[i].X - oldLocation.X),
					Points[0].Y + (Points[i].Y - oldLocation.Y));

			return new State()
			{
				Points = Points,
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
