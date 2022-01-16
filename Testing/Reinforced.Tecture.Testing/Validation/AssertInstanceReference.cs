using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Reinforced.Tecture.Testing.Validation
{
    public class AssertInstanceReference
    {
        public HashSet<string> Usings { get; private set; }
        public object Value { get; private set; }

        public Type DeclaredType { get; private set; }

        public Type ActualType { get; private set; }

        public ExpressionSyntax Expression { get; private set; }

        public string Path { get; private set; }

        public int IterationDepth { get; set; }

        public string AssertionName { get; set; } = "Assert";

        public override string ToString()
        {
            return $"{Value} ({Path}), {DeclaredType.Name}/{(ActualType == null ? "null" : ActualType.Name)}";
        }


        public AssertInstanceReference(object value, Type declaredType, ExpressionSyntax expression, string path, HashSet<string> usings)
        {
            Value = value;
            DeclaredType = declaredType;
            Expression = expression;
            if (value != null)
            {
                ActualType = value.GetType();
                if (DeclaredType != ActualType && !ActualType.IsAssignableFrom(DeclaredType) && DeclaredType!=typeof(Type))
                {
                    Expression = Cast(expression, ActualType,usings);
                }
            }
            else ActualType = null;
            Path = path;
            Usings = usings;
        }

        private static ElementAccessExpressionSyntax Bracket(ExpressionSyntax expr, Type keyType, object key)
            => // {expr}[{key}], key must be inlinable
                ElementAccessExpression(expr,
                    BracketedArgumentList(
                        SingletonSeparatedList(
                            Argument(TypeInitConstructor.Construct(keyType,key))
                        )));

        private static InvocationExpressionSyntax Cast(ExpressionSyntax expr, Type t, HashSet<string> usings)
            => // As<{t}>({expr})
                InvocationExpression(
                    GenericName(
                            Identifier("As"))
                        .WithTypeArgumentList(
                            TypeArgumentList(
                                SingletonSeparatedList<TypeSyntax>(t.TypeName(usings)))))
                .WithArgumentList(
                    ArgumentList(
                        SingletonSeparatedList<ArgumentSyntax>(
                            Argument(expr))));

        public IEnumerable<AssertInstanceReference> Properties()
        {
            var props = ActualType
                .GetProperties(BindingFlags.Public| BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.FlattenHierarchy)
                .Where(x => x.CanRead);

            var anon = ActualType.IsAnonymousType();

            foreach (var prop in props)
            {
                var value = prop.GetValue(Value);
                
                if (ValidationGenerator.ForbiddenTypes.Any(x=>x.IsAssignableFrom(prop.PropertyType)))
                    continue;

                if (prop.GetCustomAttribute<NoValidateAttribute>()!=null) continue;
                
                ExpressionSyntax expression =
                    anon ? (ExpressionSyntax)AnonymousProperty(Expression, prop,Usings) : RegularProperty(Expression, prop)
                    ;
                yield return new AssertInstanceReference(value, prop.PropertyType,
                    expression, $"{prop.Name} of {Path}", Usings)
                {
                    AssertionName = AssertionName
                };
            }
        }
        
        private static InvocationExpressionSyntax AnonymousProperty(ExpressionSyntax expr, PropertyInfo pi, HashSet<string> usings)
        {
            var args = new ArgumentSyntax[]
            {
                Argument(expr),
                Argument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(pi.Name)))
            };
            
            //Prop<{pi.PropertyType}>({expr},{pi.Name})
            return InvocationExpression(
                    GenericName(
                            Identifier("Prop"))
                        .WithTypeArgumentList(
                            TypeArgumentList(
                                SingletonSeparatedList<TypeSyntax>(pi.PropertyType.TypeName(usings)))))
                .WithArgumentList(
                    ArgumentList(SeparatedList<ArgumentSyntax>(args.ComaSeparated())));
        }

        private static MemberAccessExpressionSyntax RegularProperty(ExpressionSyntax expr, PropertyInfo pi)
            => // {expr}.{pi.PropertyName}
                MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                expr, IdentifierName(pi.Name));

        public IEnumerable<(string, AssertInstanceReference)> Items()
        {
            var enumer = Value as System.Collections.IEnumerable;
            var declared = ActualType.GetCollectionElementType();
            int idx = 0;
            foreach (var enumeration in enumer)
            {
                var instance = new AssertInstanceReference(
                    enumeration,
                    declared,
                    IdentifierName($"r{IterationDepth}_{idx}"),
                    $"item #{idx} of {Path}",
                    Usings)

                {
                    AssertionName = AssertionName,
                    IterationDepth = this.IterationDepth + 1
                };
                
                yield return ($"r{IterationDepth}_{idx}", instance);
                idx++;
            }
        }
        public int KeysCount()
        {
            var enumer = Value as System.Collections.IDictionary;
            return enumer.Count;
        }
        public IEnumerable<AssertInstanceReference> Dictionary()
        {
            var enumer = Value as System.Collections.IDictionary;
            var declaredKv = ActualType.GetDictionaryParameters();

            foreach (var key in enumer.Keys)
            {
                var value = enumer[key];
                var instance = new AssertInstanceReference(
                    value,
                    declaredKv.Item1,
                    Bracket(Expression,declaredKv.Item1,key),
                    $"value for {key} in {Path}",
                    Usings)

                {
                    AssertionName = AssertionName,
                    IterationDepth = this.IterationDepth + 1
                };
                
                yield return instance;
            }
        }
    }
}