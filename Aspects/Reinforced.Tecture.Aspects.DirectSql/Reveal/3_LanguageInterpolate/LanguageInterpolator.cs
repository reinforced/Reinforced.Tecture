using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reinforced.Tecture.Aspects.DirectSql.Reveal.Visit.Expressions;
using Reinforced.Tecture.Aspects.DirectSql.Reveal.LanguageInterpolate;
using Reinforced.Tecture.Aspects.DirectSql.Reveal.Visit;

// ReSharper disable once CheckNamespace
namespace Reinforced.Tecture.Aspects.DirectSql.Reveal.LanguageInterpolate
{
    /// <summary>
    /// Visitor-like class that allows to override particular query expressions translation
    /// </summary>
    public partial class LanguageInterpolator
    {
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
        
        protected virtual void Before(){}

        protected virtual void After(){}

        internal LanguageInterpolatedQuery Proceed(VisitedQuery query)
        {
            QueryStructureText = query.QueryStructure;
            Expressions = query.Arguments.ToDictionary(x => x.Position);
            Before();
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

            After();
            return new LanguageInterpolatedQuery(result.ToString(), Parameters.ToArray(), query.UsedTypes);
        }

        protected readonly List<object> Parameters = new List<object>();

        protected string EmitParameter(object parameter)
        {
            if (parameter != null && parameter is Array arr)
            {
                return VisitArrayParameter(arr);
            }
            Parameters.Add(parameter);
            return $"{{{Parameters.Count - 1}}}";
        }

        protected virtual string Visit(SqlQueryExpression sqe)
        {
            switch (sqe)
            {
                case SqlBinaryExpression x1: return VisitBinary(x1);
                case SqlColumnReference x2: return VisitColumnReference(x2);
                case SqlEmptyExpression x3: return VisitEmpty(x3);
                case SqlInExpression x4: return VisitIn(x4);
                case SqlObjectParameter x5: return VisitObjectParameter(x5);
                case SqlQueryLiteralExpression x6: return VisitLiteral(x6);
                case SqlNullExpression x7: return VisitNull(x7);
                case SqlBooleanExpression x8: return VisitBoolean(x8);
                case SqlTableReference x9: return VisitTableReference(x9);
                case SqlTernaryExpression x10: return VisitTernary(x10);
                case SqlUnaryExpression x11: return VisitUnary(x11);
                default:
                    throw new Exception("Unknown expression type");
            }
        }

        private string VisitTableReference(SqlTableReference x9)
        {
            return EmitParameter(x9);
        }

        private string VisitColumnReference(SqlColumnReference x2)
        {
            return EmitParameter(x2);
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

        protected virtual string VisitObjectParameter(SqlObjectParameter expr) => EmitParameter(expr.Parameter);
        protected virtual string VisitLiteral(SqlQueryLiteralExpression expr) => expr.Literal;
        protected virtual string VisitEmpty(SqlEmptyExpression expr) => string.Empty;

        #region SET
        protected virtual bool IsSetExpression(SqlBinaryExpression bex)
        {
            if (_isParsingSet) return false;
            if (bex.IsTop)
            {
                return Precends(SET, bex.Position);
            }

            return false;
        }
        protected bool _isParsingSet = false;
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


        #endregion

    }
}
