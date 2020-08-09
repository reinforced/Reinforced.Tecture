using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Storage.SideEffects;
using Reinforced.Storage.SideEffects.Exact;

namespace Reinforced.Storage.Defaults.EntityFramework.SideEffectRunners
{
    public partial class EfSideEffectRunner : 
        ISideEffectRunner<AddSideEffect>, 
        ISideEffectRunner<RemoveSideEffect>,
        ISideEffectRunner<UpdateSideEffect>,
        ISideEffectRunner<DirectSqlSideEffect>
    {
        private readonly DbContext _context;

        public EfSideEffectRunner(DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Runs side effect 
        /// </summary>
        /// <param name="effect">Side effect</param>
        public void Run(AddSideEffect effect)
        {
            var set = _context.Set(effect.EntityType);
            set.Add(effect.Entity);
        }

        /// <summary>
        /// Runs side effect asynchronously
        /// </summary>
        /// <param name="effect">Side effect</param>
        /// <returns>Side effect</returns>
        public Task RunAsync(AddSideEffect effect)
        {
            Run(effect);
            return Task.Delay(0);
        }

        /// <summary>
        /// Runs side effect 
        /// </summary>
        /// <param name="effect">Side effect</param>
        public void Run(RemoveSideEffect effect)
        {
            var set = _context.Set(effect.EntityType);
            set.Remove(effect.Entity);
        }

        /// <summary>
        /// Runs side effect asynchronously
        /// </summary>
        /// <param name="effect">Side effect</param>
        /// <returns>Side effect</returns>
        public Task RunAsync(RemoveSideEffect effect)
        {
            Run(effect);
            return  Task.Delay(0);
        }

        /// <summary>
        /// Runs side effect 
        /// </summary>
        /// <param name="effect">Side effect</param>
        public void Run(UpdateSideEffect effect)
        {
            var entity = effect.Entity;
            if (entity == null) return;
            var e = _context.Entry(entity);
            if (e.State == EntityState.Added) return;
            var set = _context.Set(effect.EntityType);
            if (e.State == EntityState.Detached)
            {
                var ent = entity as IEntity;
                if (ent != null && ent.Id == 0) set.Add(entity);
                else
                {
                    set.Attach(entity);
                    e.State = EntityState.Modified;
                }
            }
            else
            {
                e.State = EntityState.Modified;
            }

        }

        /// <summary>
        /// Runs side effect asynchronously
        /// </summary>
        /// <param name="effect">Side effect</param>
        /// <returns>Side effect</returns>
        public Task RunAsync(UpdateSideEffect effect)
        {
            Run(effect);
            return Task.Delay(0);
        }

        /// <summary>
        /// Runs side effect 
        /// </summary>
        /// <param name="effect">Side effect</param>
        public void Run(DirectSqlSideEffect effect)
        {
            _context.Database.ExecuteSqlCommand(effect.Command, effect.Parameters);
        }

        /// <summary>
        /// Runs side effect asynchronously
        /// </summary>
        /// <param name="effect">Side effect</param>
        /// <returns>Side effect</returns>
        public async Task RunAsync(DirectSqlSideEffect effect)
        {
            await _context.Database.ExecuteSqlCommandAsync(effect.Command, effect.Parameters);
        }
    }
}
