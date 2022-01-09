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

        internal static object InstanceNonpublic(this Type t, Func<Type,object> resolver = null)
        {
            var ctors = t.GetTypeInfo().DeclaredConstructors.ToArray();
            if (ctors.Length == 0)
                throw new MissingMethodException(
                    $"Service {t} cannot be created because of missing at least one private parameterless constructor");
            
            if (resolver == null)
            {
                var ctor = ctors.FirstOrDefault(x => (x.IsPrivate || x.IsAssembly) && x.GetParameters().Length == 0);
                if (ctor == null)
                {
                    throw new MissingMethodException(
                        $"Service {t} cannot be created because IoC resolver is not specified and service is missing private parameterless constructor");
                }
                return ctor.Invoke(new object[0]);
            }

            var ctorsWithParams = ctors.Where(x => x.IsPrivate).OrderByDescending(x => x.GetParameters().Length);

            foreach (var ctorWithParam in ctorsWithParams)
            {
                var parameters = ctorWithParam.GetParameters();
                if (parameters.Length == 0) return ctorWithParam.Invoke(new object[0]);

                bool inappropriate = false;
                var paramsValues = parameters.Select(v =>
                    {
                        object retVal = null;
                        try
                        {
                            retVal = resolver(v.ParameterType);
                        }
                        catch (Exception)
                        {
                            inappropriate = true;
                        }

                        return retVal;
                    })
                    .ToArray();
                
                if (inappropriate) continue;
                return ctorWithParam.Invoke(paramsValues);
            }
            
            throw new MissingMethodException(
                $"Service {t} cannot be created because appropriate constructor cannot be found");
        }
    }
}
