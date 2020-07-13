using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.SqlServer.Server;
using Reinforced.Storage.SideEffects;
using Reinforced.Storage.Testing.Stories;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Reinforced.Storage.TestCodeMaker.StoryValidation
{
    public static class StoryExtensions
    {
        public static CompilationUnitSyntax GenerateValidation(this StorageStory s, Action<StoryValidationGenerator> generatorConf, string className = "Test", string nameSpace = "Tests")
        {
            StoryValidationGenerator vg = new StoryValidationGenerator(className,nameSpace);
            generatorConf(vg);
            return vg.Generate(s);
        }
    }

    /// <summary>
    /// Generator of code to automatically validate a-priori valid story
    /// </summary>
    public class StoryValidationGenerator
    {
        private readonly string _className;
        private readonly string _nameSpace;


        private readonly Dictionary<Type, ISideEffectGeneratorSetup> _generators = new Dictionary<Type, ISideEffectGeneratorSetup>();

        public StoryValidationGenerator(string className, string nameSpace)
        {
            _className = className;
            _nameSpace = nameSpace;
        }

        public PerSideEffectGeneratorSetup<T> ForEffect<T>() where T : SideEffectBase
        {
            if (!_generators.ContainsKey(typeof(T)))
            {
                _generators[typeof(T)] = new PerSideEffectGeneratorSetup<T>();
            }

            return (PerSideEffectGeneratorSetup<T>)_generators[typeof(T)];
        }

        internal CompilationUnitSyntax Generate(StorageStory story)
        {
            HashSet<string> staticUsings = new HashSet<string>();
            HashSet<string> regularUsings = new HashSet<string>();

            ExpressionSyntax chain = InvocationExpression(
                MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    IdentifierName("story").WithLeadingTrivia(Formats.Tab,Formats.Tab,Formats.Tab),
                    IdentifierName(nameof(StorageStory.Begins))))
                .WithTrailingTrivia(LineFeed);

            foreach (var seb in story.Effects)
            {
                var effectType = seb.GetType();
                if (_generators.ContainsKey(effectType))
                {
                    var gens = _generators[effectType];
                    var validationCalls = new List<InvocationExpressionSyntax>();
                    foreach (var validationGenerator in gens.Generators)
                    {
                        var syntax = validationGenerator.GenerateValidationCall(seb, out string[] usings, out string[] regUsings);
                        if (syntax != null)
                        {
                            if (usings != null)
                                foreach (var @using in usings)
                                    if (!staticUsings.Contains(@using)) staticUsings.Add(@using);

                            if (regUsings != null)
                                foreach (var @using in regUsings)
                                    if (!regularUsings.Contains(@using)) regularUsings.Add(@using);

                            validationCalls.Add(syntax);
                        }
                    }

                    if (validationCalls.Any())
                    {
                        var args = new List<SyntaxNodeOrToken>();
                        bool first = true;
                        foreach (var validationCall in validationCalls)
                        {
                            if (!first) args.Add(Token(SyntaxKind.CommaToken).WithTrailingTrivia(LineFeed, Formats.Tab, Formats.Tab, Formats.Tab, Formats.Tab, Formats.Tab));
                            args.Add(Argument(validationCall));
                            first = false;
                        }

                        var arglist = ArgumentList(SeparatedList<ArgumentSyntax>(args.ToArray()));
                        if (args.Count > 1)
                        {
                            arglist = arglist.WithOpenParenToken(
                                    Token(
                                        TriviaList(LineFeed, Formats.Tab, Formats.Tab, Formats.Tab, Formats.Tab),
                                        SyntaxKind.OpenParenToken,
                                        TriviaList(
                                            LineFeed, Formats.Tab, Formats.Tab, Formats.Tab, Formats.Tab, Formats.Tab)))
                                .WithCloseParenToken(
                                    Token(
                                        TriviaList(LineFeed, Formats.Tab, Formats.Tab, Formats.Tab, Formats.Tab),
                                        SyntaxKind.CloseParenToken,
                                        TriviaList()));
                        }
                        var invEx = InvocationExpression(
                                MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                                    chain,
                                    IdentifierName(nameof(StoryValidator.Then)))
                                    .WithOperatorToken(Token(SyntaxKind.DotToken).WithLeadingTrivia(Formats.Tabs(4))))
                            .WithArgumentList(arglist)
                            .WithTrailingTrivia(LineFeed);
                        
                        chain = invEx;
                    }
                    else
                    {
                        chain = Skip(chain);

                    }
                }
                else
                {
                    chain = Skip(chain);
                }
            }

            chain = InvocationExpression(
                MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                    chain,
                    IdentifierName(nameof(StoryValidator.TheEnd)))
                    .WithOperatorToken(Token(SyntaxKind.DotToken).WithLeadingTrivia(Formats.Tabs(4))));

            var validateMethod = MethodDeclaration(
                    PredefinedType(Token(SyntaxKind.VoidKeyword).WithTrailingTrivia(Space)),
                    Identifier("Validate"))
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword).WithLeadingTrivia(Formats.Tabs(2)).WithTrailingTrivia(Space)))
                .WithParameterList(
                    ParameterList(
                        SingletonSeparatedList(
                            Parameter(
                                    Identifier("story"))
                                .WithType(
                                    IdentifierName(nameof(StorageStory)).WithTrailingTrivia(Space)))));

            validateMethod = validateMethod
                .WithBody(Block(
                    SingletonList<StatementSyntax>(
                        ExpressionStatement(chain))).Format());

            var allUsings = new[]
            {
                "System",
                typeof(StoryValidator).Namespace,
                typeof(StorageStory).Namespace,
            }.Union(regularUsings).Select(d => UsingDirective(ParseName(d)).FormatUsing());

            var staticUsinSyntaxes = staticUsings.Select(d => UsingDirective(ParseName(d)).Static().FormatUsing());
            var clas = ClassDeclaration(_className)
                .WithMembers(
                    SingletonList<MemberDeclarationSyntax>(validateMethod))
                .Format();
            var ns = NamespaceDeclaration(ParseName(_nameSpace)).Format().AddMembers(clas);

            var result = CompilationUnit()
                        .WithUsings(List(allUsings.Union(staticUsinSyntaxes).ToArray()))
                        .AddMembers(ns);

            return result;

        }

        private InvocationExpressionSyntax Skip(ExpressionSyntax chain)
        {
            return InvocationExpression(
                MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                        chain,
                        IdentifierName(nameof(StoryValidator.SomethingHappens)))
                    .WithOperatorToken(Token(SyntaxKind.DotToken).WithLeadingTrivia(Formats.Tabs(4))))

                .WithTrailingTrivia(LineFeed);
        }


    }

    internal static class Formats
    {
       

        

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

       

        public static UsingDirectiveSyntax Static(this UsingDirectiveSyntax uds)
        {
            return uds.WithStaticKeyword(
                Token(
                    TriviaList(),
                    SyntaxKind.StaticKeyword,
                    TriviaList(
                        Space)));
        }
    }
}
