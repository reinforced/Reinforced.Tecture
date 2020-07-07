using System;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Reinforced.Tecture.Testing.Stories;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
namespace Reinforced.Tecture.Testing
{
    partial class CSharpTestGenerator
    {
        private CompilationUnitSyntax _result;

        protected override void After()
        {
            FinishTest();
            var method = ProduceValidateMethod();
            var usings = ProduceUsings();
            _result = WrapIntoModule(method, usings);
        }

        private CompilationUnitSyntax WrapIntoModule(MemberDeclarationSyntax validationMethod,
            UsingDirectiveSyntax[] usings)
        {
            var clas = ClassDeclaration(TestClassName)
                .WithMembers(
                    SingletonList(validationMethod))
                .Format();
            var ns = NamespaceDeclaration(ParseName(TestNamespaceName)).Format().AddMembers(clas);

            return CompilationUnit()
                .WithUsings(List(usings.ToArray()))
                .AddMembers(ns);
        }

        private UsingDirectiveSyntax[] ProduceUsings()
        {
            var allUsings = new[]
            {
                "System",
                typeof(StoryValidator).Namespace,
                typeof(StorageStory).Namespace,
            }.Union(_usings).Select(d => UsingDirective(ParseName(d)).FormatUsing());

            var staticUsinSyntaxes = _staticUsings.Select(d => UsingDirective(ParseName(d)).Static().FormatUsing());
            return allUsings.Union(staticUsinSyntaxes).ToArray();
        }

        private MemberDeclarationSyntax ProduceValidateMethod()
        {
            var validateMethod = MethodDeclaration(
                    PredefinedType(Token(SyntaxKind.VoidKeyword).WithTrailingTrivia(Space)),
                    Identifier("Validate"))
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword).WithLeadingTrivia(Formats.Tabs(2)).WithTrailingTrivia(Space)))
                .WithParameterList(
                    ParameterList(
                        SingletonSeparatedList(
                            Parameter(
                                    Identifier(StoryVariableId))
                                .WithType(
                                    IdentifierName(nameof(StorageStory)).WithTrailingTrivia(Space)))));

            validateMethod = validateMethod
                .WithBody(Block(
                    SingletonList<StatementSyntax>(
                        ExpressionStatement(_chain))).Format());

            return validateMethod;
        }

        private void FinishTest()
        {
            _chain = InvocationExpression(
                MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                        _chain,
                        IdentifierName(nameof(StoryValidator.TheEnd)))
                    .WithOperatorToken(Token(SyntaxKind.DotToken).WithLeadingTrivia(Formats.Tabs(4))));
        }



        protected override void DumpTest(TextWriter tw)
        {
            tw.WriteLine(_result.ToFullString());
        }
    }
}
