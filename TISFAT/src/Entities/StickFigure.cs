using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TISFAT.Entities
{
	partial class StickFigure : IEntity
	{
		public Joint Root;

		public StickFigure() { }
		
		public IEntityState Interpolate(float t, IEntityState _current, IEntityState _target)
		{
			State current = _current as State;
			State target = _target as State;
			State state = new State();
			state.Root = Joint.State.Interpolate(t, current.Root, target.Root);
			return state;
		}

		public void Draw(IEntityState _state)
		{
			State state = _state as State;
			Root.Draw(state.Root);
		}

		public void DrawEditable(IEntityState _state)
		{
			State state = _state as State;
			Root.DrawHandle(state.Root);
		}

		public class ManipulateParams : IManipulatableParams
		{
			public bool DisableIK;
			public bool AbsoluteDrag;
			public PointF AbsoluteOffset;
		}
		
		public ManipulateResult TryManipulate(IEntityState _state, Point location, MouseButtons button, Keys modifiers)
		{
			State state = _state as State;

			ManipulateResult result = new ManipulateResult();
			ManipulateParams mparams = new ManipulateParams();
			result.Params = mparams;

			if (modifiers == Keys.Shift)
				mparams.DisableIK = true;

			if (button == System.Windows.Forms.MouseButtons.Right)
			{
				result.Target = state.Root;
				mparams.AbsoluteDrag = true;
				mparams.AbsoluteOffset = new PointF(location.X - state.Root.Location.X, location.Y - state.Root.Location.Y);
			}
			else
			{
				Joint.State target = state.Root.JointAtLocation(location);

				if (target == null)
					return null;

				result.Target = target;
				mparams.AbsoluteDrag = false;
			}

			return result;
		}

		public void ManipulateStart(IManipulatable _target, IManipulatableParams mparams, Point location)
		{
			
		}
		
		public void ManipulateUpdate(IManipulatable _target, IManipulatableParams mparams, Point location)
		{
			Joint.State target = _target as Joint.State;
			//target.Location = location;
			target.Move(location, (ManipulateParams)mparams, null);
		}

		public void ManipulateEnd(IManipulatable _target, IManipulatableParams mparams, Point location)
		{
			
		}

		public Layer CreateDefaultLayer(uint StartTime, uint EndTime, LayerCreationArgs e)
		{
			switch (e.Variant)
			{
				case 0:
					var hip = new StickFigure.Joint();
					hip.Location = new PointF(200, 200);
					this.Root = hip;
					var shoulder = StickFigure.Joint.RelativeTo(hip, new PointF(0, -53));
					var lElbow = StickFigure.Joint.RelativeTo(shoulder, new PointF(-21, 22));
					var lHand = StickFigure.Joint.RelativeTo(lElbow, new PointF(-5, 35));
					var rElbow = StickFigure.Joint.RelativeTo(shoulder, new PointF(21, 22));
					var rHand = StickFigure.Joint.RelativeTo(rElbow, new PointF(5, 35));
					var lKnee = StickFigure.Joint.RelativeTo(hip, new PointF(-16, 33));
					var lFoot = StickFigure.Joint.RelativeTo(lKnee, new PointF(-5, 41));
					var rKnee = StickFigure.Joint.RelativeTo(hip, new PointF(16, 33));
					var rFoot = StickFigure.Joint.RelativeTo(rKnee, new PointF(5, 41));
					var head = StickFigure.Joint.RelativeTo(shoulder, new PointF(0, -36));

					shoulder.HandleColor = Color.Yellow;
					hip.HandleColor = Color.Yellow;
					rElbow.HandleColor = Color.Red;
					rHand.HandleColor = Color.Red;
					rKnee.HandleColor = Color.Red;
					rFoot.HandleColor = Color.Red;

					head.HandleColor = Color.Yellow;
					head.IsCircle = true;
					break;
				default:
					throw new ArgumentException("Invalid variant provided for stick figure");
			}

			if (!Program.Form.ActiveProject.LayerCount.ContainsKey(typeof(StickFigure)))
				Program.Form.ActiveProject.LayerCount.Add(typeof(StickFigure), 0);

			int CreatedLayerCount = ++Program.Form.ActiveProject.LayerCount[typeof(StickFigure)];

			Layer layer = new Layer(this);
			layer.Name = "Figure " + CreatedLayerCount;

			layer.Framesets.Add(new Frameset());
			layer.Framesets[0].Keyframes.Add(new Keyframe(StartTime, CreateRefState()));
			layer.Framesets[0].Keyframes.Add(new Keyframe(EndTime, CreateRefState()));

			return layer;
		}

		public IEntityState CreateRefState()
		{
			State state = new State();
			state.Root = Root.Copy();
			return state;
		}

		#region File Saving / Loading
		public void Write(BinaryWriter writer)
		{
			Root.Write(writer);
		}
		public void Read(BinaryReader reader, UInt16 version)
		{
			Root = new Joint();
			Root.Read(reader, version);
		} 
		#endregion
	}
}
