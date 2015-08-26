using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using TISFAT.Util;

namespace TISFAT.Entities
{
    partial class StickFigure
    {
        public partial class Joint
        {
            public class State : ISaveable, IManipulatable
            {
                public State Parent;
                public List<State> Children;

                public PointF Location;
                public Color JointColor;
                public float Thickness;
				
				public bool Manipulatable;

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

                public void Move(PointF target, ManipulateParams mparams, State from)
                {
                    List<State> affected = new List<State>();

                    if (Parent != null && from != Parent)
                        affected.Add(Parent);

                    foreach (State child in Children)
                    {
                        if (child != from)
                            affected.Add(child);
                    }

                    if (mparams.AbsoluteDrag && from == null)
                    {
                        target = new PointF(
                            target.X - mparams.AbsoluteOffset.X,
                            target.Y - mparams.AbsoluteOffset.Y);
                    }

                    float nx = target.X;
                    float ny = target.Y;

                    float ox = Location.X;
                    float oy = Location.Y;

                    Location = target;

                    if (mparams.DisableIK)
                        return;

                    foreach (State state in affected)
                    {
                        PointF loc;

                        if (mparams.AbsoluteDrag)
                        {
                            loc = new PointF(
                                state.Location.X + (nx - ox),
                                state.Location.Y + (ny - oy)
                            );
                        }
                        else
                        {
                            float jx = state.Location.X;
                            float jy = state.Location.Y;

                            float dx = ox - jx;
                            float dy = oy - jy;
                            float dm = (float)Math.Sqrt((double)(dx * dx + dy * dy));

                            float lx = jx - nx;
                            float ly = jy - ny;
                            float lm = (float)Math.Sqrt((double)(lx * lx + ly * ly));

                            loc = new PointF(nx + (lx / lm) * dm, ny + (ly / lm) * dm);
                        }

                        state.Move(loc, mparams, this);
                    }
                }

                public State JointAtLocation(PointF location)
                {
                    if (MathUtil.IsPointInPoint(location, Location, 4) && Manipulatable)
                        return this;

                    foreach (State state in Children)
                    {
                        State found = state.JointAtLocation(location);

                        if (found != null)
                            return found;
                    }

                    return null;
                }

                public State Clone(State from = null)
                {
                    State ret = new State(from);

                    ret.JointColor = JointColor;
                    ret.Location = Location;
                    ret.Thickness = Thickness;
					ret.Manipulatable = Manipulatable;

                    foreach (State child in Children)
                        ret.Children.Add(child.Clone(ret));

                    return ret;
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
					writer.Write(Manipulatable);
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

					if (version >= 2)
						Manipulatable = reader.ReadBoolean();
					else
						Manipulatable = true;

                    Children = FileFormat.ReadList<Joint.State>(reader, version);

                    foreach (Joint.State child in Children)
                        child.Parent = this;
                }
            }
        }
    }
}
