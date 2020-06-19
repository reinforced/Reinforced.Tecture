using System;
using System.Collections.Generic;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal
{
    internal static class Extensions
    {
        internal static bool Precends(this string master, string search, int index)
        {
            index--;
            while (index >= 0 && Char.IsWhiteSpace(master, index)) index--;
            if (index - search.Length + 1 < 0) return false;
            index = index - search.Length + 1;
            return master.IndexOf(search, index, StringComparison.InvariantCultureIgnoreCase) == index;
        }

        internal static void AddIfNotExists<T>(this HashSet<T> hashSet, T val)
        {
            if (hashSet.Contains(val)) return;
            hashSet.Add(val);
        }
    }
}
