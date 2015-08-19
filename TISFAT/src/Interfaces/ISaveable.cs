using System;
using System.IO;

namespace TISFAT
{
    public interface ISaveable
	{
		void Write(BinaryWriter writer);
		void Read(BinaryReader reader, UInt16 version);
	}
}
