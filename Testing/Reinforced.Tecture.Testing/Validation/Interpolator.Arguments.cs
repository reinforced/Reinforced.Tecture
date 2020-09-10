using System;
using System.Collections;
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
            if (value == null)
            {
                return TypeInitConstructor.Null();
            }

            var type = value.GetType();
            ExpressionSyntax result;
            if (type.IsInlineable())
            {
                EnsureUsing(cecp.Type);
                result = TypeInitConstructor.Construct(cecp.Type, value);
            }
            else
            {
                if (type.IsDictionary())
                {
                    var (keyType, valueType) = type.GetDictionaryParameters();
                    if (keyType != null && valueType != null && keyType.IsInlineable())
                    {
                        return MakeDictionary(value as IDictionary, keyType, valueType);
                    }
                }
                else if (type.IsCollection())
                {
                    var elementType = type.GetCollectionElementType();
                    if (elementType != null && elementType.IsInlineable())
                    {
                        return MakeCollection(value as IEnumerable, elementType);
                    }
                }
                throw new Exception($"Can not inline {type} into tests");
            }

            return result;
        }

        private ExpressionSyntax MakeDictionary(IDictionary dct, Type keType,Type valType)
        {
            var inits = new List<InitializerExpressionSyntax>();
            foreach (DictionaryEntry dictionaryEntry in dct)
            {
                if (dictionaryEntry.Value == null || dictionaryEntry.Value.GetType().IsInlineable())
                {
                    var keyExpression = TypeInitConstructor.Construct(keType, dictionaryEntry.Key);
                    var valueExpression =
                        dictionaryEntry.Value == null
                            ? TypeInitConstructor.Null()
                          : TypeInitConstructor.Construct(dictionaryEntry.Value.GetType(), dictionaryEntry.Value);
                    inits.Add(InitializerExpression(
                        SyntaxKind.ComplexElementInitializerExpression,
                        SeparatedList<ExpressionSyntax>(
                            new SyntaxNodeOrToken[]{
                                keyExpression,
                                Token(SyntaxKind.CommaToken),
                                valueExpression})));
                }
            }

            var nt = typeof(Dictionary<,>).MakeGenericType(keType, valType);
            var newDictionary = nt.TypeName(_usings);
            var arg = SeparatedList<ExpressionSyntax>(inits.ComaSeparated());

            var objCreation = ObjectCreationExpression(newDictionary)
                .WithArgumentList(ArgumentList())
                .WithInitializer(
                    InitializerExpression(
                        SyntaxKind.CollectionInitializerExpression, arg));

            return objCreation;
        }

        private ExpressionSyntax MakeCollection(IEnumerable dct, Type elementType)
        {
            var inits = new List<ExpressionSyntax>();
            foreach (var value in dct)
            {
                if (value==null) inits.Add(TypeInitConstructor.Null());
                else
                {
                    var tp = value.GetType();
                    if (!tp.IsInlineable())
                    {
                        throw new Exception($"Can not inline value '{value}' of type '{tp}' into tests");
                    }
                    inits.Add(TypeInitConstructor.Construct(tp,value));
                }
            }

            var arrayType = ArrayType(elementType.TypeName(_usings));
            var arg = SeparatedList<ExpressionSyntax>(inits.ComaSeparated());

            var objCreation = ArrayCreationExpression(
                    arrayType
                        .WithRankSpecifiers(
                            SingletonList<ArrayRankSpecifierSyntax>(
                                ArrayRankSpecifier(
                                    SingletonSeparatedList<ExpressionSyntax>(
                                        OmittedArraySizeExpression())))))
                .WithInitializer(
                    InitializerExpression(
                        SyntaxKind.ArrayInitializerExpression,arg));

            return objCreation;
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
