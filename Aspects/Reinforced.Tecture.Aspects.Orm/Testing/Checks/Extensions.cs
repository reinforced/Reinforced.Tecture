using Reinforced.Tecture.Aspects.Orm.Testing.Checks.Add;
using Reinforced.Tecture.Aspects.Orm.Testing.Checks.Delete;
using Reinforced.Tecture.Aspects.Orm.Testing.Checks.DeletePk;
using Reinforced.Tecture.Aspects.Orm.Testing.Checks.Update;
using Reinforced.Tecture.Aspects.Orm.Testing.Checks.UpdatePk;
using Reinforced.Tecture.Testing.Checks;

namespace Reinforced.Tecture.Aspects.Orm.Testing.Checks
{
    /// <summary>
    /// ORM checks extensions
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Register all checks for ORM aspect
        /// </summary>
        /// <param name="tg">Unit test generator</param>
        public static void CheckOrm(this ValidationGenerator tg)
        {
            tg.For<Commands.Add.Add>().Basic();
            tg.For<Commands.Delete.Delete>().Basic();
            tg.For<Commands.DeletePk.DeletePk>().Basic();
            tg.For<Commands.Update.Update>().Basic();
            tg.For<Commands.UpdatePk.UpdatePk>().Basic();
        }
    }
}
