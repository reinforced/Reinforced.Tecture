using System;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Reinforced.Storage.SideEffects;

using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Reinforced.Storage.TestCodeMaker.StoryValidation
{
    public interface IValidationGenerator
    {
        InvocationExpressionSyntax GenerateValidationCall(SideEffectBase sideEffect, out string[] staticUsings, out string[] regularUsings);
    }

    public abstract class ValidationGenerator<T>  : IValidationGenerator
        where T : SideEffectBase 
    {
        public InvocationExpressionSyntax GenerateValidationCall(SideEffectBase sideEffect, out string[] staticUsings, out string[] regularUsings)
        {
            if (sideEffect is T effect)
            {
                return Generate(effect, out staticUsings, out regularUsings);
            }
            throw new Exception("Generator type mismatch");
        }

        protected InvocationExpressionSyntax Invoke(string literalName, params ExpressionSyntax[] arguments)
        {
            var inv = InvocationExpression(IdentifierName(literalName));
            if (arguments.Length > 0)
            {
                inv = inv.WithArgumentList(ArgumentList(SeparatedList(arguments.Select(d => Argument(d)))));
            }

            return inv;
        }

        public abstract InvocationExpressionSyntax Generate(T effect, out string[] staticUsings, out string[] regularUsings);
    }
}
