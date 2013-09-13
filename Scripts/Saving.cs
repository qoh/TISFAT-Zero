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
	class tzf
	{
		//This class holds the various constants used in file saving/loading.
		public static readonly byte[]
			fileSig =		{ 0x54, 0x49, 0x53, 0x46, 0x41, 0x54, 0x2D, 0x30 }, //TISFAT-0 in hex
			blockSig =		{ 0x46, 0x41, 0x46, 0x41 },
			endBSig =		{ 0xe6, 0x21, 0x21, 0xe6 }, //4 bytes because reasons
			curVersion =	{ 0x00, 0x01, 0x00, 0x01 };

		public static readonly string saveFileExt = ".tzs";
	}

	struct Block
	{
		public ushort type;
		public byte[] data;
	}

	class Saver
	{
		public static bool saveProject(string path, List<Layer> Layers)
		{
			//Create a memory stream to temporarily contain all the info
			MemoryStream memst = new MemoryStream();

			//Write the file signature and current version
			memst.Write(tzf.fileSig, 0, tzf.fileSig.Length);
			memst.Write(tzf.curVersion, 0, tzf.curVersion.Length);

			//Save all the layers to the memory stream
			try
			{
				uint y = 0;
				foreach (Layer x in Layers)
				{
					writeLayerBlock(x, y++, memst);
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

		private static void writeLayerBlock(Layer l, uint layerid, Stream stream)
		{
			if (l.type != 1)
				return;

			List<byte> bytes = new List<byte>();

			bytes.AddRange(tzf.blockSig);

			bytes.AddRange(new byte[] { 0, 0 });
			bytes.AddRange(BitConverter.GetBytes((ushort)layerid));
			bytes.AddRange(BitConverter.GetBytes(l.type));

			byte[] name = Encoding.UTF8.GetBytes(l.name);

			bytes.AddRange(BitConverter.GetBytes((byte)(name.Length-1)));
			//if the name is longer than 256 characters then that's just silly
			//(the - 1 lets it be 256 characters instead of just 255, by replacing 1 with 0, 2 with 1 and so forth)
			bytes.AddRange(name);

			stream.Write(bytes.ToArray(), 0, bytes.Count);

			foreach (KeyFrame k in l.keyFrames)
				writeFrameBlock(k, stream);

			stream.Write(tzf.endBSig, 0, 4);
		}
		
		private static void writeFrameBlock(KeyFrame f, Stream stream)
		{
			byte type = f.type;
			List<byte> bytes0 = new List<byte>();

			bytes0.AddRange(tzf.blockSig);
			bytes0.AddRange(new byte[] { 0, 1 });
			bytes0.Add(type);
			bytes0.AddRange(BitConverter.GetBytes(f.pos));


			//Each keyframe type has it's own saving algorithm. Implement them here.
			switch (type)
			{
				case 0: //StickFrame

					stream.Write(bytes0.ToArray(), 0, bytes0.Count);

					writePositionsBlock(f.Joints.ToArray(), stream);
					break;

				default:
					
					return;
			}

			stream.Write(tzf.endBSig, 0, 4);
		}

		private static void writePositionsBlock(StickJoint[] j, Stream stream)
		{
			List<byte> bytes = new List<byte>();
			bytes.AddRange(tzf.blockSig);
			bytes.AddRange(new byte[] { 0, 2 });

			bytes.AddRange(BitConverter.GetBytes((ushort)j.Length));

			//This'll get updated to support custom stick figures later.
			foreach (StickJoint s in j)
			{
				bytes.AddRange(BitConverter.GetBytes((short)s.location.X));
				bytes.AddRange(BitConverter.GetBytes((short)s.location.Y));
			}

			stream.Write(bytes.ToArray(), 0, bytes.Count);
			stream.Write(tzf.endBSig, 0, 4);
		}
	}

	class Loader
	{
		public static void loadProjectFile(string path, Canvas zeCanvas)
		{
			//1. discard first 12 bytes
			//2. get layer info
			//3. read in keyframes

			//Clear everything in the timeline (prompts to come later)
			Timeline.resetEverything(true);
			
			FileStream file = File.Open(path, FileMode.Open);
			file.Position += 12;

			long length = file.Length;
			List<Layer> layers = new List<Layer>();
			while (file.Position + 1 < length)
			{
				Block layer = readNextBlock(file); //i REALLY hope this works ;-;
				
				if (layer.type != 0)
					throw new Exception("OKAY I UH I DIDNT EXPECT THIS ;-;");

				byte layerType = layer.data[2];

				if (layerType != 1)
					throw new Exception("I STILL DONT KNOW WHAT TO DO WITH THIS HELP ME ;-;");

				int nameLength = layer.data[4] + 1;
				byte[] namebytes = new byte[nameLength+1];
				nameLength += 5;
				for(int a = 5; a < nameLength +1; a++)
					namebytes[a-5] = layer.data[a];

				string name = Encoding.UTF8.GetString(namebytes);

				StickLayer newLayer = new StickLayer(name, new StickFigure(false), zeCanvas);
				List<KeyFrame> thingy = new List<KeyFrame>();
				
				//For this I have to create a memorystream out of the layer data so that I can use readNextBlock on it to get the frame data
				int dataLength = layer.data.Length;
				MemoryStream ms = new MemoryStream();
				ms.Write(layer.data, 0, dataLength);
				ms.Position = 0;

				while (ms.Position + 1 < dataLength)
				{
					Block fBlock = readNextBlock(ms);
					if (fBlock.data == null)
						break;
					byte[] posbytes = new byte[4];
					for(int a = 0; a < 4; a++)
						posbytes[a] = fBlock.data[a + 1];

					byte[] newdata = new byte[fBlock.data.Length - 5];
					int len = fBlock.data.Length;
					for (int a = 5; a < len; a++)
						newdata[a-5] = fBlock.data[a];

					//screw it im adding in checks later
					StickFrame frm = new StickFrame(BitConverter.ToUInt32(posbytes, 0));

					MemoryStream fs = new MemoryStream();
					fs.Write(newdata, 0, newdata.Length);
					fs.Position = 0;

					Block pblock = readNextBlock(fs);

					for (int a = 0; a < 12; a++) //I dislike reading binary like this but it's kinda unavoidable.
						frm.Joints[a].location = new Point((int)BitConverter.ToInt16(new byte[] { pblock.data[4 * a + 2], pblock.data[4 * a + 3] },0), (int)BitConverter.ToInt16(new byte[] { pblock.data[4 * (a + 1)], pblock.data[4 * (a + 1) + 1] }, 0));
					thingy.Add(frm);
				}
				newLayer.keyFrames = thingy;
				layers.Add(newLayer);
			}
			Timeline.layers = layers;
			Timeline.layercount = layers.Count;
			Timeline.mainForm.doneLoading();
		}

		private static Block readNextBlock(Stream file)
		{
			Block bl = new Block();

			//Find the next occurance of the block start sig
			byte a1 = (byte)file.ReadByte(), a2 = (byte)file.ReadByte(), a3 = (byte)file.ReadByte(), a4 = (byte)file.ReadByte();
			uint x = 0;

			while(!ByteArrayCompare(new byte[]{a1, a2, a3, a4}, tzf.blockSig))
			{
				a1 = a2;
				a2 = a3;
				a3 = a4;
				a4 = (byte)file.ReadByte();
				x++;
				if (x > file.Length)
					return bl;
			}

			if(x > 0)
			{
				//Log somewhere about how many unused bytes there were
			}

			//Now make a new byte list to contain all the data.
			List<byte> bytes = new List<byte>();

			byte[] type = new byte[2];

			ReadWholeArray(file, type);
			bl.type = (ushort)BitConverter.ToInt16(type, 0); //ushort = 2 bytes = size of type
			ushort btype = bl.type;

			byte depth = 1;

			try
			{
				byte[] first4 = new byte[4];

				ReadWholeArray(file, first4);
				bytes.AddRange(first4);
			}
			catch
			{
				bl.type = ushort.MaxValue;
				return bl;
			}

			//Because I'm too damn lazy to implement reading every single type of block, I have successfully reading the block being dependant on
			//a certain combination of 4 bytes not appearing anywhere that isn't the end of a block. I know this is a bad way of doing it but whatever.
			//It's easiest. Sue me :P
			if (!ByteArrayCompare(tzf.endBSig, bytes.ToArray()))
			{
				//There's no way to instantly skip to the next instance of a byte array in a file, and reading it byte by byte is really slow.
				//So I read in chunks of 2. I'll probably make this number bigger later.
				byte[] next2 = new byte[2];
				int c = 2;
				for(;file.Position + 1 < file.Length;) //Infinite loop technically...
				{
					try
					{
						ReadWholeArray(file, next2);
					}
					catch
					{
						try
						{
							next2 = new byte[1];
							ReadWholeArray(file, next2);
						}
						catch(Exception ex)
						{
							if (!ex.Message.Contains("of stream"))
							{
								bl.type = ushort.MaxValue;
								return bl;
							}
						}
					}
					byte[] tmp1 = new byte[] { bytes[c - 1], bytes[c], bytes[c + 1], next2[0] };
					byte[] tmp2 = new byte[] { bytes[c], bytes[c + 1], next2[0], next2[1] };
					if (ByteArrayCompare(tzf.endBSig, tmp1))
					{
						depth--;

						if (depth == 0)
						{
							file.Position--;
							for (int y = 0; y < 3; y++)
								bytes.RemoveAt(bytes.Count - 1);
							bl.data = bytes.ToArray();
							return bl;
						}
					}
					else if (ByteArrayCompare(tzf.endBSig, tmp2))
					{
						depth--;

						if (depth == 0)
						{
							bytes.RemoveAt(bytes.Count - 1);
							bytes.RemoveAt(bytes.Count - 1);
							bl.data = bytes.ToArray();
							return bl;
						}
					}
					else if (ByteArrayCompare(tzf.blockSig, tmp1) || ByteArrayCompare(tzf.blockSig, tmp2))
					{
						depth++;
					}
					
					bytes.AddRange(next2);
					bl.data = bytes.ToArray();
					c += 2;
				}
			}
			bl.data = bytes.ToArray();
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
