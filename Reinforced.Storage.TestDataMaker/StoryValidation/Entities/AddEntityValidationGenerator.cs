using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Reinforced.Storage.SideEffects.Exact;
using Reinforced.Storage.TestCodeMaker.StoryValidation.Common;
using Reinforced.Storage.Testing.Stories;
using Reinforced.Storage.Testing.Stories.Add;
using Reinforced.Storage.Testing.Stories.Common;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
namespace Reinforced.Storage.TestCodeMaker.StoryValidation.Common
{
    public class AddEntityValidationGenerator : ValidationGenerator<AddSideEffect>
    {
        public override InvocationExpressionSyntax Generate(AddSideEffect effect, out string[] staticUsings, out string[] regularUsings)
        {
            staticUsings = new[] { typeof(AddAssertions).FullName };
            regularUsings = new[] { effect.EntityType.Namespace };

            return InvocationExpression(GenericName(Identifier(nameof(AddAssertions.Add))).WithTypeArgumentList(TypeArgumentList(
                SingletonSeparatedList<TypeSyntax>(
                    IdentifierName(effect.EntityType.Name)))));
        }
    }
}

namespace Reinforced.Storage.TestCodeMaker.StoryValidation
{
    public static partial class AutoValidatorExtensions
    {
        public static PerSideEffectGeneratorSetup<AddSideEffect> Validate(this PerSideEffectGeneratorSetup<AddSideEffect> x)
        {
            return x.Register(new AddEntityValidationGenerator());
        }
    }
}
