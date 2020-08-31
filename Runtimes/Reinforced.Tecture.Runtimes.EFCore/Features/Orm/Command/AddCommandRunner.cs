using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Features.Orm.Commands.Add;
using Reinforced.Tecture.Features.Orm.PrimaryKey;
using Reinforced.Tecture.Query;

namespace Reinforced.Tecture.Runtimes.EFCore.Features.Orm.Command
{
    class AddCommandRunner : CommandRunner<Add>
    {
        private readonly ILazyDisposable<DbContext> _dc;
        private readonly Auxilary _aux;
        public AddCommandRunner(Auxilary aux, ILazyDisposable<DbContext> dc)
        {
            _aux = aux;
            _dc = dc;
        }


        /// <summary>
        /// Runs side effect 
        /// </summary>
        /// <param name="cmd">Side effect</param>
        protected override void Run(Add cmd)
        {
            if (_aux.IsCommandRunNeeded)
            {
                _dc.Value.Add(cmd.Entity);
                var ent = _dc.Value.Entry(cmd.Entity);
                ent.State = EntityState.Added;
            }
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
