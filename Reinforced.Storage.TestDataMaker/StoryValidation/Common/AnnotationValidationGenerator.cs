using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Reinforced.Storage.SideEffects;
using Reinforced.Storage.TestCodeMaker.StoryValidation.Common;
using Reinforced.Storage.Testing.Stories;
using Reinforced.Storage.Testing.Stories.Common;

using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Reinforced.Storage.TestCodeMaker.StoryValidation.Common
{
    public class AnnotationValidationGenerator<T> : ValidationGenerator<T> where T : SideEffectBase
    {
        public override InvocationExpressionSyntax Generate(T effect, out string[] staticUsings, out string[] regularUsings)
        {
            if (string.IsNullOrEmpty(effect.Annotation))
            {
                staticUsings = null;
                regularUsings = null;
                return null;
            }
            staticUsings = new[] {typeof(CommonAssertions).FullName};
            regularUsings = null;

            return Invoke(nameof(CommonAssertions.Annotated), LiteralExpression(
                SyntaxKind.StringLiteralExpression,
                Literal($"@\"{effect.Annotation}\"", effect.Annotation)));
        }
    }
}

namespace Reinforced.Storage.TestCodeMaker.StoryValidation
{
    public static partial class AutoValidatorExtensions
    {
        public static PerSideEffectGeneratorSetup<T> Annotation<T>(this PerSideEffectGeneratorSetup<T> x) where T : SideEffectBase
        {
            return x.Register(new AnnotationValidationGenerator<T>());
        }
    }
}
