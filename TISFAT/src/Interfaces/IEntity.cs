using System.Drawing;
using TISFAT.Util;

namespace TISFAT
{
	public interface IEntity : ISaveable
	{
		IEntityState Interpolate(float t, IEntityState current, IEntityState target, EntityInterpolationMode mode);
		void Draw(IEntityState state);

		void DrawEditable(IEntityState state);

		ManipulateResult TryManipulate(IEntityState state, Point location, System.Windows.Forms.MouseButtons button, System.Windows.Forms.Keys modifiers);

		ManipulateResult TryManipulate(IEntityState state, Point location, System.Windows.Forms.MouseButtons button, System.Windows.Forms.Keys modifiers, bool fromEditor);

		void ManipulateStart(IManipulatable target, IManipulatableParams mparams, Point location);

		void ManipulateUpdate(IManipulatable target, IManipulatableParams mparams, Point location);

		void ManipulateEnd(IManipulatable target, IManipulatableParams mparams, Point location);

		IEntityState CreateRefState();

		Layer CreateDefaultLayer(uint StartTime, uint EndTime, LayerCreationArgs e);
	}

	public interface IEntityState : ISaveable
	{
		IEntityState Copy();
		IEntityState Interpolate(IEntityState target, float interpolationAmount);
	}
}
