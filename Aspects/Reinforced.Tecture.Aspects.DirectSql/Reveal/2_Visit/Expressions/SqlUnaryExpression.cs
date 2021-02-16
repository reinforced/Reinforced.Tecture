
// ReSharper disable CheckNamespace

namespace Reinforced.Tecture.Aspects.DirectSql.Reveal.Visit.Expressions
{
    /// <summary>
    /// Unary SQL expression
    /// </summary>
    public class SqlUnaryExpression : SqlQueryExpression
    {
        internal SqlUnaryExpression() { }

        /// <summary>
        /// Gets SQL expression that is operand
        /// </summary>
        public SqlQueryExpression Operand { get; internal set; }

        /// <summary>
        /// Gets SQL unary operator that must be applied to expression
        /// </summary>
        public SqlOperator Operator { get; internal set; }
        
    }
}