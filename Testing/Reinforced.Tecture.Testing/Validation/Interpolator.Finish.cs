using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Reinforced.Tecture.Testing.Validation.Format;
using Reinforced.Tecture.Tracing;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
namespace Reinforced.Tecture.Testing.Validation
{
    partial class CSharpUnitTestGenerator
    {
        private CompilationUnitSyntax _result;

        internal void After()
        {
            var method = ProduceValidateMethod();
            var usings = ProduceUsings();
            var r = WrapIntoModule(method, usings);
            CodeFormatter cf = new CodeFormatter();
            r = cf.Visit(r) as CompilationUnitSyntax;

            _result = r;

        }

        private CompilationUnitSyntax WrapIntoModule(MemberDeclarationSyntax validationMethod,
            UsingDirectiveSyntax[] usings)
        {
            var bs = SingletonSeparatedList<BaseTypeSyntax>(SimpleBaseType(IdentifierName(nameof(ValidationBase))).WithoutTrivia());

            var clas = 
                ClassDeclaration(TestClassName)
                .WithBaseList(BaseList(bs))
                .WithMembers(
                    SingletonList(validationMethod));

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
                typeof(TraceValidator).Namespace,
                typeof(Trace).Namespace,
                typeof(ValidationBase).Namespace,
            }.Union(_usings).Select(d => UsingDirective(ParseName(d)).FormatUsing());

            var staticUsinSyntaxes = _staticUsings.Select(d => UsingDirective(ParseName(d)).Static().FormatUsing());
            return allUsings.Union(staticUsinSyntaxes).ToArray();
        }

        private MemberDeclarationSyntax ProduceValidateMethod()
        {
            var validateMethod = MethodDeclaration(
                    PredefinedType(Token(SyntaxKind.VoidKeyword).WithTrailingTrivia(Space)),
                    Identifier(nameof(ValidationBase.Validate)))
                .WithModifiers(TokenList(
                    Token(SyntaxKind.ProtectedKeyword).WithLeadingTrivia(Formats.Tabs(2)).WithTrailingTrivia(Space),
                    Token(SyntaxKind.OverrideKeyword).WithTrailingTrivia(Space)
                    ))
                .WithParameterList(
                    ParameterList(
                        SingletonSeparatedList(
                            Parameter(
                                    Identifier(StoryVariableId))
                                .WithType(
                                    IdentifierName(nameof(TraceValidator)).WithTrailingTrivia(Space)))));

            validateMethod = validateMethod
                .WithBody(Block(SeparatedList(_chain)).Format());

            return validateMethod;
        }

        private void TheEnd()
        {
            var ex = InvocationExpression(
                MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName(StoryVariableId),
                        IdentifierName(nameof(TraceValidator.TheEnd)))
                    );
            _chain.Enqueue(ExpressionStatement(ex).WithLeadingTrivia(Formats.Tabs(4)).WithTrailingTrivia(LineFeed));
        }



        public void Dump(TextWriter tw)
        {
            tw.WriteLine(_result.ToFullString());
        }
    }
}
