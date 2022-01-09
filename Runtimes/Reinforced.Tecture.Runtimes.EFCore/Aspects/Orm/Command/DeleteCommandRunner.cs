using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reinforced.Tecture.Aspects.Orm;
using Reinforced.Tecture.Aspects.Orm.Commands.Delete;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Queries;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Runtimes.EFCore.Aspects.Orm.Command
{
    class DeleteCommandRunner : CommandRunner<Delete>
    {
        private readonly ILazyDisposable<DbContext> _dc;
        private readonly TestingContext _aux;

        public DeleteCommandRunner(TestingContext aux, ILazyDisposable<DbContext> dc)
        {
            _aux = aux;
            _dc = dc;
        }



        /// <inheritdoc />
        protected override void Run(Delete cmd)
        {
            if (!_aux.ProvidesTestData)
            {
                _dc.Value.Remove(cmd.Entity);
                var ent = _dc.Value.Entry(cmd.Entity);
                ent.State = EntityState.Deleted;
            }
        }


        /// <inheritdoc />
        protected override Task RunAsync(Delete cmd)
        {
            Run(cmd);
            return Task.FromResult(0);
        }
    }
}
