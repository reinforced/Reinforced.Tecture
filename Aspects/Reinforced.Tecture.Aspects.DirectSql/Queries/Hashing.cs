using Reinforced.Tecture.Aspects.DirectSql.Commands;
using Reinforced.Tecture.Queries;

namespace Reinforced.Tecture.Aspects.DirectSql.Queries
{
    internal static class Hashing
    {
        public static string Hash(this Sql cmd)
        {
            using (var hb = new Hashbox())
            {
                var prev = cmd.Preview;
                hb.Put(prev.Query);
                foreach (var param in prev.Parameters)
                {
                    hb.Put(param);
                }

                return hb.Compute();
            }
        }
    }
}
