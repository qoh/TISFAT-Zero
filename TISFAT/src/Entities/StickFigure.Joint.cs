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

			// Item1 = ID, Item2.Item1 = Name, Item2.Item2 = Image
			public List<Tuple<int, Tuple<string, Bitmap>>> Bitmaps;

			// BitmapOffsets[Bitmap].Item1 = rotation, BitmapOffsets[Bitmap].Item2 = offset
			public Dictionary<Bitmap, Tuple<float, PointF>> BitmapOffsets;

			public int InitialBitmapIndex;

			public DrawJointType DrawType;
			public bool HandleVisible;
			public bool Manipulatable;
			public bool Visible;

			#region Constructors
			public Joint()
			{
				Parent = null;
				Children = new List<Joint>();
				JointColor = Color.Black;
				HandleColor = Color.Blue;
				Thickness = 6;

				Bitmaps = new List<Tuple<int, Tuple<string, Bitmap>>>();
				BitmapOffsets = new Dictionary<Bitmap, Tuple<float, PointF>>();
				InitialBitmapIndex = -1;

				HandleVisible = true;
				Visible = true;
				Manipulatable = true;
			}

			public Joint(Joint parent)
			{
				Parent = parent;
				Children = new List<Joint>();
				JointColor = Color.Black;
				HandleColor = Color.Blue;
				Thickness = 6;

				Bitmaps = new List<Tuple<int, Tuple<string, Bitmap>>>();
				BitmapOffsets = new Dictionary<Bitmap, Tuple<float, PointF>>();
				InitialBitmapIndex = -1;

				HandleVisible = true;
				Visible = true;
				Manipulatable = true;
			}

			public Joint Clone(Joint parent = null)
			{
				Joint j = new Joint(parent);
				j.Parent = parent;
				j.Children = new List<Joint>();

				foreach (Joint child in Children)
					j.Children.Add(child.Clone(j));

				j.JointColor = JointColor;
				j.HandleColor = HandleColor;
				j.Thickness = Thickness;

				j.Bitmaps = new List<Tuple<int, Tuple<string, Bitmap>>>();
				foreach (var img in Bitmaps)
					j.Bitmaps.Add(img);

				j.BitmapOffsets = new Dictionary<Bitmap, Tuple<float, PointF>>();

				foreach (var offs in BitmapOffsets)
					j.BitmapOffsets.Add(offs.Key, offs.Value);

				j.InitialBitmapIndex = InitialBitmapIndex;

				j.DrawType = DrawType;
				j.HandleVisible = HandleVisible;
				j.Visible = Visible;
				j.Manipulatable = Manipulatable;

				return j;
			}

			public static Joint RelativeTo(Joint parent, PointF location)
			{
				if (parent == null) throw new NullReferenceException("wat");
				Joint joint = new Joint(parent);
				joint.Location = new PointF(parent.Location.X + location.X, parent.Location.Y + location.Y);
				parent.Children.Add(joint);
				return joint;
			}

			public void Delete()
			{
				if (Parent == null) throw new ArgumentNullException("You can't delete the root joint!");

				Parent.Children.Remove(this);
			}
			#endregion

			public void ResetFrom(State from)
			{
				if (Children.Count != from.Children.Count)
					throw new ArgumentException("what the butts");

				JointColor = from.JointColor;
				Location = from.Location;
				Thickness = from.Thickness;

				for (int i = 0; i < Children.Count; i++)
					Children[i].ResetFrom(from.Children[i]);
			}

			#region Drawing
			public void Draw(State state)
			{
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

				if(state.BitmapIndex != -1)
				{
					int ID = Bitmaps[state.BitmapIndex].Item1;
					Bitmap bitmap = Bitmaps[state.BitmapIndex].Item2.Item2;

					double angle = MathUtil.Angle(state.Location, otherState.Location);
					double angleDiff = Math.PI * BitmapOffsets[bitmap].Item1 / 180;
                    angle += angleDiff;
					PointF Offsets = MathUtil.Rotate(BitmapOffsets[bitmap].Item2, (float)angle);

					angle *= 180.0 / Math.PI;

					Drawing.BitmapOriginRotation(new PointF(
						state.Location.X + Offsets.X, 
						state.Location.Y + Offsets.Y), bitmap.Size, (float)angle, ID);
				}
			}

			public void DrawHandle(State state)
			{
				PointF Loc = new PointF(state.Location.X - 3, state.Location.Y - 3);
				SizeF Siz = new SizeF(6, 6);

				if (HandleVisible)
				{
					Drawing.Rectangle(Loc, Siz, HandleColor);
					Drawing.RectangleLine(Loc, Siz, Color.FromArgb(255, 255 - JointColor.R, 255 - JointColor.G, 255 - JointColor.B));
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

				state.BitmapIndex = InitialBitmapIndex;

				foreach (Joint child in Children)
					state.Children.Add(child.Copy(state));

				return state;
			}

			public int GetJointCount()
			{
				int count = Children.Count;

				foreach (Joint child in Children)
				{
					count += child.GetJointCount();
				}

				return count;
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

				List<string> names = new List<string>();
				List<Bitmap> images = new List<Bitmap>();
				List<float> rotations = new List<float>();
				List<PointF> offsets = new List<PointF>();

				for (int i = 0; i < Bitmaps.Count; i++)
				{
					names.Add(Bitmaps[i].Item2.Item1);
					images.Add(Bitmaps[i].Item2.Item2);
					rotations.Add(BitmapOffsets[Bitmaps[i].Item2.Item2].Item1);
					offsets.Add(BitmapOffsets[Bitmaps[i].Item2.Item2].Item2);
				}

				FileFormat.WriteList(writer, names);
				FileFormat.WriteList(writer, images);
				FileFormat.WriteList(writer, rotations);
				FileFormat.WriteList(writer, offsets);

				writer.Write(InitialBitmapIndex);
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

				if (version >= 4)
				{
					List<string> names = FileFormat.ReadStringList(reader, version);
					List<Bitmap> images = FileFormat.ReadBitmapList(reader, version);
					List<float> rotations = FileFormat.ReadFloatList(reader, version);
					List<PointF> offsets = FileFormat.ReadPointFList(reader, version);

					List<Tuple<int, Tuple<string, Bitmap>>> bitmapTuple = new List<Tuple<int, Tuple<string, Bitmap>>>();
					Dictionary<Bitmap, Tuple<float, PointF>> offsetDict = new Dictionary<Bitmap, Tuple<float, PointF>>();

					for (int i = 0; i < names.Count; i++)
					{
						bitmapTuple.Add(new Tuple<int, Tuple<string, Bitmap>>(Drawing.GenerateTexID(images[i]), new Tuple<string, Bitmap>(names[i], images[i])));
						offsetDict.Add(images[i], new Tuple<float, PointF>(rotations[i], offsets[i]));
					}

					Bitmaps = bitmapTuple;
					BitmapOffsets = offsetDict;

					InitialBitmapIndex = reader.ReadInt32();
                }
			}
			#endregion
		}
	}
}
