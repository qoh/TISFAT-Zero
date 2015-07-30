using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TISFAT
{
    public interface ISaveable
    {
        void Write(BinaryWriter writer);
        void Read(BinaryReader reader, UInt16 version);
    }
}
