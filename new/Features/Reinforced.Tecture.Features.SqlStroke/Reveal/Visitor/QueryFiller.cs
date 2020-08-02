using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor.Expressions;
using Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor.Preparation;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor
{
    /// <summary>
    /// Visitor-like class that allows to override particular query expressions translation
    /// </summary>
    public class QueryFiller
    {

        internal void Init(IMapper mapper,PreparedSqlQuery query)
        {
            Mapper = mapper;
            QueryStructureText = query.QueryStructure;
            Expressions = query.Arguments.ToDictionary(x => x.Position);
        }
        /// <summary>
        /// String containing query with cut off all the parameters replaced by empty strings.
        /// So if original query was x=>"SELECT {x.Name} FROM {x}", the query structure will be
        /// "SELECT   FROM   "
        /// </summary>
        protected string QueryStructureText { get; set; }

        /// <summary>
        /// Pairs of index -> SqlQueryExpression where key is index in <see cref="QueryStructureText"/> where
        /// expression must be embedded at and value is expression itself
        /// </summary>
        protected Dictionary<int, SqlQueryExpression> Expressions { get; private set; }

        /// <summary>
        /// Access to the Mapper instance
        /// </summary>
        public IMapper Mapper { get; internal set; }

        /// <summary>
        /// Determines whether search string appears immediately before the index. Ignores case.
        /// </summary>
        /// <param name="search">Search string</param>
        /// <param name="index">Index to look up for search string before</param>
        /// <returns>True when search string appears before the index, false otherwise</returns>
        protected bool Precends(string search, int index)
        {
            var master = QueryStructureText;
            index--;
            while (index >= 0 && Char.IsWhiteSpace(master, index)) index--;
            if (index - search.Length + 1 < 0) return false;
            index = index - search.Length + 1;
            return master.IndexOf(search, index, StringComparison.InvariantCultureIgnoreCase) == index;
        }

        internal string Proceed()
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < QueryStructureText.Length; i++)
            {
                if (Expressions.ContainsKey(i))
                {
                    result.Append(Visit(Expressions[i]));
                }
                else
                {
                    result.Append(QueryStructureText[i]);
                }
            }

            return result.ToString();
        }

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

        internal List<object> Parameters { get; private set; } = new List<object>();

        protected string EmitParameter(object parameter)
        {
            Parameters.Add(parameter);
            return $"{{{Parameters.Count - 1}}}";
        }

        protected virtual string Visit(SqlQueryExpression sqe)
        {
            if (sqe is SqlBinaryExpression x1) return VisitBinary(x1);
            if (sqe is SqlColumnReference x2) return VisitColumnReference(x2);
            if (sqe is SqlEmptyExpression x3) return VisitEmpty(x3);
            if (sqe is SqlInExpression x4) return VisitIn(x4);
            if (sqe is SqlObjectParameter x5) return VisitObjectParameter(x5);
            if (sqe is SqlQueryLiteralExpression x6) return VisitLiteral(x6);
            if (sqe is SqlNullExpression x7) return VisitNull(x7);
            if (sqe is SqlBooleanExpression x8) return VisitBoolean(x8);
            if (sqe is SqlTableReference x9) return VisitTableReference(x9);
            if (sqe is SqlTernaryExpression x10) return VisitTernary(x10);
            if (sqe is SqlUnaryExpression x11) return VisitUnary(x11);

            throw new Exception("Unknown expression type");
        }

        protected virtual string VisitUnary(SqlUnaryExpression expr)
        {
            if (expr.Operator == SqlOperator.Not && expr.Operand is SqlInExpression ex)
            {
                ex.Not = true;
                return VisitIn(ex);
            }
            return $"{OperatorText(expr.Operator)} {Visit(expr.Operand)}".Braces(!expr.IsTop);
        }

        protected virtual string VisitTernary(SqlTernaryExpression expr) =>
            $"IIF({Visit(expr.Condition)},{Visit(expr.IfTrue)},{Visit(expr.IfFalse)})";
        protected virtual string VisitObjectParameter(SqlObjectParameter expr) => EmitParameter(expr.Parameter);
        protected virtual string VisitLiteral(SqlQueryLiteralExpression expr) => expr.Literal;
        protected virtual string VisitNull(SqlNullExpression expr) => "NULL";
        protected virtual string VisitBoolean(SqlBooleanExpression expr) => (expr.Value ? "1" : "0").Braces(!expr.IsTop);
        protected virtual string VisitEmpty(SqlEmptyExpression expr) => string.Empty;

        #region SqlTableReference

        protected virtual string VisitTableReference(SqlTableReference expr)
        {
            return VisitTableReference(expr.Table, expr.ChildrenJoinedAs, expr.AsAlias);
        }

        protected virtual string VisitTableReference(TableReference tref, Join joinType, bool notExpand)
        {
            var table = Mapper.GetTableName(tref.EntityType);
            if (notExpand) return TableAlias(tref);

            StringBuilder result = new StringBuilder();
            result.Append(TableMakeAlias(tref));
            VisitChildrenReferences(tref, joinType, result);
            return result.ToString();
        }

        private void VisitChildrenReferences(TableReference tref, Join joinType, StringBuilder result)
        {
            foreach (var ntr in tref.Children)
            {
                result.AppendLine();
                result.AppendFormat(" {0} JOIN {1} ON", joinType.ToSql(), TableMakeAlias(ntr));
                var fields = Mapper.GetJoinKeys(tref.EntityType, ntr.JoinColumn);
                var first = true;
                foreach (var assocField in fields)
                {
                    if (!first) result.Append(" AND ");
                    first = false;

                    result.Append(VisitColumnReference(ntr, assocField.From));
                    result.Append(" = ");
                    result.Append(VisitColumnReference(ntr.Parent, assocField.To));
                }

                if (ntr.Children.Count > 0) VisitChildrenReferences(ntr, joinType, result);
            }
        }

        #endregion

        #region SqlInExpression

        protected virtual string VisitIn(SqlInExpression expr)
        {
            string result;
            if (expr.Not) result = $"{Visit(expr.Expression)} NOT IN ({EmitParameter(expr.Range)})";
            else result = $"{Visit(expr.Expression)} IN ({EmitParameter(expr.Range)})";

            return result.Braces(!expr.IsTop);
        }


        #endregion

        #region SqlColumnReference

        protected virtual string VisitColumnReference(SqlColumnReference expr)
        {
            return VisitColumnReference(expr.Table, Mapper.GetColumnName(expr.Table.EntityType, expr.Column));
        }

        protected virtual string VisitColumnReference(TableReference tr, string colName)
        {
            return $"{TableAlias(tr)}.[{colName}]";
        }



        #endregion

        #region SqlBinaryExpression

        private bool IsNullExpression(SqlBinaryExpression bex)
        {
            if (!((bex.Right is SqlNullExpression || bex.Left is SqlNullExpression))) return false;
            if (bex.Operator == SqlOperator.Equal) return true;
            if (bex.Operator == SqlOperator.NotEqual) return true;
            return false;
        }

        protected virtual string VisitBinary(SqlBinaryExpression bex)
        {
            string result;
            if (IsSetExpression(bex))
            {
                _isParseringSet = true;
                VisitSet(bex);
                _isParseringSet = false;
            }

            if (!_isParseringSet && IsNullExpression(bex))
            {
                result = VisitIsNull(bex);
            }
            else
            {
                if (bex.Operator == SqlOperator.Coalesce)
                {
                    result = $"{IfNull}({Visit(bex.Left)},{Visit(bex.Right)})";
                }
                else result = $"{Visit(bex.Left)} {OperatorText(bex.Operator)} {Visit(bex.Right)}";
            }
           
            return result.Braces(!bex.IsTop);
        }

        protected virtual string VisitIsNull(SqlBinaryExpression bex)
        {
            var activePart = bex.Left is SqlNullExpression ? bex.Right : bex.Left;
            var nullPart = bex.Left is SqlNullExpression ? bex.Left : bex.Right;
            var ap = Visit(activePart);
            return IsNull(ap, null, bex.Operator == SqlOperator.NotEqual).Braces(!bex.IsTop);
        }

        protected virtual string IsNull(string expression, SqlNullExpression nil, bool negate)
        {
            if (negate) return $"{expression} IS NOT {VisitNull(nil)}";
            return $"{expression} IS {VisitNull(nil)}";
        }

        #endregion

        #region SET
        protected virtual bool IsSetExpression(SqlBinaryExpression bex)
        {
            if (_isParseringSet) return false;
            if (bex.IsTop)
            {
                return Precends(SET, bex.Position);
            }

            return false;
        }
        protected bool _isParseringSet = false;
        protected virtual string VisitSet(SqlBinaryExpression bex)
        {
            if (bex.Operator == SqlOperator.Equal)
            {
                return ProceedPart(bex);
            }
            var l = ProceedPart(bex.Left);
            var r = ProceedPart(bex.Right);
            return SetConcat(l, r);
        }

        private string ProceedPart(SqlQueryExpression qx)
        {
            if (qx is SqlBinaryExpression bex)
            {
                if (bex.Operator == SqlOperator.Or)
                {
                    return VisitSet(bex);
                }

                if (bex.Operator == SqlOperator.Equal)
                {
                    if (bex.Left is SqlColumnReference cr)
                    {
                        var right = Visit(bex.Right);
                        var left = VisitColumnReference(cr);
                        return SetAssign(left, right);
                    }
                }
            }


            throw new Exception("Invalid SET expression");
        }

        protected virtual string SetAssign(string left, string right)
        {
            return $"{left} = {right}";
        }

        protected virtual string SetConcat(string left, string right)
        {
            return $"{left}, {right}";
        }
        #endregion

        protected virtual string IfNull { get { return "IFNULL"; } }
        protected virtual string SET { get { return "SET"; } }

        protected virtual string TableMakeAlias(TableReference tr)
        {
            if (string.IsNullOrEmpty(tr.Alias)) return $"[{Mapper.GetTableName(tr.EntityType)}]";
            return $"[{Mapper.GetTableName(tr.EntityType)}] [{tr.Alias}]";
        }
        protected virtual string TableAlias(TableReference tr)
        {
            if (string.IsNullOrEmpty(tr.Alias)) return $"[{Mapper.GetTableName(tr.EntityType)}]";
            return $"[{tr.Alias}]";
        }

    }

    internal static class StringExtensions
    {
        public static string Braces(this string expr, bool need)
        {
            if (need) return $"({expr})";
            return expr;
        }
    }
}
