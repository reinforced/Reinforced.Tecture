using System.Linq.Expressions;

namespace Reinforced.Tecture.Features.Orm.Queries.Fake
{
    public static class Extensions
    {
        public static string CalculateHash(this Expression ex)
        {
            using (var qh = new QueryHasher())
            {
                qh.Visit(ex);
                return $"OrmQuery_{qh.GenerateHash()}";
            }
        }

        public static string CalculateHash<T>(this Expression<T> ex)
        {
            using (var qh = new QueryHasher())
            {
                qh.Visit(ex);
                return $"OrmQuery_{qh.GenerateHash()}";
            }
        }
    }
}
