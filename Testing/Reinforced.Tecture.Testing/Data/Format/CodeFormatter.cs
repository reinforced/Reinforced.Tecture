using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Reinforced.Tecture.Testing.Data.Format
{
    partial class CodeFormatter : CSharpSyntaxRewriter
    {
        /// <summary>Called when the visitor visits a CompilationUnitSyntax node.</summary>
        public override SyntaxNode VisitCompilationUnit(CompilationUnitSyntax node)
        {
            return base.VisitCompilationUnit(node);
        }

        /// <summary>Called when the visitor visits a UsingDirectiveSyntax node.</summary>
        public override SyntaxNode VisitUsingDirective(UsingDirectiveSyntax node)
        {
            var r = base.VisitUsingDirective(node);
            return r.WithTrailingTrivia(Tabs.BrTabs());
        }

        /// <summary>Called when the visitor visits a InitializerExpressionSyntax node.</summary>
        public override SyntaxNode VisitInitializerExpression(InitializerExpressionSyntax node)
        {
            using (Tab())
            {

                var r = base.VisitInitializerExpression(node) as InitializerExpressionSyntax;
                if (r.IsKind(SyntaxKind.ObjectInitializerExpression))
                {
                    var ex = r.Expressions.Select(x => x.WithoutTrivia().WithLeadingTrivia(Tabs.BrTabs())).ToArray();
                    r = r.WithExpressions(SeparatedList<ExpressionSyntax>(ex)).WithCloseBraceToken(Token(SyntaxKind.CloseBraceToken).WithLeadingTrivia(Tabs.BrTabs_Prev()));
                }

                if (r.IsKind(SyntaxKind.CollectionInitializerExpression))
                {
                    var ex = r.Expressions.Select(x => x.WithoutTrivia().WithLeadingTrivia(Tabs.BrTabs())).ToArray();
                    r = r.WithExpressions(SeparatedList<ExpressionSyntax>(ex)).WithCloseBraceToken(Token(SyntaxKind.CloseBraceToken).WithLeadingTrivia(Tabs.BrTabs_Prev()));
                }

                return r;
            }
        }

        /// <summary>Called when the visitor visits a NamespaceDeclarationSyntax node.</summary>
        public override SyntaxNode VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            using (Tab())
            {
                var r = base.VisitNamespaceDeclaration(node) as NamespaceDeclarationSyntax;
                r = r.WithoutTrivia().WithLeadingTrivia(LineFeed);
                r = r.WithOpenBraceToken(Token(SyntaxKind.OpenBraceToken).WithLeadingTrivia(Tabs.BrTabs_Prev()))
                    .WithCloseBraceToken(Token(SyntaxKind.CloseBraceToken).WithLeadingTrivia(Tabs.BrTabs_Prev()));
                return r;
            }
        }


        /// <summary>Called when the visitor visits a ClassDeclarationSyntax node.</summary>
        public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            using (Tab())
            {
                var cds = base.VisitClassDeclaration(node) as ClassDeclarationSyntax;

                return cds.WithoutTrivia()
                    .AddModifiers(
                            Token(SyntaxKind.PublicKeyword).WithTrailingTrivia(Space),
                            Token(SyntaxKind.PartialKeyword).WithTrailingTrivia(Space)
                            )
                    .WithLeadingTrivia(Tabs.BrTabs())
                    .WithKeyword(Token(SyntaxKind.ClassKeyword).WithTrailingTrivia(Space))
                    .WithOpenBraceToken(Token(SyntaxKind.OpenBraceToken).WithLeadingTrivia(Tabs.BrTabs()))
                    .WithCloseBraceToken(Token(SyntaxKind.CloseBraceToken).WithLeadingTrivia(Tabs.BrTabs()));
            }
        }

        /// <summary>Called when the visitor visits a VariableDeclarationSyntax node.</summary>
        public override SyntaxNode VisitVariableDeclaration(VariableDeclarationSyntax node)
        {
            var r = base.VisitVariableDeclaration(node) as VariableDeclarationSyntax;
            if (r.Type.IsVar)
            {
                r = r.WithType(node.Type.WithTrailingTrivia(Space));
            }
            return r;
        }

        /// <summary>Called when the visitor visits a ParameterSyntax node.</summary>
        public override SyntaxNode VisitParameter(ParameterSyntax node)
        {
            var r = base.VisitParameter(node) as ParameterSyntax;
            if (r.Type != null)
            {
                r = r.WithType(r.Type.WithTrailingTrivia(Space));
            }

            return r;
        }


        /// <summary>Called when the visitor visits a MethodDeclarationSyntax node.</summary>
        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            using (Tab())
            {
                var r = base.VisitMethodDeclaration(node) as MethodDeclarationSyntax;
                return r.WithLeadingTrivia(Tabs.BrTabs()).WithReturnType(r.ReturnType.WithoutTrivia().WithTrailingTrivia(Space)).WithTrailingTrivia(LineFeed);
            }
        }

        /// <summary>Called when the visitor visits a BlockSyntax node.</summary>
        public override SyntaxNode VisitBlock(BlockSyntax node)
        {
            using (Tab())
            {
                var r = base.VisitBlock(node) as BlockSyntax;
                r = r.WithLeadingTrivia(Tabs.BrTabs_Prev())
                    .WithCloseBraceToken(Token(SyntaxKind.CloseBraceToken).WithLeadingTrivia(Tabs.BrTabs_Prev()));
                return r;
            }
        }



        public override SyntaxToken VisitToken(SyntaxToken token)
        {
            var r = base.VisitToken(token);
            r = r.WithoutTrivia();
            switch (token.Kind())
            {
                case SyntaxKind.PrivateKeyword:
                case SyntaxKind.PublicKeyword:
                case SyntaxKind.ProtectedKeyword:
                case SyntaxKind.StaticKeyword:
                case SyntaxKind.ClassKeyword:
                case SyntaxKind.OverrideKeyword:
                case SyntaxKind.InternalKeyword:
                case SyntaxKind.VarKeyword:
                case SyntaxKind.UsingKeyword:
                case SyntaxKind.NamespaceKeyword:
                case SyntaxKind.ReturnKeyword:
                case SyntaxKind.StructKeyword:
                case SyntaxKind.NewKeyword:
                case SyntaxKind.IfKeyword:
                case SyntaxKind.YieldKeyword:
                case SyntaxKind.CommaToken:
                    return r.WithTrailingTrivia(Space);
                case SyntaxKind.OpenBraceToken:
                case SyntaxKind.CloseBraceToken:
                case SyntaxKind.ColonToken:
                    return r.WithTrailingTrivia(Space).WithLeadingTrivia(Space);
            }
            return r;
        }

        /// <summary>Called when the visitor visits a StructDeclarationSyntax node.</summary>
        public override SyntaxNode VisitStructDeclaration(StructDeclarationSyntax node)
        {
            using (Tab())
            {
                return base.VisitStructDeclaration(node);
            }
        }

        /// <summary>Called when the visitor visits a AssignmentExpressionSyntax node.</summary>
        public override SyntaxNode VisitAssignmentExpression(AssignmentExpressionSyntax node)
        {
            if (IsWithin<InitializerExpressionSyntax>())
            {
                var r = base.VisitAssignmentExpression(node);
                return r.WithTrailingTrivia(LineFeed);
            }
            return base.VisitAssignmentExpression(node);
        }

        /// <summary>Called when the visitor visits a BinaryExpressionSyntax node.</summary>
        public override SyntaxNode VisitBinaryExpression(BinaryExpressionSyntax node)
        {

            var r = base.VisitBinaryExpression(node) as BinaryExpressionSyntax;

            return r.WithOperatorToken(r.OperatorToken.WithLeadingTrivia(Space).WithTrailingTrivia(Space));
        }
    }
}
