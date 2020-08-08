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
            InlineProperties = properties.Where(x => x.PropertyType.IsInlineable()).ToArray();
            CollectionProperties = properties.Where(x => x.PropertyType.IsEnumerable() || (x.PropertyType.IsTuple() && !x.PropertyType.IsInlineable())).ToArray();
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

            return t.Namespace;
        }

        private List<ExpressionSyntax> ProduceInlineableProperties(object instance, GenerationContext context)
        {
            List<ExpressionSyntax> initNodes = new List<ExpressionSyntax>();

            foreach (var propertyInfo in InlineProperties)
            {
                var value = _tgr.Hijack.GetValue(instance, propertyInfo);
                var defValue = propertyInfo.GetValue(_defaultInstance);
                if (!Equals(defValue, value))
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

        private void ProduceNestedProperties(string instanceName, object instance, GenerationContext context)
        {

            foreach (var propertyInfo in NestedProperties)
            {
                var value = _tgr.Hijack.GetValue(instance, propertyInfo);
                var defValue = propertyInfo.GetValue(_defaultInstance);
                if (!Equals(defValue, value))
                {
                    var generator = _tgr.GetGeneratorFor(propertyInfo.PropertyType);
                    if (!context.DefinedVariable(value, out string varName))
                    {
                        var result = generator.New(value, context);

                        var vbl = SingletonSeparatedList<VariableDeclaratorSyntax>(
                            VariableDeclarator(Identifier(varName)
                            ).WithInitializer(EqualsValueClause(result)));

                        var k = LocalDeclarationStatement(VariableDeclaration(Var)
                            .WithVariables(vbl));
                        context.Declarations.Enqueue(k);
                    }

                    var ma = MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName(instanceName),
                        IdentifierName(propertyInfo.Name));

                    var ae = AssignmentExpression(SyntaxKind.SimpleAssignmentExpression,
                        ma,
                        IdentifierName(varName)).WithTrailingTrivia(LineFeed);


                    context.LateBound.Enqueue(ExpressionStatement(ae));
                    context.AddUsing(propertyInfo.PropertyType.Namespace);
                }
            }
        }

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
        internal static ExpressionSyntax ProceedCollection(TypeGeneratorRepository tgr, Type collectionType, IEnumerable values, GenerationContext context)
        {
            var generator = tgr.GetGeneratorFor(collectionType.ElementType());

            var variables = new List<string>();
            foreach (var item in values)
            {

                generator.New(item, context);
                var name = context.GetDefined(item);
                variables.Add(name);
            }

            var collectionStrategy = tgr.CollectionStrategies.GetStrategy(collectionType);
            var identifiers = variables.Select(IdentifierName);

            return collectionStrategy.Generate(identifiers, context.Usings);
        }

        private void ProduceCollectionProperties(string instanceName, object instance, GenerationContext context)
        {
            List<ExpressionSyntax> initNodes = new List<ExpressionSyntax>();

            foreach (var propertyInfo in CollectionProperties)
            {
                var value = _tgr.Hijack.GetValue(instance, propertyInfo) as IEnumerable;
                var defValue = propertyInfo.GetValue(_defaultInstance);
                if (defValue != value)
                {

                    var collCreation =
                        propertyInfo.PropertyType.IsTuple()
                            ? ProceedTuple(_tgr, value.GetTupleValues(), context)
                            : ProceedCollection(_tgr, propertyInfo.PropertyType, value, context);

                    var ma = MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName(instanceName),
                        IdentifierName(propertyInfo.Name));

                    var ae = AssignmentExpression(SyntaxKind.SimpleAssignmentExpression,
                        ma,
                        collCreation).WithTrailingTrivia(LineFeed);

                    context.LateBound.Enqueue(ExpressionStatement(ae));


                    context.AddUsing(propertyInfo.PropertyType.Namespace);
                }
            }
        }

        private static TypeSyntax Var
        {
            get
            {
                return IdentifierName("var");
            }
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

            if (t.IsTuple())
            {
                if (t.IsInlineable()) return TypeInitConstructor.Construct(t, instance);
                return ProceedTuple(_tgr, instance.GetTupleValues(), context);
            }


            if (!context.DefinedVariable(instance, out string instanceName))
            {
                var initNodes = ProduceInlineableProperties(instance, context);

                var inExp = InitializerExpression(SyntaxKind.ObjectInitializerExpression,
                    SeparatedList(initNodes.ToArray()));

                var result = ObjectCreationExpression(ParseTypeName(TypeRef.Name))
                    .WithArgumentList(
                        ArgumentList()
                            .WithCloseParenToken(
                                Token(
                                    TriviaList(),
                                    SyntaxKind.CloseParenToken,
                                    TriviaList(LineFeed))))
                    .WithInitializer(inExp);

                var vbl = SingletonSeparatedList<VariableDeclaratorSyntax>(
                    VariableDeclarator(Identifier(instanceName)
                    ).WithInitializer(EqualsValueClause(result)));

                var assign = LocalDeclarationStatement(VariableDeclaration(Var)
                    .WithVariables(vbl));

                context.Declarations.Enqueue(assign);

                ProduceNestedProperties(instanceName, instance, context);
                ProduceCollectionProperties(instanceName, instance, context);
            }

            return IdentifierName(instanceName);
        }

        public void Proceed(object instance, GenerationContext context)
        {
            var cre = New(instance, context);

            context.Declarations.Enqueue(ReturnStatement(cre));
        }
    }
}
