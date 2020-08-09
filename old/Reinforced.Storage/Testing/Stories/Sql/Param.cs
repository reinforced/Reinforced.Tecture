using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reinforced.Storage.Testing.Stories.Sql
{
    public class SqlParameterValidator
    {
        public SqlParameterValidator(string description)
        {
            Description = description;
        }

        internal bool ShouldBeNull { get; set; }
        internal Type Type { get; set; }
        internal Delegate Validator { get; set; }
        internal bool Any { get; set; }
        internal object ExactValue { get; set; }
        internal string Description { get; private set; }
    }

    public class Param
    {
        /// <summary>
        /// Creates SQL parameter validator
        /// </summary>
        /// <typeparam name="T">Parameter type</typeparam>
        /// <param name="validator">Validator delegate</param>
        /// <param name="explanation">Optional explanation of validator</param>
        /// <returns>Sql parameter validator</returns>
        public static SqlParameterValidator Of<T>(Func<T, bool> validator, string explanation = null) => new SqlParameterValidator(string.IsNullOrEmpty(explanation)? $"of type {typeof(T).Name} and conditions": $"of type {typeof(T).Name} and {explanation}") { Validator = validator, Type = typeof(T) };

        /// <summary>
        /// Creates SQL parameter validator
        /// </summary>
        /// <typeparam name="T">Parameter type</typeparam>
        /// <returns>Sql parameter validator</returns>
        public static SqlParameterValidator Of<T>() => new SqlParameterValidator($"of type {typeof(T).Name}") { Type = typeof(T) };

        /// <summary>
        /// Creates wildcard SQL parameter validator
        /// </summary>
        /// <returns>Sql parameter validator</returns>
        public static SqlParameterValidator Any() => new SqlParameterValidator("any") { Any = true };

        /// <summary>
        /// Creates SQL parameter validator against null value of specified type
        /// </summary>
        /// <returns>Sql parameter validator</returns>
        public static SqlParameterValidator Null<T>() where T : class => new SqlParameterValidator("null") { ShouldBeNull = true };

        /// <summary>
        /// Creates SQL parameter validator against exact value
        /// </summary>
        /// <returns>Sql parameter validator</returns>
        public static SqlParameterValidator Is<T>(T value) => new SqlParameterValidator($"exactly '{value}'") { Type = typeof(T), ExactValue = value };
    }
}
