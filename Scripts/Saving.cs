using System.Runtime.Serialization.Formatters.Binary;
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

		private static void writeLayerBlock(Layer l, uint layerid, MemoryStream stream)
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
		
		private static void writeFrameBlock(KeyFrame f, MemoryStream stream)
		{
			byte type = f.type;
			List<byte> bytes0 = new List<byte>();

			bytes0.AddRange(tzf.blockSig);
			bytes0.Add(type);
			bytes0.AddRange(BitConverter.GetBytes(f.pos));


			//Each keyframe type has it's own saving algorithm. Implement them here.
			switch (type)
			{
				case 0: //StickFrame

					stream.Write(bytes0.ToArray(), 0, bytes0.Count);

					writePositionsBlock(((StickFrame)f).Joints, stream);

					break;

				default:
					return;
			}

			stream.Write(tzf.endBSig, 0, 2);
		}

		private static void writePositionsBlock(StickJoint[] j, MemoryStream stream)
		{

		}
	}
}
