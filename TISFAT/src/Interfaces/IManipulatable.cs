using System.Drawing;

namespace TISFAT
{
    public class ManipulateResult
    {
        public IManipulatable Target;
        public IManipulatableParams Params;
    }

    public interface IManipulatable
    {
    }

    public interface IManipulatableParams
    {
    }
}
