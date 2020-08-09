using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Reinforced.Storage.Defaults.EntityFramework
{
    public static class EfReflectionCache
    {
        public static Type QueryExtensionsType = typeof(QueryableExtensions);

        public static MethodInfo IncludeMethod;

        static EfReflectionCache()
        {
            IncludeMethod = QueryExtensionsType.GetMethods().Single(c => c.Name == "Include" && c.GetGenericArguments().Length == 2);
        }

        public static IQueryable<T> ApplyInclude<T>(IQueryable<T> source, Expression includeExpression)
        {
            var genericMethod = IncludeMethod.MakeGenericMethod(typeof(T), includeExpression.Type.GetGenericArguments()[1]);
            return (IQueryable<T>)genericMethod.Invoke(null, new object[] { source, includeExpression });
        }
    }
}
