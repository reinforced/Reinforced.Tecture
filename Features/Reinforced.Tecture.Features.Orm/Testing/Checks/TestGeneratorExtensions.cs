using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Features.Orm.Testing.Checks.Add;
using Reinforced.Tecture.Features.Orm.Testing.Checks.Delete;
using Reinforced.Tecture.Testing.Generation;

namespace Reinforced.Tecture.Features.Orm.Testing.Checks
{
    public static class TestGeneratorExtensions
    {
        public static void CheckOrm(this TestGenerator tg)
        {
            tg.For<Commands.Add.Add>().Basic();
            tg.For<Commands.Delete.Delete>().Basic();
        }
    }
}
