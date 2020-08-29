using System;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Features.Orm.Testing.Checks.Delete
{
    public static class DeleteChecks
    {
        public static DeletePredicateCheck<T> Delete<T>(Func<T, bool> predicate, string explanation) => new DeletePredicateCheck<T>(predicate, explanation);
    }
}
