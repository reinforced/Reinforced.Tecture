using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor;
using Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor.Expressions;
using Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor.Preparation;
using Convert = Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor.Preparation.Convert;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal
{
    partial class StrokeProcessor
    {
        private readonly IMapper _mapper;
        public StrokeProcessor(IMapper mapper)
        {
            _mapper = mapper;
        }


        public RevealedQuery RevealQuery(LambdaExpression expr)
        {
            var prepared = Prepare.Query(expr);
            var converted = Convert.Query(prepared, x => _mapper.IsEntityType(x));

            
            //first pass to declare autojoined tables
            List<string> autojoins = new List<string>();
            foreach (var resultAst in expressions.OfType<SqlAutojoinExpression>())
            {
                var aj = resultAst;
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

        private void MakeDerivedJoins(Join joinType, TableReference tableRef, StringBuilder output)
        {

        }

        private void MakeJoins(Join joinType, TableReference tableRef, StringBuilder output)
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

        private static void AppendColumn(TableReference tableRef, string columnName, StringBuilder output)
        {
            if (string.IsNullOrEmpty(tableRef.Alias)) output.AppendFormat("[{0}]", tableRef.TableName);
            else output.AppendFormat("[{0}]", tableRef.Alias);

            output.Append('.');

            output.AppendFormat("[{0}]", columnName);
        }
    }
}
