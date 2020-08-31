using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Features.Orm.Commands.Delete;
using Reinforced.Tecture.Query;

namespace Reinforced.Tecture.Runtimes.EFCore.Features.Orm.Command
{
    class DeleteCommandRunner : CommandRunner<Delete>
    {
        private readonly ILazyDisposable<DbContext> _dc;
        private readonly Auxilary _aux;

        public DeleteCommandRunner(Auxilary aux, ILazyDisposable<DbContext> dc)
        {
            _aux = aux;
            _dc = dc;
        }


        /// <summary>
        /// Runs side effect 
        /// </summary>
        /// <param name="cmd">Side effect</param>
        protected override void Run(Delete cmd)
        {
            if (_aux.IsCommandRunNeeded)
            {
                _dc.Value.Remove(cmd.Entity);
                var ent = _dc.Value.Entry(cmd.Entity);
                ent.State = EntityState.Deleted;
            }
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
