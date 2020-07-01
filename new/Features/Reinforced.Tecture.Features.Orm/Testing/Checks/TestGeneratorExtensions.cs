using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Features.Orm.Testing.Checks.Add;
using Reinforced.Tecture.Testing.Generation;

namespace Reinforced.Tecture.Features.Orm.Testing.Checks
{
    public static class TestGeneratorExtensions
    {
        public static void WithOrmCheck(this TestGenerator tg)
        {
            tg.ChecksFor<Command.Add.Add>()
                .GenerateCheck(v=>AddChecks.Add());
        }
    }
}
