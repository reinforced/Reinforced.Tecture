namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Visit.Expressions
{
    public class SqlTableReference : SqlQueryExpression
    {
        public TableReference Table { get; internal set; }

        public Join ChildrenJoinedAs { get; internal set; } = Join.Inner;

        public bool AsAlias { get; internal set; }
    }
}