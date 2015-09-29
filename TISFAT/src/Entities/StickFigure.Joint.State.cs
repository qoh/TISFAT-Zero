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

				public int BitmapIndex;

				public bool Manipulatable;

				public State()
				{
					Parent = null;
					Children = new List<State>();
					Location = new PointF();
					JointColor = Color.Black;
					Thickness = 6;

					BitmapIndex = -1;

					Manipulatable = true;
				}

				public State(State parent)
				{
					Parent = parent;
					Children = new List<State>();
					Location = new PointF();
					JointColor = Color.Black;
					Thickness = 6;

					BitmapIndex = -1;

					Manipulatable = true;
				}
				public bool StateEquals(object obj)
				{
					if (obj == null || GetType() != obj.GetType())
					{
						return false;
					}
					State state = obj as State;

					return GetStateHash() == state.GetStateHash();
				}
				
				public int GetStateHash()
				{
					int hash = 0;

					hash += Children.Count;
					hash += (int)Math.Floor(Location.X + Location.Y);
					hash += JointColor.A + JointColor.R + JointColor.G + JointColor.B;
					hash += (int)Math.Floor(Thickness);
					hash += (Manipulatable ? 1 : 0);

					return hash;
				}

				public State FindState(State target)
				{
					if (this.StateEquals(target))
						return this;

					foreach(State child in Children)
					{
						return child.FindState(target);
					}

					return null;
				}

				public static State RelativeTo(State parent, PointF location)
				{
					if (parent == null) throw new NullReferenceException("wat");
					State state = new State(parent);
					state.Location = new PointF(parent.Location.X + location.X, parent.Location.Y + location.Y);
					parent.Children.Add(state);
					return state;
				}

				public void Delete()
				{
					if (Parent == null) throw new ArgumentNullException("You can't delete the root joint!");

					Parent.Children.Remove(this);
				}
				
				public void SetColor(Color color, State from)
				{
					List<State> affected = new List<State>();

					JointColor = color;

					foreach (State child in Children)
					{
						if (child != from)
							affected.Add(child);
					}

					foreach (State state in affected)
						state.SetColor(color, this);
				}

				public static State Interpolate(float t, State current, State target, EntityInterpolationMode mode)
				{
					State state = new State(current.Parent);
					state.BitmapIndex = current.BitmapIndex;

					state.Location = Interpolation.Interpolate(t, current.Location, target.Location, mode);
					state.JointColor = Interpolation.Interpolate(t, current.JointColor, target.JointColor, mode);
                    state.Thickness = Interpolation.Interpolate(t, current.Thickness, target.Thickness, mode);

					for (var i = 0; i < current.Children.Count; i++)
					{
						state.Children.Add(Interpolate(t, current.Children[i], target.Children[i], mode));
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

					if (!mparams.PivotDrag)
					{
						float nx = target.X;
						float ny = target.Y;

						float ox = Location.X;
						float oy = Location.Y;

						Location = target;

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
								if (mparams.DisableIK)
									return;

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
					else
					{
						if (Parent != null)
						{
							float dx = target.X - Parent.Location.X;
							float dy = target.Y - Parent.Location.Y;

							float length = (float)Math.Sqrt(dx * dx + dy * dy);

							dx /= length;
							dy /= length;

							dx *= mparams.PivotLength;
							dy *= mparams.PivotLength;

							Location = new PointF(Parent.Location.X + dx, Parent.Location.Y + dy);

							float angleDiff = (float)MathUtil.Angle(Location, Parent.Location) - mparams.PivotAngle;

							Stack<State> open = new Stack<State>();
							open.Push(this);

							while (open.Count > 0)
							{
								State current = open.Pop();

								foreach (State child in current.Children)
								{
									open.Push(child);
									var info = mparams.PivotInfo[child];

									float angle = info.Item2 + angleDiff;
									float x = (float)(Parent.Location.X + Math.Cos(angle) * info.Item1);
									float y  = (float)(Parent.Location.Y + Math.Sin(angle) * info.Item1); // or maybe the other way around, depends
									child.Location = new PointF(x, y);
								}
							}
						}
						else
						{
							Stack<State> open = new Stack<State>();
							open.Push(this);

							while (open.Count > 0)
							{
								State current = open.Pop();

								foreach (State child in current.Children)
								{
									open.Push(child);

									child.Location = new PointF(
										child.Location.X + (target.X - Location.X),
										child.Location.Y + (target.Y - Location.Y));
								}
							}

							Location = target;
						}
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

				//public Joint GetEquivalentJoint(Joint Root, int testID)
				//{
				//	if (Root.ID == testID)
				//		return Root;

				//	for (int i = 0; i < Root.Children.Count; i++)
				//	{
				//		if (Root.Children[i].ID == testID)
				//			return Root.Children[i];

				//		Joint j = GetEquivalentJoint(Root.Children[i], testID);

				//		if (j != null)
				//			return j;
				//	}

				//	return null;
				//}

				public State Clone(State from = null)
				{
					State ret = new State(from);

					ret.JointColor = JointColor;
					ret.Location = Location;
					ret.Thickness = Thickness;
					ret.Manipulatable = Manipulatable;

					ret.BitmapIndex = BitmapIndex;

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
					writer.Write(BitmapIndex);
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
					{
						Manipulatable = reader.ReadBoolean();

						if (version >= 4)
						{
							BitmapIndex = reader.ReadInt32();
						}
					}
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
