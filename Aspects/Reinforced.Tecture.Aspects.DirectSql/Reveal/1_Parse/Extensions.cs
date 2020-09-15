using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Reinforced.Tecture.Aspects.DirectSql.Reveal.Parse;
using Reinforced.Tecture.Aspects.DirectSql.Reveal.Visit;

// ReSharper disable once CheckNamespace
namespace Reinforced.Tecture.Aspects.DirectSql.Parse
{
    internal static class Extensions
    {

        private const string err = "SQL Storke must be in form of context.Stroke(x=>$\"SOME SQL WITH {x} AND {x.Field} USAGE\")";
        private static void CheckQuery(LambdaExpression expr)
        {

            var bdy = expr.Body as MethodCallExpression;
            if (bdy == null) throw new Exception(err);
            if (bdy.Method.DeclaringType != typeof(String) || bdy.Method.Name != "Format")
            {
                throw new Exception(err);
            }
        }

        private static string ExtractFormatString(MethodCallExpression formatCall)
        {
            // then we obtain query format string like "SELECT {0} WHERE {1}" etc
            // 0 argument is always format string
            var fmtExpr = formatCall.Arguments[0] as ConstantExpression;
            if (fmtExpr == null) throw new Exception(err);
            var format = fmtExpr.Value.ToString();
            return format;
        }

        private static Expression[] ExtractArguments(MethodCallExpression formatCall)
        {
            //now we must determine whether expression is long or in short format
            //long = like string.Format(query, new [] { ... })
            //short one is like string.Format(query,arg,arg)
            //so if second argument is array creation then we use it
            int startingIndex = 1;
            var arguments = formatCall.Arguments;

            // so if long format, we change arguments variable to array's
            // initialization list
            if (formatCall.Arguments.Count == 2)
            {
                var secondArg = formatCall.Arguments[1];
                if (secondArg.NodeType == ExpressionType.NewArrayInit && secondArg is NewArrayExpression array)
                {
                    arguments = array.Expressions;
                    startingIndex = 0;
                }
            }

            // finally, we collect all arguments into seperate array
            List<Expression> finalArguments = new List<Expression>();

            for (int i = startingIndex; i < arguments.Count; i++)
            {
                finalArguments.Add(arguments[i]);
            }

            return finalArguments.ToArray();
        }

        private static bool LookAhead(string str, int position, char symbol)
        {
            if (position >= str.Length) return false;
            return str[position] == symbol;
        }

        private static int CrunchNumber(string str, int startPosition, ref int advancer)
        {
            //assume that startPosition < str.Length
            int adv = 0;
            StringBuilder numberBuilder = new StringBuilder();
            while (char.IsDigit(str[startPosition]))
            {
                numberBuilder.Append(str[startPosition]);
                startPosition++;
                adv++;
            }

            if (numberBuilder.Length == 0) return -1;
            advancer += adv;
            return int.Parse(numberBuilder.ToString());
        }

        private static Dictionary<string, TableReference> ExtractInitialTableReferences(LambdaExpression expr)
        {
            var tbls = new Dictionary<string, TableReference>();
            foreach (var param in expr.Parameters)
            {
                tbls[param.Name] = new TableReference(param.Type, param.Name);
            }

            return tbls;
        }

        public static ParsedQuery ParseStroke(this LambdaExpression expr)
        {
            //first we determine that query is really in form of x=>$"..."
            CheckQuery(expr);

            var formatCall = (MethodCallExpression)expr.Body;

            var format = ExtractFormatString(formatCall);
            var arguments = ExtractArguments(formatCall);

            List<PositionedExpression> positioned = new List<PositionedExpression>();

            // now we build query structure
            // it is representation of query with all the occurrences of format parameters removed
            StringBuilder queryStructure = new StringBuilder();

            // we will remove some characters from input format query, so
            // in order to correctly calculate the position of this query in resulting string
            // we must keep how much characters were removed
            int removed = 0;

            for (int i = 0; i < format.Length; i++)
            {
                // try to catch up opening format brace
                // if we succeed - regardless of what happens next -
                // we will not fail into final 'Append'
                if (format[i] == '{')
                {
                    //handle the case of {{ used to point to single '{'
                    if (LookAhead(format, i + 1, '{')) queryStructure.Append('{');
                    else
                    {
                        // now we try to consume number following the {
                        // {nnn} where n is digit, we consume nnn into int

                        // we save prevI in order to calculate balance later
                        var prevI = i;
                        i++;
                        // crunch number from string, advance iterator
                        var argNumber = CrunchNumber(format, i, ref i);

                        // if number is successfully parsed...
                        if (argNumber > -1)
                        {
                            // current iterator position = symbol after trailing }
                            
                            // now, all the characters between current i and newly advanced i
                            // were removed. so we increase removed count by this range
                            removed += (i - prevI);

                            // and we calculate argument position in query structure
                            // that is, actually, current iterator, moved back by
                            // the total number of removed symbols at this point
                            var argPos = i - removed;

                            // just in case we check that supplied argument takes place in format arguments array
                            if (argNumber >= arguments.Length)
                            {
                                throw new Exception(err);
                            }

                            // and finally, we save fresh position argument
                            positioned.Add(new PositionedExpression(argNumber, argPos,arguments[argNumber]));
                            queryStructure.Append(' ');
                        }
                        else
                        {
                            // something went wrong, it is not the number, iterator was not advanced
                            // in this case we don't skip anything, just move character to 
                            // result without any changes
                            queryStructure.Append(format[i]);
                        }
                    }
                    continue;
                }

                //
                queryStructure.Append(format[i]);
            }

            return new ParsedQuery(queryStructure.ToString(), positioned.ToArray(), ExtractInitialTableReferences(expr));
        }

        
    }
}
