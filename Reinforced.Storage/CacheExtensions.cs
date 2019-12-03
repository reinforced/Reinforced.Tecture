using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Reinforced.Storage
{
    public static class CacheExtensions
    {
        public static T GetOrAdd<T>(this IStorageCache c, string key, Func<T> getter, TimeSpan? ttl = null)
        {
            if (c.Contains(key)) return c.Get<T>(key);
            var value = getter();
            c.Set(key, value, ttl);
            return value;
        }

        public static T2 GetOrAdd<T, T2>(this EntityCache<T> c, string key, Func<T2> getter, TimeSpan? ttl = null) where T : class
        {
            if (c.Contains(key)) return c.Get<T2>(key);
            var value = getter();
            c.Set(key, value, ttl);
            return value;

        }

        public static async Task<T> GetOrAddAsync<T>(this IStorageCache c, string key, Func<T> getter, TimeSpan? ttl = null)
        {
            if (await c.ContainsAsync(key)) return await c.GetAsync<T>(key);
            var value = getter();
            await c.SetAsync(key, value, ttl);
            return value;
        }

        public static async Task<T> GetOrAddAsync<T>(this IStorageCache c, string key, Func<Task<T>> getter, TimeSpan? ttl = null)
        {
            if (await c.ContainsAsync(key)) return await c.GetAsync<T>(key);
            var value = await getter();
            await c.SetAsync(key, value, ttl);
            return value;
        }

        public static async Task<T> GetOrAddAsync<T>(this EntityCache<T> c, string key, Func<Task<T>> getter, TimeSpan? ttl = null) where T : class
        {
            if (await c.ContainsAsync(key)) return await c.GetAsync<T>(key);
            var value = await getter();
            await c.SetAsync(key, value, ttl);
            return value;
        }
        
        public static IQueryable<TQueryResult> Query<T, TQueryResult>(this EntityCache<T> c, Expression<Func<IQueryFor<T>, IQueryable<TQueryResult>>> query, TimeSpan? ttl = null) where T : class
        {
            var qf = c.FromDatabase;
            var methodKey = SerializeQueryForInvocation(qf, query, false);
            var queryKey = SerializeQueryForInvocation(qf, query, true);

            if (c.Contains(queryKey))
            {
                var results = c.Get<TQueryResult[]>(queryKey).AsQueryable();
                return results;
            }

            using (c.Lock(methodKey))
            {
                var results = query.Compile()(qf).ToArray();
                c.Set(queryKey, results, ttl);
                c.AddToSet(methodKey, queryKey);
                return results.AsQueryable();
            }
        }

        public static void Invalidate<T, TQueryResult>(this EntityCache<T> c, Expression<Func<IQueryFor<T>, TQueryResult>> query, TimeSpan? ttl = null) where T : class
        {
            var qf = c.FromDatabase;
            var methodKey = SerializeQueryForInvocation(qf, query, false);
            var queryKey = SerializeQueryForInvocation(qf, query, true);

            using (c.Lock(methodKey))
            {
                c.RemoveFromSet(methodKey, queryKey);
                c.Drop(queryKey);
            }
        }

        public static void InvalidateAll<T, TQueryResult>(this EntityCache<T> c, Expression<Func<IQueryFor<T>, TQueryResult>> query, TimeSpan? ttl = null) where T : class
        {
            var qf = c.FromDatabase;
            var methodKey = SerializeQueryForInvocation(qf, query, false);

            using (c.Lock(methodKey))
            {
                var keys = c.SetContents(methodKey);
                foreach (var key in keys)
                {
                    c.Drop(key);
                    c.RemoveFromSet(methodKey, key);
                }
            }
        }

        private static string SerializeQueryForInvocation<T>(IQueryFor<T> targetIQueryFor, LambdaExpression expression, bool includeParameters)
        {
            StringBuilder key = new StringBuilder();
            key.Append("Query:");
            var method = expression.Body as MethodCallExpression;
            if (method == null)
            {
                var property = expression.Body as MemberExpression;
                if (property == null) throw new Exception("It is only possible to cache .All or IQueryFor<> extension method invocation");
                if (property.Member.Name != "All")
                {
                    throw new Exception("It is not possible to cache IQueryFor<> properties except .All");
                }
                return "Query:All";
            }
            if (!CheckMethod(method))
            {
                throw new Exception("Only direct invocations of IQeuryFor<> are cacheable");
            }

            if (!includeParameters) return string.Format("Query:{0}", method.Method.Name);

            var arguments = method.Arguments.Skip(1).Select(c =>
            {
                var lambdaExpr = Expression.Lambda(c, expression.Parameters).Compile();
                var value = lambdaExpr.DynamicInvoke(targetIQueryFor);
                return SerializeParameterValue(value);
            }).ToArray();
            key.AppendFormat("{0}:({1})", method.Method.Name, string.Join(",", arguments));
            
            return key.ToString();
        }

        private static bool CheckMethod(MethodCallExpression method)
        {
            if (method.Object != null) return false;
            if (method.Arguments.Count < 1) return false;
            if (!(typeof(IQueryFor).IsAssignableFrom(method.Arguments[0].Type))) return false;
            return true;
        }

        private static string SerializeParameterValue(object value)
        {
            if (value == null) return "null";
            return value.ToString();
        }
    }
}
