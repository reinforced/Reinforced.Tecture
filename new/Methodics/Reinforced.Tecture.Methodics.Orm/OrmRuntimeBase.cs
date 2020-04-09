using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Integrate;
using Reinforced.Tecture.Methodics.Orm.Commands.Add;
using Reinforced.Tecture.Methodics.Orm.Commands.Delete;
using Reinforced.Tecture.Methodics.Orm.Commands.Update;
using Reinforced.Tecture.Methodics.Orm.Queries;
using Reinforced.Tecture.Queries;

namespace Reinforced.Tecture.Methodics.Orm
{
    public abstract class OrmRuntimeBase : ITectureRuntime, IDisposable
    {

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public abstract void Dispose();

        /// <summary>
        /// Override supplies savers set
        /// </summary>
        /// <returns>Savers</returns>
        public abstract ISaver[] GetSavers();

        /// <summary>
        /// Override supplies command runner for particular command
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual ICommandRunner GetRunner(CommandBase command)
        {
            if (command is Add ac) return ProvideAddRunner(ac);
            if (command is Delete dc) return ProvideDeleteRunner(dc);
            if (command is Update uc) return ProvideUpdateRunner(uc);
            return null;
        }

        /// <summary>
        /// Override returns data source supplied by registry
        /// </summary>
        /// <typeparam name="TSource">Type of supported data source</typeparam>
        /// <returns>Data source instance</returns>
        public virtual TSource GetSource<TSource>() where TSource : class, ISource
        {
            if (typeof(OrmSourceBase).IsAssignableFrom(typeof(TSource)))
            {
                var res = (object) ProvideSource(typeof(TSource));
                return (TSource) res;
            }

            return null;
        }

        /// <summary>
        /// Gets whether runtime is in testing mode
        /// </summary>
        public abstract bool Testing { get; }

        protected abstract ICommandRunner<Add> ProvideAddRunner(Add command);
        protected abstract ICommandRunner<Delete> ProvideDeleteRunner(Delete command);
        protected abstract ICommandRunner<Update> ProvideUpdateRunner(Update command);
        protected abstract OrmSourceBase ProvideSource(Type sourceType);
       
    }
}
