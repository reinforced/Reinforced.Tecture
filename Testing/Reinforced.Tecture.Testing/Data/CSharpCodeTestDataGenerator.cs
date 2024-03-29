﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Reinforced.Tecture.Testing.Data.Format;
using Reinforced.Tecture.Testing.Data.SyntaxGeneration;
using Reinforced.Tecture.Testing.Data.SyntaxGeneration.Collection;
using Reinforced.Tecture.Tracing;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using CodeFormatter = Reinforced.Tecture.Testing.Validation.Format.CodeFormatter;

namespace Reinforced.Tecture.Testing.Data
{
    internal class CSharpCodeTestDataGenerator : IGenerating
    {
        private readonly Queue<ITestDataRecord> _records = new Queue<ITestDataRecord>();
        private readonly HashSet<string> _usings = new HashSet<string>();
        private readonly Hijack _hijack = new Hijack();

        private readonly TypeGeneratorRepository _tgr;
        private readonly CSharpTestDataGeneratorSetup _cfg;

        internal CSharpCodeTestDataGenerator(CSharpTestDataGeneratorSetup cfg)
        {
            _cfg = cfg;
            _usings.Add(typeof(String).Namespace);
            _usings.Add(typeof(IEnumerable).Namespace);
            _usings.Add(typeof(IEnumerable<>).Namespace);
            _usings.Add(typeof(ITestDataRecord<>).Namespace);
            var collectionStrategies1 = cfg._collectionStrategies ?? new CollectionStrategies();
            _tgr = new TypeGeneratorRepository(_hijack, collectionStrategies1);
            cfg._hijackConfig?.Invoke(_hijack);
        }


        internal void Proceed(IEnumerable<QueryRecord> queries)
        {
            foreach (var queryRecord in queries)
            {
                ITestDataRecord tdr = null;

                var recordType = typeof(TestDataRecord<>).MakeGenericType(queryRecord.DataType);
                tdr =
                    Activator.CreateInstance(recordType, new[] { queryRecord.Result }) as ITestDataRecord;
                //}
                tdr.Description = queryRecord.Annotation;
                tdr.Hash = queryRecord.Hash;
                _records.Enqueue(tdr);
            }
        }

        private int _counter = 0;

        private readonly List<MethodDeclarationSyntax> _entryMethods = new List<MethodDeclarationSyntax>();
        private readonly List<YieldStatementSyntax> _yields = new List<YieldStatementSyntax>();

        private CompilationUnitSyntax Proceed(string className, string ns)
        {
            if (_records.Count == 0)
            {
                _yields.Add(YieldStatement(SyntaxKind.YieldBreakStatement));
            }

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
            CodeFormatter cf = new CodeFormatter();
            cds = cf.Visit(cds) as CompilationUnitSyntax;

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
                Identifier(nameof(CSharpTestData.GetRecords)));

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
            var bs = SingletonSeparatedList<BaseTypeSyntax>(SimpleBaseType(IdentifierName(nameof(CSharpTestData))));
            var @class = ClassDeclaration(Identifier(TriviaList(), className, TriviaList(Space)))
                // .WithModifiers(
                //     TokenList(
                //         new []{
                //             Token(SyntaxKind.PublicKeyword),
                //             Token(SyntaxKind.PartialKeyword)}))
                .WithBaseList(BaseList(bs).WithColonToken(
                    Token(
                        TriviaList(),
                        SyntaxKind.ColonToken,
                        TriviaList(
                            Space))));
            var str = @class.ToFullString();

            var ov = OverrideGetRecorde();
            var methods = _entryMethods.Union(new[] { ov });
            @class = @class.WithMembers(List<MemberDeclarationSyntax>(methods.ToArray()));
            return @class;
        }

        private CompilationUnitSyntax ProduceCompilationUnit(string className, string ns)
        {
            var usings = List<UsingDirectiveSyntax>(
                _usings.Where(x => !string.IsNullOrEmpty(x))
                    .OrderBy(x => x.Length)
                    .Select(x => UsingDirective(IdentifierName(x))));
            var @class = ProduceClass(className);
            var n = NamespaceDeclaration(ParseName(ns)).AddMembers(@class);

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

        private static readonly TypeSyntax AnonymousDictionaryType = typeof(Dictionary<string, object>).TypeName();

        private static readonly TypeSyntax AnonymousRecordType =
            typeof(AnonymousTestDataRecord).TypeName();

        private static readonly TypeSyntax AnonymousCollectionRecordType =
            typeof(AnonymousCollectionTestDataRecord).TypeName();

        private YieldStatementSyntax ProduceYield(ITestDataRecord trd)
        {
            TypeSyntax type = null;
            if (trd.RecordType.IsAnonymousType())
            {
                type = AnonymousRecordType;
            }
            else if (trd.RecordType.IsCollection() && trd.RecordType.ElementType().IsAnonymousType())
            {
                type = AnonymousCollectionRecordType;
            }
            else
            {
                type = trd.GetType().TypeName(_usings);
            }

            var hash = AssignmentExpression(SyntaxKind.SimpleAssignmentExpression,
                IdentifierName(nameof(ITestDataRecord.Hash)),
                TypeInitConstructor.Construct(typeof(string), trd.Hash));

            var description = AssignmentExpression(SyntaxKind.SimpleAssignmentExpression,
                IdentifierName(nameof(ITestDataRecord.Description)),
                TypeInitConstructor.Construct(typeof(string), trd.Description));

            //var data = AssignmentExpression(SyntaxKind.SimpleAssignmentExpression,
            //    IdentifierName(nameof(ITestDataRecord<int>.Data)),
            //    InvocationExpression(IdentifierName(CurrentMethodName)));
            var invokeGetData = InvocationExpression(IdentifierName(CurrentMethodName));

            var sto = (new SyntaxNodeOrToken[]
                { hash, Token(SyntaxKind.CommaToken), description });

            var init = ObjectCreationExpression(type)
                .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(invokeGetData))))
                .WithInitializer(InitializerExpression(SyntaxKind.ObjectInitializerExpression,
                    SeparatedList<ExpressionSyntax>(sto)));

            return YieldStatement(SyntaxKind.YieldReturnStatement,
                init);
        }

        private TypeSyntax GetCollectionReturnType(Type collectionType)
        {
            var ct = collectionType;

            if (collectionType.IsAbstract || collectionType.IsInterface)
            {
                ct = typeof(List<object>);
            }

            if (collectionType.ElementType().IsAnonymousType())
            {
                ct = (typeof(IEnumerable<Dictionary<string, object>>));
            }

            return ct.TypeName(_usings);
        }

        private MethodDeclarationSyntax PayloadConstructionMethod(ITestDataRecord tdr)
        {
            TypeSyntax retType = null;
            if (tdr.RecordType.IsAnonymousType())
            {
                retType = AnonymousDictionaryType;
            }
            else if (tdr.RecordType.IsCollection())
            {
                retType = GetCollectionReturnType(tdr.RecordType);
            }
            else
            {
                retType = tdr.RecordType.TypeName(_usings);
            }

            var md = MethodDeclaration(retType, CurrentMethodName);

            var body = PayloadConstructionMethodBody(tdr);

            return md.WithBody(Block(SeparatedList<StatementSyntax>(body)))
                .WithModifiers(
                    TokenList(
                        new[]
                        {
                            Token(SyntaxKind.PrivateKeyword)
                        }));
            ;
        }

        private IEnumerable<StatementSyntax> PayloadConstructionMethodBody(ITestDataRecord tdr)
        {
            if (tdr.Payload == null)
            {
                yield return ReturnStatement(LiteralExpression(SyntaxKind.NullLiteralExpression));
                yield break;
            }

            if (tdr.RecordType.IsInlineable())
            {
                yield return ReturnStatement(TypeInitConstructor.Construct(tdr.RecordType, tdr.Payload));
                yield break;
            }

            var ctx = new GenerationContext(_usings);
            if (tdr.RecordType.IsDictionary())
            {
                var dict = Generator.ProceedDictionary(_tgr, tdr.RecordType, (IDictionary)tdr.Payload, ctx);
                foreach (var s in ctx.Declarations)
                {
                    yield return s;
                }

                foreach (var s in ctx.LateBound)
                {
                    yield return s;
                }

                yield return ReturnStatement(dict);
                yield break;
                
            }else if (tdr.RecordType.IsEnumerable() || tdr.RecordType.IsTuple())
            {
                var coll =
                    tdr.RecordType.IsTuple()
                        ? SyntaxGeneration.Generator.ProceedTuple(_tgr, tdr.Payload.GetTupleValues(), ctx)
                        : SyntaxGeneration.Generator.ProceedCollection(_tgr, tdr.RecordType, (IEnumerable)tdr.Payload,
                            ctx, forceArray: true);

                foreach (var s in ctx.Declarations)
                {
                    yield return s;
                }

                foreach (var s in ctx.LateBound)
                {
                    yield return s;
                }

                yield return ReturnStatement(coll);
                yield break;
            }

            var gen = _tgr.GetGeneratorFor(tdr.RecordType);
            var result = gen.New(tdr.Payload, ctx);
            while (ctx.Declarations.Count > 0)
            {
                yield return ctx.Declarations.Dequeue();
            }
            while (ctx.LateBound.Count > 0)
            {
                yield return ctx.LateBound.Dequeue();
            }
            yield return ReturnStatement(result);
            
        }

        #endregion

        public void Dump(TextWriter tw)
        {
            var cds = Proceed(_cfg._className, _cfg._namespace);
            tw.Write(cds.ToFullString());
        }
    }
}