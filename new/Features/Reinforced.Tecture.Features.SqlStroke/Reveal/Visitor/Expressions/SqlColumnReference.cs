using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor.Expressions
{
    class SqlColumnReference : SqlQueryExpression
    {
        public TableReference Table { get; set; }

        public PropertyInfo Column { get; set; }

        public override string Serialize(List<Expression> sqlParams)
        {
            if (Table.IsDeclared && !IsAlias)
            {
                return string.Format("[{0}].[{1}]", Table.Alias, Column);
            }

            return string.Format("[{0}]", Column);
        }
    }
}