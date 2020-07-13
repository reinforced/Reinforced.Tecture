using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Entry;
using Reinforced.Tecture.Entry.Builders;
using Reinforced.Tecture.Features.SqlStroke.Query;
using Reinforced.Tecture.Savers;

namespace Reunforced.Tecture.Runtimes.EFCore
{
    public static class QueryExtensions
    {
        public static void UseEFCoreDirectSql<TDbContext>(this ChannelConfiguration<QueryChannel<DirectSql>> conf)
            where TDbContext : DbContext
        {
            conf.ForQuery(null);
        }

        
    }
}
