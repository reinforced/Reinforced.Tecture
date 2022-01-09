using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Testing.Validation.Assertion;
using Reinforced.Tecture.Testing.Validation.Format;
using Reinforced.Tecture.Tracing.Commands;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Reinforced.Tecture.Testing.Validation
{
    public partial class ValidationGenerator: IGenerating
    {
        private const string StoryVariableId = "flow";
        private readonly string TestClassName;
        private readonly string TestNamespaceName;

        private Queue<StatementSyntax> _chain;
        private HashSet<string> _usings;
        private HashSet<string> _staticUsings;

        private HashSet<string> _optOuts;

        public ValidationGenerator(string testClassName, string testNamespaceName, HashSet<string> optOuts = null)
        {
            TestClassName = testClassName;
            TestNamespaceName = testNamespaceName;
            _optOuts = optOuts??new HashSet<string>();
        }
        private void EnsureUsing(string s)
        {
            if (!_usings.Contains(s)) _usings.Add(s);
        }
        private void EnsureUsing(Type t)
        {
            EnsureUsing(t.Namespace);
        }

        internal void Before()
        {
            _chain = new Queue<StatementSyntax>();
            _usings = new HashSet<string>();
            _staticUsings = new HashSet<string>();
            EnsureUsing(typeof(AssertExtensions));
        }


        private (ValidatedAttribute,PropertyInfo)[] GetValidatedProperties(CommandBase command) =>
            command.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.Instance)
                .Where(x => x.GetCustomAttribute<ValidatedAttribute>(true) != null)
                .Select(x=>(x.GetCustomAttribute<ValidatedAttribute>(),x))
                .ToArray();

        public void Generate(IEnumerable<CommandBase> commands)
        {
            foreach (var cmd in commands)
            {
                Visit(cmd);
            }
        }
        
        private void Visit(CommandBase command)
        {
            if (command is End) TheEnd();
            else
            {
                var validatedProperties = GetValidatedProperties(command);
                if (validatedProperties.Length == 0)
                    Then(command); //needed to do not overwhelm braces
                else
                {
                    var invokations = new List<InvocationExpressionSyntax>();
                    foreach (var validatedProperty in validatedProperties)
                    {
                        if (_optOuts.Contains(validatedProperty.Item1.OptOutFlag)) continue;

                        var commandDescription = string.IsNullOrEmpty(command.Annotation)? command.GetType().Name:$"'{command.Annotation}'";
                        
                        var meaning = validatedProperty.Item1.Meaning ?? $"property {validatedProperty.Item2.Name}";
                        
                        var propMeaning =
                            validatedProperty.Item1.Meaning ??
                            $"{meaning} of command {commandDescription}";

                        var instance = new AssertInstanceReference
                        (
                            validatedProperty.Item2.GetValue(command),
                            validatedProperty.Item2.PropertyType,
                            MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                                IdentifierName("c"),
                                IdentifierName(validatedProperty.Item2.Name)),
                            propMeaning,
                            _usings
                        );
                        var assertions = TypeAssertionGenerator.AssertionFor(instance)
                            .ToArray();
                        invokations.AddRange(assertions);
                        invokations.Add(null);
                    }
                    Then(command, invokations);
                }
                    
            }
        }


        private void Then(CommandBase command, List<InvocationExpressionSyntax> checks)
        {
            var ex = 
                InvocationExpression(
                    MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName(StoryVariableId),
                            NameThenOf(command))
                    )
                .WithArgumentList(FormatThenArguments(checks))
                .WithAdditionalAnnotations(SyntaxAnnotation.ElasticAnnotation);

            _chain.Enqueue(ExpressionStatement(ex));
        }

        private void Then(CommandBase command)
        {
            var ex = InvocationExpression(
                    MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName(StoryVariableId),
                        NameThenOf(command)));

            _chain.Enqueue(ExpressionStatement(ex));
        }

        private GenericNameSyntax NameThenOf(CommandBase command)
        {
            var commandType = command.GetType();
            EnsureUsing(commandType);
            var commandTypeName = commandType.TypeName(_usings);
            
            if (command.Channel != null)
            {
                EnsureUsing(command.Channel);
                var channelName = command.Channel.TypeName(_usings);
                var types = new[]{ channelName, commandTypeName };
                return GenericName(Identifier(nameof(TraceValidator.Then)))
                    .WithTypeArgumentList(
                        TypeArgumentList(
                            SeparatedList<TypeSyntax>(types.ComaSeparated())));    
            }
            return GenericName(Identifier(nameof(TraceValidator.Then)))
                .WithTypeArgumentList(
                    TypeArgumentList(
                        SingletonSeparatedList(commandTypeName)));
        }

        private ArgumentListSyntax FormatThenArguments(List<InvocationExpressionSyntax> checks)
        {
            var lambda = TypeAssertionGenerator.WrapIntoLambda(checks, "c");
            var arglist = ArgumentList(SingletonSeparatedList(Argument(lambda))).WithAdditionalAnnotations(Annotations.ThenArgument);
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
    }
}