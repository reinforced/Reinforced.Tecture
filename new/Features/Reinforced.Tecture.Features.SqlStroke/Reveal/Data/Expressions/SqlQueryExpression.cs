namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Data.Expressions
{
    public abstract class SqlQueryExpression
    {
        /// <summary>
        /// Gets whether expression is topmost, not nested into other expression
        /// </summary>
        public bool IsTop { get; internal set; }

        /// <summary>
        /// Expression position within Query Structure text
        /// </summary>
        public int Position { get; internal set; }

        /// <summary>
        /// Zero-based expression index
        /// </summary>
        public int Index { get; internal set; }

    }
}
