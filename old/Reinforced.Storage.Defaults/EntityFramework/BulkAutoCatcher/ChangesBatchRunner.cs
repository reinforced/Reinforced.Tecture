using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Storage.SideEffects;

namespace Reinforced.Storage.Defaults.EntityFramework.BulkAutoCatcher
{
    class ChangesBatchRunner : ISideEffectRunner<ChangesBatchSideEffect>
    {
        private readonly DbContext _context;
        private static readonly Dictionary<PropertyInfo, LambdaExpression> _cachedExpressions = new Dictionary<PropertyInfo, LambdaExpression>();

        private static Expression<Func<T, object>> MakeLambda<T>(PropertyInfo pi)
        {
            return (Expression<Func<T, object>>)_cachedExpressions.GetOrCreate(pi, () =>
            {
                var param = Expression.Parameter(typeof(T));
                var propExp = Expression.Property(param, pi);
                var convert = Expression.Convert(propExp, typeof(object));
                return Expression.Lambda(convert, param);
            });
        }
        public void ApplyBatchGeneric<T>(ChangesBatchSideEffect batch) where T : class
        {
            var removed = batch.Removed[typeof(T)];
            if (removed.Count > 0)
            {
                if (!typeof(IEntity).IsAssignableFrom(typeof(T)))
                    throw new Exception("Batch operations are fully supported only for IEntity now");
                var tableName = _context.GetTableName<T>();
                var removals = batch.Removed[typeof(T)].Cast<IEntity>().ToArray();
                if (removals.Any())
                {
                    var ids = string.Join(",", removals.Select(c => c.Id));
                    string sql = String.Format("DELETE FROM {0} WHERE Id IN ({1})", tableName, ids);
                    _context.Database.ExecuteSqlCommand(TransactionalBehavior.EnsureTransaction, sql);
                }
            }

            var additions = batch.Added[typeof(T)].Cast<T>().ToArray();
            var updates = batch.Updated[typeof(T)].Cast<T>().ToArray();

            if (additions.Length > 0)
            {
                //_context.BulkInsert(additions); UNCOMMENT THIS
            }

            if (updates.Length > 0)
            {
                var updatedProps = batch.UpdatedProperties[typeof(T)];
                //_context.BulkInsert(additions); UNCOMMENT THIS
            }

            //context.BulkSaveChanges();

        }
        private static readonly Dictionary<Type, MethodInfo> _cachedGenericMethods = new Dictionary<Type, MethodInfo>();

        public ChangesBatchRunner(DbContext context)
        {
            _context = context;
        }

        private void ApplyBatch(Type entityType, ChangesBatchSideEffect changes)
        {
            var method = _cachedGenericMethods.GetOrCreate(entityType, () =>
            {
                var m = typeof(ChangesBatchRunner).GetMethod("ApplyBatchGeneric",
                    BindingFlags.Public | BindingFlags.Static);
                return m.MakeGenericMethod(entityType);
            });
            method.Invoke(this, new object[] { changes });
        }

        public void ApplyAll(ChangesBatchSideEffect changes)
        {
            
        }

        /// <summary>
        /// Runs side effect 
        /// </summary>
        /// <param name="effect">Side effect</param>
        public void Run(ChangesBatchSideEffect effect)
        {
            foreach (var type in effect.KnownTypes)
            {
                ApplyBatch(type, effect);
            }
        }

        /// <summary>
        /// Runs side effect asynchronously
        /// </summary>
        /// <param name="effect">Side effect</param>
        /// <returns>Side effect</returns>
        public Task RunAsync(ChangesBatchSideEffect effect)
        {
            foreach (var type in effect.KnownTypes)
            {
                ApplyBatch(type, effect);
            }
            return Task.FromResult(0);
        }
    }
}
