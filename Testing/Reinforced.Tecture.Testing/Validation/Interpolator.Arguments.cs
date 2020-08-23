using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Testing.Checks;
using Reinforced.Tecture.Testing.Checks.ParameterDescription;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Reinforced.Tecture.Testing.Validation
{
    partial class CSharpUnitTestGenerator
    {
        private IEnumerable<ExpressionSyntax> ProduceArguments(CommandBase command, CheckDescription desc)
        {
            var args = desc.GetCheckParameters(command);
            foreach (var o in args)
            {
                if (o is CommandExtractCheckParameter cecp)
                {
                    yield return ExtractFromCommand(command, cecp);
                }

                if (o is AssertionCheckParameter acp)
                {
                    yield return MakeAssertions(command, acp);
                }
            }
        }

        private ExpressionSyntax ExtractFromCommand(CommandBase command, CommandExtractCheckParameter cecp)
        {
            var value = cecp.Extractor.DynamicInvoke(command);
            ExpressionSyntax result;
            if (cecp.Type.IsInlineable())
            {
                EnsureUsing(cecp.Type);
                result = TypeInitConstructor.Construct(cecp.Type, value);
            }
            else
            {
                throw new Exception($"{cecp.Type} is not inlineable into tests");
            }

            return result;
        }

        private ExpressionSyntax MakeAssertions(CommandBase command, AssertionCheckParameter cecp)
        {
            var value = cecp.Extractor.DynamicInvoke(command);

            var statements = new Stack<StatementSyntax>();
            const string varName = "x";
            statements.Push(ReturnStatement(LiteralExpression(SyntaxKind.TrueLiteralExpression)));
            if (value == null)
            {
                statements.Push(IfReturnFalse(IdentifierName(varName), LiteralExpression(SyntaxKind.NullLiteralExpression)));
            }
            else
            {
                var tp = value.GetType();
                if (tp.IsInlineable())
                {
                    var v = TypeInitConstructor.Construct(tp, value);
                    statements.Push(IfReturnFalse(IdentifierName(varName), v));
                }
                else
                {
                    var assertProps = cecp.PropertiesToAssert;
                    if (assertProps == null || assertProps.Length == 0)
                        assertProps =
                            tp.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);

                    var props = assertProps
                        .Where(x => x.PropertyType.IsInlineable());
                    foreach (var propertyInfo in props)
                    {
                        var access = MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName(varName), IdentifierName(propertyInfo.Name));
                        var propValue = propertyInfo.GetValue(value);
                        var inlinedValue = TypeInitConstructor.Construct(propertyInfo.PropertyType, propValue);

                        statements.Push(IfReturnFalse(access, inlinedValue));
                    }
                }
            }

            var blk = Block(List(statements));

            var lambda = SimpleLambdaExpression(Parameter(Identifier(varName)), blk);
            return lambda;
        }


        private StatementSyntax IfReturnFalse(ExpressionSyntax left, ExpressionSyntax right)
        {
            return IfStatement(
                BinaryExpression(
                    SyntaxKind.NotEqualsExpression,
                    left,
                    right),
                ReturnStatement(
                    LiteralExpression(
                        SyntaxKind.FalseLiteralExpression)).WithoutTrivia().WithAdditionalAnnotations(Annotations.IfStatement));
        }


    }
}
