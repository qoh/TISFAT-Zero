namespace TISFAT
{
	public interface IAction
	{
		bool Do();

		bool Undo();
	}
}
