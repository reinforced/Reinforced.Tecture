namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Visit.Expressions
{
    public class SqlBinaryExpression : SqlQueryExpression
    {
        public SqlQueryExpression Left { get; internal set; }
        public SqlQueryExpression Right { get; internal set; }
        public SqlOperator Operator { get; internal set; }

        public bool IsSetSuspect { get; internal set; }
    }
}