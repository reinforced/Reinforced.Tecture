using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reinforced.Storage.Testing.Stories.Remove
{
    public static class RemoveAssertions
    {
        public static RemoveEntityTypeAssertion<T> Remove<T>(Memorize<T> mem = null) => new RemoveEntityTypeAssertion<T>();
        public static RemovePredicateAssertion<T> Remove<T>(Func<T, bool> predicate, string explanation, Memorize<T> mem = null) => new RemovePredicateAssertion<T>(predicate, explanation);
    }
}
