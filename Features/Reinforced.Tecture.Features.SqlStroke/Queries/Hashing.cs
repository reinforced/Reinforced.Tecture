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
                hb.Put(cmd.Command);
                foreach (var param in cmd.Parameters)
                {
                    hb.Put(param);
                }

                return hb.Compute();
            }
        }
    }
}
