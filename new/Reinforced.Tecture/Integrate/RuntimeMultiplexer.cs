using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Queries;

namespace Reinforced.Tecture.Integrate
{
    /// <summary>
    /// Multiplexes several runtime into one interface
    /// </summary>
    class RuntimeMultiplexer : IRuntimeLocator //: ITectureRuntime just to let you know
    {
        private readonly List<ITectureRuntime> _runtimes = new List<ITectureRuntime>();

        public IEnumerable<TRuntime> GetRuntimes<TRuntime>(Func<TRuntime, bool> predicate) where TRuntime : ITectureRuntime
        {
            return _runtimes
                .OfType<TRuntime>()
                .Where(predicate);
        }

        public void AddRuntime(ITectureRuntime runtime)
        {
            if (runtime==null)
                throw new ArgumentNullException("runtime");
            _runtimes.Add(runtime);
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            foreach (var tectureRuntime in _runtimes)
            {
                tectureRuntime.Dispose();
            }
        }

        /// <summary>
        /// Override supplies savers set
        /// </summary>
        /// <returns>Savers</returns>
        public ISaver[] GetSavers()
        {
            return _runtimes.SelectMany(x => x.GetSavers()).ToArray();
        }

        /// <summary>
        /// Override supplies command runner for particular command
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public ICommandRunner GetRunner(CommandBase command)
        {
            foreach (var tectureRuntime in _runtimes)
            {
                var r = tectureRuntime.GetRunner(command);
                if (r != null) return r;
            }

            return null;
        }

        /// <summary>
        /// Override returns data source supplied by registry
        /// </summary>
        /// <typeparam name="TSource">Type of supported data source</typeparam>
        /// <returns>Data source instance</returns>
        public TSource GetSource<TSource>() where TSource : class, ISource
        {
            foreach (var tectureRuntime in _runtimes)
            {
                var src = tectureRuntime.GetSource<TSource>();
                if (src != null) return src;
            }

            return null;
        }
    }
}
