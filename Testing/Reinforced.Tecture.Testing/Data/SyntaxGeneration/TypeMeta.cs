using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reinforced.Tecture.Testing.Data.SyntaxGeneration
{
    internal class TypeMeta
    {
        private IEnumerable<PropertyInfo> GetProperties()
        {
            var props = TypeRef.GetProperties(BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.Instance);
            foreach (var propertyInfo in props)
            {
                if (typeof(LambdaExpression).IsAssignableFrom(propertyInfo.PropertyType))
                    continue;
                if (typeof(Task).IsAssignableFrom(propertyInfo.PropertyType))
                    continue;
                if (typeof(Delegate).IsAssignableFrom(propertyInfo.PropertyType))
                    continue;
                yield return propertyInfo;
            }

        }

        private bool _isAnonymous = false;
        private Hijack _hijack;
        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public TypeMeta(Type typeRef,Hijack h)
        {
            _hijack = h;
            TypeRef = typeRef;
            _isAnonymous = typeRef.IsAnonymousType();
            if (!_isAnonymous)
            {
                try
                {
                    DefaultInstance = Activator.CreateInstance(typeRef);
                }
                catch (Exception)
                {
                    DefaultInstance = typeRef.InstanceNonpublic();
                }
            }

            var properties = GetProperties();
            InlineProperties = properties.Where(x => x.PropertyType.IsInlineable()).ToArray();
            CollectionProperties = properties.Where(x => x.PropertyType.IsEnumerable() || (x.PropertyType.IsTuple() && !x.PropertyType.IsInlineable())).ToArray();
            NestedProperties =
                properties.Where(x => !InlineProperties.Contains(x) && !CollectionProperties.Contains(x)).ToArray();
        }

        public Type TypeRef { get; private set; }
        public PropertyInfo[] InlineProperties { get; private set; }
        public PropertyInfo[] CollectionProperties { get; private set; }
        public PropertyInfo[] NestedProperties { get; private set; }

        public object DefaultInstance { get; private set; }

        public bool IsDefault(PropertyInfo pi, object anotherInstance)
        {
            if (_isAnonymous) return false;
            var value = Value(pi,anotherInstance);
            var defValue = pi.GetValue(DefaultInstance);
            return Equals(value, defValue);
        }

        public object Value(PropertyInfo pi, object anotherInstance)
        {
            if (_isAnonymous) return pi.GetValue(anotherInstance);
            return _hijack.GetValue(anotherInstance, pi);
        }

    }
}
