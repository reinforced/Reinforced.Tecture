
using System.Collections.Generic;

namespace Reinforced.Tecture.Aspects.Orm.Testing.Checks.UpdatePk
{
    /// <summary>
    /// Checks factory for delete by primary key command
    /// </summary>
    public static class UpdatePkChecks
    {
        /// <summary>
        /// Deletion by primary key check
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="explanation">Explanation</param>
        /// <param name="keys">Primary key values</param>
        /// <returns>Delete by primary key check</returns>
        public static UpdatePkKeyAndTypeCheck<T> UpdateByPK<T>(string explanation, params object[] keys) => new UpdatePkKeyAndTypeCheck<T>(keys, explanation);

        /// <summary>
        /// Update predicate check
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expected">Predicate to validate entity</param>
        /// <param name="explanation">Check explanation</param>
        /// <returns>Check instance</returns>
        public static UpdatePkDictionaryCheck<T> UpdatedValues<T>(Dictionary<string, object> expected, string explanation) => new UpdatePkDictionaryCheck<T>(expected, explanation);
    }
}
