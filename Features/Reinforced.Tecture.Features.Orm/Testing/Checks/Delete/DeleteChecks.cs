using System;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Features.Orm.Testing.Checks.Delete
{
    /// <summary>
    /// Checks factory for delete command
    /// </summary>
    public static class DeleteChecks
    {
        /// <summary>
        /// Delete predicate check
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">Predicate to validate entity</param>
        /// <param name="explanation">Check explanation</param>
        /// <returns>Check instance</returns>
        public static DeletePredicateCheck<T> Delete<T>(Func<T, bool> predicate, string explanation) => new DeletePredicateCheck<T>(predicate, explanation);
    }
}
