using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Reinforced.Tecture.Testing.Data.SyntaxGeneration.Collection;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Reinforced.Tecture.Testing.Data.SyntaxGeneration
{
    class Generator
    {
        private readonly TypeGeneratorRepository _tgr;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public Generator(Type typeRef, TypeGeneratorRepository tgr)
        {
            TypeRef = typeRef;
            _defaultInstance = Activator.CreateInstance(typeRef);
            var properties = typeRef.GetProperties(BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.Instance);
            InlineProperties = properties.Where(x => TypeInitConstructor.IsInlineable(x.PropertyType)).ToArray();
            CollectionProperties = properties.Where(x => x.PropertyType.IsEnumerable()).ToArray();
            NestedProperties =
                properties.Where(x => !InlineProperties.Contains(x) && !CollectionProperties.Contains(x)).ToArray();

            foreach (var np in NestedProperties)
            {
                tgr.EnsureGeneratorFor(np.PropertyType);
            }

            _tgr = tgr;
        }

        public Type TypeRef { get; set; }

        public PropertyInfo[] InlineProperties { get; private set; }
        public PropertyInfo[] CollectionProperties { get; private set; }
        public PropertyInfo[] NestedProperties { get; private set; }

        private readonly object _defaultInstance;

        private string ExtractEnumUsing(Type t)
        {
            if (t.IsNullable())
            {
                t = Nullable.GetUnderlyingType(t);
            }

            if (t.IsEnum)
            {
                return t.Namespace;
            }

            return null;
        }

        private List<ExpressionSyntax> ProduceInlineableProperties(object instance, GenerationContext context)
        {
            List<ExpressionSyntax> initNodes = new List<ExpressionSyntax>();

            foreach (var propertyInfo in InlineProperties)
            {
                var value = _tgr.Hijack.GetValue(instance, propertyInfo);
                var defValue = propertyInfo.GetValue(_defaultInstance);
                if (defValue != value)
                {
                    var pName = propertyInfo.Name;
                    var ae = AssignmentExpression(SyntaxKind.SimpleAssignmentExpression,
                        IdentifierName(pName),
                        TypeInitConstructor.Construct(propertyInfo.PropertyType, value)).WithTrailingTrivia(LineFeed);


                    initNodes.Add(ae);
                    var u = ExtractEnumUsing(propertyInfo.PropertyType);
                    context.AddUsing(u);
                }
            }

            return initNodes;
        }

        private List<ExpressionSyntax> ProduceNestedProperties(object instance, GenerationContext context)
        {
            List<ExpressionSyntax> initNodes = new List<ExpressionSyntax>();

            foreach (var propertyInfo in NestedProperties)
            {
                var value = _tgr.Hijack.GetValue(instance, propertyInfo);
                var defValue = propertyInfo.GetValue(_defaultInstance);
                if (defValue != value)
                {
                    var generator = _tgr.GetGeneratorFor(propertyInfo.PropertyType);
                    if (!context.DefinedVariable(value, out string varName))
                    {
                        var result = generator.New(value, context);

                        var vbl = SingletonSeparatedList<VariableDeclaratorSyntax>(
                            VariableDeclarator(Identifier(varName)
                            ).WithInitializer(EqualsValueClause(result)));

                        var k = LocalDeclarationStatement(VariableDeclaration(IdentifierName("var"))
                            .WithVariables(vbl));
                        context.Statements.Enqueue(k);
                    }

                    var pName = propertyInfo.Name;
                    var ae = AssignmentExpression(SyntaxKind.SimpleAssignmentExpression,
                        IdentifierName(pName),
                        IdentifierName(varName)).WithTrailingTrivia(LineFeed);


                    initNodes.Add(ae);
                    context.AddUsing(propertyInfo.PropertyType.Namespace);
                }
            }

            return initNodes;
        }

        internal static ExpressionSyntax ProceedCollection(TypeGeneratorRepository tgr, Type collectionType, IEnumerable values, GenerationContext context)
        {
            var generator = tgr.GetGeneratorFor(collectionType.ElementType());

            var variables = new List<string>();
            foreach (var item in values)
            {
                if (!context.DefinedVariable(item, out string varName))
                {
                    var result = generator.New(item, context);

                    var vbl = SingletonSeparatedList<VariableDeclaratorSyntax>(
                        VariableDeclarator(Identifier(varName)
                        ).WithInitializer(EqualsValueClause(result)));

                    var k = LocalDeclarationStatement(VariableDeclaration(IdentifierName("var")).WithVariables(vbl));
                    context.Statements.Enqueue(k);
                }

                variables.Add(varName);
            }

            var collectionStrategy = tgr.CollectionStrategies.GetStrategy(collectionType);
            var identifiers = variables.Select(IdentifierName);

            return collectionStrategy.Generate(identifiers, context.Usings);
        }

        private List<ExpressionSyntax> ProduceCollectionProperties(object instance, GenerationContext context)
        {
            List<ExpressionSyntax> initNodes = new List<ExpressionSyntax>();

            foreach (var propertyInfo in CollectionProperties)
            {
                var value = _tgr.Hijack.GetValue(instance, propertyInfo) as IEnumerable;
                var defValue = propertyInfo.GetValue(_defaultInstance);
                if (defValue != value)
                {
                    var elementType = propertyInfo.PropertyType.GetGenericArguments()[0];

                    var collCreation = ProceedCollection(_tgr, propertyInfo.PropertyType, value, context);


                    var pName = propertyInfo.Name;
                    var ae = AssignmentExpression(SyntaxKind.SimpleAssignmentExpression,
                        IdentifierName(pName),
                        collCreation).WithTrailingTrivia(LineFeed);


                    initNodes.Add(ae);
                    context.AddUsing(propertyInfo.PropertyType.Namespace);
                }
            }

            return initNodes;
        }

        private ExpressionSyntax New(object instance, GenerationContext context)
        {
            if (instance == null)
                return LiteralExpression(SyntaxKind.NullLiteralExpression);

            var t = instance.GetType();
            if (t.IsEnumerable())
            {
                return ProceedCollection(_tgr, t, (IEnumerable)instance, context);
            }

            var nested = ProduceNestedProperties(instance, context);
            var initNodes = ProduceInlineableProperties(instance, context);
            var collectionNodes = ProduceCollectionProperties(instance, context);


            var inExp = InitializerExpression(SyntaxKind.ObjectInitializerExpression,
                SeparatedList(initNodes.Union(nested).Union(collectionNodes).ToArray()));

            var result = ObjectCreationExpression(ParseTypeName(TypeRef.Name))
                .WithArgumentList(
                    ArgumentList()
                        .WithCloseParenToken(
                            Token(
                                TriviaList(),
                                SyntaxKind.CloseParenToken,
                                TriviaList(
                                    LineFeed))))
                .WithInitializer(inExp);

            return result;
        }

        public void Proceed(object instance, GenerationContext context)
        {
            var cre = New(instance, context);

            context.Statements.Enqueue(ReturnStatement(cre));
        }
    }
}
