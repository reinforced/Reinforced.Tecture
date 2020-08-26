using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Features.Orm.Testing.Checks.Add;
using Reinforced.Tecture.Features.Orm.Testing.Checks.Delete;
using Reinforced.Tecture.Testing.Checks;

namespace Reinforced.Tecture.Features.Orm.Testing.Checks
{
    public static class Extensions
    {
        public static void CheckOrm(this UnitTestGenerator tg)
        {
            tg.For<Commands.Add.Add>().Basic();
            tg.For<Commands.Delete.Delete>().Basic();
        }
    }
}
