using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture.Features.Orm.Testing.Checks.DeletePk
{
    /// <summary>
    /// Checks factory for delete by primary key command
    /// </summary>
    public static class DeletePKChecks
    {
        /// <summary>
        /// Deletion by primary key check
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="explanation">Explanation</param>
        /// <param name="keys">Primary key values</param>
        /// <returns>Delete by primary key check</returns>
        public static DeletePkKeyAndTypeCheck<T> DeleteByPK<T>(string explanation, params object[] keys) => new DeletePkKeyAndTypeCheck<T>(keys, explanation);
    }
}
