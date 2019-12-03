using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reinforced.Storage.Testing.Stories.Add
{
    public static class AddAssertions
    {
        public static AddEntityTypeAssertion<T> Add<T>(Memorize<T> mem = null) => new AddEntityTypeAssertion<T>();
        public static AddPredicateAssertion<T> Add<T>(Func<T, bool> predicate, string explanation, Memorize<T> mem = null) => new AddPredicateAssertion<T>(predicate, explanation);
    }
}
