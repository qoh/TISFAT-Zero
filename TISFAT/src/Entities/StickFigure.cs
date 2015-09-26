using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TISFAT.Util;

namespace TISFAT.Entities
{
	public partial class StickFigure : IEntity
	{
		public Joint Root;

		public StickFigure() { }

		public IEntityState Interpolate(float t, IEntityState _current, IEntityState _target, EntityInterpolationMode mode)
		{
			State current = _current as State;
			State target = _target as State;
			State state = new State();
			state.Root = Joint.State.Interpolate(t, current.Root, target.Root, mode);
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

			public bool PivotDrag;
			public float PivotLength;
			public float PivotAngle;

            public Dictionary<Joint.State, Tuple<float, float>> PivotInfo = new Dictionary<Joint.State, Tuple<float, float>>();

			public bool AbsoluteDrag;
			public PointF AbsoluteOffset;
		}

		public void SetColor(IEntityState _state, Color color)
		{
			State state = _state as State;

			state.Root.SetColor(color, null);
		}

		public ManipulateResult TryManipulate(IEntityState _state, Point location, MouseButtons button, Keys modifiers)
		{
			State state = _state as State;

			if (state == null)
				return null;

			ManipulateResult result = new ManipulateResult();
			ManipulateParams mparams = new ManipulateParams();
			result.Params = mparams;

			if (modifiers == Keys.Shift)
				mparams.PivotDrag = true;
			else if (modifiers == Keys.Alt)
				mparams.DisableIK = true;

			if (button == MouseButtons.Right)
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

				if (mparams.PivotDrag && target.Parent != null)
				{
					mparams.PivotLength = (float)MathUtil.Length(target.Location, target.Parent.Location);
					mparams.PivotAngle = (float)MathUtil.Angle(target.Location, target.Parent.Location);

					// you need to go through every descendant (not child) of target here and store their angle and distance to target's parent
					mparams.PivotInfo = new Dictionary<Joint.State, Tuple<float, float>>();
					Stack<Joint.State> open = new Stack<Joint.State>();
					open.Push(target);

					while (open.Count > 0)
					{
						Joint.State current = open.Pop();

						foreach (Joint.State child in current.Children)
						{
							open.Push(child);

							float distance = (float)MathUtil.Length(target.Parent.Location, child.Location);
							float angle = (float)MathUtil.Angle(child.Location, target.Parent.Location); // or the other way around, depends

							mparams.PivotInfo[child] = new Tuple<float, float>(distance, angle);
						}
					}
				}

				result.Target = target;
				mparams.AbsoluteDrag = false;
			}

			return result;
		}

		public ManipulateResult TryManipulate(IEntityState _state, Point location, System.Windows.Forms.MouseButtons button, System.Windows.Forms.Keys modifiers, bool fromEditor)
		{
			State state = _state as State;

			if (state == null)
				return null;

			ManipulateResult result = new ManipulateResult();
			ManipulateParams mparams = new ManipulateParams();
			result.Params = mparams;

			if (fromEditor)
				mparams.DisableIK = true;

			if (button == MouseButtons.Right)
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
			target.Move(location, (ManipulateParams)mparams, null);
		}

		public void ManipulateEnd(IManipulatable _target, IManipulatableParams mparams, Point location)
		{

		}

		public virtual Layer CreateDefaultLayer(uint StartTime, uint EndTime, LayerCreationArgs e)
		{
			if (e.Variant == 0)
			{
				var hip = new Joint();
				hip.Location = new PointF(200, 200);
				int ID = 0;
				Root = hip;
				Root.ID = 0;
				var shoulder = Joint.RelativeTo(hip, new PointF(0, -53), ref ID);
				var lElbow = Joint.RelativeTo(shoulder, new PointF(-21, 22), ref ID);
				var lHand = Joint.RelativeTo(lElbow, new PointF(-5, 35), ref ID);
				var rElbow = Joint.RelativeTo(shoulder, new PointF(21, 22), ref ID);
				var rHand = Joint.RelativeTo(rElbow, new PointF(5, 35), ref ID);
				var lKnee = Joint.RelativeTo(hip, new PointF(-16, 33), ref ID);
				var lFoot = Joint.RelativeTo(lKnee, new PointF(-5, 41), ref ID);
				var rKnee = Joint.RelativeTo(hip, new PointF(16, 33), ref ID);
				var rFoot = Joint.RelativeTo(rKnee, new PointF(5, 41), ref ID);
				var head = Joint.RelativeTo(shoulder, new PointF(0, -36), ref ID);

				shoulder.HandleColor = Color.Yellow;
				hip.HandleColor = Color.Yellow;
				rElbow.HandleColor = Color.Red;
				rHand.HandleColor = Color.Red;
				rKnee.HandleColor = Color.Red;
				rFoot.HandleColor = Color.Red;

				head.HandleColor = Color.Yellow;
				head.DrawType = DrawJointType.CircleLine;
			}
			else if (e.Variant == 1)
			{
				var shoulder = new Joint();
				shoulder.Location = new PointF(200, 200);
				int ID = 0;
				Root = shoulder;
				Root.ID = 0;
				var neck = Joint.RelativeTo(shoulder, new PointF(0, -3), ref ID);
				var lElbow = Joint.RelativeTo(shoulder, new PointF(-16, 11), ref ID);
				var lHand = Joint.RelativeTo(lElbow, new PointF(-8, 18), ref ID);
				var rElbow = Joint.RelativeTo(shoulder, new PointF(16, 11), ref ID);
				var rHand = Joint.RelativeTo(rElbow, new PointF(8, 18), ref ID);
				var hip = Joint.RelativeTo(shoulder, new PointF(0, 40), ref ID);
				var lKnee = Joint.RelativeTo(hip, new PointF(-11, 23), ref ID);
				var lFoot = Joint.RelativeTo(lKnee, new PointF(-9, 23), ref ID);
				var rKnee = Joint.RelativeTo(hip, new PointF(11, 23), ref ID);
				var rFoot = Joint.RelativeTo(rKnee, new PointF(9, 23), ref ID);
				var head = Joint.RelativeTo(neck, new PointF(0, -8), ref ID);

				shoulder.HandleColor = Color.Yellow;
				hip.HandleColor = Color.Yellow;
				rElbow.HandleColor = Color.Red;
				rHand.HandleColor = Color.Red;
				rKnee.HandleColor = Color.Red;
				rFoot.HandleColor = Color.Red;

				head.HandleColor = Color.Yellow;
				head.Thickness = 13;
				head.DrawType = DrawJointType.CircleRadius;

				neck.HandleVisible = false;
				neck.Manipulatable = false;
			}
			else
				throw new ArgumentException("Invalid variant provided for stick figure");

			if (!Program.ActiveProject.LayerCount.ContainsKey(typeof(StickFigure)))
				Program.ActiveProject.LayerCount.Add(typeof(StickFigure), 0);

			int CreatedLayerCount = ++Program.ActiveProject.LayerCount[typeof(StickFigure)];

			Layer layer = new Layer(this);
			layer.Name = "Figure " + CreatedLayerCount;

			layer.Framesets.Add(new Frameset());
			layer.Framesets[0].Keyframes.Add(new Keyframe(StartTime, CreateRefState(), Util.EntityInterpolationMode.Linear));
			layer.Framesets[0].Keyframes.Add(new Keyframe(EndTime, CreateRefState(), Util.EntityInterpolationMode.Linear));

			return layer;
		}

		public int GetJointCount()
		{
			int count = Root.Children.Count + 1;

			foreach(Joint child in Root.Children)
			{
				count += child.GetJointCount();
			}

			return count;
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
