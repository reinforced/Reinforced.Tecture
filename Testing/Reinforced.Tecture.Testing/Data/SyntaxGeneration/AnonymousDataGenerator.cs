using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Reinforced.Tecture.Testing.Data.SyntaxGeneration
{
    class AnonymousDataGenerator : Generator
    {
        protected override ExpressionSyntax SafeAssignment(string instanceName, string propertyName, ExpressionSyntax value)
        {
            // v1["something"]
            var ea = ElementAccessExpression(
                    IdentifierName(instanceName))
                .WithArgumentList(
                    BracketedArgumentList(
                        SingletonSeparatedList<ArgumentSyntax>(
                            Argument(LiteralExpression(SyntaxKind.StringLiteralExpression,
                                Literal(propertyName))))));
            // v1["something"] = "value"
            var assignment = AssignmentExpression(SyntaxKind.SimpleAssignmentExpression,
                ea, value);
            return assignment;
        }

        protected override InvocationExpressionSyntax NewInstanceExpression(Type t, GenerationContext context)
        {
            var result = InvocationExpression(GenericName(nameof(CSharpTestData.New))
                .WithTypeArgumentList(TypeArgumentList(
                    SingletonSeparatedList<TypeSyntax>(typeof(Dictionary<string, object>).TypeName(context.Usings)))));
            return result;
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public AnonymousDataGenerator(TypeMeta typeRef, TypeGeneratorRepository tgr) : base(typeRef, tgr)
        {
        }
    }
}
