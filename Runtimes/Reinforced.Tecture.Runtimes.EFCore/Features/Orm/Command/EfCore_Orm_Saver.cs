using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Features.Orm.Commands.Add;
using Reinforced.Tecture.Features.Orm.Commands.Delete;
using Reinforced.Tecture.Features.Orm.Commands.DeletePk;
using Reinforced.Tecture.Features.Orm.Commands.Derelate;
using Reinforced.Tecture.Features.Orm.Commands.Relate;
using Reinforced.Tecture.Features.Orm.Commands.Update;
using Reinforced.Tecture.Savers;

namespace Reinforced.Tecture.Runtimes.EFCore.Features.Orm.Command
{
    class EfCore_Orm_Saver : Saver<Add, Delete, Update, Relate, Derelate, DeletePk>
    {
        private readonly ILazyDisposable<DbContext> _dc;
        private AddCommandRunner _add;
        private DeleteCommandRunner _del;
        private UpdateCommandRunner _upd;
        private DeletePkCommandRunner _dpk;
        private RelateCommandRunner _rel;
        private DerelateCommandRunner _drel;

        public EfCore_Orm_Saver(ILazyDisposable<DbContext> dc)
        {
            _dc = dc;
        }

        protected override void OnRegister()
        {
            _add = new AddCommandRunner(Aux, _dc);
            _del = new DeleteCommandRunner(Aux, _dc);
            _upd = new UpdateCommandRunner(Aux, _dc);
            _dpk = new DeletePkCommandRunner(Aux, _dc);
            _rel = new RelateCommandRunner(Aux, _dc);
            _drel = new DerelateCommandRunner(Aux, _dc);
        }


        /// <inheritdoc />
        protected override void Save()
        {
            if (Aux.IsSavingNeeded)
            {
                _dc.Value.SaveChanges();

                //var changedEntriesCopy = _dc.Value.ChangeTracker.Entries()
                //    .Where(e => e.State == EntityState.Added ||
                //                e.State == EntityState.Modified ||
                //                e.State == EntityState.Deleted)
                //    .ToList();

                //foreach (var entry in changedEntriesCopy)
                //    entry.State = EntityState.Detached;
            }
        }

        /// <inheritdoc />
        protected override Task SaveAsync()
        {
            if (Aux.IsSavingNeeded)
            {
                return _dc.Value.SaveChangesAsync();
            }
            return Task.FromResult(0);
        }


        /// <inheritdoc />
        public override void Dispose()
        {
            if (Aux.IsSavingNeeded)
            {
                _dc.Dispose();
            }
        }


        /// <inheritdoc />
        protected override CommandRunner<Add> GetRunner1(Add command)
        {
            return _add;
        }


        /// <inheritdoc />
        protected override CommandRunner<Delete> GetRunner2(Delete command)
        {
            return _del;
        }


        /// <inheritdoc />
        protected override CommandRunner<Update> GetRunner3(Update command)
        {
            return _upd;
        }


        /// <inheritdoc />
        protected override CommandRunner<Relate> GetRunner4(Relate command)
        {
            return _rel;
        }


        /// <inheritdoc />
        protected override CommandRunner<Derelate> GetRunner5(Derelate command)
        {
            return _drel;
        }

        protected override CommandRunner<DeletePk> GetRunner6(DeletePk command)
        {
            return _dpk;
        }
    }
}
