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

        ManipulateResult TryManipulate(IEntityState state, Point location, System.Windows.Forms.MouseButtons button, System.Windows.Forms.Keys modifiers);

        void ManipulateStart(IManipulatable target, IManipulatableParams mparams, Point location);

        void ManipulateUpdate(IManipulatable target, IManipulatableParams mparams, Point location);

        void ManipulateEnd(IManipulatable target, IManipulatableParams mparams, Point location);

    }

    public interface IEntityState : ISaveable
    {
    }
}
