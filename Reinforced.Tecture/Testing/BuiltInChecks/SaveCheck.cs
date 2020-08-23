using Reinforced.Tecture.Testing.Validation;
using Reinforced.Tecture.Tracing;

namespace Reinforced.Tecture.Testing.BuiltInChecks
{
    public class SaveCheck : CommandCheck<Save>
    {
        internal SaveCheck() { }

        protected override string GetMessage(Save command)
        {
            return "expected saving to storage, but something went wrong";
        }

        protected override bool IsActuallyValid(Save effect)
        {
            return true;
        }
    }
}
