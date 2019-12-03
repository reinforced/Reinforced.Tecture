using Microsoft.CodeAnalysis.CSharp.Syntax;
using Reinforced.Storage.SideEffects.Exact;
using Reinforced.Storage.TestCodeMaker.StoryValidation.Bulk;
using Reinforced.Storage.Testing.Stories.Bulk;

namespace Reinforced.Storage.TestCodeMaker.StoryValidation.Bulk
{
    public class AsyncBulkValidationGenerator : ValidationGenerator<AsyncBulkSideEffect>
    {
        public override InvocationExpressionSyntax Generate(AsyncBulkSideEffect effect, out string[] staticUsings, out string[] regularUsings)
        {
            staticUsings = new[] {typeof(BulkAssertions).FullName};
            regularUsings = null;

            return Invoke(nameof(BulkAssertions.ValidatedAsyncBulk));
        }
    }
}

namespace Reinforced.Storage.TestCodeMaker.StoryValidation
{
    public static partial class AutoValidatorExtensions
    {
        public static PerSideEffectGeneratorSetup<AsyncBulkSideEffect> Validate(this PerSideEffectGeneratorSetup<AsyncBulkSideEffect> x)
        {
            return x.Register(new AsyncBulkValidationGenerator());
        }
    }
}
