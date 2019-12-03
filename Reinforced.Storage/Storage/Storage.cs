using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Reinforced.Storage.Adapters;
using Reinforced.Storage.QueryBuilders;
using Reinforced.Storage.Services;
using Reinforced.Storage.SideEffects;
using Reinforced.Storage.SideEffects.Exact;
using Reinforced.Storage.Strokes;
using Reinforced.Storage.Transactions;

namespace Reinforced.Storage
{
    public partial class Storage : IMasterStorage
    {
        private readonly OuterTransactionMode? _saveChangesTransactionMode;
        private readonly OuterTransactionIsolationLevel? _saveChangesTransactionIsolation;

        private readonly IStorageCache _cache;
        private readonly StorageStats _stats;
        private readonly ITransactionManager _tranManager;
        
        private readonly ActionsQueue _postSaveActions;
        private readonly ActionsQueue _finallyActions;
        private readonly StrokeProcessor _stroke;
        private readonly ServiceManager _serviceManager;
        private readonly SetManager _setManager;

        internal readonly Effects _effectsQueue;
        private readonly SideEffectsDispatcher _dispatcher;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public Storage(
            ISetAccess sets,
            SideEffectsDispatcher dispatcher,
            ITransactionManager tranManager,
            IMapper mapper,
            IStorageCache cache = null, OuterTransactionMode? saveChangesTransactionMode = null, OuterTransactionIsolationLevel? saveChangesTransactionIsolation = null,
            bool debug = false)
        {
            _saveChangesTransactionMode = saveChangesTransactionMode;
            _saveChangesTransactionIsolation = saveChangesTransactionIsolation;
            _cache = cache;
            _tranManager = tranManager;
            _dispatcher = dispatcher;
            _effectsQueue = new Effects(debug);

            _stroke = new StrokeProcessor(mapper);
            _postSaveActions = new ActionsQueue(true);
            _finallyActions = new ActionsQueue(false);
            _stats = new StorageStats();

            _setManager = new SetManager(sets, new StorageStats());
            _serviceManager = new ServiceManager(_stroke, _finallyActions, _postSaveActions, _setManager,_effectsQueue);
            _postSaveActions._serviceManager = _serviceManager;
            _finallyActions._serviceManager = _serviceManager;
        }



        /// <summary>
        /// Saves changes to database
        /// </summary>
        public void SaveChanges()
        {
            if (_postSaveActions.IsRunning) throw new Exception(".SaveChanges cannot be called within post-save action");
            if (_finallyActions.IsRunning) throw new Exception(".SaveChanges cannot be called within finally action");
            IOuterTransaction tran = null;

            Exception thrown = null;
            try
            {
                if (_saveChangesTransactionMode.HasValue && _saveChangesTransactionIsolation.HasValue)
                {
                    tran = BeginOuterTransaction(_saveChangesTransactionMode.Value,
                        _saveChangesTransactionIsolation.Value);
                }

                _serviceManager.OnSave();

                _dispatcher.Dispatch(_effectsQueue,_postSaveActions);

                _serviceManager.OnFinally();
                _finallyActions.Run();

                if (tran != null) tran.Commit();
            }
            catch (Exception ex)
            {
                if (_dispatcher._exHandler!=null) _dispatcher._exHandler(ex);
                thrown = ex;
            }
            finally
            {
                try
                {
                    if (tran != null) tran.Dispose();
                }
                catch (Exception)
                {
                    if (thrown == null) throw;
                }
                if (thrown != null) throw thrown;
            }
        }

        /// <summary>
        /// Returns statistics of queries usage
        /// </summary>
        /// <returns></returns>
        public string Stats()
        {
            return _stats.Stats();
        }

        /// <summary>
        /// Saves changes to database (async version)
        /// </summary>
        /// <returns></returns>
        public async Task SaveChangesAsync()
        {
            if (_postSaveActions.IsRunning) throw new Exception(".SaveChangesAsync cannot be called within post-save action");
            if (_finallyActions.IsRunning) throw new Exception(".SaveChangesAsync cannot be called within finally action");
            IOuterTransaction tran = null;
            Exception thrown = null;
            try
            {
                if (_saveChangesTransactionMode.HasValue && _saveChangesTransactionIsolation.HasValue)
                {
                    tran = BeginOuterTransaction(_saveChangesTransactionMode.Value,
                        _saveChangesTransactionIsolation.Value);
                }

                _serviceManager.OnSave();

                await _dispatcher.DispatchAsync(_effectsQueue, _postSaveActions);

                _serviceManager.OnFinally();
                await _finallyActions.RunAsync();

                if (tran != null) tran.Commit();
                //CleanupAfterSave();
            }
            catch (Exception ex)
            {
                if (_dispatcher._exHandler != null) _dispatcher._exHandler(ex);
                thrown = ex;
            }
            finally
            {
                try
                {
                    if (tran != null) tran.Dispose();
                }
                catch (Exception)
                {
                    if (thrown == null) throw;
                }
                if (thrown != null) throw thrown;
            }
        }

        /// <summary>
        /// Begins outer transaction
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="isolationLevel"></param>
        /// <returns></returns>
        public IOuterTransaction BeginOuterTransaction(OuterTransactionMode mode,
            OuterTransactionIsolationLevel isolationLevel = OuterTransactionIsolationLevel.ReadUncommitted)
        {
            switch (mode)
            {
                case OuterTransactionMode.DbTransaction:
                    return _tranManager.BeginDbTransaction(isolationLevel);
                case OuterTransactionMode.TransactionScope:
                    return _tranManager.BeginTransactionScopeTransaction(isolationLevel);
            }
            throw new Exception("Unknown transaction type");
        }

        /// <summary>
        /// Query storage interface
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <returns>Query builder</returns>
        public IQueryFor<T> Get<T>() where T : class
        {
            return _setManager.Get<T>();
        }

        /// <summary>
        /// Retrieves cache manager for specified entity. 
        /// Cacheable entity is not mandatory to be IEntity inheritor
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <returns>Cache manager</returns>
        public EntityCache<T> Cached<T>() where T : class
        {
            if (_cache == null)
            {
                throw new Exception("Storage is being used without cache. Please connect cache to storage to be able to use it.");
            }
            return new EntityCache<T>(_cache, Get<T>());
        }

        /// <summary>
        /// Executes raw query and fetches data from it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">SQL query to be executed</param>
        /// <returns>Set of results</returns>
        public IEnumerable<T> Query<T>(DirectSqlSideEffect sql) where T : class
        {
            return _setManager.RawQuery<T>(sql);
        }

        /// <summary>
        /// Obtains instance of uncontexted service to make it to do something
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        /// <returns>Service <typeparamref name="T"/></returns>
        public T Do<T>() where T : StorageService, INoContext
        {
            return _serviceManager.Do<T>();
        }

        /// <summary>
        /// Obtains context service to make it to do something
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        /// <returns>Context service <typeparamref name="T"/></returns>
        public LetBuilder<T> Let<T>() where T : StorageService, IWithContext
        {
            return _serviceManager.Let<T>();
        }

        public StrokeProcessor.RevealedQuery RevealQuery(LambdaExpression expr)
        {
            return _stroke.RevealQuery(expr);
        }

        [Obsolete("Please avoid using that")]
        public Effects Effects => _effectsQueue;

        /// <summary>
        /// Use await on this field to defer actions after save changes call
        /// </summary>
        public SaveTask Save { get => new SaveTask(_postSaveActions); }

        /// <summary>
        /// Defers action that will be executed after .SaveChanges call.
        /// Warning! Dont use logic API that is using this method within bulk context. 
        /// Since EFBatchOperation has no support for resolving dangling IDs then calls to post-save actions 
        /// may lead to unexpected result. 
        /// To avoid this, any trying to perform post-save action in bulk mode will lead to exception.
        /// </summary>
        /// <param name="action">Action to execute after .SaveChanges completed</param>
        public void QueuePostSaveAction(Action action)
        {
            _postSaveActions.Enqueue(action);
        }

        /// <summary>
        /// Defers async action that will be executed after .SaveChanges call.
        /// Warning! Dont use logic API that is using this method within bulk context. 
        /// Since EFBatchOperation has no support for resolving dangling IDs then calls to post-save actions 
        /// may lead to unexpected result. 
        /// To avoid this, any trying to perform post-save action in bulk mode will lead to exception.
        /// </summary>
        /// <param name="action">Action to execute after .SaveChanges completed</param>
        public void QueueAsyncPostSaveAction(Func<Task> action)
        {
            _postSaveActions.Enqueue(action);
        }

        [Obsolete("Use strokes")]
        public void DeferDirectSqlBefore(string sql, params object[] parameters)
        {
            this.DeferDirectSql(sql, parameters);
        }

        [Obsolete("Use strokes")]
        public void DeferDirectSqlAfter(string sql, params object[] parameters)
        {
            Save.ContinueWith(() =>
            {
                this.DeferDirectSql(sql, parameters);
            });
        }

        /// <summary>
        /// Defers action that will be executed after .SaveChanges call and ALL actions that are in post-actions queue
        /// </summary>
        /// <param name="action">Action to execute after .SaveChanges and post-actions queue completed</param>
        public void Finally(Action action)
        {
            _finallyActions.Enqueue(action);
        }

        /// <summary>
        /// Defers async action that will be executed after .SaveChanges call and ALL actions that are in post-actions queue
        /// </summary>
        /// <param name="action">Action to execute after .SaveChanges and post-actions queue completed</param>
        public void FinallyAsync(Func<Task> action)
        {
            _finallyActions.Enqueue(action);
        }

        /// <summary>
        /// Enqueues execution of direct SQL command BEFORE saving changes to DB
        /// this method is not suitable for fetching queries data.
        /// </summary>
        /// <param name="sql">SQL command</param>
        /// <param name="parameters">optional parameters</param>
        public DirectSqlSideEffect DeferDirectSql(string sql, params object[] parameters)
        {
            return _effectsQueue.Enqueue(new DirectSqlSideEffect(sql,parameters));
        }

        public void Defer(DirectSqlSideEffect cmd)
        {
            _effectsQueue.Enqueue(cmd);
        }

        /// <summary>
        /// Enqueues operations to be performed with bulk-uploaded data before SaveChanges is called
        /// </summary>
        /// <typeparam name="T">Type of entity line to bulk upload and change</typeparam>
        /// <param name="dataToUpload">Set of object to upload</param>
        /// <param name="bo">Actions to be done with uploaded data</param>
        public BulkSideEffect DeferBulk<T>(IEnumerable<T> dataToUpload, Action<BulkOperator> bo)
        {
            return _effectsQueue.Enqueue(new BulkSideEffect(_stroke,null)
            {
                Actions = bo,Data = dataToUpload,ElementType = typeof(T)
            });
        }
    }
}
