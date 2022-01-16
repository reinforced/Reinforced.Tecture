using Microsoft.CodeAnalysis.CSharp.Syntax;

using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Reinforced.Tecture.Testing.Data.SyntaxGeneration
{
    internal interface IGenerator
    {
        ExpressionSyntax New(object instance, GenerationContext context);
    }

    internal static class GeneratorExtensions
    {
        public static void Proceed(this IGenerator gen, object instance, GenerationContext context)
        {
            

        }
    }
}