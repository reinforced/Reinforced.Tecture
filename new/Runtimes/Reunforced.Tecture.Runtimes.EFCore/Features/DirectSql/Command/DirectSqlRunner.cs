using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Features.SqlStroke.Command;

namespace Reunforced.Tecture.Runtimes.EFCore.Features.DirectSql.Command
{
    class DirectSqlRunner : CommandRunner<Sql>
    {
        /// <summary>
        /// Runs side effect 
        /// </summary>
        /// <param name="cmd">Side effect</param>
        protected override void Run(Sql cmd)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Runs side effect asynchronously
        /// </summary>
        /// <param name="cmd">Side effect</param>
        /// <returns>Side effect</returns>
        protected override Task RunAsync(Sql cmd)
        {
            throw new NotImplementedException();
        }
    }
}
