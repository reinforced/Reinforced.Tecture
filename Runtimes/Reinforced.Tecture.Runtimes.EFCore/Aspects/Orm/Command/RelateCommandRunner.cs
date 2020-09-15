using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reinforced.Tecture.Aspects.Orm.Commands.Relate;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Query;

namespace Reinforced.Tecture.Runtimes.EFCore.Aspects.Orm.Command
{
    class RelateCommandRunner : CommandRunner<Relate>
    {
        private readonly ILazyDisposable<DbContext> _dc;
        private readonly Auxiliary _aux;

        public RelateCommandRunner(Auxiliary aux, ILazyDisposable<DbContext> dc)
        {
            _dc = dc;
            _aux = aux;
        }

        /// <summary>
        /// Runs side effect 
        /// </summary>
        /// <param name="cmd">Side effect</param>
        protected override void Run(Relate cmd)
        {
            var prop = cmd.PrimaryType.GetProperty(cmd.ForeignKeySpecifier,
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty);
            prop.SetValue(cmd.Primary,cmd.Secondary);
            if (_aux.IsCommandRunNeeded)
            {
                var ent = _dc.Value.Entry(cmd.Primary);
                ent.DetectChanges();
            }
            
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
