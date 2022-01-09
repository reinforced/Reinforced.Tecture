using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Reinforced.Tecture.Aspects.Orm;
using Reinforced.Tecture.Aspects.Orm.Commands.Update;
using Reinforced.Tecture.Aspects.Orm.PrimaryKey;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Queries;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Runtimes.EFCore.Aspects.Orm.Command
{
    class UpdateCommandRunner : CommandRunner<Update>
    {
        private readonly ILazyDisposable<DbContext> _dc;
        private readonly TestingContext _aux;

        public UpdateCommandRunner(TestingContext aux, ILazyDisposable<DbContext> dc)
        {
            _aux = aux;
            _dc = dc;
        }

        private EntityEntry FindEntry(Type t, object instance)
        {
            
            var entryQuery = _dc.Value.ChangeTracker
                .Entries<IPrimaryKey>()
                .FirstOrDefault(x => x.Entity.GetType() == t);

            if (entryQuery == null)
            {
                var eq = _dc.Value.Entry(instance);
                eq.State = EntityState.Modified;
            }
            return entryQuery;
        }

        /// <summary>
        /// Runs side effect 
        /// </summary>
        /// <param name="cmd">Side effect</param>
        protected override void Run(Update cmd)
        {
            if (!_aux.ProvidesTestData)
            {
                var entry = FindEntry(cmd.EntityType, cmd.Entity);
                foreach (var cmdUpdateValue in cmd.UpdateValues)
                {
                    cmdUpdateValue.Key.SetValue(cmd.Entity, cmdUpdateValue.Value);
                    entry.Property(cmdUpdateValue.Key.Name).IsModified = true;
                }
                entry.State = EntityState.Modified;
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
