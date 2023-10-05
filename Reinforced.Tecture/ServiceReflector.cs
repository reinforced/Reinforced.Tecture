using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Reinforced.Tecture
{
    internal static class ServiceReflector
    {
        private static readonly ConcurrentDictionary<Type, Delegate> _serviceMakers
            = new ConcurrentDictionary<Type, Delegate>();

        private static Delegate CreateServiceMaker(Type t)
        {
            var constructors = t
                .GetTypeInfo()
                .DeclaredConstructors
                .Where(x => x.IsPrivate)
                .OrderByDescending(x => x.GetParameters().Length)
                .ToArray();
            
            if (constructors.Length == 0)
                throw new MissingMethodException(
                    $"Service {t} cannot be created because of missing at least one private parameterless constructor");
            var resolverParam = Expression.Parameter(typeof(Func<Type, object>));
            var returnOptions = new Stack<Expression>();
            foreach (var ctor in constructors)
            {
                var parameters = ctor.GetParameters();
                if (parameters.Length == 0)
                {
                    returnOptions.Push(Expression.New(ctor));
                }
                else
                {
                    var paramExpressions = parameters.Select(x =>
                        Expression.Convert(
                            Expression.Invoke(resolverParam, Expression.Constant(x.ParameterType)),
                            x.ParameterType));
                    returnOptions.Push(Expression.New(ctor,paramExpressions));
                }
            }

            var def = returnOptions.Pop();
            var mmex = new MissingMethodException(
                $"Service {t} cannot be created because appropriate constructor cannot be found");

            var defExp = Expression.Throw(Expression.Constant(mmex),def.Type);
            var tryBlock = Expression.TryCatch(Expression.Block(def), Expression.Catch(typeof(Exception),Expression.Block(defExp)));
            while (returnOptions.Count>0)
            {
                var ro = returnOptions.Pop();
                tryBlock = Expression.TryCatch(Expression.Block(ro), Expression.Catch(typeof(Exception),Expression.Block(tryBlock)));
            }

            var ret = Expression.Lambda(tryBlock, new[] { resolverParam });

            return ret.Compile();
        }
        
        public static object CreateServiceInstance(this Type t, Func<Type,object> resolver = null)
        {
            var del = _serviceMakers.GetOrAdd(t, CreateServiceMaker);
            return del.DynamicInvoke(new object[]{resolver});
        }
    }
}