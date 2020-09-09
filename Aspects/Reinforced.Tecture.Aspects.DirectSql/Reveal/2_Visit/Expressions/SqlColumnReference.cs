using System.Reflection;
using Reinforced.Tecture.Aspects.DirectSql.Reveal.Visit;
// ReSharper disable CheckNamespace

namespace Reinforced.Tecture.Aspects.DirectSql.Reveal.Visit.Expressions
{
    /// <summary>
    /// Column reference expression
    /// </summary>
    public class SqlColumnReference : SqlQueryExpression
    {
        internal SqlColumnReference() { }
        
        /// <summary>
        /// Gets table that referenced column belongs to
        /// </summary>
        public TableReference Table { get; internal set; }

        /// <summary>
        /// Gets column property that is being referenced
        /// </summary>
        public PropertyInfo Column { get; internal set; }
    }
}