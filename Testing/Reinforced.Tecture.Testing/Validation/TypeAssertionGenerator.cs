using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Reinforced.Tecture.Testing.Validation.Assertion;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Reinforced.Tecture.Testing.Validation
{
    public class TypeAssertionGenerator
    {
        internal static IEnumerable<InvocationExpressionSyntax> AssertionFor(AssertInstanceReference instance)
        {
            if (instance.Value == null)
            {
                yield return AssertionNull(instance.AssertionName,instance.Expression, $"{instance.Path} must be null");
            }
            else
            {
                if (!instance.ActualType.IsValueType) yield return AssertionNotNull(instance.AssertionName,instance.Expression,$"{instance.Path} must not be null");

                foreach (var expr in AssertionForNotNull(instance))
                    yield return expr;
            }
        }

        private static bool AssertionForBool(AssertInstanceReference instance, out InvocationExpressionSyntax expression)
        {
            expression = null;
            if (instance.ActualType == typeof(bool))
            {
                var tValue = (bool)instance.Value;
                if (tValue) expression = AssertionTrue(instance.AssertionName,instance.Expression, $"{instance.Path} must be true");
                else expression = AssertionFalse(instance.AssertionName,instance.Expression, $"{instance.Path} must be false");
            }
            if (instance.ActualType == typeof(bool?))
            {
                var tValue = (bool?)instance.Value;
                if (tValue.Value) expression = AssertionTrue(instance.AssertionName,instance.Expression, $"{instance.Path} must be true");
                else expression = AssertionFalse(instance.AssertionName,instance.Expression, $"{instance.Path} must be false");
            }

            return expression != null;
        }
        
        private static IEnumerable<InvocationExpressionSyntax> AssertionForNotNull(AssertInstanceReference instance)
        {
            if (AssertionForBool(instance, out var expr))
            {
                yield return expr;
                yield break;
            }
            
            if (instance.ActualType.IsInlineable())
            {
                var inlined = TypeInitConstructor.Construct(instance.ActualType, instance.Value);
                yield return AssertionEquals(instance.AssertionName,instance.Expression, inlined, $"{instance.Path} has invalid value");
                yield break;
            }

            if (instance.ActualType.IsDictionary())
            {
                var dicParams = instance.ActualType.GetDictionaryParameters();
                yield return AssertionEquals(
                    instance.AssertionName,
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        instance.Expression,
                        IdentifierName(nameof(IDictionary.Count)))
                    , LiteralExpression(SyntaxKind.NumericLiteralExpression,
                        Literal(instance.KeysCount())),
                    $"{instance.Path} has invalid size");
                
                if (dicParams.Item1.IsInlineable())
                {
                    foreach (var dictionaryItem in instance.Dictionary())
                    {
                        foreach (var check in AssertionFor(dictionaryItem))
                        {
                            yield return check;
                        }
                    }
                }
                yield break;
            }

            if (instance.ActualType.IsEnumerable())
            {
                var arguments = new List<ExpressionSyntax>();
                arguments.Add((instance.Expression));
                arguments.Add((T($"{instance.Path} must be composed correctly")));
                
                foreach (var lValueTuple in instance.Items())
                {
                    var setOfChecks = AssertionFor(lValueTuple.Item2).ToArray();
                    arguments.Add((WrapIntoLambda(setOfChecks,lValueTuple.Item1)));
                }

                yield return AssertForCollection(instance.AssertionName,arguments.ToArray());
                yield break;
            }

            foreach (var property in instance.Properties())
            {
                foreach (var assrt in AssertionFor(property))
                {
                    yield return assrt;
                }
            }
        }

        private static IEnumerable<ExpressionStatementSyntax> InvokationsToStatements(
            IEnumerable<InvocationExpressionSyntax> invokations)
        {
            bool needEmptyLine = false;
            foreach (var expressionSyntax in invokations)
            {
                if (expressionSyntax == null)
                {
                    needEmptyLine = true;
                    continue;
                }

                if (needEmptyLine)
                {
                    yield return ExpressionStatement(expressionSyntax).WithLeadingTrivia(CarriageReturnLineFeed);
                    needEmptyLine = false;
                }
                else yield return ExpressionStatement(expressionSyntax);
            }
        }
        internal static LambdaExpressionSyntax WrapIntoLambda(IEnumerable<InvocationExpressionSyntax> invokations,
            string parameterName)
            => // {parameterName} => { {invokations} }
                SimpleLambdaExpression(Parameter(Identifier(parameterName)))
                    .WithBlock(
                        Block(
                           InvokationsToStatements(invokations)
                        )
                    );

        private static InvocationExpressionSyntax AssertWithArguments(string assertClass, string methodName, params ExpressionSyntax[] arguments)
            =>  //Assert.{methodName}({arguments});
                InvocationExpression(
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName(assertClass),
                            IdentifierName(methodName)))
                    .WithArgumentList(
                        ArgumentList(
                            SeparatedList<ArgumentSyntax>(arguments.Select(Argument).ComaSeparated())
                            )
                        );


        private static LiteralExpressionSyntax T(string value)
            => // "{value}" (string)
                LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(value));

        private static InvocationExpressionSyntax AssertionNull(string assertClass, ExpressionSyntax expressionSyntax, string explanation)
            => // Assert.Null({expressionSyntax}, {explanation})
                AssertWithArguments(assertClass,nameof(AssertExtensions.Null), expressionSyntax, T(explanation));
        
        private static InvocationExpressionSyntax AssertionEquals(string assertClass, ExpressionSyntax actual, ExpressionSyntax expected, string explanation)
            => // Assert.Equals({actual}, {expected}, {explanation})
                AssertWithArguments(assertClass,nameof(AssertExtensions.Equal), actual, expected, T(explanation));
        
        private static InvocationExpressionSyntax AssertionNotNull(string assertClass, ExpressionSyntax expressionSyntax, string explanation)
            => // Assert.NotNull({expressionSyntax}, {explanation})
                AssertWithArguments(assertClass,nameof(AssertExtensions.NotNull), expressionSyntax, T(explanation));
        
        private static InvocationExpressionSyntax AssertionTrue(string assertClass, ExpressionSyntax expressionSyntax, string explanation)
            => // Assert.True({expressionSyntax}, {explanation})
                AssertWithArguments(assertClass,nameof(AssertExtensions.True), expressionSyntax, T(explanation));
        
        private static InvocationExpressionSyntax AssertionFalse(string assertClass, ExpressionSyntax expressionSyntax, string explanation)
            => // Assert.False({expressionSyntax}, {explanation})
                AssertWithArguments(assertClass,nameof(AssertExtensions.False), expressionSyntax, T(explanation));
        
        private static InvocationExpressionSyntax AssertForCollection(string assertClass, ExpressionSyntax[] arguments)
            => // Assert.Collection({expressionSyntax}, {explanation})
                AssertWithArguments(assertClass,nameof(AssertExtensions.Collection), arguments);
    }
}