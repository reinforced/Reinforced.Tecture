using System.Collections.Generic;
using System.Linq.Expressions;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor.Expressions
{
    class SqlBinaryExpression : SqlQueryExpression
    {
        public SqlQueryExpression Left { get; set; }
        public SqlQueryExpression Right { get; set; }
        public string Symbol { get; set; }

        public bool IsSetSuspect { get; set; }

        public override string Serialize(List<Expression> sqlParams)
        {
            string result;
            if (Symbol == "??")
            {
                result = string.Format("ISNULL({0},{1})", Left.Serialize(sqlParams), Right.Serialize(sqlParams));
            }
            else result = string.Format("{0} {1} {2}", Left.Serialize(sqlParams), Symbol, Right.Serialize(sqlParams));

            if (!IsTop)
            {
                result = string.Format("({0})", result);
            }
            return result;
        }
    }
}