using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Reinforced.Tecture.Testing
{
    internal static class Formats
    {
        public static SyntaxTriviaList BrWith4Tabs()
        {
            return TriviaList(LineFeed, Formats.Tab, Formats.Tab, Formats.Tab, Formats.Tab);
        }

        public static SyntaxTriviaList BrWith5Tabs()
        {
            return TriviaList(LineFeed, Formats.Tab, Formats.Tab, Formats.Tab, Formats.Tab, Formats.Tab);
        }

        public static SyntaxToken BrWith5Tabs(this SyntaxToken t)
        {
            return t.WithTrailingTrivia(LineFeed, Tab, Tab, Tab, Tab, Tab);
        }

        public static readonly SyntaxTrivia Tab = Whitespace("\t");

        public static SyntaxTrivia[] Tabs(int t)
        {
            var l = new List<SyntaxTrivia>();
            for (int i = 0; i < t; i++)
            {
                l.Add(Tab);
            }

            return l.ToArray();
        }

        /// <summary>
        /// Formats class declaration
        /// </summary>
        /// <param name="cds">Existing class declaration</param>
        /// <returns></returns>
        public static ClassDeclarationSyntax Format(this ClassDeclarationSyntax cds)
        {
            var idnt = cds.Identifier.WithTrailingTrivia(LineFeed);
            return cds
                .WithIdentifier(idnt)
                .WithKeyword(
                    Token(
                        TriviaList(Tab),
                        SyntaxKind.ClassKeyword,
                        TriviaList(
                            Space)))
                .WithOpenBraceToken(
                    Token(
                        TriviaList(Tab),
                        SyntaxKind.OpenBraceToken,
                        TriviaList(
                            LineFeed)))
                .WithCloseBraceToken(
                    Token(
                        TriviaList(
                            new[]
                            {
                                LineFeed,
                                Tab
                            }),
                        SyntaxKind.CloseBraceToken,
                        TriviaList(
                            LineFeed)));

        }

        public static NamespaceDeclarationSyntax Format(this NamespaceDeclarationSyntax nds)
        {
            var newName = nds.Name.WithTrailingTrivia(LineFeed);

            return nds
                .WithName(newName)
                .WithNamespaceKeyword(
                    Token(
                        TriviaList(LineFeed),
                        SyntaxKind.NamespaceKeyword,
                        TriviaList(Space)))
                .WithOpenBraceToken(
                    Token(
                        TriviaList(),
                        SyntaxKind.OpenBraceToken,
                        TriviaList(LineFeed)))
                .WithCloseBraceToken(
                    Token(
                        TriviaList(LineFeed),
                        SyntaxKind.CloseBraceToken,
                        TriviaList()));
        }

        public static BlockSyntax Format(this BlockSyntax mds)
        {
            return mds.WithOpenBraceToken(
                    Token(
                        TriviaList(LineFeed, Tab, Tab),
                        SyntaxKind.OpenBraceToken,
                        TriviaList(
                            LineFeed)))
                .WithCloseBraceToken(
                    Token(
                        TriviaList(
                            new[]
                            {
                                LineFeed,
                                Tab,Tab
                            }),
                        SyntaxKind.CloseBraceToken,
                        TriviaList(
                            LineFeed)));
        }

        public static UsingDirectiveSyntax FormatUsing(this UsingDirectiveSyntax uds)
        {
            return uds.WithUsingKeyword(
                    Token(
                        TriviaList(),
                        SyntaxKind.UsingKeyword,
                        TriviaList(
                            Space)))
                .WithSemicolonToken(
                    Token(
                        TriviaList(),
                        SyntaxKind.SemicolonToken,
                        TriviaList(
                            LineFeed)));
        }
    }
}
