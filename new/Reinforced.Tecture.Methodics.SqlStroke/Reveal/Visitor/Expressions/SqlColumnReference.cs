using System.Collections.Generic;
using System.Linq.Expressions;

namespace Reinforced.Tecture.Methodics.SqlStroke.Reveal.Visitor.Expressions
{
    class SqlColumnReference : SqlQueryExpression
    {
        public TableParameterReference Table { get; set; }

        public string ColumnName { get; set; }
        public bool IsAlias { get; set; }
        public override string Serialize(List<Expression> sqlParams)
        {
            if (Table.IsDeclared && !IsAlias)
            {
                return string.Format("[{0}].[{1}]", Table.Alias, ColumnName);
            }

            return string.Format("[{0}]", ColumnName);
        }
    }
}