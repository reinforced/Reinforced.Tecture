using System;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Features.Orm.Testing.Checks.Update
{
    /// <summary>
    /// Checks factory for delete command
    /// </summary>
    public static class UpdateChecks
    {
        /// <summary>
        /// Update predicate check
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">Predicate to validate entity</param>
        /// <param name="explanation">Check explanation</param>
        /// <returns>Check instance</returns>
        public static UpdatePredicateCheck<T> Update<T>(Func<T, bool> predicate, string explanation) => new UpdatePredicateCheck<T>(predicate, explanation);
    }
}
