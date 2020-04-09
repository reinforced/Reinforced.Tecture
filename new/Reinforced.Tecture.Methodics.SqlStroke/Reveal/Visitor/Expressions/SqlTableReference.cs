using System.Collections.Generic;
using System.Linq.Expressions;

namespace Reinforced.Tecture.Methodics.SqlStroke.Reveal.Visitor.Expressions
{
    class SqlTableReference : SqlQueryExpression
    {
        public TableParameterReference Table { get; set; }
        public bool IsDeclaration { get; set; }
        public override string Serialize(List<Expression> sqlParams)
        {
            if (IsDeclaration)
            {
                return string.Format("[{0}] [{1}]", Table.TableName, Table.Alias);
            }
            if (Table.IsDeclared) return string.Format("[{0}]", Table.Alias);
            return string.Format("[{0}]", Table.TableName);
        }
    }
}