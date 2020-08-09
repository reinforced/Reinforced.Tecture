using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Storage.Adapters;
using Reinforced.Storage.Defaults.EntityFramework.BulkAutoCatcher;
using Reinforced.Storage.Defaults.EntityFramework.SideEffectRunners;
using Reinforced.Storage.SideEffects;
using Reinforced.Storage.SideEffects.Exact;
using Reinforced.Storage.Transactions;
using EntityState = System.Data.Entity.EntityState;

namespace Reinforced.Storage.Defaults.EntityFramework
{
    public partial class EntityFrameworkDbContextAdapter : IMapper, 
        ISetAccess, 
        ITransactionManager, ISideEffectSaver
    {
        public Storage CreateStorage(OuterTransactionMode? saveChangesTransactionMode = null,
            OuterTransactionIsolationLevel? saveChangesTransactionIsolation = null)
        {
            var ed = new SideEffectsDispatcher();
            ed.RegisterExceptionHandler(this.HandleSaveChangesException);
            ed.RegisterSaver(this);
            var baseRunner = new EfSideEffectRunner(_dataContext);

            ed.RegisterRunner<AddSideEffect>(baseRunner);
            ed.RegisterRunner<RemoveSideEffect>(baseRunner);
            ed.RegisterRunner<UpdateSideEffect>(baseRunner);
            ed.RegisterRunner<DirectSqlSideEffect>(baseRunner);

            var bulkRunner = new EfBulkSideEffectRunner(_dataContext);
            ed.RegisterRunner(bulkRunner);

            var bulkAsyncRunner = new EfAsyncBulkSideEffectRunner(_dataContext);
            ed.RegisterRunner(bulkAsyncRunner);
            ed.RegisterRunner(new ChangesBatchRunner(_dataContext));

            return new Storage(this, ed, this, this, null, saveChangesTransactionMode, saveChangesTransactionIsolation);
        }

        private readonly DbContext _dataContext;

        public EntityFrameworkDbContextAdapter(DbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public DbContext DataContext
        {
            get { return _dataContext; }
        }

        public IQueryable<TEntity> Set<TEntity>() where TEntity : class
        {
            return _dataContext.Set<TEntity>();
        }

        
        public IOuterTransaction BeginDbTransaction(OuterTransactionIsolationLevel isolationLevel)
        {
            return OuterDbTransactionAdapter.Create(_dataContext, isolationLevel);
        }

        public IOuterTransaction BeginTransactionScopeTransaction(OuterTransactionIsolationLevel isolationLevel)
        {
            return OuterTransactionScopeAdapter.Create(_dataContext, isolationLevel);
        }

        public void Save()
        {
            _dataContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _dataContext.SaveChangesAsync();
        }

       public IQueryable<TEntity> NoTracking<TEntity>(IQueryable<TEntity> q) where TEntity : class
        {
            return q.AsNoTracking();
        }

        public IQueryable<TEntity> Include<TEntity>(IQueryable<TEntity> q, Expression includeExpression) where TEntity : class
        {
            return EfReflectionCache.ApplyInclude(q, includeExpression);
        }

        
        public void HandleSaveChangesException(Exception e)
        {
            if (e is DbUpdateException)
            {
                HandleDbUpdateException((DbUpdateException)e);
            }

            if (e is DbEntityValidationException)
            {
                HandleDbValidationException((DbEntityValidationException)e);
            }
            throw e;
        }

        public IEnumerable<T> RawQuery<T>(DirectSqlSideEffect sql) where T : class
        {
            return _dataContext.Database.SqlQuery<T>(sql.Command, sql.Parameters);
        }


        private void HandleDbValidationException(DbEntityValidationException ex)
        {
            var sb = new StringBuilder();

            foreach (var failure in ex.EntityValidationErrors)
            {
                sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                foreach (var error in failure.ValidationErrors)
                {
                    sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                    sb.AppendLine();
                }
            }

            throw new DbEntityValidationException(
                "Entity Validation Failed - errors follow:\n" +
                sb.ToString(), ex
                ); // Add the original exception as the innerException
        }


        private void HandleDbUpdateException(DbUpdateException ex)
        {
            var sb = new StringBuilder();

            foreach (var failure in ex.Entries)
            {
                sb.AppendFormat("{0} failed update\n", failure.Entity.GetType());
            }

            throw new DbUpdateException(
                "Entity Update Failed - errors follow:\n" +
                sb.ToString(), ex
                ); // Add the original exception as the innerException
        }

    }
}
