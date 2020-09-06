using System.Linq.Expressions;

namespace Reinforced.Tecture.Features.Orm.Queries.Fake
{
    /// <summary>
    /// Query hashing extensions
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Calculates hash of lambda expression
        /// </summary>
        /// <param name="ex">Lambda expression</param>
        /// <returns>Hash of expression</returns>
        public static string CalculateHash(this Expression ex)
        {
            using (var qh = new QueryHasher())
            {
                qh.Visit(ex);
                return $"OrmQuery_{qh.GenerateHash()}";
            }
        }

        /// <summary>
        /// Calculates hash of lambda expression
        /// </summary>
        /// <param name="ex">Lambda expression</param>
        /// <returns>Hash of expression</returns>
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
