using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Features.Orm.Commands.Derelate;

namespace Reinforced.Tecture.Runtimes.EFCore.Features.Orm.Command
{
    class DerelateCommandRunner : CommandRunner<Derelate>
    {
        /// <summary>
        /// Runs side effect 
        /// </summary>
        /// <param name="cmd">Side effect</param>
        protected override void Run(Derelate cmd)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Runs side effect asynchronously
        /// </summary>
        /// <param name="cmd">Side effect</param>
        /// <returns>Side effect</returns>
        protected override Task RunAsync(Derelate cmd)
        {
            Run(cmd);
            return Task.FromResult(0);
        }
    }
}
