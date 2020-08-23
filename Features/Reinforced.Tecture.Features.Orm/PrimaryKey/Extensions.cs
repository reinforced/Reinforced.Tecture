using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Reinforced.Tecture.Features.Orm.Commands.Add;

namespace Reinforced.Tecture.Features.Orm.PrimaryKey
{
    public static partial class Extensions
    {
        internal static PropertyInfo AsPropertyExpression(this LambdaExpression lex)
        {
            var bdy = lex.Body;
            MemberExpression mex;
            if (bdy is UnaryExpression ue)
            {
                mex = ue.Operand as MemberExpression;
            }
            else
            {
                mex = bdy as MemberExpression;
            }
            if (mex == null)
                throw new Exception("Property lambda expected here");

            if (mex.Member is PropertyInfo pi)
            {
                return pi;
            }
            throw new Exception("Expression does not refer to property");
        }

        private static T Value<T>(object target, LambdaExpression lex)
        {
            var pi = lex.AsPropertyExpression();
            return (T) pi.GetValue(target);
        }
        internal static IEnumerable<T> FromTuple<T>(object tuple)
        {
            var tupleType = tuple.GetType();
            if (typeof(T).IsAssignableFrom(tupleType)) yield return (T) tuple;
            else
            {
                var r = tupleType.GetProperties(BindingFlags.Public | BindingFlags.GetProperty |
                                                 BindingFlags.Instance)
                    .Where(x=>x.Name.StartsWith("Item"))
                    .OrderBy(x=>x.Name)
                    .Select(p=>(T)p.GetValue(tuple));
                foreach (var v in r)
                {
                    yield return v;
                }
            }
        }

        public static PropertyInfo[] KeyProperties(this IPrimaryKey pk)
        {
            var t = pk.GetType().GetProperty(nameof(IPrimaryKey<int>.PrimaryKey),BindingFlags.Public|BindingFlags.Instance);
            
            var keyProps = t.GetValue(pk);

            var extr =  FromTuple<LambdaExpression>(keyProps).Select(x => x.AsPropertyExpression());
            return extr.ToArray();
        }
    }
}
