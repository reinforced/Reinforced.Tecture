using System;
using System.Collections.Generic;

namespace Reinforced.Tecture.Aspects.Orm.Testing.Checks.Update
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
        /// <param name="expected">Predicate to validate entity</param>
        /// <param name="explanation">Check explanation</param>
        /// <returns>Check instance</returns>
        public static UpdateDictionaryCheck<T> Update<T>(Dictionary<string,object> expected, string explanation) => new UpdateDictionaryCheck<T>(expected, explanation);
    }
}
