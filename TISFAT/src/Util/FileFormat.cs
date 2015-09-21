using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using TISFAT.Entities;

namespace TISFAT.Util
{
	static class FileFormat
	{
		public static UInt16 Version = 2;

		static Dictionary<UInt16, Type> EntityTypes = new Dictionary<UInt16, Type>()
		{
			{0, typeof(StickFigure)},
			{1, typeof(BitmapObject)},
			{2, typeof(PointLight)},
			{3, typeof(LineObject)},
			{4, typeof(RectObject)},
			{5, typeof(CircleObject)},
			{6, typeof(PolyObject)},
			{7, typeof(TextObject)},
			{8, typeof(CustomFigure)}
		};

		static Dictionary<UInt16, Type> EntityStateTypes = new Dictionary<UInt16, Type>()
		{
			{0, typeof(StickFigure.State)},
			{1, typeof(BitmapObject.State)},
			{2, typeof(PointLight.State)},
            {3, typeof(LineObject.State)},
			{4, typeof(RectObject.State)},
			{5, typeof(CircleObject.State)},
			{6, typeof(PolyObject.State)},
			{7, typeof(TextObject.State)},
			{8, typeof(CustomFigure.State)}
		};

		public static UInt16 GetEntityID(Type type)
		{
			foreach (UInt16 id in EntityTypes.Keys)
			{
				if (EntityTypes[id] == type)
				{
					return id;
				}
			}
			// EntityTypes.ContainsValue

			throw new ArgumentException("Type is not present in EntityTypes table");
		}

		public static Type ResolveEntityID(UInt16 id)
		{
			return EntityTypes[id];
		}

		public static UInt16 GetEntityStateID(Type type)
		{
			foreach (UInt16 id in EntityStateTypes.Keys)
			{
				if (EntityStateTypes[id] == type)
				{
					return id;
				}
			}

			throw new ArgumentException("Type is not present in EntityStateTypes table");
		}

		public static Type ResolveEntityStateID(UInt16 id)
		{
			return EntityStateTypes[id];
		}

		public static void WriteList<T>(BinaryWriter writer, List<T> list) where T : ISaveable
		{
			writer.Write((UInt16)list.Count);

			foreach (T item in list)
			{
				item.Write(writer);
			}
		}

		public static List<T> ReadList<T>(BinaryReader reader, UInt16 version) where T : ISaveable, new()
		{
			List<T> list = new List<T>();
			UInt16 count = reader.ReadUInt16();

			for (UInt16 i = 0; i < count; i++)
			{
				T item = new T();
				item.Read(reader, version);
				list.Add(item);
			}

			return list;
		}

		public static void WriteColor(Color color, BinaryWriter writer)
		{
			writer.Write(color.A);
			writer.Write(color.R);
			writer.Write(color.G);
			writer.Write(color.B);
		}

		public static Color ReadColor(BinaryReader reader)
		{
			byte a, r, g, b;

			a = reader.ReadByte();
			r = reader.ReadByte();
			g = reader.ReadByte();
			b = reader.ReadByte();

			return Color.FromArgb(a, r, g, b);
		}

		public static void WriteBitmap(Bitmap img, BinaryWriter writer)
		{
			Stream bitmapStream = new MemoryStream();

			img.Save(bitmapStream, System.Drawing.Imaging.ImageFormat.Bmp);

			if (bitmapStream.Length > int.MaxValue)
				throw new ArgumentOutOfRangeException("Bitmap length exceeds int MaxValue (What the fuck are you saving?! O_o)");

			writer.Write((int)bitmapStream.Length);
			bitmapStream.Seek(0, SeekOrigin.Begin);
			bitmapStream.CopyTo(writer.BaseStream);
		}

		public static Bitmap ReadBitmap(BinaryReader reader)
		{
			int bytes = reader.ReadInt32();
			byte[] buffer = reader.ReadBytes(bytes);

			MemoryStream bitmapStream = new MemoryStream(buffer);
			return new Bitmap(bitmapStream);
		}

		public static void WriteVec3(OpenTK.Vector3 vec, BinaryWriter writer)
		{
			writer.Write((double)vec.X);
			writer.Write((double)vec.Y);
			writer.Write((double)vec.Z);
		}

		public static OpenTK.Vector3 ReadVec3(BinaryReader reader)
		{
			float x, y, z;

			x = (float)reader.ReadDouble();
			y = (float)reader.ReadDouble();
			z = (float)reader.ReadDouble();

			return new OpenTK.Vector3(x, y, z);
		}
	}
}
