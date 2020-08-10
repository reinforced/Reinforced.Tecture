using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Testing.Generation;
using Reinforced.Tecture.Testing.Stories;
using static Reinforced.Tecture.Testing.Generator.Extensions;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Reinforced.Tecture.Testing.Generator
{
    partial class CSharpTestGenerator : TestGenerator
    {
        private const string StoryVariableId = "story";
        private readonly string TestClassName;
        private readonly string TestNamespaceName;

        private ExpressionSyntax _chain;
        private HashSet<string> _usings;
        private HashSet<string> _staticUsings;

        public CSharpTestGenerator(string testClassName, string testNamespaceName)
        {
            TestClassName = testClassName;
            TestNamespaceName = testNamespaceName;
        }
        private void EnsureUsing(string s)
        {
            if (!_usings.Contains(s)) _usings.Add(s);
        }
        private void EnsureUsing(Type t)
        {
            EnsureUsing(t.Namespace);
        }

        private void EnsureUsingStatic(MethodInfo mi)
        {
            if (mi.DeclaringType==null) return;
            if (!_staticUsings.Contains(mi.DeclaringType.Namespace)) _staticUsings.Add(mi.DeclaringType.Namespace);
        }

        protected override void Before()
        {
            _chain = MakeEmptyChain(StoryVariableId);
            _usings = new HashSet<string>();
            _staticUsings = new HashSet<string>();
        }



        protected override void Visit(CommandBase command, CheckDescription[] checks)
        {
            if (checks.Length == 0)
            {
                Skip();
            }
            else
            {
                var validationCalls = new List<InvocationExpressionSyntax>();
                foreach (var checkMethodDescription in checks)
                {
                    validationCalls.Add(Generate(command, checkMethodDescription));
                }
                Then(command, validationCalls);
            }
        }

        private void Then(CommandBase command, List<InvocationExpressionSyntax> checks)
        {
            _chain = InvocationExpression(
                    MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                            _chain,
                            NameThenOf(command))
                        .WithOperatorToken(Token(SyntaxKind.DotToken).WithLeadingTrivia(Formats.Tabs(4))))
                .WithArgumentList(FormatThenArguments(checks))
                .WithTrailingTrivia(LineFeed);
        }

        private GenericNameSyntax NameThenOf(CommandBase command)
        {
            var type = command.GetType();
            EnsureUsing(type);
            return GenericName(Identifier(nameof(TraceValidator.Then)))
                .WithTypeArgumentList(
                    TypeArgumentList(
                        SingletonSeparatedList<TypeSyntax>(
                            IdentifierName(type.Name))));
        }



        private ArgumentListSyntax FormatThenArguments(List<InvocationExpressionSyntax> checks)
        {
            var args = ComaSeparatedChecks(checks);
            var arglist = ArgumentList(SeparatedList<ArgumentSyntax>(args.ToArray()));
            if (checks.Count > 1)
            {
                //wrap into multiple lines if we have several checks
                arglist = arglist.WithOpenParenToken(
                        Token(
                            Formats.BrWith4Tabs(),
                            SyntaxKind.OpenParenToken,
                            Formats.BrWith5Tabs()))
                    .WithCloseParenToken(
                        Token(
                            Formats.BrWith4Tabs(),
                            SyntaxKind.CloseParenToken,
                            TriviaList()));
            }

            return arglist;
        }

        private IEnumerable<SyntaxNodeOrToken> ComaSeparatedChecks(IEnumerable<InvocationExpressionSyntax> validationCalls)
        {
            bool first = true;
            foreach (var validationCall in validationCalls)
            {
                if (!first) yield return Token(SyntaxKind.CommaToken).BrWith5Tabs();
                yield return Argument(validationCall);
                first = false;
            }
        }


        private void Skip()
        {
            _chain = InvocationExpression(
                    MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                            _chain,
                            IdentifierName(nameof(TraceValidator.SomethingHappens)))
                        .WithOperatorToken(Token(SyntaxKind.DotToken).WithLeadingTrivia(Formats.Tabs(4))))
                .WithTrailingTrivia(LineFeed);
        }
    }
}
