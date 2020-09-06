using Reinforced.Tecture.Features.Orm.Testing.Checks.Add;
using Reinforced.Tecture.Features.Orm.Testing.Checks.Delete;
using Reinforced.Tecture.Features.Orm.Testing.Checks.DeletePk;
using Reinforced.Tecture.Features.Orm.Testing.Checks.Update;
using Reinforced.Tecture.Testing.Checks;

namespace Reinforced.Tecture.Features.Orm.Testing.Checks
{
    /// <summary>
    /// ORM checks extensions
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Register all checks for ORM feature
        /// </summary>
        /// <param name="tg">Unit test generator</param>
        public static void CheckOrm(this UnitTestGenerator tg)
        {
            tg.For<Commands.Add.Add>().Basic();
            tg.For<Commands.Delete.Delete>().Basic();
            tg.For<Commands.DeletePk.DeletePk>().Basic();
            tg.For<Commands.Update.Update>().Basic();
        }
    }
}
