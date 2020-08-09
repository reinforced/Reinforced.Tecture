using System;
using System.Text;
using Reinforced.Tecture.Features.SqlStroke.Reveal.Visit;
using Reinforced.Tecture.Features.SqlStroke.Reveal.Visit.Expressions;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal.LanguageInterpolate
{
    partial class LanguageInterpolator
    {
        protected virtual string IfNull { get { return "IFNULL"; } }
        protected virtual string SET { get { return "SET"; } }

        protected virtual string IsNull(string expression, SqlNullExpression nil, bool negate)
        {
            if (negate) return $"{expression} IS NOT {VisitNull(nil)}";
            return $"{expression} IS {VisitNull(nil)}";
        }
        protected virtual string SetAssign(string left, string right)
        {
            return $"{left} = {right}";
        }

        protected virtual string SetConcat(string left, string right)
        {
            return $"{left}, {right}";
        }

        protected virtual string VisitNull(SqlNullExpression expr) => "NULL";
        protected virtual string VisitBoolean(SqlBooleanExpression expr) => (expr.Value ? "1" : "0").Braces(!expr.IsTop);

        protected virtual string VisitIn(SqlInExpression expr)
        {
            string result;
            if (expr.Not) result = $"{Visit(expr.Expression)} NOT IN ({EmitParameter(expr.Range)})";
            else result = $"{Visit(expr.Expression)} IN ({EmitParameter(expr.Range)})";

            return result.Braces(!expr.IsTop);
        }

        
        protected virtual string VisitTernary(SqlTernaryExpression expr) =>
            $"IIF({Visit(expr.Condition)},{Visit(expr.IfTrue)},{Visit(expr.IfFalse)})";

        protected virtual string OperatorText(SqlOperator op)
        {
            switch (op)
            {
                case SqlOperator.Add: return "+";
                case SqlOperator.Subtract: return "-";
                case SqlOperator.Divide: return "/";
                case SqlOperator.Multiply: return "*";
                case SqlOperator.And: return "&";
                case SqlOperator.AndAlso: return "AND";
                case SqlOperator.Or: return "|";
                case SqlOperator.OrElse: return "OR";
                case SqlOperator.Not: return "NOT";
                case SqlOperator.Equal: return "=";
                case SqlOperator.NotEqual: return "<>";
                case SqlOperator.Negate: return "-";
                case SqlOperator.UnaryPlus: return "+";
                case SqlOperator.Assign: return "=";
                case SqlOperator.GreaterThan: return ">";
                case SqlOperator.LessThan: return "<";
                case SqlOperator.LessThanOrEqual: return "<=";
                case SqlOperator.GreaterThanOrEqual: return ">=";
                case SqlOperator.Coalesce: return "??";
                case SqlOperator.Modulo: return "%";
            }
            throw new Exception("Invalid SqlOperator");
        }

        protected string VisitArrayParameter(Array p)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < p.Length; i++)
            {
                var val = p.GetValue(i);
                if (val != null && val.GetType().IsEnum) val = Convert.ToInt64(val);
                sb.Append(val);
                if (i < p.Length - 1) sb.Append(", ");
            }

            return sb.ToString();
        }
    }
}
