using System;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Features.Orm.Testing.Checks.Update
{
    public static class UpdateChecks
    {
        public static UpdatePredicateCheck<T> Update<T>(Func<T, bool> predicate, string explanation) => new UpdatePredicateCheck<T>(predicate, explanation);
    }
}
