using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Integrate;
using Reinforced.Tecture.Queries;

namespace Reinforced.Tecture.Entry
{
    interface IMultiplexer
    {
        IEnumerable<TRuntime> GetRuntimes<TRuntime>(Func<TRuntime, bool> predicate = null) where TRuntime : ITectureRuntime;
        /// <summary>
        /// Override returns data source supplied by registry
        /// </summary>
        /// <typeparam name="TSource">Type of supported data source</typeparam>
        /// <returns>Data source instance</returns>
        TSource GetSource<TSource>() where TSource : class, ISource;

        ISaver[] GetSavers();
        ICommandRunner GetRunner(CommandBase command);
    }
}
