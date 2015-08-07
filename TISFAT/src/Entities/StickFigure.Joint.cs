using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using TISFAT.Util;

namespace TISFAT.Entities
{
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

            public bool IsCircle;

            #region Constructors
            public Joint()
            {
                Parent = null;
                Children = new List<Joint>();
                Thickness = 6;
                JointColor = Color.Black;
                HandleColor = Color.Blue;
            }

            public Joint(Joint parent)
            {
                Parent = parent;
                Children = new List<Joint>();
                Thickness = 6;
                JointColor = Color.Black;
                HandleColor = Color.Blue;
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
                if (IsCircle)
                {
                    float dx = state.Location.X - otherState.Location.X;
                    float dy = state.Location.Y - otherState.Location.Y;
                    float r = (float)Math.Sqrt((double)(dx * dx + dy * dy)) / 2;

                    float x = otherState.Location.X + dx / 2;
                    float y = otherState.Location.Y + dy / 2;

                    Drawing.Circle(new PointF(x, y), r, state.JointColor);
                }
                else
                    Drawing.CappedLine(state.Location, otherState.Location, state.Thickness, state.JointColor);
            }

            public void DrawHandle(State state)
            {
                PointF Loc = new PointF(state.Location.X - 3, state.Location.Y - 3);
                SizeF Siz = new SizeF(6, 6);

                Drawing.Rectangle(Loc, Siz, HandleColor);
                Drawing.RectangleLine(Loc, Siz, Color.FromArgb(255 - JointColor.R, 255 - JointColor.G, 255 - JointColor.B));

                for (var i = 0; i < Children.Count; i++)
                {
                    Children[i].DrawHandle(state.Children[i]);
                }
            }
            #endregion

            public State CreateRefState(State parent = null)
            {
                State state = new State(parent);
                state.Location = Location;
                state.Thickness = Thickness;
                state.JointColor = JointColor;

                foreach (Joint child in Children)
                    state.Children.Add(child.CreateRefState(state));

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
                writer.Write(IsCircle);
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
                IsCircle = reader.ReadBoolean();

                Children = FileFormat.ReadList<Joint>(reader, version);

                foreach (Joint child in Children)
                    child.Parent = this;
            }
            #endregion
        }
    }
}
