using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using TISFAT.Util;

namespace TISFAT.Entities
{
    class StickFigure : IEntity
    {
        public class Joint : ISaveable
        {
            public class State : ISaveable
            {
                public State Parent;
                public List<State> Children;

                public PointF Location;
                public Color JointColor;
                public float Thickness;

                public State()
                {
                    Parent = null;
                    Children = new List<State>();
                    Location = new PointF();
                    JointColor = Color.Black;
                    Thickness = 6;
                }

                public State(State parent)
                {
                    Parent = parent;
                    Children = new List<State>();
                    Location = new PointF();
                    JointColor = Color.Black;
                    Thickness = 6;
                }

                public static State Interpolate(float t, State current, State target)
                {
                    State state = new State(current.Parent);
                    state.Location = Interpolation.Linear(t, current.Location, target.Location);
                    state.JointColor = current.JointColor;
                    state.Thickness = Interpolation.Linear(t, current.Thickness, target.Thickness);

                    for (var i = 0; i < current.Children.Count; i++)
                    {
                        state.Children.Add(Interpolate(t, current.Children[i], target.Children[i]));
                    }

                    return state;
                }

                public void Write(BinaryWriter writer)
                {
                    writer.Write((double)Location.X);
                    writer.Write((double)Location.Y);
                    writer.Write(JointColor.A);
                    writer.Write(JointColor.R);
                    writer.Write(JointColor.G);
                    writer.Write(JointColor.B);
                    writer.Write((double)Thickness);
                    FileFormat.WriteList(writer, Children);
                }

                public void Read(BinaryReader reader, UInt16 version)
                {
                    float x = (float)reader.ReadDouble();
                    float y = (float)reader.ReadDouble();
                    Location = new PointF(x, y);
                    byte a = reader.ReadByte();
                    byte r = reader.ReadByte();
                    byte g = reader.ReadByte();
                    byte b = reader.ReadByte();
                    JointColor = Color.FromArgb(a, r, g, b);
                    Thickness = (float)reader.ReadDouble();

                    Children = FileFormat.ReadList<Joint.State>(reader, version);

                    foreach (Joint.State child in Children)
                        child.Parent = this;
                }
            }

            public Joint Parent;
            public List<Joint> Children;

            public PointF Location;
            public Color JointColor;
            public Color HandleColor;
            public float Thickness;

            #region Constructors
            public Joint()
            {
                Parent = null;
                Children = new List<Joint>();
                JointColor = Color.Blue;
            }

            public Joint(Joint parent)
            {
                Parent = parent;
                Children = new List<Joint>();
                JointColor = Color.Blue;
            } 
            #endregion

            #region Drawing
            public void Draw(State state)
            {
                if (Parent != null)
                    Drawing.CappedLine(state.Location, state.Parent.Location, state.Thickness, state.JointColor);

                if (Children.Count != state.Children.Count)
                    throw new ArgumentException("State does not match this Joint");

                for (var i = 0; i < Children.Count; i++)
                {
                    Children[i].Draw(state.Children[i]);
                }
            }

            public void DrawHandle(State state)
            {
                PointF Loc = new PointF(state.Location.X - 4, state.Location.Y - 4);
                SizeF Siz = new SizeF(6, 6);

                Drawing.Rectangle(Loc, Siz, JointColor);
                Drawing.RectangleLine(Loc, Siz, Color.Black);

                for (var i = 0; i < Children.Count; i++)
                {
                    Children[i].DrawHandle(state.Children[i]);
                }
            }
            #endregion

            public bool JointAtLocation(State state, Point _location)
            {
                if (state.Location.X + 4 > _location.X &&
                    state.Location.X - 4 < _location.X &&
                    state.Location.Y + 4 > _location.Y &&
                    state.Location.Y - 4 < _location.Y)
                    return true;

                for(var i = 0; i < Children.Count; i++)
                {
                    if (Children[i].JointAtLocation(state.Children[i], _location))
                        return true;
                }

                return false;
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

                Children = FileFormat.ReadList<Joint>(reader, version);

                foreach (Joint child in Children)
                    child.Parent = this;
            } 
            #endregion
        }

        public class State : IEntityState, ISaveable
        {
            public Joint.State Root;

            public State() { }

            public void Write(BinaryWriter writer)
            {
                Root.Write(writer);
            }

            public void Read(BinaryReader reader, UInt16 version)
            {
                Root = new Joint.State();
                Root.Read(reader, version);
            }
        }

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

        public bool TryManipulate(IEntityState _state, Point location)
        {
            State state = _state as State;
            return Root.JointAtLocation(state.Root, location);
        }

        public void ManipulateStart(IEntityState state, Point location)
        {
            
        }

        public void ManipulateUpdate(IEntityState state, Point location)
        {
            
        }

        public void ManipulateEnd(IEntityState state, Point location)
        {
            
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
