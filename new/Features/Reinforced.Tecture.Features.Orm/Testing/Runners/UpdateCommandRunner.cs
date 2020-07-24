using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Features.Orm.Commands.Update;

namespace Reinforced.Tecture.Features.Orm.Testing.Runners
{
   
    class UpdateCommandRunner : CommandRunner<Update>
    {
        
        public UpdateCommandRunner()
        {
        }


        /// <summary>
        /// Runs side effect 
        /// </summary>
        /// <param name="cmd">Side effect</param>
        protected override void Run(Update cmd)
        {
        }

        /// <summary>
        /// Runs side effect asynchronously
        /// </summary>
        /// <param name="cmd">Side effect</param>
        /// <returns>Side effect</returns>
        protected override Task RunAsync(Update cmd)
        {
            Run(cmd);
            return Task.FromResult(0);
        }
    }
}
