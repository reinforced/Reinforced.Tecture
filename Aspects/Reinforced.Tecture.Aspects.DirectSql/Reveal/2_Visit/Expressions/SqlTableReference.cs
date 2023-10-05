using Reinforced.Tecture.Aspects.DirectSql.Reveal.Visit;
// ReSharper disable CheckNamespace

namespace Reinforced.Tecture.Aspects.DirectSql.Reveal.Visit.Expressions
{
    /// <summary>
    /// SQL expression being expanded to the table reference
    /// </summary>
    public class SqlTableReference : SqlQueryExpression
    {
        internal SqlTableReference() { }

        /// <summary>
        /// Gets table that is being referenced
        /// </summary>
        public TableReference Table { get; internal set; }

        /// <summary>
        /// Gets join type to join children extents
        /// </summary>
        public Join ChildrenJoinedAs { get; internal set; } = Join.Inner;

        /// <summary>
        /// Gets whether table must be mentioned as alias, not with full usage declaration
        /// </summary>
        public bool NotExpand { get; internal set; }
    }
}