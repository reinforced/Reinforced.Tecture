using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Methodics.SqlStroke.Commands;

namespace Reinforced.Tecture.Methodics.SqlStroke
{
    class StrokeRunner : ICommandRunner<SqlCommand>
    {
        private SqlStrokeRuntimeBase _runtime;

        public StrokeRunner(SqlStrokeRuntimeBase runtime)
        {
            _runtime = runtime;
        }

        /// <summary>
        /// Runs side effect 
        /// </summary>
        /// <param name="effect">Side effect</param>
        public void Run(SqlCommand effect)
        {
            _runtime.ExecuteSql(effect.Command,effect.Parameters);
        }

        /// <summary>
        /// Runs side effect asynchronously
        /// </summary>
        /// <param name="effect">Side effect</param>
        /// <returns>Side effect</returns>
        public Task RunAsync(SqlCommand effect)
        {
            return _runtime.ExecuteSqlAsync(effect.Command, effect.Parameters);
        }
    }
}
