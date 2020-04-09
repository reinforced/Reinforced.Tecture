using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Reinforced.Tecture.Methodics.SqlStroke.Reveal.Visitor.Expressions
{
    class SqlAutojoinExpression : SqlQueryExpression
    {
        public List<TableParameterReference> Entities { get; set; }

        public Join Join { get; set; }

        public override string Serialize(List<Expression> sqlParams)
        {
            throw new NotImplementedException();
        }
    }
}