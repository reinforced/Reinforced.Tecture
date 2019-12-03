using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Reinforced.Storage.TestCodeMaker.TestData
{
    /// <summary>
    /// Generator of test data classes
    /// </summary>
    public partial class TestDataClassGenerator
    {
        private const string invalidChars = " ,;()\"#@$%^&*!'-/\\\t.";
        public static string ValidVariableName(string s)
        {
            StringBuilder sb = new StringBuilder(s.Length);
            for (int i = 0; i < s.Length; i++)
            {
                if (i == 0 && char.IsDigit(s[i])) sb.Append('_');
                if (invalidChars.Contains(s[i])) sb.Append('_');
                else sb.Append(s[i]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Adds entities that will be included into test data class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">Entities set</param>
        /// <param name="profile">Data extract configuration</param>
        public void AddEntities<T>(T[] entities, Action<ExtractProfile<T>> profile)
        {
            var ep = new ExtractProfile<T>();
            profile(ep);
            AddEntities(entities, ep);
        }

        private readonly Dictionary<Type, Delegate> _nameGenerators = new Dictionary<Type, Delegate>();

        /// <summary>
        /// Registers default name generator for type of test entity
        /// </summary>
        /// <typeparam name="T">Test entity type</typeparam>
        /// <param name="gen">Name generator function</param>
        public void DefaultNameGenerator<T>(Func<T, string> gen)
        {
            _nameGenerators[typeof(T)] = gen;
        }

        private string GetName(Type tp, object entity)
        {
            if (!_nameGenerators.ContainsKey(tp))
                throw new Exception($"No name generator for {tp}");

            return (string)_nameGenerators[tp].DynamicInvoke(entity);
        }
        private void AddEntities(Array entities, NongenericExtractProfile ep)
        {
            var tp = entities.GetType().GetElementType();
            var bm = GetTypeMeta(tp);
            bm.HasContent = true;
            List<FieldDeclarationSyntax> fields = new List<FieldDeclarationSyntax>();

            foreach (var entity in entities)
            {
                var vNameDec = GetName(tp, entity);
                if (bm.RegisteredNames.Contains(vNameDec)) continue;

                fields.Add(bm.Field(entity, ep.New(entity, bm.UsingDirectives)));

                foreach (var lbp in ep.LateBound.Values)
                {
                    Console.WriteLine($"Retrieving {lbp.Property.Name} of {lbp.Property.DeclaringType.Name}");

                    if (lbp.Property.PropertyType.IsEnumerable()) FillCollection(bm, lbp, entity);
                    else FillAggregatedEntity(bm, lbp, entity);
                }

                bm.RegisteredNames.Add(vNameDec);
            }

            bm.ClassDecl = bm.ClassDecl.AddMembers(fields.ToArray());
        }

        private void FillAggregatedEntity(TypeMeta meta, LateBoundProperty lbp, object entity)
        {
            var prop = lbp.Property;
            var val = prop.GetValue(entity);
            if (val != null)
            {
                var arr = Array.CreateInstance(prop.PropertyType, 1);
                arr.SetValue(val, 0);
                _entitiesToAdd.Enqueue((arr, lbp.Profile));

                var nestMeta = GetTypeMeta(prop.PropertyType);
                meta.LateBound(entity, prop.Name, nestMeta.AccessLib(val));
            }
            else
            {
                meta.LateBoundNull(entity, prop.Name);
            }
        }

        private void FillCollection(TypeMeta meta, LateBoundProperty lbp, object entity)
        {
            var prop = lbp.Property;

            var at = lbp.Profile.TypeRef;
            var arr = (Array)lbp.OverrideCollection.DynamicInvoke(entity);

            if (arr.Length > 0) _entitiesToAdd.Enqueue((arr, lbp.Profile));

            var nestMeta = GetTypeMeta(at);
            List<ExpressionSyntax> initExpressions = new List<ExpressionSyntax>();
            foreach (var entry in arr)
            {
                initExpressions.Add(nestMeta.AccessLib(entry));
            }

            var newHashSet = SyntaxFactory.ObjectCreationExpression(
                    SyntaxFactory.GenericName(SyntaxFactory.Identifier("HashSet"))
                        .WithTypeArgumentList(
                            SyntaxFactory.TypeArgumentList(
                                SyntaxFactory.SingletonSeparatedList<TypeSyntax>(
                                    SyntaxFactory.ParseTypeName(at.Name))
                            )))
                .WithArgumentList(SyntaxFactory.ArgumentList())
                .WithInitializer(
                    SyntaxFactory.InitializerExpression(
                        SyntaxKind.CollectionInitializerExpression,
                        SyntaxFactory.SeparatedList(initExpressions.ToArray())));

            meta.LateBound(entity, prop.Name, newHashSet);
        }

        private readonly Queue<(Array, NongenericExtractProfile)> _entitiesToAdd = new Queue<(Array, NongenericExtractProfile)>();
        private int _level = 1;

        /// <summary>
        /// Finishes generation with specified data class name and namespace
        /// </summary>
        /// <param name="dataClassName">Name of test data class</param>
        /// <param name="nameSpace">Namespace of test data class</param>
        /// <returns>Syntax tree of generated data class</returns>
        public CompilationUnitSyntax Finish(string dataClassName, string nameSpace)
        {
            while (_entitiesToAdd.Count > 0)
            {
                var level = _entitiesToAdd.ToArray();
                _entitiesToAdd.Clear();
                _level++;
                Console.WriteLine($"Extracting level {_level}");
                foreach (var itemLevel in level)
                {
                    AddEntities(itemLevel.Item1, itemLevel.Item2);
                }
            }

            return Generate(dataClassName, nameSpace);
        }

        private readonly Dictionary<Type, TypeMeta> _meta
            = new Dictionary<Type, TypeMeta>();

        private TypeMeta GetTypeMeta(Type t)
        {
            if (!_meta.ContainsKey(t))
            {
                var setClassDecl = SyntaxFactory.ClassDeclaration(TypeMeta.GetClassName(t))
                    .WithModifiers(SyntaxFactory.TokenList(
                        SyntaxFactory.Token(SyntaxKind.PublicKeyword)));

                _meta[t] = new TypeMeta(t, GetName)
                {
                    ClassDecl = setClassDecl,
                    UsingDirectives = new List<string>()
                    {
                        t.Namespace
                    },
                    LateBoundStatements = new List<StatementSyntax>(),
                    RegisteredNames = new HashSet<string>()
                };
            }

            return _meta[t];
        }


    }
}
