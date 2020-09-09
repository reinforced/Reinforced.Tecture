using System;

namespace Reinforced.Tecture.Aspects.DirectSql.Reveal
{
    /// <summary>
    /// SQL query that is being interpolated at the some stage
    /// </summary>
    public abstract class InterpolatedQuery
    {
        /// <summary>
        /// Gets query text
        /// </summary>
        public string Query { get; }

        /// <summary>
        /// Gets query parameters
        /// </summary>
        public object[] Parameters { get; }

        /// <summary>
        /// Get entity types  that are being used within query
        /// </summary>
        public Type[] UsedTypes { get; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        internal InterpolatedQuery(string query, object[] parameters, Type[] usedTypes)
        {
            Query = query;
            Parameters = parameters;
            UsedTypes = usedTypes;
        }

        internal abstract InterpolatedQuery Clone();
    }
}
