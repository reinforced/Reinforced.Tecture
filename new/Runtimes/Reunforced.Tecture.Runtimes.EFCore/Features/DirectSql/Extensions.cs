using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Entry.Builders;
using Reinforced.Tecture.Runtimes.EFCore.Features.DirectSql.Command;
using Reinforced.Tecture.Runtimes.EFCore.Features.DirectSql.Query;

namespace Reinforced.Tecture.Runtimes.EFCore.Features.DirectSql
{
    public static class Extensions
    {
        /// <summary>
        /// Use Direct SQL commands feature implementation based on EntityFramework Core
        /// </summary>
        /// <param name="conf">Channel configuration</param>
        /// <param name="context">Lazy disposable wrapper around DbContext</param>
        public static void UseEfCoreDirectSqlCommand(this ChannelConfiguration<CommandChannel<Tecture.Features.SqlStroke.Command>> conf, LazyDisposable<DbContext> context)
        {
            conf.ForCommand(new EFCore_DirectSql_CommandFeature(context, conf.Channel), new DirectSqlSaver(context));
        }

        /// <summary>
        /// Use Direct SQL query feature implementation based on EntityFramework Core
        /// </summary>
        /// <param name="conf">Channel configuration</param>
        /// <param name="context">Lazy disposable wrapper around DbContext</param>
        public static void UseEfCoreDirectSqlQuery(this ChannelConfiguration<QueryChannel<Tecture.Features.SqlStroke.Query>> conf, LazyDisposable<DbContext> context)
        {
            conf.ForQuery(new EFCore_DirectSql_QueryFeature(context, conf.Channel));
        }

        /// <summary>
        /// Use Direct SQL feature implementation based on EntityFramework Core
        /// </summary>
        /// <param name="conf">Channel configuration</param>
        /// <param name="context">Lazy disposable wrapper around DbContext</param>
        public static void UseEfCoreDirectSql(this ChannelConfiguration<CommandQueryChannel<Tecture.Features.SqlStroke.Command,Tecture.Features.SqlStroke.Query>> conf, LazyDisposable<DbContext> context)
        {
            conf.UseEfCoreDirectSqlCommand(context);
            conf.UseEfCoreDirectSqlQuery(context);
        }
    }
}
