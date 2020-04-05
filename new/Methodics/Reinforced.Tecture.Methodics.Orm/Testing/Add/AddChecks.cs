using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Tecture.Methodics.Orm.Testing.Add;
using Reinforced.Tecture.Testing;

namespace Reinforced.Storage.Testing.Stories.Add
{
    public static class AddChecks
    {
        public static AddEntityTypeCheck<T> Add<T>(Memorize<T> mem = null) => new AddEntityTypeCheck<T>();
        public static AddPredicateCheck<T> Add<T>(Func<T, bool> predicate, string explanation, Memorize<T> mem = null) => new AddPredicateCheck<T>(predicate, explanation);
    }
}
