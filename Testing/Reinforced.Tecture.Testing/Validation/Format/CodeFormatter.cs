using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Reinforced.Tecture.Testing.Validation.Format
{

    public partial class CodeFormatter : Data.Format.CodeFormatter
    {
        /// <summary>Called when the visitor visits a ArgumentSyntax node.</summary>
        public override SyntaxNode VisitArgument(ArgumentSyntax node)
        {
            if (node.ContainsAnnotations && node.HasAnnotation(Annotations.ThenArgument))
            {
                var r = base.VisitArgument(node) as ArgumentSyntax;
                return r.WithLeadingTrivia(Tabs.BrTabs());
            }
            return base.VisitArgument(node);
        }

        /// <summary>Called when the visitor visits a ArgumentListSyntax node.</summary>
        public override SyntaxNode VisitArgumentList(ArgumentListSyntax node)
        {
            if (node.ContainsAnnotations && node.HasAnnotation(Annotations.ThenArgument))
            {
                using (Tab())
                {
                    var r = base.VisitArgumentList(node) as ArgumentListSyntax;

                    return r
                        .WithCloseParenToken(Token(SyntaxKind.CloseParenToken).WithLeadingTrivia(Tabs.BrTabs_Prev()))
                        .WithOpenParenToken(Token(SyntaxKind.OpenParenToken).WithLeadingTrivia(Tabs.BrTabs_Prev()))
                        ;
                }
            }
            return base.VisitArgumentList(node);
        }
    }
}
