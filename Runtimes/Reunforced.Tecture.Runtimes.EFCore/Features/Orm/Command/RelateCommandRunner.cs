using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Features.Orm.Commands.Relate;

namespace Reinforced.Tecture.Runtimes.EFCore.Features.Orm.Command
{
    class RelateCommandRunner : CommandRunner<Relate>
    {
        /// <summary>
        /// Runs side effect 
        /// </summary>
        /// <param name="cmd">Side effect</param>
        protected override void Run(Relate cmd)
        {
            var prop = cmd.PrimaryType.GetProperty(cmd.ForeignKeySpecifier,
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty);
            prop.SetValue(cmd.Primary,cmd.Secondary);
        }

        /// <summary>
        /// Runs side effect asynchronously
        /// </summary>
        /// <param name="cmd">Side effect</param>
        /// <returns>Side effect</returns>
        protected override Task RunAsync(Relate cmd)
        {
            Run(cmd);
            return Task.FromResult(0);
        }
    }
}
