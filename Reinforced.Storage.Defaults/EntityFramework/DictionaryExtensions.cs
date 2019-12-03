using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Text.RegularExpressions;

namespace Reinforced.Storage.Defaults.EntityFramework
{
    internal static class DictionaryExtensions
    {
        public static TV GetOrNull<T, TV>(this Dictionary<T, TV> dictionary, T key)
        {
            if (!dictionary.ContainsKey(key)) return default(TV);
            return dictionary[key];
        }

        public static TV GetOr<T, TV>(this Dictionary<T, TV> dictionary, T key, Func<TV> or)
        {
            if (!dictionary.ContainsKey(key))
            {
                return or();
            }
            return dictionary[key];
        }

        public static TV GetOrCreate<T, TV>(this Dictionary<T, TV> dictionary, T key) where TV : new()
        {
            TV result;
            if (dictionary.ContainsKey(key)) result = dictionary[key];
            else
            {
                result = new TV();
                dictionary[key] = result;
            }
            return result;
        }

        public static TV GetOrCreate<T, TV>(this Dictionary<T, TV> dictionary, T key, Func<TV> createDelegate)
        {
            TV result;
            if (dictionary.ContainsKey(key)) result = dictionary[key];
            else
            {
                result = createDelegate();
                dictionary[key] = result;
            }
            return result;
        }

        public static void AddIfNotExists<T>(this HashSet<T> hashSet, T val)
        {
            if (hashSet.Contains(val)) return;
            hashSet.Add(val);
        }

        public static string GetTableName<T>(this DbContext context) where T : class
        {
            System.Data.Entity.Core.Objects.ObjectContext objectContext = ((IObjectContextAdapter)context).ObjectContext;

            return objectContext.GetTableName<T>();
        }

        public static string GetTableName<T>(this System.Data.Entity.Core.Objects.ObjectContext context) where T : class
        {
            string sql = context.CreateObjectSet<T>().ToTraceString();
            Regex regex = new Regex("FROM\\s+(?<table>.+)\\s+AS");
            Match match = regex.Match(sql);

            string table = match.Groups["table"].Value;
            return table;
        }
    }
}
