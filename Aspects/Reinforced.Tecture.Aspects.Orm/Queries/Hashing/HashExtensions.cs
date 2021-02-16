using System.Linq.Expressions;

namespace Reinforced.Tecture.Aspects.Orm.Queries.Hashing
{
    /// <summary>
    /// Query hashing extensions
    /// </summary>
    public static class HashExtensions
    {
        /// <summary>
        /// Calculates hash of lambda expression
        /// </summary>
        /// <param name="ex">Lambda expression</param>
        /// <param name="another">Another expression to be included into hash</param>
        /// <returns>Hash of expression</returns>
        public static string CalculateHash(this Expression ex, params Expression[] another)
        {
            using (var qh = new QueryHasher())
            {
                qh.Visit(ex);
                foreach (var expression in another)
                {
                    qh.Visit(expression);
                }
                return $"OrmQuery_{qh.GenerateHash()}";
            }
        }
    }
}
