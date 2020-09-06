using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Entry.Builders;
using Reinforced.Tecture.Runtimes.EFCore.Features.DirectSql.Command;
using Reinforced.Tecture.Runtimes.EFCore.Features.DirectSql.Query;
using Reinforced.Tecture.Runtimes.EFCore.Features.DirectSql.Runtime;
using Reinforced.Tecture.Runtimes.EFCore.Features.Orm.Command;
using Reinforced.Tecture.Runtimes.EFCore.Features.Orm.Query;

namespace Reinforced.Tecture.Runtimes.EFCore.Features.Orm
{
    public static class Extensions
    {
        /// <summary>
        /// Use Direct SQL commands feature implementation based on EntityFramework Core
        /// </summary>
        /// <param name="conf">Channel configuration</param>
        /// <param name="context">Lazy disposable wrapper around DbContext</param>
        public static void UseEfCoreOrmCommand(this ChannelBinding<CommandChannel<Tecture.Features.Orm.Command>> conf, ILazyDisposable<DbContext> context)
        {
            var fe = new EfCore_Orm_CommandFeature(context);
            conf.ForCommand(fe, new EfCore_Orm_Saver(context));
        }

        /// <summary>
        /// Use Direct SQL query feature implementation based on EntityFramework Core
        /// </summary>
        /// <param name="conf">Channel configuration</param>
        /// <param name="context">Lazy disposable wrapper around DbContext</param>
        public static void UseEfCoreOrmQuery(this ChannelBinding<QueryChannel<Tecture.Features.Orm.Query>> conf, ILazyDisposable<DbContext> context)
        {
            conf.ForQuery(new EfCore_Orm_QueryFeature(context));
        }

        /// <summary>
        /// Use Direct SQL feature implementation based on EntityFramework Core
        /// </summary>
        /// <param name="conf">Channel configuration</param>
        /// <param name="context">Lazy disposable wrapper around DbContext</param>
        public static void UseEfCoreOrm(this ChannelBinding<CommandQueryChannel<Tecture.Features.Orm.Command, Tecture.Features.Orm.Query>> conf, ILazyDisposable<DbContext> context)
        {
            conf.UseEfCoreOrmCommand(context);
            conf.UseEfCoreOrmQuery(context);
        }
    }
}
