using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Reinforced.Tecture.Aspects.Orm
{
    internal static class ReflectionCache
    {
        private static readonly Dictionary<Type, PropertyInfo[]> _cachedProperties = new Dictionary<Type, PropertyInfo[]>();
        private static readonly Dictionary<Type, Type[]> _cachedGenericInterfaces = new Dictionary<Type, Type[]>();
        private static readonly Dictionary<Type, bool> _collectionMarkers = new Dictionary<Type, bool>();


        public static readonly Dictionary<Type, MethodInfo> ClearCollectionMethods = new Dictionary<Type, MethodInfo>();
        public static readonly Dictionary<Type, MethodInfo> AddCollectionMethods = new Dictionary<Type, MethodInfo>();


        public static PropertyInfo[] GetCachedProperties(this Type t)
        {
            return _cachedProperties.GetOrCreate(t, () => t.GetProperties(BindingFlags.Instance | BindingFlags.Public));
        }

        public static Type[] GetCachedGenericInterfaces(this Type t)
        {
            return _cachedGenericInterfaces.GetOrCreate(t, () => t.GetInterfaces().Where(c => c.IsGenericType).ToArray());
        }


        internal static bool IsCollection(this Type t)
        {
            return _collectionMarkers.GetOrCreate(t, () =>
            {
                if (t.IsGenericType && (t.GetGenericTypeDefinition() == typeof(ICollection<>))) return true;
                var i = t.GetCachedGenericInterfaces();
                return i.Any(c => c.GetGenericTypeDefinition() == typeof(ICollection<>));
            });
        }

        public static PropertyInfo ParsePropertyLambda(LambdaExpression lambda)
        {
            var mex = lambda.Body as MemberExpression;
            var unary = lambda.Body as UnaryExpression;
            if (unary != null)
            {
                mex = unary.Operand as MemberExpression;
            }
            if (mex == null) throw new TectureException("Here should be property");
            var pi = mex.Member as PropertyInfo;
            if (pi == null) throw new TectureException("Here should be property");
            return pi;
        }

        public static MethodInfo ParseMethodLambda(LambdaExpression lambda)
        {
            var mex = lambda.Body as MethodCallExpression;
            if (mex == null)
                throw new TectureException("Here should be method");
            return mex.Method;
        }

    }
}
