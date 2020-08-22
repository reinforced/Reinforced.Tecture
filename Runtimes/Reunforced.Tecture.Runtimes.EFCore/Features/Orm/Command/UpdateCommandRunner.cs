using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Features.Orm.Commands.Delete;
using Reinforced.Tecture.Features.Orm.Commands.Update;
using Reinforced.Tecture.Query;

namespace Reinforced.Tecture.Runtimes.EFCore.Features.Orm.Command
{
    class UpdateCommandRunner : CommandRunner<Update>
    {
        private readonly ILazyDisposable<DbContext> _dc;
        private readonly Auxilary _aux;

        public UpdateCommandRunner(Auxilary aux, ILazyDisposable<DbContext> dc)
        {
            _aux = aux;
            _dc = dc;
        }


        /// <summary>
        /// Runs side effect 
        /// </summary>
        /// <param name="cmd">Side effect</param>
        protected override void Run(Update cmd)
        {
            if (_aux.IsCommandRunNeeded)
            {
                _dc.Value.Update(cmd.Entity);
                var ent = _dc.Value.Entry(cmd.Entity);
                ent.State = EntityState.Modified;
            }
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
