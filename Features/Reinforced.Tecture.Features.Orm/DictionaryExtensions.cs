using System;
using System.Collections.Generic;

namespace Reinforced.Tecture.Features.Orm
{
    internal static class DictionaryExtensions
    {
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
    }
}