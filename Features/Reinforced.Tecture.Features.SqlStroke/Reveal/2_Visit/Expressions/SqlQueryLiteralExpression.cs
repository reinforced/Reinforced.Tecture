namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Visit.Expressions
{
    /// <summary>
    /// Literal query expression
    /// </summary>
    public class SqlQueryLiteralExpression : SqlQueryExpression
    {
        internal SqlQueryLiteralExpression() { }

        /// <summary>
        /// Verbatim literal
        /// </summary>
        public string Literal { get; internal set; }
        
    }
}