using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Reinforced.Tecture.Cloning
{
    public static class CloningDelegateEmitter
    {
        internal static bool IsInlineCloning(this Type t)
        {
            if (t.IsValueType) return true;
            return t == typeof(string);
        }

        private static readonly MethodInfo ToArrayMethod;

        static CloningDelegateEmitter()
        {
            ToArrayMethod = typeof(Enumerable).GetMethod(nameof(Enumerable.ToArray),
                BindingFlags.Public | BindingFlags.Static);
        }

        private const BindingFlags CtorFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        private const BindingFlags PropertyFlags = BindingFlags.Public | BindingFlags.NonPublic |
                                                   BindingFlags.SetProperty | BindingFlags.GetProperty |
                                                   BindingFlags.Instance;

        private const string VarName = "result";
        private static Delegate EmitShallow(Type objectType)
        {
            var ctor = objectType.GetConstructors(CtorFlags)
                .FirstOrDefault(x => x.GetParameters().Length == 0);
            if (ctor == null)
            {
                throw new TectureCloningException($"cannot emit cloning delegate. Parameterless construtor is missing on type '{objectType.FullName}'. Please register cloning delegate for it manually");
            }

            var properties = objectType.GetProperties(PropertyFlags).Where(x => x.CanWrite);
            var source = Expression.Parameter(objectType);
            var clonerParam = Expression.Parameter(typeof(DeepCloneOperator));

            List<Expression> statements = new List<Expression>();

            var tmpVariable = Expression.Variable(objectType, VarName);

            // Step1: copy all shallow-copyable properties
            var newEx = Expression.New(ctor);
            List<MemberAssignment> initialize = new List<MemberAssignment>();
            var simpleProperties = properties.Where(p => p.PropertyType.IsInlineCloning());
            foreach (var pi in simpleProperties)
            {
                var mex = Expression.Property(source, pi);
                var binding = Expression.Bind(pi, mex);
                initialize.Add(binding);
            }

            var inits = Expression.MemberInit(newEx, initialize);
            var assign = Expression.Assign(tmpVariable, inits);

            statements.Add(assign);

            // Step2: enqueue complex properties into clone queue
            var nullEx = Expression.Constant(null);
            var complex = properties.Where(p => !p.PropertyType.IsInlineCloning());
            bool any = false;
            foreach (var pi in complex)
            {
                any = true;
                var mex = Expression.MakeMemberAccess(source, pi);
                var call = Expression.Call(clonerParam, DeepCloneOperator.DeferCloneMethod, mex);
                var variableMes = Expression.MakeMemberAccess(tmpVariable, pi);
                var assignNull = Expression.Assign(variableMes, Expression.Constant(null, pi.PropertyType));

                var comparison = Expression.Equal(mex, nullEx);
                var ifSt = Expression.IfThenElse(comparison, assignNull, call);
                statements.Add(ifSt);
            }

            // Step 3: enqueue self for late bound
            if (any)
            {
                var call = Expression.Call(clonerParam, DeepCloneOperator.DeferBindMethod, Expression.Constant(objectType), source, tmpVariable);
                statements.Add(call);
            }

            var target = Expression.Label(objectType);
            statements.Add(Expression.Return(target, tmpVariable));
            statements.Add(Expression.Label(target, Expression.Constant(null, objectType)));
            var blk = Expression.Block(new[] { tmpVariable }, statements);
            var result = Expression.Lambda(blk, source, clonerParam);
            return result.Compile();
        }

        private static Delegate EmitLateBound(Type objectType)
        {
            var properties = objectType.GetProperties(PropertyFlags).Where(x => x.CanWrite);
            var complex = properties.Where(p => !p.PropertyType.IsInlineCloning());
            var source = Expression.Parameter(objectType);
            var target = Expression.Parameter(objectType);
            var cloner = Expression.Parameter(typeof(DeepCloneOperator));
            var nullEx = Expression.Constant(null);
            List<Expression> statements = new List<Expression>();
            foreach (var pi in complex)
            {
                var srcMex = Expression.MakeMemberAccess(source, pi);
                var trgMex = Expression.MakeMemberAccess(target, pi);

                var meth = DeepCloneOperator.ResolveMethod.MakeGenericMethod(pi.PropertyType);
                Expression initializer = Expression.Call(cloner, meth, srcMex);

                var assign = Expression.Assign(trgMex, initializer);

                var comparison = Expression.NotEqual(srcMex, nullEx);
                var ifSt = Expression.IfThen(comparison, assign);
                statements.Add(ifSt);
            }

            if (statements.Count == 0) return null;
            var block = Expression.Block(statements);

            var lambda = Expression.Lambda(block, source, target, cloner);
            return lambda.Compile();

        }

        public static TypeCloneTooling EmitCloneDelegate(Type objectType)
        {
            return new TypeCloneTooling()
            {
                Shallow = EmitShallow(objectType),
                Bind = EmitLateBound(objectType)
            };
        }

        internal static bool IsDictionary(this Type collectionType)
        {
            return typeof(IDictionary).IsAssignableFrom(collectionType);
        }

        internal static bool IsCollection(this Type collectionType)
        {
            if (collectionType.IsArray) return true;
            return typeof(IEnumerable).IsAssignableFrom(collectionType);
        }

        private static readonly string Explanation =
            $"Please register cloning delegate manually using {nameof(DeepCloner)}.{nameof(DeepCloner.RegisterCloner)} method";
        internal static Type GetCollectionElementType(this Type collectionType)
        {
            if (collectionType.IsArray) return collectionType.GetElementType();
            var implementingEnumerable =
                collectionType
                    .GetInterfaces()
                    .Where(x => x.IsGenericType)
                    .FirstOrDefault(x => x.GetGenericTypeDefinition() == typeof(IEnumerable<>));
            if (implementingEnumerable != null)
            {
                return implementingEnumerable.GetGenericArguments()[0];
            }

            throw new TectureCloningException(
                $"cannot emit cloning delegate for collection '{collectionType.FullName}'. Could not determine collection element type. {Explanation}");
        }

        public static Expression EmitCollectionCloneExpression(Expression arrayParameter, Type collectionType)
        {
            var elementType = GetCollectionElementType(collectionType);
            var enumerableOfType = typeof(IEnumerable<>).MakeGenericType(elementType);

            var ctor = collectionType.GetConstructors(CtorFlags)
                .Where(x =>
                {
                    var p = x.GetParameters();
                    if (p.Length != 1) return false;
                    if (p[0].ParameterType == enumerableOfType) return true;
                    if (p[0].ParameterType.IsArray && p[0].ParameterType.GetElementType() == elementType) return true;
                    return false;
                }).FirstOrDefault();

            if (ctor == null)
            {
                throw new TectureCloningException($"cannot emit cloning delegate for collection '{collectionType.FullName}'. Constructor of enumerable parameter is missing. {Explanation}");
            }

            var isArray = ctor.GetParameters()[0].ParameterType.IsArray;

            Expression initEx = arrayParameter;

            if (isArray)
            {
                var toArr = ToArrayMethod.MakeGenericMethod(elementType);
                initEx = Expression.Call(toArr, initEx);
            }

            var creation = Expression.New(ctor, initEx);
            return creation;
        }
        public static TypeCloneTooling EmitCollectionCloningDelegate(Type collectionType)
        {
            var elementType = GetCollectionElementType(collectionType);
            var enumerableOfType = typeof(IEnumerable<>).MakeGenericType(elementType);
            var param = Expression.Parameter(enumerableOfType);
            var creation = EmitCollectionCloneExpression(param, collectionType);
            var lambda = Expression.Lambda(creation, param);
            return new TypeCloneTooling()
            {
                Shallow = lambda.Compile()
            };
        }

        internal static (Type, Type) GetDictionaryParameters(this Type collectionType)
        {

            var dictionary =
                collectionType
                    .GetInterfaces()
                    .Where(x => x.IsGenericType)
                    .FirstOrDefault(x => x.GetGenericTypeDefinition() == typeof(IDictionary<,>));
            if (dictionary != null)
            {
                var args = dictionary.GetGenericArguments();
                return (args[0], args[1]);
            }

            throw new TectureCloningException(
                $"cannot emit cloning delegate for dictionary '{collectionType.FullName}'. Could not find proper IDictionary<,> implementation. {Explanation}");
        }

        private static Expression EmitDictionaryCloningExpression(Expression iDictionary, Type dictionaryType)
        {
            var (keyType, valueType) = GetDictionaryParameters(dictionaryType);
            var idictionaryOfType = typeof(IDictionary<,>).MakeGenericType(keyType, valueType);

            var ctor = dictionaryType.GetConstructors(CtorFlags)
                .Where(x =>
                {
                    var p = x.GetParameters();
                    if (p.Length != 1) return false;
                    if (p[0].ParameterType == idictionaryOfType) return true;
                    return false;
                }).FirstOrDefault();

            if (ctor == null)
            {
                throw new TectureCloningException($"cannot emit cloning delegate for dictionary '{dictionaryType.FullName}'. Constructor of IDictionary<,> parameter is missing. {Explanation}");
            }


            var creation = Expression.New(ctor, iDictionary);

            return creation;
        }

        public static TypeCloneTooling EmitDictionaryCloningDelegate(Type dictionaryType)
        {
            var (keyType, valueType) = GetDictionaryParameters(dictionaryType);
            var idictionaryOfType = typeof(IDictionary<,>).MakeGenericType(keyType, valueType);

            var param = Expression.Parameter(idictionaryOfType);
            var creation = EmitDictionaryCloningExpression(param, dictionaryType);
            var lambda = Expression.Lambda(creation, param);

            return new TypeCloneTooling()
            {
                Shallow = lambda.Compile()
            };
        }
    }
}
