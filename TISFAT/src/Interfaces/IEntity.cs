using System;
using System.Collections.Generic;
using System.Drawing;
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

        void DrawEditable(IEntityState state);

        bool TryManipulate(IEntityState state, Point location);

        void ManipulateStart(IEntityState state, Point location);

        void ManipulateUpdate(IEntityState state, Point location);

        void ManipulateEnd(IEntityState state, Point location);

    }

    public interface IEntityState : ISaveable
    {
    }
}
