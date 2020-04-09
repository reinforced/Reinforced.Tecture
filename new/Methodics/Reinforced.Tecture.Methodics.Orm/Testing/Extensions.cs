using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Methodics.Orm.Testing
{
    public static class Extensions
    {
        public static TestingEnvironment WithOrmTesting(this TestingEnvironment te, 
            bool strict = false,
            Action<IPrefetch> prefetch = null
            )
        {
            TestingOrmSource src = new TestingOrmSource(strict);
            if (prefetch != null) prefetch(src);

            TestingOrmRuntime torr = new TestingOrmRuntime(src);

            te.WithTestRuntime(torr);
            return te;
        }
    }
}
