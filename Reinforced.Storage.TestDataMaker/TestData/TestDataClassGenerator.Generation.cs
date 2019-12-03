using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Reinforced.Storage.Testing;

namespace Reinforced.Storage.TestCodeMaker.TestData
{
    public partial class TestDataClassGenerator
    {
        private MethodDeclarationSyntax CreateStuffMethod(IEnumerable<TypeMeta> validMeta)
        {
            List<StatementSyntax> prefetchStatements = new List<StatementSyntax>();

            foreach (var vm in validMeta)
            {
                List<ExpressionSyntax> accessExpressions = new List<ExpressionSyntax>();
                foreach (var idn in vm.RegisteredNames)
                {
                    accessExpressions.Add(vm.AccessLibName(idn));
                }

                var arrc = SyntaxFactory.ImplicitArrayCreationExpression(
                    SyntaxFactory.InitializerExpression(
                        SyntaxKind.ArrayInitializerExpression,
                        SyntaxFactory.SeparatedList(accessExpressions.ToArray())));

                var stmt = SyntaxFactory.ExpressionStatement(
                    SyntaxFactory.InvocationExpression(
                            SyntaxFactory.MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                SyntaxFactory.IdentifierName("env"),
                                SyntaxFactory.IdentifierName(nameof(TestingEnvironment.Prefetched))))
                        .WithArgumentList(SyntaxFactory.ArgumentList(SyntaxFactory.SingletonSeparatedList(SyntaxFactory.Argument(arrc)))));
                prefetchStatements.Add(stmt);
            }

            return SyntaxFactory.MethodDeclaration(
                    SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
                    SyntaxFactory.Identifier("Stuff"))
                .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
                .WithParameterList(
                    SyntaxFactory.ParameterList(
                        SyntaxFactory.SingletonSeparatedList(
                            SyntaxFactory.Parameter(SyntaxFactory.Identifier("env"))
                                .WithType(SyntaxFactory.IdentifierName(nameof(TestingEnvironment))))))
                .WithBody(SyntaxFactory.Block(prefetchStatements.ToArray()));
        }

        private PropertyDeclarationSyntax[] GenerateProperties(IEnumerable<TypeMeta> types)
        {
            var result = new List<PropertyDeclarationSyntax>();
            foreach (var vm in types)
            {
                var ys = new List<YieldStatementSyntax>();

                foreach (var trn in vm.RegisteredNames)
                {
                    ys.Add(SyntaxFactory.YieldStatement(SyntaxKind.YieldReturnStatement,
                        vm.AccessLibName(trn)
                        ));
                }

                var typeName = SyntaxFactory.GenericName(SyntaxFactory.Identifier(nameof(IEnumerable)))
                    .WithTypeArgumentList(SyntaxFactory.TypeArgumentList(
                        SyntaxFactory.SingletonSeparatedList<TypeSyntax>(SyntaxFactory.ParseTypeName(vm.TypeRef.Name))));

                result.Add(SyntaxFactory.PropertyDeclaration(typeName,SyntaxFactory.Identifier($"Enumerate_{vm.TypeRef.Name}"))
                    .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
                    .WithAccessorList(
                        SyntaxFactory.AccessorList(
                            SyntaxFactory.SingletonList(
                                SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                                    .WithBody(SyntaxFactory.Block(ys.ToArray())))))
                );
            }
            return result.ToArray();
        }

        private CompilationUnitSyntax Generate(string dataClassName, string nameSpace)
        {
            var validMeta = _meta.Values.Where(d => d.HasContent);
            var allUsings = new[]
            {
                "System",
                "System.Collections",
                typeof(TestingEnvironment).Namespace,
                "System.Collections.Generic"
            }.Union(validMeta.SelectMany(d => d.UsingDirectives))
                .Distinct()
                .Select(d => SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(d)))
                .ToArray();

            var allInitStatments = validMeta.SelectMany(d => d.LateBoundStatements).ToArray();

            var ctor = SyntaxFactory.ConstructorDeclaration(SyntaxFactory.Identifier(dataClassName))
                .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
                .WithBody(SyntaxFactory.Block(allInitStatments));

            var allClasses = validMeta.Select(d => d.ClassDecl)
                .Cast<MemberDeclarationSyntax>().ToArray();

            var allFields = validMeta.Select(d =>
                SyntaxFactory.FieldDeclaration(
                    SyntaxFactory.VariableDeclaration(SyntaxFactory.ParseTypeName(d.GetClassName()))
                        .WithVariables(
                            SyntaxFactory.SingletonSeparatedList(
                                SyntaxFactory.VariableDeclarator(SyntaxFactory.Identifier(d.GetPropName()))
                                    .WithInitializer(
                                        SyntaxFactory.EqualsValueClause(
                                            SyntaxFactory.ObjectCreationExpression(SyntaxFactory.ParseTypeName(d.GetClassName()))
                                                .WithArgumentList(SyntaxFactory.ArgumentList())
                                        )))))
                    .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword))))
                .ToArray();

            var clas = SyntaxFactory.ClassDeclaration(dataClassName)
                .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.PartialKeyword)))
                .AddMembers(ctor)
                .AddMembers(allClasses)
                .AddMembers(allFields)
                .AddMembers(GenerateProperties(validMeta))
                .AddMembers(CreateStuffMethod(validMeta));

            var r = SyntaxFactory.CompilationUnit()
                .WithUsings(SyntaxFactory.List(allUsings))
                .AddMembers(SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(nameSpace)).AddMembers(clas));

            return Format(r);

        }

        private CompilationUnitSyntax Format(CompilationUnitSyntax s)
        {
            //todo
            return s.NormalizeWhitespace();
        }
    }
}
