using Microsoft.CodeAnalysis.CSharp.Syntax;
using Reinforced.Storage.SideEffects.Exact;
using Reinforced.Storage.TestCodeMaker.StoryValidation.Bulk;
using Reinforced.Storage.Testing.Stories.Bulk;

namespace Reinforced.Storage.TestCodeMaker.StoryValidation.Bulk
{
    public class BulkValidationGenerator : ValidationGenerator<BulkSideEffect>
    {
        public override InvocationExpressionSyntax Generate(BulkSideEffect effect, out string[] staticUsings, out string[] regularUsings)
        {
            staticUsings = new[] {typeof(BulkAssertions).FullName};
            regularUsings = null;

            return Invoke(nameof(BulkAssertions.ValidatedBulk));
        }
    }
}

namespace Reinforced.Storage.TestCodeMaker.StoryValidation
{
    public static partial class AutoValidatorExtensions
    {
        public static PerSideEffectGeneratorSetup<BulkSideEffect> Validate(this PerSideEffectGeneratorSetup<BulkSideEffect> x)
        {
            return x.Register(new BulkValidationGenerator());
        }
    }
}
