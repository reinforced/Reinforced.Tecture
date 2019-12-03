using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reinforced.Storage.Testing.Stories.Update
{
    public static class UpdateAssertions
    {
        public static UpdateEntityTypeAssertion<T> Update<T>(Memorize<T> mem = null) => new UpdateEntityTypeAssertion<T>();
        public static UpdatePredicateAssertion<T> Update<T>(Func<T, bool> predicate, string explanation, Memorize<T> mem = null) => new UpdatePredicateAssertion<T>(predicate, explanation);
    }
}
