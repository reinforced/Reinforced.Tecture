using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Reinforced.Tecture.Testing.Data.SyntaxGeneration.Collection.Strategies
{
    class ArrayCreationStrategy : ICollectionCreationStrategy
    {
        private readonly Type _elementType;

        public ArrayCreationStrategy(Type elementType)
        {
            _elementType = elementType;
        }

        private IEnumerable<SyntaxNodeOrToken> WithComas(IEnumerable<ExpressionSyntax> items)
        {
            bool first = true;
            foreach (var expressionSyntax in items)
            {
                if (!first) yield return Token(SyntaxKind.CommaToken);
                else first = false;
                yield return expressionSyntax;
            }
        }
        public ArrayCreationExpressionSyntax GenerateArray(IEnumerable<ExpressionSyntax> members, HashSet<string> usings)
        {
            var comaSeparated = WithComas(members).ToArray();

            return ArrayCreationExpression(
                    ArrayType(_elementType.TypeName(usings))
                        .WithRankSpecifiers(
                            SingletonList<ArrayRankSpecifierSyntax>(
                                ArrayRankSpecifier(
                                    SingletonSeparatedList<ExpressionSyntax>(
                                        OmittedArraySizeExpression())))))
                .WithInitializer(
                    InitializerExpression(
                        SyntaxKind.ArrayInitializerExpression,
                        SeparatedList<ExpressionSyntax>(comaSeparated)));
        }
        public ExpressionSyntax Generate(IEnumerable<ExpressionSyntax> members, HashSet<string> usings)
        {
            return GenerateArray(members, usings);
        }
    }
}
