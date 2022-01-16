using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Reinforced.Tecture.Testing.Data.SyntaxGeneration
{
    partial class Generator : IGenerator
    {
        internal static ExpressionSyntax ProceedTuple(TypeGeneratorRepository tgr, IEnumerable<(Type, object)> values,
           GenerationContext context)
        {
            var variables = new List<ExpressionSyntax>();
            foreach (var item in values)
            {
                if (item.Item1.IsInlineable() || item.Item2 == null)
                {
                    variables.Add(TypeInitConstructor.Construct(item.Item1, item.Item2));
                }
                else
                {
                    var generator = tgr.GetGeneratorFor(item.Item1);
                    generator.New(item.Item2, context);
                    var name = context.GetDefined(item.Item2);
                    variables.Add(IdentifierName(name));
                }
            }

            var collectionStrategy = tgr.CollectionStrategies.GetTupleStrategy(values.Select(x => x.Item1));

            return collectionStrategy.Generate(variables, context.Usings);
        }
        internal static ExpressionSyntax ProceedCollection(TypeGeneratorRepository tgr, Type collectionType, IEnumerable values, GenerationContext context,
            bool forceArray = false)
        {
            var elementType = collectionType.ElementType();
            var generator = elementType.IsInlineable() ? null : tgr.GetGeneratorFor(collectionType.ElementType());

            var variables = new List<ExpressionSyntax>();
            foreach (var item in values)
            {
                if (item == null)
                {
                    variables.Add(LiteralExpression(SyntaxKind.NullLiteralExpression));
                }
                else
                {
                    if (generator != null)
                    {
                        generator.New(item, context);
                        var name = context.GetDefined(item);
                        variables.Add(IdentifierName(name));
                    }
                    else
                    {
                        var inline = TypeInitConstructor.Construct(elementType, item);
                        variables.Add(inline);
                    }
                }
            }

            if (elementType.IsAnonymousType())
            {
                elementType = typeof(Dictionary<string, object>);
            }

            if (generator is AnonymousDataGenerator && forceArray)
            {
                var typeName = elementType.TypeName(context.Usings);
                
                return ArrayCreationExpression(
                        ArrayType(typeName)
                            .WithRankSpecifiers(
                                SingletonList<ArrayRankSpecifierSyntax>(
                                    ArrayRankSpecifier(
                                        SingletonSeparatedList<ExpressionSyntax>(
                                            OmittedArraySizeExpression())))))
                    .WithInitializer(
                        InitializerExpression(
                            SyntaxKind.ArrayInitializerExpression,
                            SeparatedList<ExpressionSyntax>(
                                variables.ComaSeparated())));
            }

            var collectionStrategy = tgr.CollectionStrategies.GetStrategy(collectionType);

            return collectionStrategy.Generate(variables, context.Usings);
        }
    }
}
