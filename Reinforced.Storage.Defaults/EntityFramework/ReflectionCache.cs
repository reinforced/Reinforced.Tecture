using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reinforced.Storage.Defaults.EntityFramework
{
    internal static class ReflectionCache
    {
        private static readonly Dictionary<Type, PropertyInfo[]> _cachedProperties = new Dictionary<Type, PropertyInfo[]>();
        private static readonly Dictionary<Type, Type[]> _cachedGenericInterfaces = new Dictionary<Type, Type[]>();
        private static readonly Dictionary<Type, bool> _collectionMarkers = new Dictionary<Type, bool>();


        private static readonly Dictionary<Type, MethodInfo> _clearCollectionMethods = new Dictionary<Type, MethodInfo>();
        private static readonly Dictionary<Type, MethodInfo> _addCollectionMethods = new Dictionary<Type, MethodInfo>();

        public static void ClearCollection(object collection)
        {
            var coll = collection.SafeGetType();
            var method = _clearCollectionMethods.GetOrCreate(coll, () =>
            {
                return coll.GetMethod("Clear");
            });
            method.Invoke(collection, null);
        }

        public static void AddToCollection(object collection, object objectToAdd)
        {
            var coll = collection.SafeGetType();
            var method = _addCollectionMethods.GetOrCreate(coll, () =>
            {
                return coll.GetMethod("Add");
            });
            method.Invoke(collection, new[] { objectToAdd });
        }

        public static PropertyInfo[] GetCachedProperties(this Type t)
        {
            return _cachedProperties.GetOrCreate(t, () => t.GetProperties(BindingFlags.Instance | BindingFlags.Public));
        }

        public static Type[] GetCachedGenericInterfaces(this Type t)
        {
            return _cachedGenericInterfaces.GetOrCreate(t, () => t.GetInterfaces().Where(c => c.IsGenericType).ToArray());
        }


        internal static Type SafeGetType(this object o)
        {
            return System.Data.Entity.Core.Objects.ObjectContext.GetObjectType(o.GetType());
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
            if (mex == null) throw new Exception("Here should be property");
            var pi = mex.Member as PropertyInfo;
            if (pi == null) throw new Exception("Here should be property");
            return pi;
        }

        public static MethodInfo ParseMethodLambda(LambdaExpression lambda)
        {
            var mex = lambda.Body as MethodCallExpression;
            if (mex == null)
                throw new Exception("Here should be method");
            return mex.Method;
        }

    }
}
