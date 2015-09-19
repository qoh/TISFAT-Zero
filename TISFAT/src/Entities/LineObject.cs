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
	public partial class LineObject : IEntity
	{
		public LineObject() { }

		public IEntityState Interpolate(float t, IEntityState _current, IEntityState _target, EntityInterpolationMode mode)
		{
			State current = _current as State;
			State target = _target as State;
			State state = new State();

			state.Handle1 = Interpolation.Interpolate(t, current.Handle1, target.Handle1, mode);
			state.Handle2 = Interpolation.Interpolate(t, current.Handle2, target.Handle2, mode);
			state.Thickness = Interpolation.Interpolate(t, current.Thickness, target.Thickness, mode);
			state.Color = Interpolation.Interpolate(t, current.Color, target.Color, mode);

			return state;
		}

		public void Draw(IEntityState _state)
		{
			State state = _state as State;
			Drawing.CappedLine(state.Handle1, state.Handle2, state.Thickness, state.Color);
		}

		public void DrawEditable(IEntityState _state)
		{
			State state = _state as State;

			PointF Loc1 = new PointF(state.Handle1.X - 3, state.Handle1.Y - 3);
			PointF Loc2 = new PointF(state.Handle2.X - 3, state.Handle2.Y - 3);
			SizeF Siz = new SizeF(6, 6);

			Drawing.Rectangle(Loc1, Siz, Color.Blue);
			Drawing.RectangleLine(Loc1, Siz, Color.FromArgb(127, 255 - state.Color.R, 255 - state.Color.G, 255 - state.Color.B));
			Drawing.Rectangle(Loc2, Siz, Color.Red);
			Drawing.RectangleLine(Loc2, Siz, Color.FromArgb(127, 255 - state.Color.R, 255 - state.Color.G, 255 - state.Color.B));
		}

		public class ManipulateParams : IManipulatableParams
		{
			public bool AbsoluteDrag;
			public PointF Handle1Offset;
			public PointF Handle2Offset;

			public int HandleGrabbed;
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
				mparams.Handle1Offset = new PointF(location.X - state.Handle1.X, location.Y - state.Handle1.Y);
				mparams.Handle2Offset = new PointF(location.X - state.Handle2.X, location.Y - state.Handle2.Y);
			}
			else
			{
				mparams.HandleGrabbed = state.HandleAtLocation(location);

				if (mparams.HandleGrabbed == -1)
					return null;

				result.Target = state;
				mparams.AbsoluteDrag = false;
			}

			return result;
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
			if (!Program.ActiveProject.LayerCount.ContainsKey(typeof(LineObject)))
				Program.ActiveProject.LayerCount.Add(typeof(LineObject), 0);

			int CreatedLayerCount = ++Program.ActiveProject.LayerCount[typeof(LineObject)];

			Layer layer = new Layer(this);
			layer.Name = "Line " + CreatedLayerCount;

			layer.Framesets.Add(new Frameset());
			layer.Framesets[0].Keyframes.Add(new Keyframe(StartTime, CreateRefState(), EntityInterpolationMode.Linear));
			layer.Framesets[0].Keyframes.Add(new Keyframe(EndTime, CreateRefState(), EntityInterpolationMode.Linear));

			return layer;
		}

		public IEntityState CreateRefState()
		{
			return new State()
			{
				Color = Color.Black,
				Thickness = 6,
				Handle1 = new PointF(150, 210),
				Handle2 = new PointF(350, 210)
			};
		}

		public void Write(BinaryWriter writer)
		{
			// Nothing to save here, folks.
		}

		public void Read(BinaryReader reader, UInt16 version)
		{
			// Nothing to load here, folks.
		}
	}
}
