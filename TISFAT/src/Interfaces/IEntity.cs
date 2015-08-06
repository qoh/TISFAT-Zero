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

        IManipulatable TryManipulate(IEntityState state, Point location);

        void ManipulateStart(IManipulatable target, Point location);

        void ManipulateUpdate(IManipulatable target, Point location);

        void ManipulateEnd(IManipulatable target, Point location);

    }

    public interface IEntityState : ISaveable
    {
    }
}
