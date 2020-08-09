using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Features.Orm.Commands.Add;

namespace Reinforced.Tecture.Features.Orm.Testing.Runners
{
    class AddCommandRunner : CommandRunner<Add>
    {
        
        public AddCommandRunner()
        {
        }

        /// <summary>
        /// Runs side effect 
        /// </summary>
        /// <param name="cmd">Side effect</param>
        protected override void Run(Add cmd)
        {
            
            
        }

        /// <summary>
        /// Runs side effect asynchronously
        /// </summary>
        /// <param name="cmd">Side effect</param>
        /// <returns>Side effect</returns>
        protected override Task RunAsync(Add cmd)
        {
            Run(cmd);
            return Task.FromResult(0);
        }
    }
}
