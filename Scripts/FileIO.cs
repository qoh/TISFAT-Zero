using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.IO;
using System;

namespace TISFAT_ZERO
{
	class tzs
	{
		//This class holds the various constants used in file saving/loading.
		public static readonly byte[]
			fileSig = { 0x54, 0x49, 0x53, 0x46, 0x41, 0x54, 0x2D, 0x30 }, //TISFAT-0 in hex
			curVersion = { 0x00, 0x03, 0x00, 0x00 };

		public static readonly string saveFileExt = ".tzs";
	}

	class tzf
	{
		public static readonly byte[]
			fileSig = { 0x53, 0x74, 0x69, 0x63, 0x6b, 0x20, 0x46, 0x69, 0x67 }, //Stick Fig in hex
			curVersion = { 0x00, 0x03, 0x00, 0x00 };

		public static readonly string saveFileExt = ".tzf";
	}

	public class LegacyTJointBitmap
	{
		MemoryStream ms;
		public Bitmap bitmap;
		public string name;

		public void LegacyT0JointBitmap(BinaryReader s)
		{
			Load(s);
		}

		public void Load(BinaryReader bin)
		{
			int l;
			string n;

			l = bin.ReadInt32();
			n = bin.ReadString();
			name = n;
			l = bin.ReadInt32();

			byte[] buffer = bin.ReadBytes(l);

			MemoryStream ms = new MemoryStream(buffer);
			Image bitty;
			ms.Seek(0, SeekOrigin.Begin);

			try
			{
				bitty = Image.FromStream(ms);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return;
			}

			bitmap = (Bitmap)bitty;
		}
	}

	class LegacyTStickFileLoader
	{
		public static List<StickJoint> Read(string file)
		{
			int nCount;
			LegacyFigure fig = new LegacyFigure();

			LegacyJoint pJoint;
			LegacyTJointBitmap bmp;

			BinaryReader bin = new BinaryReader(File.Open(file, FileMode.Open));

			fig.DrawMode = bin.ReadInt32();
			fig.Tension = bin.ReadSingle();
			fig.CurveWidth = bin.ReadInt32();
			fig.JointCount = bin.ReadInt32();
			nCount = bin.ReadInt32();
			for (int i = 0;i < nCount;i++)
			{
				pJoint = new LegacyJoint();
				pJoint.Read(bin, fig);
			}

			fig.PoseCount = bin.ReadInt32();
			fig.Alpha = 1 - (fig.PoseCount / 255);
			fig.BitmapCount = bin.ReadInt32();
			for (int i = 0;i < fig.BitmapCount;i++)
			{
				bmp = new LegacyTJointBitmap();
				bmp.Load(bin);
				fig.Bitmaps.Add(bmp);
			}

			List<StickJoint> x = new List<StickJoint>();
			StickCustom cust = new StickCustom();

			foreach (LegacyJoint j in fig.Joints)
				x.Add(new StickJoint(j));

			for (int i = 0;i < fig.Joints.Count;i++)
				for (int j = 0;j < fig.Joints[i].children.Count;j++)
				{
					StickJoint lj = x[fig.Joints.IndexOf(fig.Joints[i].children[j])];
					x[i].children.Add(lj);
					lj.parent = x[i];
				}

			for (int z = 0;z < fig.Bitmaps.Count; z++)
				for (int i = 0;i < x.Count;i++)
				{
					x[i].bitmaps.Add(fig.Bitmaps[z].bitmap);
					x[i].Bitmap_names.Add(fig.Bitmaps[z].name);
					x[i].Bitmap_Offsets.Add(new Point(fig.Joints[i].BitmapXOffs, fig.Joints[i].BitmapYOffs));
					x[i].Bitmap_Rotations.Add(Math.Abs(Convert.ToInt32(fig.Joints[i].BitmapRotation)));

					x[i].Bitmap_IDs.Add(z);
					Functions.AssignGlid(x[i], x[i].bitmaps.IndexOf(fig.Bitmaps[z].bitmap));
				}

			return x;
		}

		public static void AddToList(List<StickJoint> z, List<LegacyJoint> j, StickJoint parent = null)
		{
			foreach (LegacyJoint x in j)
			{
				StickJoint sj = new StickJoint(x);

				if (parent != null)
					sj.parent = parent;
				z.Add(sj);
				AddToList(sj.children, x.children, sj);
			}
		}

		public static void AddToList(List<StickJoint> z, LegacyJoint j, LegacyJoint parent = null)
		{
			//foreach(LegacyJoint x in j.children)
			//	recursiveAdd(z, x);
			StickJoint sJoint;
			StickJoint sParent = null;
			if (parent != null)
				sParent = new StickJoint(parent);

			sJoint = new StickJoint(j);
			sJoint.parent = sParent;

			if (!z.Contains(sJoint))
				z.Add(sJoint);
			if (sParent != null)
				z.Add(sParent);
		}
	}

	public class LegacyJoint
	{
		public List<LegacyJoint> children;

		public int x, y, state, length, index, Bitmap, BitmapXOffs, BitmapYOffs, LineWidth, DrawAs, DrawWidth, Color, InColor;
		public LegacyJoint parent;
		public Single AngleToParent, BitmapRotation;
		public byte BitmapAlpha;
		public string name;
		public bool ShowLine, Fill;

		public int nCount;

		public LegacyJoint()
		{
			children = new List<LegacyJoint>();
		}

		public bool Read(BinaryReader bin, LegacyFigure pFig)
		{
			LegacyFigure parentFigure = pFig;

			DrawAs = bin.ReadInt32();
			DrawWidth = bin.ReadInt32();
			LineWidth = bin.ReadInt32();
			ShowLine = bin.ReadBoolean();
			Color = bin.ReadInt32();
			InColor = bin.ReadInt32();
			Fill = bin.ReadBoolean();
			x = bin.ReadInt32();
			y = bin.ReadInt32();
			state = bin.ReadInt32();
			length = bin.ReadInt32();
			index = bin.ReadInt32();
			AngleToParent = bin.ReadSingle();
			Bitmap = bin.ReadInt32();
			BitmapXOffs = bin.ReadInt32();
			BitmapYOffs = bin.ReadInt32();
			BitmapRotation = bin.ReadSingle();
			BitmapAlpha = bin.ReadByte();

			nCount = bin.ReadInt32();
			for (int i = 0;i < nCount;i++)
			{
				LegacyJoint pChild = new LegacyJoint();
				pChild.parent = this;
				pChild.Read(bin, pFig);

				children.Add(pChild);
			}

			pFig.Joints.Add(this);
			return true;
		}
	}

	public class LegacyFigure
	{
		public List<LegacyJoint> Joints;
		public List<LegacyTJointBitmap> Bitmaps;

		public bool CalcIK, ShowJoints, ShallowBitmaps;
		public int Index, JointCount, DrawMode, CurveWidth, PoseCount, BitmapCount;
		public Single Alpha, Tension;

		public LegacyFigure()
		{
			Joints = new List<LegacyJoint>();
			Bitmaps = new List<LegacyTJointBitmap>();
		}
	}

	class LegacyFunctions
	{
		public static Color DWORDtoRGB(int l)
		{
			int r, g, b;

			b = l >> 16;
			g = l >> 8;
			r = l;

			return Color.FromArgb(255, r, g, b);
		}
	}

	struct Block
	{
		public ushort type;
		public byte[] data;

		//00: layer
		//01: end layer
		//02: keyframe
		//03: end keyframe
		//04: keyframe properties
		//05: int list
		//06: Properties
		//07: custom fig block
	}

	class CustomFigSaver
	{
		public static bool saveFigure(string path, StickCustom figure)
		{
			BinaryWriter bin = new BinaryWriter(File.Open(path, FileMode.Create));
			bin.Write(tzf.fileSig, 0, tzs.fileSig.Length);
			bin.Write(tzf.curVersion, 0, tzf.curVersion.Length);

			bin.Write(figure.Joints.Count);
			bin.Write(figure.getBitmapCount());

			try
			{
				foreach (StickJoint joint in figure.Joints)
					writeJointBlock(bin, figure, joint);

				foreach (StickJoint joint in figure.Joints)
					if (joint.Bitmap_IDs.Count > 0)
						for (int x = 0;x < joint.Bitmap_IDs.Count;x++)
							writeBitmapBlock(x, joint, bin);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Saving failed. Error Detail:\n" + ex.Message, "Saving Failed!");
				bin.Close();
				bin.Dispose();

				return false;
			}

			bin.Close();
			bin.Dispose();
			return true;
		}

		public static bool saveFigure(BinaryWriter bin, StickCustom figure)
		{
			bin.Write(tzf.fileSig, 0, tzf.fileSig.Length);
			bin.Write(tzf.curVersion, 0, tzf.curVersion.Length);

			bin.Write(figure.Joints.Count);
			bin.Write(figure.getBitmapCount());

			try
			{
				foreach (StickJoint joint in figure.Joints)
					writeJointBlock(bin, figure, joint);

				foreach (StickJoint joint in figure.Joints)
					if (joint.Bitmap_IDs.Count > 0)
						for (int x = 0;x < joint.Bitmap_IDs.Count;x++)
							writeBitmapBlock(x, joint, bin);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Saving failed. Error Detail:\n" + ex.Message, "Saving Failed!");
				bin.Close();
				bin.Dispose();

				return false;
			}
			return true;
		}

		private static void writeJointBlock(BinaryWriter bin, StickCustom figure, StickJoint j)
		{
			bin.Write(j.location.X);
			bin.Write(j.location.Y);

			bin.Write(j.color.ToArgb());
			bin.Write(j.handleColor.ToArgb());
			bin.Write(j.defaultHandleColor.ToArgb());
			bin.Write(j.thickness);
			bin.Write(j.drawState);
			bin.Write(j.drawOrder);
			bin.Write(j.visible);
			bin.Write(j.handleDrawn);

			bin.Write(j.Bitmap_CurrentID);
			bin.Write(j.Bitmap_IDs.Count);
			for (int i = 0;i < j.Bitmap_IDs.Count;i++)
				bin.Write(j.Bitmap_IDs[i]);

			if (!(j.parent == null))
				bin.Write(figure.Joints.IndexOf(j.parent));
			else
				bin.Write(-1);
		}

		private static void writeBitmapBlock(int id, StickJoint j, BinaryWriter bin)
		{
			Stream bitmapStream = new MemoryStream();

			j.bitmaps[id].Save(bitmapStream, System.Drawing.Imaging.ImageFormat.Png);

			bin.Write(id);
			bin.Write(j.Bitmap_names[id]);
			bin.Write(j.Bitmap_Rotations[id]);

			bin.Write(j.Bitmap_Offsets[id].X);
			bin.Write(j.Bitmap_Offsets[id].Y);

			bin.Write(bitmapStream.Length);
			j.bitmaps[id].Save(bin.BaseStream, System.Drawing.Imaging.ImageFormat.Png);
		}
	}

	class CustomFigLoader
	{
		public static StickCustom loadStickFile(BinaryReader bin)
		{
			bin.BaseStream.Position += 12;

			int jointCount = bin.ReadInt32();
			int bitmapCount = bin.ReadInt32();

			StickCustom figure = new StickCustom(1);
			figure.drawFig = true;
			figure.drawHandles = true;
			figure.isActiveFig = false;
			List<int> parentList = new List<int>();
			List<T0JointBitmap> bitmapList = new List<T0JointBitmap>();

			for (int i = 0;i < jointCount;i++)
			{
				int x = bin.ReadInt32();
				int y = bin.ReadInt32();

				Color col = Color.FromArgb(bin.ReadInt32());
				Color hCol = Color.FromArgb(bin.ReadInt32());
				Color defHCol = Color.FromArgb(bin.ReadInt32());
				int thickness = bin.ReadInt32();
				int drawState = bin.ReadInt32();
				int drawOrder = bin.ReadInt32();
				bool visible = bin.ReadBoolean();
				bool handleDrawn = bin.ReadBoolean();

				int Bitmap_CurrentID = bin.ReadInt32();
				int bitmapIndexes = bin.ReadInt32();

				List<int> bitmapis = new List<int>();
				for (int z = 0;z < bitmapIndexes;z++)
					bitmapis.Add(bin.ReadInt32());

				int parentIndex = bin.ReadInt32();

				parentList.Add(parentIndex);

				figure.Joints.Add(new StickJoint("Joint " + i, new Point(x, y), thickness, col, hCol, 0, drawState, false, null, handleDrawn));

				figure.Joints[figure.Joints.Count - 1].Bitmap_CurrentID = Bitmap_CurrentID;
				figure.Joints[figure.Joints.Count - 1].Bitmap_IDs = bitmapis;

				figure.Joints[figure.Joints.Count - 1].drawOrder = drawOrder;
			}

			for (int i = 0;i < bitmapCount;i++)
			{
				int id = bin.ReadInt32();
				string name = bin.ReadString();
				int Rotation = bin.ReadInt32();
				int OffsetX = bin.ReadInt32();
				int OffsetY = bin.ReadInt32();

				long bytesToRead = bin.ReadInt64();

				byte[] buffer = bin.ReadBytes((int)bytesToRead);

				//bin.Read(buffer, (int)bin.BaseStream.Position, (int)bytesToRead);
				Stream bitmapStream = new MemoryStream(buffer);

				Image bitty = Bitmap.FromStream(bitmapStream);

				bitmapList.Add(new T0JointBitmap(id, name, Rotation, OffsetX, OffsetY, bitty));
			}
			for (int i = 0;i < jointCount;i++)
			{
				foreach (T0JointBitmap bitmap in bitmapList)
					if (figure.Joints[i].Bitmap_IDs.Contains(bitmap.id))
						bitmap.ApplyTo(figure.Joints[i], bitmap.id);

				if (parentList[i] != -1)
					figure.Joints[i].parent = figure.Joints[parentList[i]];
			}

			return figure;
		}

		public static void loadStickFile(string path)
		{
			StickEditor sticked = StickEditor.theSticked;

			BinaryReader bin;
			try
			{
				bin = new BinaryReader(File.Open(path, FileMode.Open));
			}
			catch
			{
				throw new Exception("Failed to load project file. Reason: Unable to open file.");
			}

			bin.BaseStream.Position += 12;

			int jointCount = bin.ReadInt32();
			int bitmapCount = bin.ReadInt32();

			sticked.figure = new StickCustom(1);
			sticked.figure.drawFig = true;
			sticked.figure.drawHandles = true;
			sticked.figure.isActiveFig = true;
			List<int> parentList = new List<int>();
			List<T0JointBitmap> bitmapList = new List<T0JointBitmap>();

			for (int i = 0;i < jointCount;i++)
			{
				int x = bin.ReadInt32();
				int y = bin.ReadInt32();

				Color col = Color.FromArgb(bin.ReadInt32());
				Color hCol = Color.FromArgb(bin.ReadInt32());
				Color defHCol = Color.FromArgb(bin.ReadInt32());
				int thickness = bin.ReadInt32();
				int drawState = bin.ReadInt32();
				int drawOrder = bin.ReadInt32();
				bool visible = bin.ReadBoolean();
				bool handleDrawn = bin.ReadBoolean();

				int Bitmap_CurrentID = bin.ReadInt32();
				int bitmapIndexes = bin.ReadInt32();

				List<int> bitmapis = new List<int>();
				for (int z = 0;z < bitmapIndexes;z++)
					bitmapis.Add(bin.ReadInt32());

				int parentIndex = bin.ReadInt32();

				parentList.Add(parentIndex);

				sticked.figure.Joints.Add(new StickJoint("Joint " + i.ToString(), new Point(x, y), thickness, col, hCol, 0, drawState, false, null, handleDrawn));

				sticked.figure.Joints[sticked.figure.Joints.Count - 1].Bitmap_CurrentID = Bitmap_CurrentID;
				sticked.figure.Joints[sticked.figure.Joints.Count - 1].Bitmap_IDs = bitmapis;

				sticked.figure.Joints[sticked.figure.Joints.Count - 1].drawOrder = drawOrder;
			}

			for (int i = 0;i < bitmapCount;i++)
			{
				int id = bin.ReadInt32();
				string name = bin.ReadString();
				int Rotation = bin.ReadInt32();
				int OffsetX = bin.ReadInt32();
				int OffsetY = bin.ReadInt32();

				long bytesToRead = bin.ReadInt64();

				byte[] buffer = bin.ReadBytes((int)bytesToRead);

				//bin.Read(buffer, (int)bin.BaseStream.Position, (int)bytesToRead);
				Stream bitmapStream = new MemoryStream(buffer);

				Image bitty = Bitmap.FromStream(bitmapStream);

				bitmapList.Add(new T0JointBitmap(id, name, Rotation, OffsetX, OffsetY, bitty));
			}
			for (int i = 0;i < jointCount;i++)
			{
				foreach (T0JointBitmap bitmap in bitmapList)
					if (sticked.figure.Joints[i].Bitmap_IDs.Contains(bitmap.id))
						bitmap.ApplyTo(sticked.figure.Joints[i], bitmap.id);

				if (parentList[i] != -1)
					sticked.figure.Joints[i].parent = sticked.figure.Joints[parentList[i]];
			}

			sticked.recalcFigureJoints();
			bin.Close();
			bin.Dispose();
		}
	}

	class T0JointBitmap
	{
		public int id, Rotation, OffsetX, OffsetY;
		public string name;
		public Image bitmap;

		public T0JointBitmap(int id, string name, int Rotation, int OffsetX, int OffsetY, Image Img)
		{
			this.id = id;
			this.name = name;
			this.Rotation = Rotation;
			this.OffsetX = OffsetX;
			this.OffsetY = OffsetY;

			this.bitmap = Img;
		}

		public void ApplyTo(StickJoint j, int index)
		{
			j.bitmaps.Add((Bitmap)bitmap);
			j.Bitmap_names.Add(name);
			j.Bitmap_Offsets.Add(new Point(OffsetX, OffsetY));
			j.Bitmap_Rotations.Add(Rotation);

			Functions.AssignGlid(j, index);
		}
	}

	class Saver
	{
		public static bool saveProject(string path, List<Layer> Layers)
		{
			//Create a memory stream to temporarily contain all the info
			MemoryStream memst = new MemoryStream();

			//Write the file signature and current version
			memst.Write(tzs.fileSig, 0, tzs.fileSig.Length);
			memst.Write(tzs.curVersion, 0, tzs.curVersion.Length);

			writePreferencesBlock(memst);

			//Save all the layers to the memory stream
			try
			{
				foreach (Layer x in Layers)
				{
					writeLayerBlock(x, memst);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Saving failed. Error Detail:\n" + ex.Message, "Saving Failed!");
				memst.Close();
				memst.Dispose();

				return false;
			}

			//Write the memory stream to the file
			FileStream file = File.Create(path);

			memst.WriteTo(file);

			//Close the streams and return.
			file.Close();
			file.Dispose();

			memst.Close();
			memst.Dispose();

			return true;
		}

		private static void writePreferencesBlock(Stream s)
		{
			List<byte> bytes = new List<byte>();

			bytes.AddRange(BitConverter.GetBytes((ushort)6));

			Size canSize = Canvas.theCanvas.Size;
			bytes.AddRange(BitConverter.GetBytes((ushort)canSize.Width));
			bytes.AddRange(BitConverter.GetBytes((ushort)canSize.Height));

			Color canColr = Canvas.theCanvas.BackColor;

			bytes.Add((byte)canColr.R);
			bytes.Add((byte)canColr.G);
			bytes.Add((byte)canColr.B);

			bytes.InsertRange(0, BitConverter.GetBytes(bytes.Count));

			s.Write(bytes.ToArray(), 0, bytes.Count);
		}

		private static void writeLayerBlock(Layer l, Stream stream)
		{
			List<byte> bytes = new List<byte>();

			bytes.AddRange(new byte[] { 0, 0 });

			bytes.Add(l.type);

			byte[] name = Encoding.UTF8.GetBytes(l.name);

			bytes.Add((byte)(name.Length - 1));
			//(the - 1 allows 256 characters instead of just 255, by replacing 1 with 0, 2 with 1 and so forth)

			bytes.AddRange(name);

			bytes.InsertRange(0, BitConverter.GetBytes(bytes.Count));

			stream.Write(bytes.ToArray(), 0, bytes.Count);

			if (l.type == 4)
				writeCustomFigBlock(l, stream);

			for (int x = 0;x < l.keyFrames.Count;x++)
				writeFrameBlock(l.keyFrames[x], stream);

			List<byte> b2 = new List<byte>();
			b2.AddRange(BitConverter.GetBytes((int)3));
			b2.AddRange(BitConverter.GetBytes((ushort)1));
			b2.Add(0);

			stream.Write(b2.ToArray(), 0, 7);
		}

		private static void writeCustomFigBlock(Layer l, Stream s)
		{
			List<byte> bytes = new List<byte>();

			bytes.AddRange(BitConverter.GetBytes((ushort)7));
			List<StickJoint> customFig = l.keyFrames[0].Joints;
			bytes.AddRange(BitConverter.GetBytes((ushort)customFig.Count));

			for (int a = 0;a < customFig.Count;a++)
			{
				StickJoint j = customFig[a];
				//Parent index
				if (j.parent != null)
					bytes.AddRange(BitConverter.GetBytes((ushort)(customFig.IndexOf(j.parent) + 1)));
				else
					bytes.AddRange(BitConverter.GetBytes((ushort)0));

				//Joint Color
				bytes.Add((byte)j.color.A);
				bytes.Add((byte)j.color.R);
				bytes.Add((byte)j.color.G);
				bytes.Add((byte)j.color.B);

				//Handle Color
				bytes.Add((byte)j.handleColor.A);
				bytes.Add((byte)j.handleColor.R);
				bytes.Add((byte)j.handleColor.G);
				bytes.Add((byte)j.handleColor.B);

				//Thickness
				bytes.Add((byte)j.thickness);

				//DrawState
				bytes.Add((byte)j.drawState);


				//Draw order
				bytes.AddRange(BitConverter.GetBytes((ushort)j.drawOrder));

				//handle visible and visible
				bytes.AddRange(BitConverter.GetBytes(j.visible));
				bytes.AddRange(BitConverter.GetBytes(j.handleDrawn));

			}

			bytes.InsertRange(0, BitConverter.GetBytes(bytes.Count));

			s.Write(bytes.ToArray(), 0, bytes.Count);
		}

		private static void writeFrameBlock(KeyFrame f, Stream stream)
		{
			byte type = f.type;
			List<byte> bytes = new List<byte>();

			bytes.AddRange(BitConverter.GetBytes((ushort)2));

			bytes.AddRange(BitConverter.GetBytes(f.pos));
			bytes.InsertRange(0, BitConverter.GetBytes(bytes.Count));

			stream.Write(bytes.ToArray(), 0, bytes.Count);

			writeKeyFramePropertiesBlock(f, stream);

			writePositionsBlock(f.Joints.ToArray(), stream);

			List<byte> b2 = new List<byte>();
			b2.AddRange(BitConverter.GetBytes((int)3));
			b2.AddRange(BitConverter.GetBytes((ushort)3));
			b2.Add(0);

			stream.Write(b2.ToArray(), 0, 7);
		}

		private static void writeKeyFramePropertiesBlock(KeyFrame k, Stream s)
		{
			List<byte> bytes = new List<byte>();

			bytes.AddRange(BitConverter.GetBytes((ushort)4));
			if (k.type != 4 && k.type != 3)
			{
				bytes.AddRange(BitConverter.GetBytes(true));
				bytes.Add((byte)k.figColor.A);
				bytes.Add((byte)k.figColor.R);
				bytes.Add((byte)k.figColor.G);
				bytes.Add((byte)k.figColor.B);
			}
			else if (k.type == 3)
			{
				RectFrame x = (RectFrame)k;
				bytes.AddRange(BitConverter.GetBytes(x.filled));
				bytes.Add((byte)x.figColor.A);
				bytes.Add((byte)x.figColor.R);
				bytes.Add((byte)x.figColor.G);
				bytes.Add((byte)x.figColor.B);

				Color outlineColor = k.Joints[0].color;

				bytes.Add((byte)outlineColor.A);
				bytes.Add((byte)outlineColor.R);
				bytes.Add((byte)outlineColor.G);
				bytes.Add((byte)outlineColor.B);
			}
			else
			{
				bytes.AddRange(BitConverter.GetBytes(false));
				bytes.AddRange(new byte[4]);
			}

			if (k.type == 2)
				bytes.Add((byte)k.Joints[0].thickness);

			bytes.InsertRange(0, BitConverter.GetBytes(bytes.Count));

			s.Write(bytes.ToArray(), 0, bytes.Count);
		}

		private static void writePositionsBlock(StickJoint[] j, Stream stream)
		{
			List<byte> bytes = new List<byte>();

			bytes.AddRange(BitConverter.GetBytes((ushort)5));

			bytes.AddRange(BitConverter.GetBytes((ushort)j.Length));

			//This'll get updated to support custom stick figures later.
			foreach (StickJoint s in j)
			{
				bytes.AddRange(BitConverter.GetBytes((short)s.location.X));
				bytes.AddRange(BitConverter.GetBytes((short)s.location.Y));
			}

			bytes.InsertRange(0, BitConverter.GetBytes(bytes.Count));

			stream.Write(bytes.ToArray(), 0, bytes.Count);
		}
	}

	class Loader
	{
		public static void loadProjectFile(string path)
		{
			//1. discard first 12 bytes
			//2. get layer info
			//3. read in keyframes

			//Clear everything in the timeline (prompts to come later)
			Timeline.resetEverything(true);

			Canvas zeCanvas = Canvas.theCanvas;

			FileStream file;
			try
			{
				file = File.Open(path, FileMode.Open);
			}
			catch
			{
				throw new Exception("Failed to load project file. Reason: Unable to open file.");
			}

			file.Position += 12;

			long length = file.Length;
			List<Layer> layers = new List<Layer>();

			Block layer = new Block();
			layer.type = ushort.MaxValue;

			try
			{
				while (layer.type != 6 && layer.type != 0)
					layer = readNextBlock(file);

				if (layer.type == 6)
				{
					try
					{
						ushort width = BitConverter.ToUInt16(layer.data, 0);
						ushort height = BitConverter.ToUInt16(layer.data, 2);
						Color canColor = Color.FromArgb(layer.data[4], layer.data[5], layer.data[6]);

						zeCanvas.Size = new Size(width, height);
						zeCanvas.BackColor = canColor;
					}
					catch
					{
						//Do nothing. The parser should attempt to load the rest of the file even if the preferences aren't valid.
					}
				}
			}
			catch
			{
				throw new Exception("Failed to load project file. Reason: No Layer or Preferences block found.");
			}

			while (file.Position + 1 < length)
			{
				try
				{
					layer = readNextBlock(file);
				}
				catch
				{
					throw new Exception("Failed to load project file. Reason: Unable to read from file.");
				}

				if (layer.type != 0) //If it isn't the actual layer type, then skip.
					continue;

				byte layerType = layer.data[0];

				int nameLength = layer.data[1] + 1;

				string name = Encoding.UTF8.GetString(layer.data, 2, nameLength);

				Layer newLayer;
				Block otherTmpBlock = new Block();

				if (layerType == 1)
					newLayer = new StickLayer(name, new StickFigure(false), zeCanvas);
				else if (layerType == 2)
					newLayer = new LineLayer(name, new StickLine(false), zeCanvas);
				else if (layerType == 3)
					newLayer = new RectLayer(name, new StickRect(false), zeCanvas);
				else if (layerType == 4)
				{
					newLayer = new CustomLayer(name, new StickCustom(false), zeCanvas);
					otherTmpBlock = readNextBlock(file);
				}
				else if (layerType == 5)
					newLayer = new LightLayer(name, new LightObject(false), zeCanvas);
				else
					continue; //Only 1, 2, 3, 4, and 5 have been coded so far, so only load those types.

				List<KeyFrame> thingy = new List<KeyFrame>();

				for (Block tmpBlk = readNextBlock(file);tmpBlk.type != 1;tmpBlk = readNextBlock(file))
				{

					if (tmpBlk.type != 2)
						continue;

					KeyFrame f;

					if (layerType == 1)
						f = new StickFrame(0);
					else if (layerType == 2)
						f = new LineFrame(0);
					else if (layerType == 3)
						f = new RectFrame(0);
					else if (layerType == 4)
					{
						f = new custObjectFrame(0);
						int JC = BitConverter.ToUInt16(otherTmpBlock.data, 0);

						f.Joints.AddRange(new StickJoint[JC]);

						int[] parents = new int[JC];

						for (int a = 0;a < JC;a++)
						{
							f.Joints[a] = new StickJoint("", new Point(0, 0), 0, Color.Black, Color.Black, 0, 0, false, null, false);
							int blockItr = a * 16;

							ushort parentIndex = (ushort)BitConverter.ToInt16(otherTmpBlock.data, blockItr + 2);

							parents[a] = -1;
							if (parentIndex != 0)
							{
								parents[a] = parentIndex - 1;
							}

							f.Joints[a].color = Color.FromArgb(otherTmpBlock.data[blockItr + 4], otherTmpBlock.data[blockItr + 5], otherTmpBlock.data[blockItr + 6], otherTmpBlock.data[blockItr + 7]);
							f.Joints[a].handleColor = Color.FromArgb(otherTmpBlock.data[blockItr + 8], otherTmpBlock.data[blockItr + 9], otherTmpBlock.data[blockItr + 10], otherTmpBlock.data[blockItr + 11]);
							f.Joints[a].defaultHandleColor = f.Joints[a].handleColor;
							f.Joints[a].thickness = otherTmpBlock.data[blockItr + 12];
							f.Joints[a].drawState = otherTmpBlock.data[blockItr + 13];
							f.Joints[a].drawOrder = (ushort)BitConverter.ToInt16(otherTmpBlock.data, blockItr + 14);
							f.Joints[a].visible = (bool)BitConverter.ToBoolean(otherTmpBlock.data, 16);
							f.Joints[a].handleDrawn = (bool)BitConverter.ToBoolean(otherTmpBlock.data, 17);
						}

						for (int i = 0;i < JC;i++)
						{
							if (parents[i] != -1)
							{
								f.Joints[i].parent = f.Joints[parents[i]];
							}
						}

						foreach (StickJoint j in f.Joints)
						{
							if (j.parent != null)
							{
								j.parent.children.Add(j);
								j.CalcLength(null);
							}

						}
						newLayer.tweenFig = new StickCustom(true);
						newLayer.tweenFig.Joints = custObjectFrame.createClone(f.Joints, parents);
					}
					else if (layerType == 5)
						f = new LightFrame(0);
					else
						continue; //Nothing past layer type 5 has even begun implementation, so if we encounter any just skip.

					int kPos = BitConverter.ToInt32(tmpBlk.data, 0);

					f.pos = kPos;

					//Read the next block, which contains the other properties of the keyframe, like the colour of the joints.
					Block propBlock = readNextBlock(file);

					try
					{
						//We can also just skip the keyframe properties totally in case we're loading an older file format.
						while (propBlock.type != 4 && propBlock.type != 5)
							propBlock = readNextBlock(file);
					}
					catch
					{
						throw new Exception("Loading failed. Reason: Unable to load keyframe properties.");
					}

					Block posblk = propBlock;
					Color figColor = Color.Black;

					if (propBlock.type == 4)
					{
						//Obtain the colour that's stored in the properties block
						if (layerType != 4)
							figColor = Color.FromArgb(propBlock.data[1], propBlock.data[2], propBlock.data[3], propBlock.data[4]);

						if (layerType == 3)
						{
							((RectFrame)f).figColor = figColor;

							Color outlineColor = Color.FromArgb(propBlock.data[5], propBlock.data[6], propBlock.data[7], propBlock.data[8]);

							foreach (StickJoint j in f.Joints)
								j.color = outlineColor;
						}

						//Obtain the joints positions block
						posblk = readNextBlock(file); //Oh readNextBlock method, how you make my life simpler so
					}

					int jointcount = BitConverter.ToUInt16(posblk.data, 0);

					try
					{
						for (int a = 0;a < jointcount;a++)
						{
							int x = 4 * a + 2;

							if (layerType != 4 && layerType != 3)
								f.Joints[a].color = figColor;

							f.Joints[a].location = new Point(BitConverter.ToInt16(posblk.data, x),
															BitConverter.ToInt16(posblk.data, x + 2));

							if (layerType == 2)
								f.Joints[a].thickness = propBlock.data[5];
						}
					}
					catch
					{
						//Do nothing. The loader should try to continue loading if it encounters any sort of error here.
					}

					foreach (StickJoint x in f.Joints)
						x.ParentFigure = newLayer.fig;

					thingy.Add(f);
				}
				newLayer.keyFrames = thingy;
				newLayer.firstKF = newLayer.keyFrames[0].pos;
				newLayer.lastKF = newLayer.keyFrames[newLayer.keyFrames.Count - 1].pos;
				layers.Add(newLayer);
			}
			Timeline.layers = layers;
			Timeline.layer_cnt = layers.Count;
			Timeline.mainForm.doneLoading();
		}

		private static Block readNextBlock(Stream file)
		{
			Block bl = new Block();

			byte[] countbytes = new byte[4];
			ReadWholeArray(file, countbytes);

			int count = BitConverter.ToInt32(countbytes, 0);

			if (count < 3)
			{
				throw new Exception("Invalid block data length: must be > 2");
			}

			byte[] typebytes = new byte[2], data = new byte[count - 2];

			ReadWholeArray(file, typebytes);
			ReadWholeArray(file, data);

			bl.type = BitConverter.ToUInt16(typebytes, 0);
			bl.data = data;

			return bl;
		}

		#region Others
		[DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern int memcmp(byte[] b1, byte[] b2, long count);

		//Super duper fast byte array compare
		static bool ByteArrayCompare(byte[] b1, byte[] b2)
		{
			// Validate buffers are the same length.
			// This also ensures that the count does not exceed the length of either buffer.  
			return b1.Length == b2.Length && memcmp(b1, b2, b1.Length) == 0;
		}

		private static void ReadWholeArray(Stream stream, byte[] data)
		{
			int offset = 0;
			int remaining = data.Length;
			while (remaining > 0)
			{
				int read = stream.Read(data, offset, remaining);
				if (read <= 0)
					throw new EndOfStreamException
						(String.Format("End of stream reached with {0} bytes left to read", remaining));
				remaining -= read;
				offset += read;
			}
		}
		#endregion Others
	}
}
