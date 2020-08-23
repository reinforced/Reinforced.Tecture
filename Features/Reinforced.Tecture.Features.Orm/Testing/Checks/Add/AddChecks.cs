using System;
using System.Reflection;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Features.Orm.Testing.Checks.Add
{
    public static class AddChecks
    {
        public static AddEntityTypeCheck<T> Add<T>(Memorize<T> mem = null) => new AddEntityTypeCheck<T>();
        public static AddPredicateCheck<T> Add<T>(Func<T, bool> predicate, string explanation, Memorize<T> mem) => new AddPredicateCheck<T>(predicate, explanation);
        public static AddPredicateCheck<T> Add<T>(Func<T, bool> predicate, string explanation) => new AddPredicateCheck<T>(predicate, explanation);
    }
}
