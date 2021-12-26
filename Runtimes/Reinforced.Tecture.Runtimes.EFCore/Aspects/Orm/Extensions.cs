using Microsoft.EntityFrameworkCore;
using Reinforced.Tecture.Aspects.Orm;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Entry.Builders;
using Reinforced.Tecture.Runtimes.EFCore.Aspects.Orm.Command;
using Reinforced.Tecture.Runtimes.EFCore.Aspects.Orm.Query;

namespace Reinforced.Tecture.Runtimes.EFCore.Aspects.Orm
{
    public static class Extensions
    {
        /// <summary>
        /// Use Direct SQL commands aspect implementation based on EntityFramework Core
        /// </summary>
        /// <param name="conf">Channel configuration</param>
        /// <param name="context">Lazy disposable wrapper around DbContext</param>
        public static void UseEfCoreOrmCommand(this ChannelBinding<CommandChannel<Reinforced.Tecture.Aspects.Orm.Orm.Command>> conf, ILazyDisposable<DbContext> context)
        {
            var fe = new EfCore_Orm_CommandAspect(context);
            conf.ForCommand(fe);
        }

        /// <summary>
        /// Use Direct SQL query aspect implementation based on EntityFramework Core
        /// </summary>
        /// <param name="conf">Channel configuration</param>
        /// <param name="context">Lazy disposable wrapper around DbContext</param>
        public static void UseEfCoreOrmQuery(this ChannelBinding<QueryChannel<Reinforced.Tecture.Aspects.Orm.Orm.Query>> conf, ILazyDisposable<DbContext> context)
        {
            conf.ForQuery(new EfCore_Orm_QueryAspect(context));
        }

        /// <summary>
        /// Use Direct SQL aspect implementation based on EntityFramework Core
        /// </summary>
        /// <param name="conf">Channel configuration</param>
        /// <param name="context">Lazy disposable wrapper around DbContext</param>
        public static void UseEfCoreOrm(this ChannelBinding<CommandQueryChannel<Reinforced.Tecture.Aspects.Orm.Orm.Command, Reinforced.Tecture.Aspects.Orm.Orm.Query>> conf, ILazyDisposable<DbContext> context)
        {
            conf.UseEfCoreOrmCommand(context);
            conf.UseEfCoreOrmQuery(context);
        }
    }
}
