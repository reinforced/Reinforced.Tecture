using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Entry.Builders;
using Reinforced.Tecture.Features.SqlStroke.Command;
using Reunforced.Tecture.Runtimes.EFCore.Features.DirectSql.Command;

namespace Reunforced.Tecture.Runtimes.EFCore
{
    public static class CommandExtensions
    {
        public static void UseEfCommand<TDc>(this ChannelConfiguration<CommandChannel<DirectSql>> conf) where TDc:DbContext
        {
            conf.ForCommand(new DirectSqlFeature(null), new DirectSqlSaver());
        }
    }
}
