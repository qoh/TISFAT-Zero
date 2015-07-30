using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using TISFAT.Util;

namespace TISFAT.Entities
{
    class StickFigure : IEntity
    {
        public class Joint
        {
            public class State
            {
                public State Parent;
                public List<State> Children;

                public PointF Location;
                public Color JointColor;
                public float Thickness;

                public State(State parent=null)
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
            }

            public Joint Parent;
            public List<Joint> Children;

            public PointF Location;
            public Color JointColor;
            public float Thickness;

            public Joint(Joint parent=null)
            {
                Parent = parent;
                Children = new List<Joint>();
            }

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
        }

        public class State : IEntityState
        {
            public Joint.State Root;

            public State() { }
            public void Write(BinaryWriter reader) { }
            public void Read(BinaryReader reader, UInt16 version) { }
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

        public void Write(BinaryWriter reader) { }
        public void Read(BinaryReader reader, UInt16 version) { }
    }
}
