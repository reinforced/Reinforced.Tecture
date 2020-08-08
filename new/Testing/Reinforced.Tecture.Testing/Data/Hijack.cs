using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Reinforced.Tecture.Testing.Data
{
    public class Result
    {
        internal Result() { }
        internal bool IsSet = false;
        internal object Value;

        public void Set(object value)
        {
            Value = value;
            IsSet = true;
        }

        public void Unset()
        {
            Value = null;
            IsSet = false;
        }
    }
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
        private readonly List<Action<object,PropertyInfo,Result>> _allHooks = new List<Action<object, PropertyInfo, Result>>();
        internal object GetValue(object instance, PropertyInfo prop)
        {
            if (instance == null) return null;
            
            var result = new Result();
            foreach (var allHook in _allHooks)
            {
                allHook(instance, prop, result);
                if (result.IsSet) return result.Value;
            }

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
        public Hijack ForAll(Action<object, PropertyInfo, Result> hijack)
        {
            _allHooks.Add(hijack);
            return this;
        }

    }
}
