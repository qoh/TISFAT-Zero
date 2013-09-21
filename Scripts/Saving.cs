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
			curVersion =	{ 0x00, 0x02, 0x00, 0x00 };

		public static readonly string saveFileExt = ".tzs";
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

		private static void writeLayerBlock(Layer l, Stream stream)
		{
			if (l.type != 1)
				return;

			List<byte> bytes = new List<byte>();

			bytes.AddRange(new byte[] { 0, 0 });

			bytes.Add(l.type);

			byte[] name = Encoding.UTF8.GetBytes(l.name);

			bytes.Add((byte)(name.Length-1));
			//if the name is longer than 256 characters then that's just silly
			//(the - 1 lets it be 256 characters instead of just 255, by replacing 1 with 0, 2 with 1 and so forth)
			bytes.AddRange(name);

			bytes.InsertRange(0, BitConverter.GetBytes(bytes.Count));

			stream.Write(bytes.ToArray(), 0, bytes.Count);

			foreach (KeyFrame k in l.keyFrames)
				writeFrameBlock(k, stream);

			List<byte> b2 = new List<byte>();
			b2.AddRange(BitConverter.GetBytes((int)3));
			b2.AddRange(BitConverter.GetBytes((ushort)1));
			b2.Add(0);

			stream.Write(b2.ToArray(), 0, 7);
		}
		
		private static void writeFrameBlock(KeyFrame f, Stream stream)
		{
			byte type = f.type;
			List<byte> bytes = new List<byte>();

			bytes.AddRange(new byte[] { 0, 2 });

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

			bytes.AddRange(new byte[] { 0, 4 });
			bytes.AddRange(BitConverter.GetBytes(true)); //This true is a placeholder for the (to come) IsAllOneColour bool in the keyframe class.
			bytes.AddRange(BitConverter.GetBytes(k.figColor.R));
			bytes.AddRange(BitConverter.GetBytes(k.figColor.G));
			bytes.AddRange(BitConverter.GetBytes(k.figColor.B));

			bytes.InsertRange(0, BitConverter.GetBytes(bytes.Count));

			s.Write(bytes.ToArray(), 0, bytes.Count);
		}

		private static void writePositionsBlock(StickJoint[] j, Stream stream)
		{
			List<byte> bytes = new List<byte>();

			bytes.AddRange(new byte[] { 0, 5 });

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
					continue;

				byte layerType = layer.data[0];

				int nameLength = layer.data[1] + 1;
				byte[] namebytes = new byte[nameLength];

				for(int a = 2; a < nameLength + 2; a++)
					namebytes[a-2] = layer.data[a];

				string name = Encoding.UTF8.GetString(namebytes);

				StickLayer newLayer = new StickLayer(name, new StickFigure(false), zeCanvas);
				List<KeyFrame> thingy = new List<KeyFrame>();

				for (Block tmpBlk = readNextBlock(file); tmpBlk.type != 1; tmpBlk = readNextBlock(file))
				{
					//Make sure we're reading in a frame
					if (tmpBlk.type != 2)
					{
						continue;
					}

					KeyFrame f;

					if (layerType == 1)
						f = new StickFrame(0);

					byte[] posbytes = new byte[4];
				}

				/*
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
					StickFrame frm = new StickFrame(BitConverter.ToInt32(posbytes, 0));

					MemoryStream fs = new MemoryStream();
					fs.Write(newdata, 0, newdata.Length);
					fs.Position = 0;

					Block pblock = readNextBlock(fs);

					for (int a = 0; a < 12; a++) //I dislike reading binary like this but it's kinda unavoidable.
						frm.Joints[a].location = new Point((int)BitConverter.ToInt16(new byte[] { pblock.data[4 * a + 2], pblock.data[4 * a + 3] },0), (int)BitConverter.ToInt16(new byte[] { pblock.data[4 * (a + 1)], pblock.data[4 * (a + 1) + 1] }, 0));
					thingy.Add(frm);
				}
				newLayer.keyFrames = thingy;
				layers.Add(newLayer);*/
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
