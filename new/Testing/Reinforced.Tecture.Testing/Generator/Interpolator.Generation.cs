using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Testing.Generation;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Reinforced.Tecture.Testing.Generator
{
    partial class CSharpTestGenerator
    {
        private InvocationExpressionSyntax Generate(CommandBase command, CheckDescription desc)
        {
            var result = ProduceInvoke(command, desc);
            
            var args = ProduceArguments(command, desc).ToArray();
            if (args.Length > 0)
            {
                result = result.WithArgumentList(ArgumentList(SeparatedList(args.Select(Argument))));
            }

            foreach (var descAdditionalUsing in desc.AdditionalUsings)
            {
                EnsureUsing(descAdditionalUsing);
            }
            return result;
        }

        private IEnumerable<ExpressionSyntax> ProduceArguments(CommandBase command, CheckDescription desc)
        {
            var args = desc.MethodArgumentsEvaluator(command);
            foreach (var o in args)
            {
                if (o != null)
                {
                    var t = o.GetType();
                    EnsureUsing(t);
                    yield return TypeInitConstructor.Construct(t, o);
                }
                else
                {
                    yield return LiteralExpression(SyntaxKind.NullLiteralExpression);
                }
            }
        }

        private InvocationExpressionSyntax ProduceInvoke(CommandBase command, CheckDescription desc)
        {
            EnsureUsingStatic(desc.Method);
            if (desc.Method.IsGenericMethodDefinition) return InvokeTyped(command, desc);
            return InvokeSimple(desc);
        }

        private InvocationExpressionSyntax InvokeSimple(CheckDescription desc)
        {
            return InvocationExpression(IdentifierName(desc.Method.Name));
        }

        private InvocationExpressionSyntax InvokeTyped(CommandBase command, CheckDescription desc)
        {
            var types = desc.MethodTypeArgumentsEvaluator(command);
            foreach (var type in types)
            {
                EnsureUsing(type);
            }

            if (types.Length == 1)
            {
                return InvocationExpression(GenericName(desc.Method.Name)
                    .WithTypeArgumentList(
                        TypeArgumentList(
                        SingletonSeparatedList<TypeSyntax>(
                            IdentifierName(types[0].Name)))));
            }
            else
            {
                var typeParams = SeparatedList<TypeSyntax>(types.Select(x => IdentifierName(x.Name)));
                return InvocationExpression(GenericName(desc.Method.Name)
                    .WithTypeArgumentList(
                        TypeArgumentList(typeParams)));
            }

        }
    }
}
