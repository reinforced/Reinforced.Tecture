using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Reinforced.Tecture.Methodics.SqlStroke.Reveal.Visitor;
using Reinforced.Tecture.Methodics.SqlStroke.Reveal.Visitor.Expressions;

namespace Reinforced.Tecture.Methodics.SqlStroke.Reveal
{
    partial class StrokeProcessor : IMapper
    {
        private readonly IMapper _mapper;
        public StrokeProcessor(IMapper mapper)
        {
            _mapper = mapper;
        }

        public RevealedQuery RevealQuery(LambdaExpression expr)
        {
            const string err = "SQL Storke must be in form of context.Stroke(x=>$\"SOME SQL WITH {x} AND {x.Field} USAGE\")";
            var bdy = expr.Body as MethodCallExpression;
            if (bdy == null) throw new Exception(err);
            if (bdy.Method.DeclaringType != typeof(String) || bdy.Method.Name != "Format")
            {
                throw new Exception(err);
            }

            var fmtExpr = bdy.Arguments[0] as ConstantExpression;
            if (fmtExpr == null) throw new Exception(err);
            var format = fmtExpr.Value.ToString();

            int startingIndex = 1;
            var arguments = bdy.Arguments;
            bool longFormat = false;
            if (bdy.Arguments.Count == 2)
            {
                var secondArg = bdy.Arguments[1];
                if (secondArg.NodeType == ExpressionType.NewArrayInit)
                {
                    var array = secondArg as NewArrayExpression;
                    arguments = array.Expressions;
                    startingIndex = 0;
                    longFormat = true;
                }
            }

            var tbls = new Dictionary<string, TableParameterReference>();
            foreach (var param in expr.Parameters)
            {
                tbls[param.Name] = new TableParameterReference(param.Type)
                {
                    Alias = param.Name,
                    TableName = GetTableName(param.Type)
                };
            }

            var visitor = new StrokeVisitor(this, format, tbls);
            List<SqlQueryExpression> expressions = new List<SqlQueryExpression>();
            List<string> formatArgs = new List<string>();
            List<Expression> sqlParams = new List<Expression>();
            for (int i = startingIndex; i < arguments.Count; i++)
            {
                var argIdx = longFormat ? i : i - 1;
                visitor.ArgIdx = argIdx;
                visitor.Visit(arguments[i]);
                var resultAst = visitor.Retrieve();
                resultAst.IsTop = true;
                expressions.Add(resultAst);

            }
            //first pass to declare autojoined tables
            List<string> autojoins = new List<string>();
            foreach (var resultAst in expressions)
            {
                var aj = resultAst as SqlAutojoinExpression;
                if (aj != null)
                {
                    StringBuilder joinsOutput = new StringBuilder();
                    foreach (var paramTableRef in aj.Entities)
                    {
                        foreach (var chld in paramTableRef.Children)
                        {
                            if (chld.IsDerivedJoin) MakeDerivedJoins(aj.Join, chld, joinsOutput);
                            else MakeJoins(aj.Join, chld, joinsOutput);
                        }
                    }
                    autojoins.Add(joinsOutput.ToString());
                }
            }

            //2nd pass to reveal the rest
            int ajIdx = 0;
            foreach (var resultAst in expressions)
            {
                var aj = resultAst as SqlAutojoinExpression;
                if (aj != null) formatArgs.Add(autojoins[ajIdx++]);
                else formatArgs.Add(resultAst.Serialize(sqlParams));
            }

            var sqlString = string.Format(format, formatArgs.ToArray());


            object[] parameters;
            if (sqlParams.Count > 0)
            {

                var sqlParamsExpression = Expression.NewArrayInit(typeof(object), sqlParams);
                var sqlParamsLambda = Expression.Lambda(sqlParamsExpression);
                var del = sqlParamsLambda.Compile();
                parameters = (object[])del.DynamicInvoke();
            }
            else
            {
                parameters = new object[0];
            }
            return new RevealedQuery(sqlString, parameters, visitor.UsedTypes.ToArray());
        }

        private void MakeDerivedJoins(Join joinType, TableParameterReference tableRef, StringBuilder output)
        {

        }

        private void MakeJoins(Join joinType, TableParameterReference tableRef, StringBuilder output)
        {
            if (!tableRef.IsDeclared)
            {
                tableRef.IsDeclared = true;

                var fields = GetJoinKeys(tableRef.Parent.EntityType, tableRef.JoinColumn);
                output.Append((tableRef.JoinOveride ?? joinType).ToSql());
                output.Append(" JOIN ");
                if (!string.IsNullOrEmpty(tableRef.Alias))
                {
                    output.AppendFormat("[{0}] [{1}] ON ", tableRef.TableName, tableRef.Alias);
                }
                else
                {
                    output.AppendFormat("[{0}] ON ", tableRef.TableName, tableRef.Alias);
                }

                var first = true;
                foreach (var assocField in fields)
                {
                    if (!first) output.Append(" AND ");
                    first = false;

                    AppendColumn(tableRef, assocField.From, output);
                    output.Append(" = ");
                    AppendColumn(tableRef.Parent, assocField.To, output);
                }

                output.AppendLine();
            }


            foreach (var child in tableRef.Children)
            {
                MakeJoins(joinType, child, output);
            }

        }

        private static void AppendColumn(TableParameterReference tableRef, string columnName, StringBuilder output)
        {
            if (string.IsNullOrEmpty(tableRef.Alias)) output.AppendFormat("[{0}]", tableRef.TableName);
            else output.AppendFormat("[{0}]", tableRef.Alias);

            output.Append('.');

            output.AppendFormat("[{0}]", columnName);
        }
    }
}
