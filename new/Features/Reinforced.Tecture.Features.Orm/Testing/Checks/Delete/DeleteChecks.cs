using System;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Features.Orm.Testing.Checks.Delete
{
    public static class DeleteChecks
    {
        public static DeleteEntityTypeCheck<T> Delete<T>(Memorize<T> mem = null) => new DeleteEntityTypeCheck<T>();
        public static DeletePredicateCheck<T> Delete<T>(Func<T, bool> predicate, string explanation, Memorize<T> mem = null) => new DeletePredicateCheck<T>(predicate, explanation);
    }
}
