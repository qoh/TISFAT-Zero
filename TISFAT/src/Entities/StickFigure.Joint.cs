using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using TISFAT.Util;

namespace TISFAT.Entities
{
	public enum DrawJointType
	{
		Normal,
		CircleLine,
		CircleRadius
	}

    partial class StickFigure
    {
        public partial class Joint : ISaveable
        {
            public Joint Parent;
            public List<Joint> Children;

            public PointF Location;
            public Color JointColor;
            public Color HandleColor;
            public float Thickness;

			public DrawJointType DrawType;
			public bool HandleVisible;
			public bool Manipulatable;
			public bool Visible;

            #region Constructors
            public Joint()
            {
                Parent = null;
                Children = new List<Joint>();
                Thickness = 6;
                JointColor = Color.Black;
                HandleColor = Color.Blue;

				HandleVisible = true;
				Visible = true;
				Manipulatable = true;
			}

            public Joint(Joint parent)
            {
                Parent = parent;
                Children = new List<Joint>();
                Thickness = 6;
                JointColor = Color.Black;
                HandleColor = Color.Blue;

				HandleVisible = true;
				Visible = true;
				Manipulatable = true;
			}

            public static Joint RelativeTo(Joint parent, PointF location)
            {
                if (parent == null) throw new NullReferenceException("wat");
                Joint joint = new Joint(parent);
                joint.Location = new PointF(parent.Location.X + location.X, parent.Location.Y + location.Y);
                parent.Children.Add(joint);
                return joint;
            }
            #endregion

            #region Drawing
            public void Draw(State state)
            {
                //if (Parent != null)
                //    Drawing.CappedLine(state.Location, state.Parent.Location, state.Thickness, state.JointColor);

                if (Children.Count != state.Children.Count)
                    throw new ArgumentException("State does not match this Joint");

                for (var i = 0; i < Children.Count; i++)
                {
                    Children[i].DrawTo(state.Children[i], this, state);
                    Children[i].Draw(state.Children[i]);
                }
            }

            public void DrawTo(State state, Joint otherJoint, Joint.State otherState)
            {
				if (!Visible)
					return;
				
                if (DrawType == DrawJointType.CircleLine)
                {
                    float dx = state.Location.X - otherState.Location.X;
                    float dy = state.Location.Y - otherState.Location.Y;
                    float r = (float)Math.Sqrt((double)(dx * dx + dy * dy)) / 2;

                    float x = otherState.Location.X + dx / 2;
                    float y = otherState.Location.Y + dy / 2;

                    Drawing.Circle(new PointF(x, y), r, state.JointColor);
                }
				else if (DrawType == DrawJointType.CircleRadius)
				{
					float r = state.Thickness;
					float x = state.Location.X;
					float y = state.Location.Y;

					Drawing.Circle(new PointF(x, y), r, state.JointColor, 5 * (int)Math.Sqrt(r));
				}
                else
                    Drawing.CappedLine(state.Location, otherState.Location, state.Thickness, state.JointColor);
            }

            public void DrawHandle(State state)
            {
                PointF Loc = new PointF(state.Location.X - 3, state.Location.Y - 3);
                SizeF Siz = new SizeF(6, 6);

				if (HandleVisible)
				{
					Drawing.Rectangle(Loc, Siz, HandleColor);
					Drawing.RectangleLine(Loc, Siz, Color.FromArgb(127, 255 - JointColor.R, 255 - JointColor.G, 255 - JointColor.B));
				}

                for (var i = 0; i < Children.Count; i++)
                {
                    Children[i].DrawHandle(state.Children[i]);
                }
            }
            #endregion

            public State Copy(State parent = null)
            {
                State state = new State(parent);
                state.Location = Location;
                state.Thickness = Thickness;
                state.JointColor = JointColor;
				state.Manipulatable = Manipulatable;

				foreach (Joint child in Children)
                    state.Children.Add(child.Copy(state));

                return state;
            }

            #region File Saving / Loading
            public void Write(BinaryWriter writer)
            {
                writer.Write((double)Location.X);
                writer.Write((double)Location.Y);
                writer.Write(JointColor.A);
                writer.Write(JointColor.R);
                writer.Write(JointColor.G);
                writer.Write(JointColor.B);
                writer.Write(HandleColor.A);
                writer.Write(HandleColor.R);
                writer.Write(HandleColor.G);
                writer.Write(HandleColor.B);
                writer.Write((double)Thickness);
                writer.Write(DrawType.ToString());
				writer.Write(HandleVisible);
				writer.Write(Manipulatable);
				writer.Write(Visible);
                FileFormat.WriteList(writer, Children);
            }

            public void Read(BinaryReader reader, UInt16 version)
            {
                float x = (float)reader.ReadDouble();
                float y = (float)reader.ReadDouble();
                Location = new PointF(x, y);
                byte joint_a = reader.ReadByte();
                byte joint_r = reader.ReadByte();
                byte joint_g = reader.ReadByte();
                byte joint_b = reader.ReadByte();
                JointColor = Color.FromArgb(joint_a, joint_r, joint_g, joint_b);
                byte handle_a = reader.ReadByte();
                byte handle_r = reader.ReadByte();
                byte handle_g = reader.ReadByte();
                byte handle_b = reader.ReadByte();
                HandleColor = Color.FromArgb(handle_a, handle_r, handle_g, handle_b);
                Thickness = (float)reader.ReadDouble();

				if (version >= 2)
				{
					DrawType = (DrawJointType)Enum.Parse(typeof(DrawJointType), reader.ReadString());

					HandleVisible = reader.ReadBoolean();
					Manipulatable = reader.ReadBoolean();
					Visible = reader.ReadBoolean();
				}
				else
				{
					DrawType = reader.ReadBoolean() ? DrawJointType.CircleLine : DrawJointType.Normal;

					HandleVisible = true;
					Manipulatable = true;
					Visible = true;
				}

                Children = FileFormat.ReadList<Joint>(reader, version);

                foreach (Joint child in Children)
                    child.Parent = this;
            }
            #endregion
        }
    }
}
