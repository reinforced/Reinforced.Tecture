using System.Threading.Tasks;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Features.SqlStroke.Commands;

namespace Reinforced.Tecture.Features.SqlStroke
{
    class StrokeRunner : CommandRunner<Sql>
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
        protected override void Run(Sql cmd)
        {
            _runtime.ExecuteSql(cmd.Command,cmd.Parameters);
        }

        /// <summary>
        /// Runs side effect asynchronously
        /// </summary>
        /// <param name="cmd">Side effect</param>
        /// <returns>Side effect</returns>
        protected override Task RunAsync(Sql cmd)
        {
            return _runtime.ExecuteSqlAsync(cmd.Command, cmd.Parameters);
        }
    }
}
