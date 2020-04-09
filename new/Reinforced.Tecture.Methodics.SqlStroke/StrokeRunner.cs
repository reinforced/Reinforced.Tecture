using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Methodics.SqlStroke.Commands;

namespace Reinforced.Tecture.Methodics.SqlStroke
{
    class StrokeRunner : ICommandRunner<Sql>
    {
        private readonly SqlStrokeRuntimeBase _runtime;

        public StrokeRunner(SqlStrokeRuntimeBase runtime)
        {
            _runtime = runtime;
        }

        /// <summary>
        /// Runs side effect 
        /// </summary>
        /// <param name="cmd">Side effect</param>
        public void Run(Sql cmd)
        {
            _runtime.ExecuteSql(cmd.Command,cmd.Parameters);
        }

        /// <summary>
        /// Runs side effect asynchronously
        /// </summary>
        /// <param name="cmd">Side effect</param>
        /// <returns>Side effect</returns>
        public Task RunAsync(Sql cmd)
        {
            return _runtime.ExecuteSqlAsync(cmd.Command, cmd.Parameters);
        }
    }
}
