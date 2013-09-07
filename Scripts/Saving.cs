using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Collections.Generic;
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
			blockSig =		{ 0x46, 0x41 },
			endBSig =		{ 0xe6, 0x21 }, //Yes I just did that
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
			List<byte> bytes = new List<byte>();

			bytes.AddRange(tzf.blockSig);

			bytes.AddRange(new byte[] { 0, 1 });
			bytes.AddRange(BitConverter.GetBytes(layerid));
			bytes.AddRange(BitConverter.GetBytes(l.type));

			byte[] name = Encoding.ASCII.GetBytes(l.name);
			bytes.AddRange(BitConverter.GetBytes((uint)name.Length));
			bytes.AddRange(name);

			bytes.AddRange(tzf.endBSig);

			stream.Write(bytes.ToArray(), 0, bytes.Count);

			foreach (KeyFrame k in l.keyFrames)
				writeFrameBlock(k, stream);
		}
		
		private static void writeFrameBlock(KeyFrame f, Stream stream)
		{
			byte type = f.type;
			List<byte> bytes0 = new List<byte>();

			bytes0.AddRange(tzf.blockSig);
			bytes0.AddRange(new byte[] { 0, 0 });
			bytes0.Add(type);
			bytes0.AddRange(BitConverter.GetBytes(f.pos));


			//Each keyframe type has it's own saving algorithm. Implement them here.
			switch (type)
			{
				case 0: //StickFrame

					stream.Write(bytes0.ToArray(), 0, bytes0.Count);

					writePositionsBlock(((StickFrame)f).Joints.ToArray(), stream);
					break;

				default:
					return;
			}

			stream.Write(tzf.endBSig, 0, 2);
		}

		private static void writePositionsBlock(StickJoint[] j, Stream stream)
		{
			List<byte> bytes = new List<byte>();
			bytes.AddRange(tzf.blockSig);
			bytes.AddRange(new byte[] { 0x80, 0 });

			bytes.AddRange(BitConverter.GetBytes(j.Length));

			//This'll get updated to support custom stick figures later.
			foreach (StickJoint s in j)
			{
				bytes.AddRange(BitConverter.GetBytes((short)s.location.X));
				bytes.AddRange(BitConverter.GetBytes((short)s.location.Y));
			}

			stream.Write(bytes.ToArray(), 0, bytes.Count);
			stream.Write(tzf.endBSig, 0, 2);
		}
	}

	class Loader
	{
		private static Block readNextBlock(Stream file)
		{
			Block bl = new Block();

			//Find the next occurance of the block start sig
			byte l = (byte)file.ReadByte(), c = (byte)file.ReadByte();
			uint x = 0;

			while(!ByteArrayCompare(new byte[]{l, c}, tzf.blockSig))
			{
				l = c;
				c = (byte)file.ReadByte();
				x++;
			}

			if(x > 0)
			{
				//Log somewhere about how many unused bytes there were
			}

			//Now make a new byte list to contain all the data.
			List<byte> bytes = new List<byte>();

			byte[] type = new byte[2];

			ReadWholeArray(file, type);
			bl.type = (ushort)BitConverter.ToInt16(type, 0);
			ushort btype = bl.type;

			//Read all the data based on the type
			switch (btype)
			{
				case 0:

					break;

				case 1:

					break;

				case 2:

					break;
					
				case 3:

					break;

				default:
					throw new Exception("Unknown block type encountered: " + btype);
			}

			return bl;
		}

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
	}
}
