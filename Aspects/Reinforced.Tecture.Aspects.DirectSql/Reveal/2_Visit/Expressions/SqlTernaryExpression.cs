// ReSharper disable CheckNamespace
namespace Reinforced.Tecture.Aspects.DirectSql.Reveal.Visit.Expressions
{
    /// <summary>
    /// SQL ternary operator
    /// </summary>
    public class SqlTernaryExpression : SqlQueryExpression
    {
        internal SqlTernaryExpression() { }

        /// <summary>
        /// Gets condition to test
        /// </summary>
        public SqlQueryExpression Condition { get; internal set; }

        /// <summary>
        /// Gets expression to be returned in case if condition met
        /// </summary>
        public SqlQueryExpression IfTrue { get; internal set; }

        /// <summary>
        /// Gets expression to be returned in case if condition did not met
        /// </summary>
        public SqlQueryExpression IfFalse { get; internal set; }
    }
}