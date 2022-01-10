using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reinforced.Tecture.Aspects.Orm.Commands.Add;
using Reinforced.Tecture.Aspects.Orm.Commands.Delete;
using Reinforced.Tecture.Aspects.Orm.Commands.DeletePk;
using Reinforced.Tecture.Aspects.Orm.Commands.Derelate;
using Reinforced.Tecture.Aspects.Orm.Commands.Relate;
using Reinforced.Tecture.Aspects.Orm.Commands.Update;
using Reinforced.Tecture.Aspects.Orm.Commands.UpdatePk;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Runtimes.EFCore.Aspects.Orm.Command
{
    partial class EfCore_Orm_CommandAspect
    {
        private AddCommandRunner _add;
        private DeleteCommandRunner _del;
        private UpdateCommandRunner _upd;
        private DeletePkCommandRunner _dpk;
        private RelateCommandRunner _rel;
        private DerelateCommandRunner _drel;
        private UpdatePkCommandRunner _upk;

        protected override void OnRegister()
        {
            _add = new AddCommandRunner(Context, _context);
            _del = new DeleteCommandRunner(Context, _context);
            _upd = new UpdateCommandRunner(Context, _context);
            _dpk = new DeletePkCommandRunner(Context, _context);
            _rel = new RelateCommandRunner(Context, _context);
            _drel = new DerelateCommandRunner(Context, _context);
            _upk = new UpdatePkCommandRunner(Context, _context);
        }

        /// <inheritdoc />
        protected override void Save()
        {
            if (!Context.ProvidesTestData)
            {
                _context.Value.ChangeTracker.DetectChanges();
                _context.Value.SaveChanges();

                var changedEntriesCopy = _context.Value.ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Added ||
                                e.State == EntityState.Modified ||
                                e.State == EntityState.Unchanged ||
                                e.State == EntityState.Deleted)
                    .ToList();

                foreach (var entry in changedEntriesCopy)
                    entry.State = EntityState.Detached;
            }
        }

        /// <inheritdoc />
        protected override Task SaveAsync(CancellationToken token = default)
        {
            if (!Context.ProvidesTestData)
            {
                return _context.Value.SaveChangesAsync(token);
            }

            return Task.FromResult(0);
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            if (!Context.ProvidesTestData)
            {
                _context.Dispose();
            }
        }

        /// <inheritdoc />
        protected override CommandRunner<Add> GetRunner1(Add command) => _add;

        /// <inheritdoc />
        protected override CommandRunner<Delete> GetRunner2(Delete command) => _del;

        /// <inheritdoc />
        protected override CommandRunner<Update> GetRunner3(Update command) => _upd;

        /// <inheritdoc />
        protected override CommandRunner<Relate> GetRunner4(Relate command) => _rel;

        /// <inheritdoc />
        protected override CommandRunner<Derelate> GetRunner5(Derelate command) => _drel;

        protected override CommandRunner<DeletePk> GetRunner6(DeletePk command) => _dpk;

        /// <inheritdoc />
        protected override CommandRunner<UpdatePk> GetRunner7(UpdatePk command) => _upk;
    }
}