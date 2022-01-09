using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Reinforced.Tecture.Aspects.Orm.Queries.Wrapped.Queryables
{
    public static class StopHashingCrutch
    {
        private static IQueryable<T> Stop<T>(IQueryable<T> source) => source;

        private static readonly MethodInfo StopGenericMethodInfo =
            typeof(StopHashingCrutch)
                .GetMethods(BindingFlags.NonPublic|BindingFlags.Static)
                .FirstOrDefault(x => x.IsGenericMethod && x.Name == nameof(Stop));
        private static IQueryable Stop(IQueryable source) => source;
        
        private static readonly MethodInfo StopMethodInfo =
            typeof(StopHashingCrutch)
                .GetMethods(BindingFlags.NonPublic|BindingFlags.Static)
                .FirstOrDefault(x => !x.IsGenericMethod && x.Name == nameof(Stop));

        static StopHashingCrutch()
        {
            
        }

        public static Expression Apply<T>(Expression ex)
        {
            var generic = StopGenericMethodInfo.MakeGenericMethod(typeof(T));

            return Expression.Call(generic, ex);
        }
        
        public static Expression Apply(Expression ex)
        {
            return Expression.Call(StopMethodInfo, ex);
        }
    }
}