using System;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Features.Orm.Testing
{
    public static class Extensions
    {
        public static TestingEnvironment WithOrmTesting(this TestingEnvironment te, 
            bool strict = false,
            Action<IPrefetch> prefetch = null
            )
        {
            TestingOrmRuntime torr = new TestingOrmRuntime(strict);
            if (prefetch != null) prefetch(torr._testingDataSource);

            te.WithTestRuntime(torr);
            return te;
        }
    }
}
