using System.Collections.Generic;
using System.Linq.Expressions;

namespace Reinforced.Tecture.Methodics.SqlStroke.Reveal.Visitor.Expressions
{
    class SqlSetAssignmentExpression : SqlQueryExpression
    {
        public SqlColumnReference Column { get; set; }

        public SqlQueryExpression Expression { get; set; }


        public override string Serialize(List<Expression> sqlParams)
        {
            return string.Format("{0} = {1}", Column.Serialize(sqlParams), Expression.Serialize(sqlParams));
        }
    }
}