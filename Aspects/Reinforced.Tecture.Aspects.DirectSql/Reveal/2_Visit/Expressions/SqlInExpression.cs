// ReSharper disable CheckNamespace
namespace Reinforced.Tecture.Aspects.DirectSql.Reveal.Visit.Expressions
{
    /// <summary>
    /// In/contains expression
    /// </summary>
    public class SqlInExpression : SqlQueryExpression
    {
        internal SqlInExpression() { }

        /// <summary>
        /// Gets expression that must contain supplied range
        /// </summary>
        public SqlQueryExpression Expression { get; internal set; }

        /// <summary>
        /// Gets range of values that expression must be tested against
        /// </summary>
        public object[] Range { get; internal set; }

        /// <summary>
        /// Gets whether IN must be negated
        /// </summary>
        public bool Not { get; internal set; }
        
    }
}