using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Integrate;
using Reinforced.Tecture.Queries;

namespace Reinforced.Tecture.Methodics.SqlStroke
{
    public abstract class SqlStrokeRuntimeBase: ITectureRuntime
    {
        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public abstract void Dispose();

        private static readonly ISaver[] _empty = new ISaver[0];
        public ISaver[] GetSavers() => _empty;
        public TSource GetSource<TSource>() where TSource : class, ISource => null;

        /// <summary>
        /// Override supplies command runner for particular command
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public ICommandRunner GetRunner(CommandBase command)
        {
            return new StrokeRunner(this);
        }

        public abstract IMapp
        public abstract void ExecuteSql(string command, object[] parameters);
        public abstract Task ExecuteSqlAsync(string command, object[] parameters);
    }
}
