using System;
using System.Reflection;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Features.Orm.Testing.Checks.Add
{
    public static class AddChecks
    {
        public static AddPredicateCheck<T> Add<T>(Func<T, bool> predicate, string explanation) => new AddPredicateCheck<T>(predicate, explanation);
    }
}
