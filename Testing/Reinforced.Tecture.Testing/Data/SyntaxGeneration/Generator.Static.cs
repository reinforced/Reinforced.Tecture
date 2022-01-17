using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
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
        
        internal static ExpressionSyntax ProceedDictionary(TypeGeneratorRepository tgr, Type dictionaryType, IDictionary values, GenerationContext context)
        {
            var dictionaryTypes = dictionaryType.GetDictionaryParameters();

            var keyType = dictionaryTypes.Item1;
            var valueType = dictionaryTypes.Item2;
            
            var keyGenerator = keyType.IsInlineable() ? null : tgr.GetGeneratorFor(keyType);
            var valueGenerator = valueType.IsInlineable() ? null : tgr.GetGeneratorFor(valueType);

            var keyValues = new List<(ExpressionSyntax, ExpressionSyntax)>();
            
            foreach (var key in values.Keys)
            {
                ExpressionSyntax keySyntax;
                ExpressionSyntax valueSyntax;
                
                var value = values[key];

                if (key == null)
                    keySyntax = LiteralExpression(SyntaxKind.NullLiteralExpression);
                else
                {
                    if (keyGenerator != null)
                    {
                        keyGenerator.New(key, context);
                        var name = context.GetDefined(key);
                        keySyntax = IdentifierName(name);
                    }
                    else
                    {
                        keySyntax = TypeInitConstructor.Construct(keyType, key,context.Usings);
                    }
                }
                
                if (value == null)
                    valueSyntax = LiteralExpression(SyntaxKind.NullLiteralExpression);
                else
                {
                    if (valueGenerator != null)
                    {
                        valueGenerator.New(value, context);
                        var name = context.GetDefined(value);
                        valueSyntax = IdentifierName(name);
                    }
                    else
                    {
                        valueSyntax = TypeInitConstructor.Construct(keyType, key,context.Usings);
                    }
                }
                keyValues.Add((keySyntax,valueSyntax));
            }

            var initializers = new List<InitializerExpressionSyntax>();

            foreach (var keyValue in keyValues)
            {
                initializers.Add(InitializerExpression(
                    SyntaxKind.ComplexElementInitializerExpression,
                    SeparatedList<ExpressionSyntax>(
                        new SyntaxNodeOrToken[]
                        {
                            keyValue.Item1,
                            Token(SyntaxKind.CommaToken),
                            keyValue.Item2
                        })));
            }
            
            var dictionaryCreation = ObjectCreationExpression(
                    GenericName(
                            Identifier("Dictionary"))
                        .WithTypeArgumentList(
                            TypeArgumentList(
                                SeparatedList<TypeSyntax>(
                                    new SyntaxNodeOrToken[]
                                    {
                                        keyType.TypeName(context.Usings),
                                        Token(SyntaxKind.CommaToken),
                                        valueType.TypeName(context.Usings)
                                    }))))
                .WithArgumentList(ArgumentList())
                .WithInitializer(
                    InitializerExpression(
                        SyntaxKind.CollectionInitializerExpression,
                        SeparatedList<ExpressionSyntax>(
                            initializers.ComaSeparated()
                            )));
            

            return dictionaryCreation;
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
