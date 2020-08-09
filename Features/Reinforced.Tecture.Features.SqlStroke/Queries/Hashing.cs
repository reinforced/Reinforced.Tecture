using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Features.SqlStroke.Commands;
using Reinforced.Tecture.Testing.Query;

namespace Reinforced.Tecture.Features.SqlStroke.Queries
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
