using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TISFAT
{
    public interface IEntity : ISaveable
    {
        IEntityState Interpolate(float t, IEntityState current, IEntityState target);
        void Draw(IEntityState state);
    }

    public interface IEntityState : ISaveable
    {
    }
}
