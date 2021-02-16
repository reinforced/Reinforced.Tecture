using Reinforced.Tecture.Aspects.DirectSql.Reveal.Visit;
// ReSharper disable CheckNamespace

namespace Reinforced.Tecture.Aspects.DirectSql.Reveal.Visit.Expressions
{

    /// <summary>
    /// Binary SQL expression
    /// </summary>
    public class SqlBinaryExpression : SqlQueryExpression
    {
        internal SqlBinaryExpression() { }
        /// <summary>
        /// Gets expression left part
        /// </summary>
        public SqlQueryExpression Left { get; internal set; }

        /// <summary>
        /// Gets expression right part
        /// </summary>
        public SqlQueryExpression Right { get; internal set; }

        /// <summary>
        /// Gets expression operator
        /// </summary>
        public SqlOperator Operator { get; internal set; }

        /// <summary>
        /// Gets whether this expression is suspected to be used in SET argument
        /// </summary>
        public bool IsSetSuspect { get; internal set; }
    }
}