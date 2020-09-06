using Reinforced.Tecture.Features.SqlStroke.Commands;
using Reinforced.Tecture.Features.SqlStroke.Testing.Checks;
using Reinforced.Tecture.Testing.Checks;

namespace Reinforced.Tecture.Features.SqlStroke.Testing
{
    /// <summary>
    /// SQL checks description
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// SQL command text check
        /// </summary>
        /// <param name="ck">Checks builder</param>
        /// <returns>Fluent</returns>
        public static ChecksBuilderFor<Sql> Text(this ChecksBuilderFor<Sql> ck)
        {
            ck.Enlist(new SqlCommandTextCheckDescription());
            return ck;
        }

        /// <summary>
        /// SQL command parameters check
        /// </summary>
        /// <param name="ck">Checks builder</param>
        /// <returns>Fluent</returns>
        public static ChecksBuilderFor<Sql> Parameters(this ChecksBuilderFor<Sql> ck)
        {
            ck.Enlist(new SqlCommandParmatersCheckDescription());
            return ck;
        }

        /// <summary>
        /// Checks set for SQL commands
        /// </summary>
        /// <param name="tg">Unit test generator</param>
        public static void CheckSql(this UnitTestGenerator tg)
        {
            var fr = tg.For<Sql>();
            fr.Text();
            fr.Parameters();
        }
    }
}
