using System.Linq.Expressions;

namespace Reinforced.Tecture.Aspects.Orm.Queries.Hashing
{
    public class ExpressionHashData
    {
        public string Hash { get; internal set; }

        public Expression ModifiedExpression { get; internal set; }
    }
    
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
        public static ExpressionHashData CalculateHash(this Expression ex, params Expression[] another)
        {
            var result = new ExpressionHashData();
            using (var qh = new QueryHasher())
            {
                var ret = qh.Visit(ex);
                result.ModifiedExpression = ret;
                
                foreach (var expression in another)
                {
                    qh.Visit(expression);
                }

                var log = string.Join( "|",qh.Box.ObjectsLog);
                result.Hash = $"OrmQuery_{qh.GenerateHash()}";
            }

            return result;
        }
        
        /// <summary>
        /// Calculates hash of lambda expression
        /// </summary>
        /// <param name="ex">Lambda expression</param>
        /// <param name="another">Another expression to be included into hash</param>
        /// <returns>Hash of expression</returns>
        public static string CalculateJustHash(this Expression ex, params Expression[] another)
        {
            using (var qh = new QueryHasher())
            {
                qh.Visit(ex);
                foreach (var expression in another)
                {
                    qh.Visit(expression);
                }

                var log = string.Join( "|",qh.Box.ObjectsLog);
                return $"OrmQuery_{qh.GenerateHash()}";
            }
        }
    }
}
