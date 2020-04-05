using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Queries;

namespace Reinforced.Tecture.Integrate
{
    /// <summary>
    /// Tecture runtime
    /// </summary>
    public interface ITectureRuntime : IDisposable
    {
        /// <summary>
        /// Override supplies savers set
        /// </summary>
        /// <returns>Savers</returns>
        ISaver[] GetSavers();

        /// <summary>
        /// Override supplies command runner for particular command
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        ICommandRunner GetRunner(CommandBase command);

        /// <summary>
        /// Override returns data source supplied by registry
        /// </summary>
        /// <typeparam name="TSource">Type of supported data source</typeparam>
        /// <returns>Data source instance</returns>
        TSource GetSource<TSource>() where TSource : class, ISource;
    }
}
