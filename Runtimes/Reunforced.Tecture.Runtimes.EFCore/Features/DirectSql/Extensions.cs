using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Entry.Builders;
using Reinforced.Tecture.Runtimes.EFCore.Features.DirectSql.Command;
using Reinforced.Tecture.Runtimes.EFCore.Features.DirectSql.Query;
using Reinforced.Tecture.Runtimes.EFCore.Features.DirectSql.Runtime;

namespace Reinforced.Tecture.Runtimes.EFCore.Features.DirectSql
{
    public static class Extensions
    {
        /// <summary>
        /// Use Direct SQL commands feature implementation based on EntityFramework Core
        /// </summary>
        /// <param name="conf">Channel configuration</param>
        /// <param name="context">Lazy disposable wrapper around DbContext</param>
        public static void UseEfCoreDirectSqlCommand(this ChannelConfiguration<CommandChannel<Tecture.Features.SqlStroke.Command>> conf, LazyDisposable<DbContext> context, InterpolatorFactory fac = null)
        {
            if (fac == null) fac = new InterpolatorFactory();
            var fe = new EFCore_DirectSql_CommandFeature(context, conf.Channel, fac);
            conf.ForCommand(fe, new EFCore_DirectSql_Saver(fe));
        }

        /// <summary>
        /// Use Direct SQL query feature implementation based on EntityFramework Core
        /// </summary>
        /// <param name="conf">Channel configuration</param>
        /// <param name="context">Lazy disposable wrapper around DbContext</param>
        public static void UseEfCoreDirectSqlQuery(this ChannelConfiguration<QueryChannel<Tecture.Features.SqlStroke.Query>> conf, LazyDisposable<DbContext> context, InterpolatorFactory fac = null)
        {
            if (fac == null) fac = new InterpolatorFactory();
            conf.ForQuery(new EFCore_DirectSql_QueryFeature(context, conf.Channel, fac));
        }

        /// <summary>
        /// Use Direct SQL feature implementation based on EntityFramework Core
        /// </summary>
        /// <param name="conf">Channel configuration</param>
        /// <param name="context">Lazy disposable wrapper around DbContext</param>
        public static void UseEfCoreDirectSql(this ChannelConfiguration<CommandQueryChannel<Tecture.Features.SqlStroke.Command, Tecture.Features.SqlStroke.Query>> conf, LazyDisposable<DbContext> context, InterpolatorFactory fac = null)
        {
            if (fac == null) fac = new InterpolatorFactory();
            conf.UseEfCoreDirectSqlCommand(context, fac);
            conf.UseEfCoreDirectSqlQuery(context, fac);
        }
    }
}
