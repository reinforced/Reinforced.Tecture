using Microsoft.CodeAnalysis.CSharp.Syntax;
using Reinforced.Storage.SideEffects.Exact;
using Reinforced.Storage.TestCodeMaker.StoryValidation.Common;
using Reinforced.Storage.Testing.Stories.Remove;
using Reinforced.Storage.Testing.Stories.Update;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
namespace Reinforced.Storage.TestCodeMaker.StoryValidation.Common
{
    public class UpdateEntityValidationGenerator : ValidationGenerator<UpdateSideEffect>
    {
        public override InvocationExpressionSyntax Generate(UpdateSideEffect effect, out string[] staticUsings, out string[] regularUsings)
        {
            staticUsings = new[] { typeof(UpdateAssertions).FullName };
            regularUsings = new[] { effect.EntityType.Namespace };

            return InvocationExpression(GenericName(Identifier(nameof(UpdateAssertions.Update))).WithTypeArgumentList(TypeArgumentList(
                SingletonSeparatedList<TypeSyntax>(
                    IdentifierName(effect.EntityType.Name)))));
        }
    }
}

namespace Reinforced.Storage.TestCodeMaker.StoryValidation
{
    public static partial class AutoValidatorExtensions
    {
        public static PerSideEffectGeneratorSetup<UpdateSideEffect> Validate(this PerSideEffectGeneratorSetup<UpdateSideEffect> x)
        {
            return x.Register(new UpdateEntityValidationGenerator());
        }
    }
}
