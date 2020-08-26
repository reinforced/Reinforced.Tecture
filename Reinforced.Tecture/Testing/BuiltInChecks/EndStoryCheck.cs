using Reinforced.Tecture.Testing.Validation;
using Reinforced.Tecture.Tracing;
using Reinforced.Tecture.Tracing.Commands;

namespace Reinforced.Tecture.Testing.BuiltInChecks
{
    public class EndStoryCheck : CommandCheck<End>
    {
        internal EndStoryCheck(){}

        protected override string GetMessage(End command)
        {
            return $"story unexpectedly ends";
        }

        protected override bool IsActuallyValid(End command)
        {
            return true;
        }
    }
}
