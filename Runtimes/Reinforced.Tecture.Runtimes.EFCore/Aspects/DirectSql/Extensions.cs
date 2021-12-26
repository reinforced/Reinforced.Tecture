using Microsoft.EntityFrameworkCore;
using Reinforced.Tecture.Aspects.Orm;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Entry.Builders;
using Reinforced.Tecture.Runtimes.EFCore.Aspects.DirectSql.Command;
using Reinforced.Tecture.Runtimes.EFCore.Aspects.DirectSql.Query;
using Reinforced.Tecture.Runtimes.EFCore.Aspects.DirectSql.Runtime;
using Reinforced.Tecture.Aspects.DirectSql;

namespace Reinforced.Tecture.Runtimes.EFCore.Aspects.DirectSql
{
    public static class Extensions
    {
        /// <summary>
        /// Use Direct SQL commands aspect implementation based on EntityFramework Core
        /// </summary>
        /// <param name="conf">Channel configuration</param>
        /// <param name="context">Lazy disposable wrapper around DbContext</param>
        public static void UseEfCoreDirectSqlCommand(this ChannelBinding<CommandChannel<Tecture.Aspects.DirectSql.DirectSql.Command>> conf, ILazyDisposable<DbContext> context, InterpolatorFactory fac = null)
        {
            if (fac == null) fac = new InterpolatorFactory();
            var fe = new EFCore_DirectSql_CommandAspect(context, conf.Channel, fac);
            conf.ForCommand(fe);
        }

        /// <summary>
        /// Use Direct SQL query aspect implementation based on EntityFramework Core
        /// </summary>
        /// <param name="conf">Channel configuration</param>
        /// <param name="context">Lazy disposable wrapper around DbContext</param>
        public static void UseEfCoreDirectSqlQuery(this ChannelBinding<QueryChannel<Tecture.Aspects.DirectSql.DirectSql.Query>> conf, ILazyDisposable<DbContext> context, InterpolatorFactory fac = null)
        {
            if (fac == null) fac = new InterpolatorFactory();
            conf.ForQuery(new EFCore_DirectSql_QueryAspect(context, conf.Channel, fac));
        }

        /// <summary>
        /// Use Direct SQL aspect implementation based on EntityFramework Core
        /// </summary>
        /// <param name="conf">Channel configuration</param>
        /// <param name="context">Lazy disposable wrapper around DbContext</param>
        public static void UseEfCoreDirectSql(this ChannelBinding<CommandQueryChannel<Tecture.Aspects.DirectSql.DirectSql.Command, Tecture.Aspects.DirectSql.DirectSql.Query>> conf, 
            ILazyDisposable<DbContext> context, InterpolatorFactory fac = null)
        {
            if (fac == null) fac = new InterpolatorFactory();
            conf.UseEfCoreDirectSqlCommand(context, fac);
            conf.UseEfCoreDirectSqlQuery(context, fac);
        }
    }
}
