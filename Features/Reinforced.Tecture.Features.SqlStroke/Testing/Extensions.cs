using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Features.SqlStroke.Commands;
using Reinforced.Tecture.Features.SqlStroke.Testing.Checks;
using Reinforced.Tecture.Testing.Checks;

namespace Reinforced.Tecture.Features.SqlStroke.Testing
{
    public static class Extensions
    {
        public static ChecksConfigurator<Sql> Text(this ChecksConfigurator<Sql> ck)
        {
            ck.Enlist(new SqlCommandTextCheckDescription());
            return ck;
        }

        public static ChecksConfigurator<Sql> Parameters(this ChecksConfigurator<Sql> ck)
        {
            ck.Enlist(new SqlCommandParmatersCheckDescription());
            return ck;
        }
        public static void CheckSql(this UnitTestGenerator tg)
        {
            var fr = tg.For<Sql>();
            fr.Text();
            fr.Parameters();
        }
    }
}
