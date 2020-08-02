using System.Reflection;
using Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Data.Expressions
{
    /// <summary>
    /// Column reference expression
    /// </summary>
    public class SqlColumnReference : SqlQueryExpression
    {
        /// <summary>
        /// Table reference
        /// </summary>
        public TableReference Table { get; internal set; }

        /// <summary>
        /// Column that is being referenced
        /// </summary>
        public PropertyInfo Column { get; internal set; }
    }
}