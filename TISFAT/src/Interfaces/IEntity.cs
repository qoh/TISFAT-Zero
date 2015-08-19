using System.Drawing;

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

		IEntityState CreateRefState();

	}

	public interface IEntityState : ISaveable
	{
		IEntityState CreateRefState();
	}
}
