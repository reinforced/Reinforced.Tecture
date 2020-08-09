using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Reinforced.Storage.SideEffects;
using Reinforced.Storage.SideEffects.Exact;
using Reinforced.Storage.TestCodeMaker.StoryValidation.Common;
using Reinforced.Storage.Testing.Stories;
using Reinforced.Storage.Testing.Stories.Common;

using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Reinforced.Storage.TestCodeMaker.StoryValidation.Common
{
    public class CommentValidationGenerator : ValidationGenerator<CommentSideEffect>
    {
        public override InvocationExpressionSyntax Generate(CommentSideEffect effect, out string[] staticUsings, out string[] regularUsings)
        {
            if (string.IsNullOrEmpty(effect.Annotation))
            {
                staticUsings = null;
                regularUsings = null;
                return null;
            }
            staticUsings = new[] {typeof(CommonAssertions).FullName};
            regularUsings = null;

            return Invoke(nameof(CommonAssertions.Comment), LiteralExpression(
                SyntaxKind.StringLiteralExpression,
                Literal($"@\"{effect.Annotation}\"", effect.Annotation)));
        }
    }
}

namespace Reinforced.Storage.TestCodeMaker.StoryValidation
{
    public static partial class AutoValidatorExtensions
    {
        public static PerSideEffectGeneratorSetup<CommentSideEffect> Validate(this PerSideEffectGeneratorSetup<CommentSideEffect> x)
        {
            return x.Register(new CommentValidationGenerator());
        }
    }
}
