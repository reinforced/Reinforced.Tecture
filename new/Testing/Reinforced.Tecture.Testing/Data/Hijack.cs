using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Reinforced.Tecture.Testing.Data
{
    public class Hijack
    {
        public class TypeHijack<T>
        {
            
            private readonly Dictionary<PropertyInfo, Delegate> _typeEntry;
            internal TypeHijack(Hijack hijack)
            {
                if (!hijack._hijacks.ContainsKey(typeof(T)))
                {
                    hijack._hijacks[typeof(T)] = new Dictionary<PropertyInfo, Delegate>();
                }

                _typeEntry = hijack._hijacks[typeof(T)];
            }

            public TypeHijack<T> Hijack<TVal>(Expression<Func<T,TVal>> property,Func<T,TVal> getter)
            {
                var bdy = property.Body;
                if (bdy is UnaryExpression ue)
                {
                    bdy = ue.Operand;
                }

                if (bdy is MemberExpression mex)
                {
                    if (mex.Member is PropertyInfo pi)
                    {
                        _typeEntry[pi] = getter;
                        return this;
                    }
                }
                
                throw new Exception("Invalid usage of hijacks");
            }
        }


        private readonly Dictionary<Type,Dictionary<PropertyInfo,Delegate>> _hijacks = new Dictionary<Type, Dictionary<PropertyInfo, Delegate>>();

        internal object GetValue(object instance, PropertyInfo prop)
        {
            if (instance == null) return null;
            var t = prop.DeclaringType;
            if (_hijacks.ContainsKey(t))
            {
                var props = _hijacks[t];
                if (props.ContainsKey(prop))
                {
                    return props[prop].DynamicInvoke(instance);
                }
            }

            return prop.GetValue(instance);
        }

        public TypeHijack<T> For<T>()
        {
            return new TypeHijack<T>(this);
        }
    }
}
