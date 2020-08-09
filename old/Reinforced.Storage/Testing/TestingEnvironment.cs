using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reinforced.Storage.Adapters;
using Reinforced.Storage.SideEffects;
using Reinforced.Storage.SideEffects.Exact;
using Reinforced.Storage.Testing.Namer;
using Reinforced.Storage.Testing.SideEffects;
using Reinforced.Storage.Testing.Stories;
using Reinforced.Storage.Transactions;
#pragma warning disable 1591

namespace Reinforced.Storage.Testing
{
    public interface ICollectionProvider
    {
        IList<T> GetCollection<T>();
    }

    /// <summary>
    /// Testing environment for iStorage
    /// </summary>
    public partial class TestingEnvironment : ITransactionManager, ISetAccess, ICollectionProvider
    {
        /// <summary>
        /// Additions runner
        /// </summary>
        public AddSideEffectRunner Additions { get; private set; }

        /// <summary>
        /// Updates runner
        /// </summary>
        public UpdateSideEfectRunner Updates { get; private set; }

        /// <summary>
        /// Removals runner
        /// </summary>
        public RemoveSideEfectRunner Removals { get; private set; }

        /// <summary>
        /// Bulk runner
        /// </summary>
        public BulkSideEffectRunner BulkOperations { get; private set; }

        /// <summary>
        /// Async bulk runner
        /// </summary>
        public AsyncBulkSideEffectRunner AsyncBulkOperations { get; private set; }

        /// <summary>
        /// Sql runner
        /// </summary>
        public AssumptionEffectRunner<DirectSqlSideEffect> Sqls { get; private set; }

        /// <summary>
        /// Side effect dispatcher
        /// </summary>
        public SideEffectsDispatcher Dispatcher => _storyDispatcher;
        private readonly StorySideEffectDispatcher _storyDispatcher = new StorySideEffectDispatcher();

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public TestingEnvironment(MapperRepository mapperRepository, bool strict = true)
        {
            _mapperRepository = mapperRepository;
            _strict = strict;
            Additions = new AddSideEffectRunner(this);
            Updates = new UpdateSideEfectRunner(this);
            Removals = new RemoveSideEfectRunner(this);
            Sqls = new AssumptionEffectRunner<DirectSqlSideEffect>(this);
            BulkOperations = new BulkSideEffectRunner(this);
            AsyncBulkOperations = new AsyncBulkSideEffectRunner(this);
            _storyDispatcher.RegisterRunner(Additions);
            _storyDispatcher.RegisterRunner(Removals);
            _storyDispatcher.RegisterRunner(Updates);
            _storyDispatcher.RegisterRunner(Sqls);
            _storyDispatcher.RegisterRunner(BulkOperations);
            _storyDispatcher.RegisterRunner(AsyncBulkOperations);
            _storyDispatcher.RegisterExceptionHandler(HandleSaveChangesException);
        }

        public Storage Create()
        {
            return new Storage(this, _storyDispatcher, this, this, new TestInMemoryCache(), debug: true);
        }

        public StorageStory TellAbout(Storage s)
        {
            _storyDispatcher.BeginStory();
            s.SaveChanges();
            return _storyDispatcher.EndStory(this);
        }

        public async Task<StorageStory> TellAboutAsync(Storage s)
        {
            _storyDispatcher.BeginStory();
            await s.SaveChangesAsync();
            return _storyDispatcher.EndStory(this);
        }

        public virtual IOuterTransaction BeginDbTransaction(OuterTransactionIsolationLevel isolationLevel)
        {
            return new OuterTransactionFake(isolationLevel);
        }

        public virtual IOuterTransaction BeginTransactionScopeTransaction(OuterTransactionIsolationLevel isolationLevel)
        {
            return new OuterTransactionFake(isolationLevel);
        }

        public virtual void HandleSaveChangesException(Exception e)
        {
            throw e;
        }

        public virtual IEnumerable<TEntity> RawQuery<TEntity>(DirectSqlSideEffect sql) where TEntity : class
        {
            throw new Exception($"Please override .RawQuery of TestingEnvironment in order to process query {sql}");
        }
    }
}
