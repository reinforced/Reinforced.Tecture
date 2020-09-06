using System;
using System.Reflection;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Features.Orm.Testing.Checks.Add
{
    /// <summary>
    /// Checks factory for add command
    /// </summary>
    public static class AddChecks
    {
        /// <summary>
        /// Add predicate check
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">Predicate to validate entity</param>
        /// <param name="explanation">Check explanation</param>
        /// <returns>Check instance</returns>
        public static AddPredicateCheck<T> Add<T>(Func<T, bool> predicate, string explanation) => new AddPredicateCheck<T>(predicate, explanation);
    }
}
