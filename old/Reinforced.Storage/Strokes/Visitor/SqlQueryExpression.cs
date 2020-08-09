using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Reinforced.Storage.Strokes.Visitor
{
    abstract class SqlQueryExpression
    {
        public bool IsTop { get; set; }
        public abstract string Serialize(List<Expression> sqlParams);
    }

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

    class SqlSetAssignmentExpression : SqlQueryExpression
    {
        public SqlColumnReference Column { get; set; }

        public SqlQueryExpression Expression { get; set; }


        public override string Serialize(List<Expression> sqlParams)
        {
            return string.Format("{0} = {1}", Column.Serialize(sqlParams), Expression.Serialize(sqlParams));
        }
    }

    class SqlQueryLiteralExpression : SqlQueryExpression
    {
        public string Literal { get; set; }
        public override string Serialize(List<Expression> sqlParams)
        {
            return Literal;
        }
    }

    class SqlBinaryExpression : SqlQueryExpression
    {
        public SqlQueryExpression Left { get; set; }
        public SqlQueryExpression Right { get; set; }
        public string Symbol { get; set; }
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

    class SqlUnaryExpression : SqlQueryExpression
    {
        public SqlQueryExpression Operand { get; set; }

        public string Symbol { get; set; }
        public override string Serialize(List<Expression> sqlParams)
        {
            if (Symbol == "NOT")
            {
                var op = Operand as SqlInExpression;
                if (op != null)
                {
                    op.Not = true;
                    return string.Format("({0})", op.Serialize(sqlParams));
                }
            }
            return string.Format("({0} {1})", Symbol, Operand.Serialize(sqlParams));
        }
    }


    class SqlTernaryExpression : SqlQueryExpression
    {
        public SqlQueryExpression Condition { get; set; }
        public SqlQueryExpression IfTrue { get; set; }
        public SqlQueryExpression IfFalse { get; set; }
        public override string Serialize(List<Expression> sqlParams)
        {
            return string.Format("(IIF({0},{1},{2}))", Condition.Serialize(sqlParams), IfTrue.Serialize(sqlParams),
                IfFalse.Serialize(sqlParams));
        }
    }

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

    class SqlInExpression : SqlQueryExpression
    {
        public SqlQueryExpression Expression { get; set; }

        public object[] Range { get; set; }

        internal bool Not { get; set; }
        public override string Serialize(List<Expression> sqlParams)
        {
            sqlParams.Add(System.Linq.Expressions.Expression.Constant(Range));
            string oper = Not ? "NOT IN" : "IN";
            return string.Format("{0} {2} ({{{1}}})", Expression.Serialize(sqlParams), sqlParams.Count - 1, oper);
        }
    }

    class SqlObjectParameter : SqlQueryExpression
    {
        public object Parameter { get; set; }
        public override string Serialize(List<Expression> sqlParams)
        {
            sqlParams.Add(Expression.Convert(Expression.Constant(Parameter), typeof(object)));
            return string.Format("{{{0}}}", sqlParams.Count - 1);
        }
    }

    class SqlAutojoinExpression : SqlQueryExpression
    {
        public List<TableParameterReference> Entities { get; set; }

        public Join Join { get; set; }

        public override string Serialize(List<Expression> sqlParams)
        {
            throw new NotImplementedException();
        }
    }

    class SqlEmptyExpression : SqlQueryExpression
    {
        public override string Serialize(List<Expression> sqlParams)
        {
            return string.Empty;
        }
    }
}
