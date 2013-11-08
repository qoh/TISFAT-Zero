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
			fileSig =		{ 0x54, 0x49, 0x53, 0x46, 0x41, 0x54, 0x2D, 0x30 }, //TISFAT-0 in hex
			curVersion =	{ 0x00, 0x03, 0x00, 0x00 };

		public static readonly string saveFileExt = ".tzs";
	}

	class tzf
	{
		public static readonly byte[]
			fileSig =		{ 0x53, 0x74, 0x69, 0x63, 0x6b, 0x20, 0x46, 0x69, 0x67 }, //Stick Fig in hex
			curVersion =	{ 0x00, 0x03, 0x00, 0x00 };

		public static readonly string saveFileExt = ".tzf";
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
			
			try
			{
				foreach (StickJoint joint in figure.Joints)
					writeJointBlock(bin, figure, joint);
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

			bin.Write(j.bitmapName);
			j.Bitmap.Save(bin.BaseStream, System.Drawing.Imaging.ImageFormat.Png);

			if (!(j.parent == null))
				bin.Write(figure.Joints.IndexOf(j.parent));
			else
				bin.Write(-1);
		}
	}

	class CustomFigLoader
	{
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
			sticked.figure = new StickCustom(1);
			sticked.figure.drawFig = true;
			sticked.figure.drawHandles = true;
			sticked.figure.isActiveFig = true;
			List<int> parentList = new List<int>();

			for(int i = 0; i < jointCount; i++)
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
				int parentIndex = bin.ReadInt32();

				String bittyName = bin.ReadString();
				Bitmap bitty = (Bitmap)Bitmap.FromStream(bin.BaseStream);

				parentList.Add(parentIndex);


				sticked.figure.Joints.Add(new StickJoint("Joint " + i.ToString(), new Point(x, y), thickness, col, hCol, 0, drawState, false, null, handleDrawn, bitty, bittyName));

				sticked.figure.Joints[sticked.figure.Joints.Count - 1].drawOrder = drawOrder;
			}
			for (int i = 0; i < jointCount; i++)
				if (parentList[i] != -1)
					sticked.figure.Joints[i].parent = sticked.figure.Joints[parentList[i]];

			sticked.recalcFigureJoints();
			bin.Close();
			bin.Dispose();
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

			Size canSize = Properties.User.Default.CanvasSize;
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

			bytes.Add((byte)(name.Length-1));
			//(the - 1 allows 256 characters instead of just 255, by replacing 1 with 0, 2 with 1 and so forth)

			bytes.AddRange(name);

			bytes.InsertRange(0, BitConverter.GetBytes(bytes.Count));

			stream.Write(bytes.ToArray(), 0, bytes.Count);

			if (l.type == 4)
				writeCustomFigBlock(l, stream);

			for(int x = 0; x < l.keyFrames.Count; x++)
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

			for(int a = 0; a < customFig.Count; a++)
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
				while(layer.type != 6 && layer.type != 0)
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
				else
					continue; //Only 1, 2, 3, and 4 have been coded so far, so only load those types.

				List<KeyFrame> thingy = new List<KeyFrame>();

				for (Block tmpBlk = readNextBlock(file); tmpBlk.type != 1; tmpBlk = readNextBlock(file))
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
						
						for (int a = 0; a < JC; a++)
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

						for (int i = 0; i < JC; i++)
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
					else
						continue; //Nothing past layer type 4 has even begun implementation, so if we encounter any just skip.

					int kPos = BitConverter.ToInt32(tmpBlk.data, 0);

					f.pos = kPos;

					//Read the next block, which contains the other properties of the keyframe, like the colour of the joints.
					Block propBlock = readNextBlock(file);

					try
					{
						//We can also just skip the keyframe properties totally in case we're loading an older file format.
						while(propBlock.type != 4 && propBlock.type != 5)
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
						if(layerType != 4)
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
						for (int a = 0; a < jointcount; a++)
						{
							int x = 4 * a + 2;

							if(layerType != 4 && layerType != 3)
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
