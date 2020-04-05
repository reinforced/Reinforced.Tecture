using System;
using Reinforced.Storage.Testing.Stories.Update;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Methodics.Orm.Testing.Update
{
    public static class UpdateChecks
    {
        public static UpdateEntityTypeCheck<T> Update<T>(Memorize<T> mem = null) => new UpdateEntityTypeCheck<T>();
        public static UpdatePredicateCheck<T> Update<T>(Func<T, bool> predicate, string explanation, Memorize<T> mem = null) => new UpdatePredicateCheck<T>(predicate, explanation);
    }
}
