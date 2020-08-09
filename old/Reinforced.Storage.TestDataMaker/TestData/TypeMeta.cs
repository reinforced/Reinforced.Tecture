using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Reinforced.Storage.TestCodeMaker.TestData
{
    class TypeMeta
    {
        public ClassDeclarationSyntax ClassDecl { get; set; }
        public List<string> UsingDirectives { get; set; }
        public List<StatementSyntax> LateBoundStatements { get; set; }
        public bool HasContent { get; set; }
        public HashSet<string> RegisteredNames { get; set; }
        public Type TypeRef { get; private set; }
        public Func<Type, object, string> GetName { get; private set; }

        public TypeMeta(Type typeRef, Func<Type, object, string> getName)
        {
            TypeRef = typeRef;
            GetName = getName;
        }

        public static string GetClassName(Type t)
        {
            return $"{t.Name}_Lib";
        }

        public string GetClassName()
        {
            return GetClassName(TypeRef);
        }

        public string GetPropName()
        {
            return $"Set_{TypeRef.Name}";
        }

        public void LateBound(object left, string propertyName, ExpressionSyntax right)
        {
            var leftPartId = SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                SyntaxFactory.IdentifierName(GetPropName()),
                SyntaxFactory.IdentifierName(GetName(TypeRef, left)));

            var leftPart = SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                leftPartId,
                SyntaxFactory.IdentifierName(propertyName));

            LateBoundStatements.Add(
                SyntaxFactory.ExpressionStatement(
                    SyntaxFactory.AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        leftPart,
                        right))
            );
        }

        public void LateBoundNull(object left, string propertyName)
        {
            LateBound(left, propertyName, SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression));
        }

        public FieldDeclarationSyntax Field(object entity, ObjectCreationExpressionSyntax newExpr)
        {
            var field = SyntaxFactory.FieldDeclaration(
                        SyntaxFactory.VariableDeclaration(SyntaxFactory.IdentifierName(TypeRef.Name))
                        .WithVariables(
                            SyntaxFactory.SingletonSeparatedList(
                                SyntaxFactory.VariableDeclarator(SyntaxFactory.Identifier(GetName(TypeRef, entity)))
                                    .WithInitializer(SyntaxFactory.EqualsValueClause(newExpr))
                                )))
                .WithModifiers(
                    SyntaxFactory.TokenList(
                        SyntaxFactory.Token(SyntaxKind.PublicKeyword), 
                        SyntaxFactory.Token(SyntaxKind.ReadOnlyKeyword)
                        ));

            return field;
        }

        public MemberAccessExpressionSyntax AccessLib(object entity)
        {
            return SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                SyntaxFactory.IdentifierName(GetPropName()),
                SyntaxFactory.IdentifierName(GetName(TypeRef, entity)));
        }

        public MemberAccessExpressionSyntax AccessLibName(string name)
        {
            return SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                SyntaxFactory.IdentifierName(GetPropName()),
                SyntaxFactory.IdentifierName(name));
        }
    }
}