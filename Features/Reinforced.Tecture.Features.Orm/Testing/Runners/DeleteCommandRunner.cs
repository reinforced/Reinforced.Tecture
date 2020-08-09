using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Features.Orm.Commands.Delete;

namespace Reinforced.Tecture.Features.Orm.Testing.Runners
{
    class DeleteCommandRunner : CommandRunner<Delete>
    {
        public DeleteCommandRunner()
        {
            
        }

        /// <summary>
        /// Runs side effect 
        /// </summary>
        /// <param name="cmd">Side effect</param>
        protected override void Run(Delete cmd)
        {
            
        }

        /// <summary>
        /// Runs side effect asynchronously
        /// </summary>
        /// <param name="cmd">Side effect</param>
        /// <returns>Side effect</returns>
        protected override Task RunAsync(Delete cmd)
        {
            Run(cmd);
            return Task.FromResult(0);
        }
    }
}
