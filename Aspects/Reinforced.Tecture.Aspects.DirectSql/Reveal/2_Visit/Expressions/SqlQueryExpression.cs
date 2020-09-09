// ReSharper disable CheckNamespace
namespace Reinforced.Tecture.Aspects.DirectSql.Reveal.Visit.Expressions
{
    /// <summary>
    /// Base class for all SQL expressions
    /// </summary>
    public abstract class SqlQueryExpression
    {
        /// <summary>
        /// Gets whether expression is topmost, not nested into other expression
        /// </summary>
        public bool IsTop { get; internal set; }

        /// <summary>
        /// Gets expression position within Query Structure text
        /// </summary>
        public int Position { get; internal set; }

        /// <summary>
        /// Gets zero-based expression index
        /// </summary>
        public int Index { get; internal set; }

    }
}
