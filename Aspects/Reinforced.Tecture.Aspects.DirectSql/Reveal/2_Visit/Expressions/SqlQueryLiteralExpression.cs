// ReSharper disable CheckNamespace
namespace Reinforced.Tecture.Aspects.DirectSql.Reveal.Visit.Expressions
{
    /// <summary>
    /// Literal query expression
    /// </summary>
    public class SqlQueryLiteralExpression : SqlQueryExpression
    {
        internal SqlQueryLiteralExpression() { }

        /// <summary>
        /// Gets verbatim literal that is expressed by current expression
        /// </summary>
        public string Literal { get; internal set; }
        
    }
}