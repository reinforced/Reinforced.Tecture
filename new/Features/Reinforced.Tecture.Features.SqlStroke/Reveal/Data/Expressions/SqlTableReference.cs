using System.Collections.Generic;
using System.Linq.Expressions;
using Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Data.Expressions
{
    public class SqlTableReference : SqlQueryExpression
    {
        public TableReference Table { get; internal set; }

        public Join ChildrenJoinedAs { get; internal set; } = Join.Inner;

        public bool AsAlias { get; internal set; }
    }
}