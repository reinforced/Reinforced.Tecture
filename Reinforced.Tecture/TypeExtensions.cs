using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Reinforced.Tecture
{
    internal static class TypeExtensions
    {
        internal static bool IsDictionary(this Type collectionType)
        {
            return typeof(IDictionary).IsAssignableFrom(collectionType);
        }

        internal static bool IsCollection(this Type collectionType)
        {
            if (collectionType.IsArray) return true;
            return typeof(IEnumerable).IsAssignableFrom(collectionType);
        }

        internal static (Type, Type) GetDictionaryParameters(this Type collectionType)
        {

            var dictionary =
                collectionType
                    .GetInterfaces()
                    .Where(x => x.IsGenericType)
                    .FirstOrDefault(x => x.GetGenericTypeDefinition() == typeof(IDictionary<,>));
            if (dictionary != null)
            {
                var args = dictionary.GetGenericArguments();
                return (args[0], args[1]);
            }

            return (null,null);
        }

        internal static Type GetCollectionElementType(this Type collectionType)
        {
            if (collectionType.IsArray) return collectionType.GetElementType();
            var implementingEnumerable =
                collectionType
                    .GetInterfaces()
                    .Where(x => x.IsGenericType)
                    .FirstOrDefault(x => x.GetGenericTypeDefinition() == typeof(IEnumerable<>));
            if (implementingEnumerable != null)
            {
                return implementingEnumerable.GetGenericArguments()[0];
            }

            return null;
        }

        internal static bool IsAnonymousType(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            // HACK: The only way to detect anonymous types right now.
            return Attribute.IsDefined(type, typeof(CompilerGeneratedAttribute), false)
                   && (
                       type.IsGenericType && type.Name.Contains("AnonymousType")
                       ||
                       (
                           (type.Name.StartsWith("<>") || type.Name.StartsWith("VB$"))
                       )
                    )
                   && type.Attributes.HasFlag(TypeAttributes.NotPublic);
        }

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
            catch (MissingMethodException)
            {
                throw new Exception($"Service {t} must contain private constructor");
            }
        }
    }
}
