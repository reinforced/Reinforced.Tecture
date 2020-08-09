using Microsoft.CodeAnalysis.CSharp.Syntax;
using Reinforced.Storage.SideEffects.Exact;
using Reinforced.Storage.TestCodeMaker.StoryValidation.Common;
using Reinforced.Storage.Testing.Stories.Remove;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
namespace Reinforced.Storage.TestCodeMaker.StoryValidation.Common
{
    public class RemoveEntityValidationGenerator : ValidationGenerator<RemoveSideEffect>
    {
        public override InvocationExpressionSyntax Generate(RemoveSideEffect effect, out string[] staticUsings, out string[] regularUsings)
        {
            staticUsings = new[] { typeof(RemoveAssertions).FullName };
            regularUsings = new[] { effect.EntityType.Namespace };

            return InvocationExpression(GenericName(Identifier(nameof(RemoveAssertions.Remove))).WithTypeArgumentList(TypeArgumentList(
                SingletonSeparatedList<TypeSyntax>(
                    IdentifierName(effect.EntityType.Name)))));
        }
    }
}

namespace Reinforced.Storage.TestCodeMaker.StoryValidation
{
    public static partial class AutoValidatorExtensions
    {
        public static PerSideEffectGeneratorSetup<RemoveSideEffect> Validate(this PerSideEffectGeneratorSetup<RemoveSideEffect> x)
        {
            return x.Register(new RemoveEntityValidationGenerator());
        }
    }
}
