using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Reinforced.Tecture.Testing.Data.SyntaxGeneration
{
    partial class Generator : IGenerator
    {
        private readonly TypeGeneratorRepository _tgr;

        protected TypeMeta Meta { get; set; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public Generator(TypeMeta typeRef, TypeGeneratorRepository tgr)
        {
            Meta = typeRef;
            foreach (var np in Meta.NestedProperties)
            {
                tgr.EnsureGeneratorFor(np.PropertyType);
            }

            _tgr = tgr;
        }

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

        protected virtual ExpressionSyntax SafeAssignment(string instanceName, string propertyName, ExpressionSyntax value)
        {
            //Set<X,Y>
            var setWithArguments = IdentifierName(nameof(CSharpTestData.Set));

            var vName = "x";
            var ident = IdentifierName(vName);
            // x.Property
            var memberAccess = MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                ident, IdentifierName(propertyName));

            // x => x.Property
            var propLambda = SimpleLambdaExpression(Parameter(Identifier(vName)), memberAccess);

            // instance, x=>x.Property, "value"
            var arguments =
                SeparatedList<ArgumentSyntax>(new[] { Argument(IdentifierName(instanceName)), Argument(propLambda), Argument(value) }
                    .ComaSeparated());

            //Set<X,Y>(instance, x=>x.Property,"value")
            var invocation = InvocationExpression(setWithArguments)
                .WithArgumentList(ArgumentList(arguments));

            return invocation;
        }
        private List<StatementSyntax> ProduceInlineableProperties(string instanceName, object instance, GenerationContext context)
        {
            List<StatementSyntax> initNodes = new List<StatementSyntax>();

            foreach (var propertyInfo in Meta.InlineProperties)
            {
                if (!Meta.IsDefault(propertyInfo, instance))
                {
                    var pName = propertyInfo.Name;
                    // "value"
                    var propValue = TypeInitConstructor.Construct(propertyInfo.PropertyType, Meta.Value(propertyInfo, instance));

                    var invocation = SafeAssignment(instanceName, pName, propValue);

                    initNodes.Add(ExpressionStatement(invocation));
                    var u = ExtractEnumUsing(propertyInfo.PropertyType);
                    context.AddUsing(u);
                }
            }

            return initNodes;
        }

        protected void ProduceNestedProperties(string instanceName, object instance, GenerationContext context)
        {

            foreach (var propertyInfo in Meta.NestedProperties)
            {
                if (!Meta.IsDefault(propertyInfo, instance))
                {
                    var generator = _tgr.GetGeneratorFor(propertyInfo.PropertyType);
                    var value = Meta.Value(propertyInfo, instance);
                    generator.New(value, context);
                    var varName = context.GetDefined(value);
                    var ae = SafeAssignment(instanceName, propertyInfo.Name, IdentifierName(varName));

                    context.LateBound.Enqueue(ExpressionStatement(ae));
                    context.AddUsing(propertyInfo.PropertyType.Namespace);
                }
            }
        }

        private void ProduceCollectionProperties(string instanceName, object instance, GenerationContext context)
        {
            List<ExpressionSyntax> initNodes = new List<ExpressionSyntax>();

            foreach (var propertyInfo in Meta.CollectionProperties)
            {
                if (!Meta.IsDefault(propertyInfo, instance))
                {
                    var value = Meta.Value(propertyInfo, instance);
                    var collCreation =
                        propertyInfo.PropertyType.IsTuple()
                            ? ProceedTuple(_tgr, value.GetTupleValues(), context)
                            : ProceedCollection(_tgr, propertyInfo.PropertyType, value as IEnumerable, context);

                    var ae = SafeAssignment(instanceName, propertyInfo.Name, collCreation);

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

        protected virtual InvocationExpressionSyntax NewInstanceExpression(Type t, GenerationContext context)
        {
            var tn = t.TypeName(context.Usings);
            var result = InvocationExpression(GenericName(nameof(CSharpTestData.New))
                .WithTypeArgumentList(TypeArgumentList(
                    SingletonSeparatedList<TypeSyntax>(tn))));
            return result;
        }

        protected virtual ExpressionSyntax EarlyChecks(Type t, object instance, GenerationContext context)
        {
            if (t.IsDictionary())
            {
                throw new Exception("TODO: implement dictionaries support in test data capture");
            }

            if (t.IsEnumerable())
            {
                return ProceedCollection(_tgr, t, (IEnumerable)instance, context);
            }

            if (t.IsTuple())
            {
                if (t.IsInlineable()) return TypeInitConstructor.Construct(t, instance);
                return ProceedTuple(_tgr, instance.GetTupleValues(), context);
            }

            return null;
        }

        public virtual ExpressionSyntax New(object instance, GenerationContext context)
        {
            if (instance == null)
                return LiteralExpression(SyntaxKind.NullLiteralExpression);

            var t = instance.GetType();

            var r = EarlyChecks(t, instance, context);
            if (r != null) return r;

            if (!context.DefinedVariable(instance, out string instanceName))
            {
                var initNodes = ProduceInlineableProperties(instanceName, instance, context);
                var result = NewInstanceExpression(t, context);

                var vbl = SingletonSeparatedList<VariableDeclaratorSyntax>(
                    VariableDeclarator(Identifier(instanceName)
                    ).WithInitializer(EqualsValueClause(result)));

                var assign = LocalDeclarationStatement(VariableDeclaration(Var)
                    .WithVariables(vbl));

                context.Declarations.Enqueue(assign);
                foreach (var nd in initNodes)
                {
                    context.Declarations.Enqueue(nd);
                }

                ProduceNestedProperties(instanceName, instance, context);
                ProduceCollectionProperties(instanceName, instance, context);
            }

            return IdentifierName(instanceName);
        }


    }
}
