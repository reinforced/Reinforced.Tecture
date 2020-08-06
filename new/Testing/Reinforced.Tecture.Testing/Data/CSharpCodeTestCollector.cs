using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Reinforced.Tecture.Testing.Data.SyntaxGeneration;
using Reinforced.Tecture.Testing.Data.SyntaxGeneration.Collection;
using Reinforced.Tecture.Testing.Generator;
using Reinforced.Tecture.Testing.Query;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Reinforced.Tecture.Testing.Data
{
    public class CSharpCodeTestCollector : Collecting
    {
        private readonly Queue<ITestDataRecord> _records = new Queue<ITestDataRecord>();
        private readonly HashSet<string> _usings = new HashSet<string>();
        private readonly Hijack _hijack = new Hijack();

        private readonly TypeGeneratorRepository _tgr;
        public CSharpCodeTestCollector(Action<Hijack> configureHijaks = null, CollectionStrategies collectionStrategies = null)
        {
            _usings.Add(typeof(IEnumerable).Namespace);
            _usings.Add(typeof(ITestDataRecord<>).Namespace);
            var collectionStrategies1 = collectionStrategies ?? new CollectionStrategies();
            _tgr = new TypeGeneratorRepository(_hijack, collectionStrategies1);
            configureHijaks?.Invoke(_hijack);
        }

        public void Put<T>(string hash, T result, string description = null)
        {
            if (string.IsNullOrEmpty(description)) description = string.Empty;
            _records.Enqueue(new TestDataRecord<T>()
            {
                Data = result,
                Description = description,
                Hash = hash
            });
        }

        private int _counter = 0;

        private List<MethodDeclarationSyntax> _entryMethods = new List<MethodDeclarationSyntax>();
        private List<YieldStatementSyntax> _yields = new List<YieldStatementSyntax>();

        public CompilationUnitSyntax Proceed(string className,string ns)
        {
            while (_records.Count > 0)
            {
                _counter++;
                var rec = _records.Dequeue();
                var getRegordN = PayloadConstructionMethod(rec);
                var yield = ProduceYield(rec);
                _entryMethods.Add(getRegordN);
                _yields.Add(yield);
            }

            var cds = ProduceCompilationUnit(className, ns);
            return cds;
        }

        #region Compilation unit generation

        private MethodDeclarationSyntax OverrideGetRecorde()
        {
            var overriden = MethodDeclaration(
                GenericName(
                        Identifier(nameof(IEnumerable)))
                    .WithTypeArgumentList(
                        TypeArgumentList(
                            SingletonSeparatedList<TypeSyntax>(
                                IdentifierName(nameof(ITestDataRecord))))),
                Identifier(nameof(CSharpTestDataProvider.GetRecords)));

            overriden = overriden.WithModifiers(
                TokenList(
                    new[]
                    {
                        Token(SyntaxKind.PublicKeyword),
                        Token(SyntaxKind.OverrideKeyword)
                    }));

            overriden = overriden.WithBody(Block(SeparatedList<StatementSyntax>(_yields)));
            return overriden;
        }

        private ClassDeclarationSyntax ProduceClass(string className)
        {
            var clas = ClassDeclaration(className)
                .WithBaseList(
                    BaseList(SingletonSeparatedList<BaseTypeSyntax>(SimpleBaseType(IdentifierName(nameof(CSharpTestDataProvider))))));
            var ov = OverrideGetRecorde();
            var methods = _entryMethods.Union(new[] { ov });
            clas = clas.WithMembers(List<MemberDeclarationSyntax>(methods.ToArray())).Format();
            return clas;
        }

        private CompilationUnitSyntax ProduceCompilationUnit(string className, string ns)
        {
            var usings = List<UsingDirectiveSyntax>(_usings.Select(x => UsingDirective(IdentifierName(x)).FormatUsing()));
            var clas = ProduceClass(className);
            var n = NamespaceDeclaration(ParseName(ns)).Format().AddMembers(clas);

            return CompilationUnit()
                .WithUsings(usings)
                .AddMembers(n);
        }

        #endregion


        #region Generation routine

        private string CurrentMethodName
        {
            get { return $"GetEntry_{_counter}"; }
        }
        private YieldStatementSyntax ProduceYield(ITestDataRecord trd)
        {
            var type = trd.GetType().TypeName(_usings);
            var hash = AssignmentExpression(SyntaxKind.SimpleAssignmentExpression,
                IdentifierName(nameof(ITestDataRecord.Hash)),
                TypeInitConstructor.Construct(typeof(string), trd.Hash));

            var description = AssignmentExpression(SyntaxKind.SimpleAssignmentExpression,
                IdentifierName(nameof(ITestDataRecord.Description)),
                TypeInitConstructor.Construct(typeof(string), trd.Description));

            var data = AssignmentExpression(SyntaxKind.SimpleAssignmentExpression,
                IdentifierName(nameof(ITestDataRecord<int>.Data)),
                InvocationExpression(IdentifierName(CurrentMethodName)));

            var sto = (new SyntaxNodeOrToken[]
                {hash, Token(SyntaxKind.CommaToken), description, Token(SyntaxKind.CommaToken), data});

            var init = ObjectCreationExpression(type)
                .WithInitializer(InitializerExpression(SyntaxKind.ObjectInitializerExpression,
                    SeparatedList<ExpressionSyntax>(sto)));

            return YieldStatement(SyntaxKind.YieldReturnStatement,
                init);
        }

        private MethodDeclarationSyntax PayloadConstructionMethod(ITestDataRecord tdr)
        {
            var retType = tdr.RecordType.TypeName(_usings);
            var md = MethodDeclaration(retType, CurrentMethodName);

            var body = PayloadConstructionMethodBody(tdr);

            return md.WithBody(Block(SeparatedList<StatementSyntax>(body)).Format())
                .WithModifiers(
                    TokenList(
                        new[]
                        {
                            Token(SyntaxKind.PrivateKeyword)
                        })); ;
        }

        private IEnumerable<StatementSyntax> PayloadConstructionMethodBody(ITestDataRecord tdr)
        {
            if (tdr.Payload == null)
            {
                yield return ReturnStatement(LiteralExpression(SyntaxKind.NullLiteralExpression));
                yield break;
            }

            if (TypeInitConstructor.IsInlineable(tdr.RecordType))
            {
                yield return ReturnStatement(TypeInitConstructor.Construct(tdr.RecordType, tdr.Payload));
                yield break;
            }
            var ctx = new GenerationContext(_usings);

            if (tdr.RecordType.IsEnumerable())
            {
                var coll = SyntaxGeneration.Generator.ProceedCollection(_tgr, tdr.RecordType, (IEnumerable)tdr.Payload,
                    ctx);
                foreach (var s in ctx.Statements)
                {
                    yield return s;
                }

                yield return ReturnStatement(coll);
                yield break;
            }

            var gen = _tgr.GetGeneratorFor(tdr.RecordType);
            gen.Proceed(tdr.Payload, ctx);
            foreach (var statementSyntax in ctx.Statements)
            {
                yield return statementSyntax;
            }
        }

        #endregion

    }
}
