using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Reinforced.Tecture
{
    internal static class TypeExtensions
    {
        internal static object InstanceNonpublic(this Type t, params object[] parameters)
        {
            try
            {
#if NETCOREAPP1_0
            var dc = t.GetTypeInfo().DeclaredConstructors;
            var needed = dc.Where(d=>d.GetParameters().Length == parameters.Length).FirstOrDefault();
            return needed.Invoke(parameters);
#elif NETSTANDARD1_5
            var dc = t.GetTypeInfo().DeclaredConstructors;
            var needed = dc.Where(d=>d.GetParameters().Length == parameters.Length).FirstOrDefault();
            return needed.Invoke(parameters);
#elif NETSTANDARD1_6
                var dc = t.GetTypeInfo().DeclaredConstructors;
                var needed = dc.Where(d => d.GetParameters().Length == parameters.Length).FirstOrDefault();
                return needed.Invoke(parameters);
#else
                return Activator.CreateInstance(t, BindingFlags.NonPublic | BindingFlags.Instance, null, parameters,
                    null);
#endif
            }
            catch (MissingMethodException ex)
            {
                throw new Exception($"Service {t} must contain private constructor");
            }
        }
    }
}
