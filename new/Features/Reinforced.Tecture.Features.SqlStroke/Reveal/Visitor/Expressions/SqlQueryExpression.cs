using System.Collections.Generic;
using System.Linq.Expressions;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor.Expressions
{
    public abstract class SqlQueryExpression
    {
        public bool IsTop { get; set; }
        public abstract string Serialize(List<Expression> sqlParams);
    }
}
