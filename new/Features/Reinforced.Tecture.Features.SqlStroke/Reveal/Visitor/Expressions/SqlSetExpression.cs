using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor.Expressions
{
    class SqlSetExpression : SqlQueryExpression
    {
        public List<SqlSetAssignmentExpression> Assignments { get; set; }

        public SqlSetExpression()
        {
            Assignments = new List<SqlSetAssignmentExpression>();
        }

        public override string Serialize(List<Expression> sqlParams)
        {
            var assignments = Assignments.Select(d => d.Serialize(sqlParams)).ToArray();
            return string.Join(",", assignments);
        }
    }
}