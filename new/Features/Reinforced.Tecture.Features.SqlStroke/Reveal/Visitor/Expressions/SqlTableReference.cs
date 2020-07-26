using System.Collections.Generic;
using System.Linq.Expressions;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor.Expressions
{
    public class SqlTableReference : SqlQueryExpression
    {
        public TableReference Table { get; set; }

        public Join ChildrenJoinedAs { get; set; } = Join.Inner;

        public bool AsAlias { get; set; }

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