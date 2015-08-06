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
            public class State : ISaveable, IManipulatable
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

                public State JointAtLocation(PointF location)
                {
                    if (
                        Location.X + 4 > location.X &&
                        Location.X - 4 < location.X &&
                        Location.Y + 4 > location.Y &&
                        Location.Y - 4 < location.Y
                    )
                        return this;

                    foreach (State state in Children)
                    {
                        State found = state.JointAtLocation(location);

                        if (found != null)
                            return found;
                    }

                    return null;
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
            
            public void Move(object _state, Point location)
            {
                State state = _state as State;

                state.Location = location;
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

        public IManipulatable TryManipulate(IEntityState _state, Point location)
        {
            State state = _state as State;
            return state.Root.JointAtLocation(location);
        }

        public void ManipulateStart(IManipulatable _target, Point location)
        {
            
        }
        
        public void ManipulateUpdate(IManipulatable _target, Point location)
        {
            Joint.State target = _target as Joint.State;
            target.Location = location;
        }

        public void ManipulateEnd(IManipulatable _target, Point location)
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
