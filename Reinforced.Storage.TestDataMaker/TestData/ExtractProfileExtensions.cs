using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Reinforced.Storage.TestCodeMaker.TestData
{
    public static class ExtractProfileExtensions
    {
        private static T MakeConfiguration<T>(this T t, LambdaExpression prop, Action<PropertyInfo> todo)
        where T:NongenericExtractProfile
        {
            Expression e = prop.Body;
            if (e is UnaryExpression uex) e = uex.Operand;

            if (e is MemberExpression mex)
            {
                if (mex.Expression.NodeType != ExpressionType.Parameter)
                    throw new Exception("Only first-level properties are supported");
                var pi = mex.Member as PropertyInfo;
                if (pi != null)
                {
                    if (pi.DeclaringType != t.TypeRef)
                        throw new Exception("Property from invalid class taken");
                    todo(pi);
                    return t;
                }
            }
            throw new Exception("Property must be here");
        }

        public static ExtractProfile<T> With<T, TVal>(this ExtractProfile<T> t, Expression<Func<T, TVal>> prop, Action<ExtractProfile<TVal>> nested = null)
        {
            return t.MakeConfiguration(prop, (pi) =>
            {
                ExtractProfile<TVal> ep;
                if (!t.LateBound.ContainsKey(pi))
                {
                    ep = new ExtractProfile<TVal>();

                    t.LateBound[pi] = new LateBoundProperty()
                    {
                        Property = pi,
                        Profile = ep
                    };
                }
                else
                {
                    ep = (ExtractProfile<TVal>)t.LateBound[pi].Profile;
                }
                if (nested != null) nested(ep);
            });
        }

        public static ExtractProfile<T> WithOut<T, TVal>(this ExtractProfile<T> t, Expression<Func<T, TVal>> prop)
        {
            return t.MakeConfiguration(prop, (pi) =>
            {
                if (t.LateBound.ContainsKey(pi)) t.LateBound.Remove(pi);
            });
        }

        /// <summary>
        /// DO NOT DO SO! Use .WithCollection!!!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <param name="t"></param>
        /// <param name="prop"></param>
        /// <param name="nested"></param>
        /// <returns></returns>
        public static ExtractProfile<T> With<T, TVal>(this ExtractProfile<T> t,
            Expression<Func<T, IEnumerable<TVal>>> prop,
            Action<ExtractProfile<TVal>> nested = null)
        {
            throw new Exception("Do not do so");
        }


        public static ExtractProfile<T> WithCollection<T, TVal>(this ExtractProfile<T> t, Expression<Func<T, IEnumerable<TVal>>> prop, Func<T, TVal[]> collection, Action<ExtractProfile<TVal>> nested = null)
        {
            return t.MakeConfiguration(prop, (pi) =>
            {
                ExtractProfile<TVal> ep;
                if (!t.LateBound.ContainsKey(pi))
                {
                    ep = new ExtractProfile<TVal>();
                    t.LateBound[pi] = new LateBoundProperty()
                    {
                        Property = pi,
                        Profile = ep,
                        OverrideCollection = collection,
                        IsCollection = true
                    };
                }
                else
                {
                    ep = (ExtractProfile<TVal>) t.LateBound[pi].Profile;
                }
                if (nested != null) nested(ep);
            });
        }

        public static T WithNestedAggregates<T>(this T ep) where T : NongenericExtractProfile
        {
            var allProperties = ep.TypeRef.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var latebound = new List<PropertyInfo>();

            foreach (var propertyInfo in allProperties)
            {
                if (!TypeInitConstructor.IsInlineable(propertyInfo.PropertyType) && !propertyInfo.PropertyType.IsEnumerable())
                {
                    latebound.Add(propertyInfo);
                }
            }

            foreach (var pi in latebound)
            {
                var pt = typeof(ExtractProfile<>).MakeGenericType(pi.PropertyType);
                var profile = (NongenericExtractProfile) Activator.CreateInstance(pt);
                ep.LateBound[pi] = new LateBoundProperty()
                {
                    Property = pi,
                    Profile = profile
                };
            }

            return ep;
        }
    }
}
