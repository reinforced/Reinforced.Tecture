using System;
using System.Threading.Tasks;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Channels.Multiplexer;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Query;
using Reinforced.Tecture.Services;
using Reinforced.Tecture.Testing;
using Reinforced.Tecture.Tracing;
using Reinforced.Tecture.Transactions;
using Reinforced.Tecture.Transactions.Testing;

namespace Reinforced.Tecture.Entry
{
    internal class Tecture : ITecture
    {

        private readonly ServiceManager _serviceManager;
        private readonly ChannelMultiplexer _mx;
        internal readonly Pipeline _pipeline;
        internal readonly ActionsQueue _actions = new ActionsQueue(true);
        internal readonly ActionsQueue _finallyActions = new ActionsQueue(false);
        private readonly ITransactionManager _tranManager;
        private readonly Action<Exception> _exceptionHandler;
        private readonly AuxilaryContainer _aux;
        public Tecture(
            ChannelMultiplexer mx,
            AuxilaryContainer aux,
            bool debugMode = false,
            ITransactionManager tranManager = null,
            Action<Exception> exceptionHandler = null)
        {
            _mx = mx;
            _aux = aux;
            _pipeline = new Pipeline(debugMode, _actions, _finallyActions);
            _tranManager = tranManager;
            _exceptionHandler = exceptionHandler;
            _serviceManager = new ServiceManager(_pipeline, _mx, _aux);
        }

        /// <summary>
        /// Obtains instance of uncontexted service to make it to do something
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        /// <returns>Service <typeparamref name="T"/></returns>
        public T Do<T>() where T : TectureServiceBase, INoContext
        {
            return _serviceManager.Do<T>();
        }

        /// <summary>
        /// Obtains context service to make it to do something
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        /// <returns>Context service <typeparamref name="T"/></returns>
        public LetBuilder<T> Let<T>() where T : TectureServiceBase, IWithContext
        {
            return _serviceManager.Let<T>();
        }

        /// <summary>
        /// Obtains data source to query data from
        /// </summary>
        /// <typeparam name="T">Type of data source</typeparam>
        /// <returns>Data source instance</returns>
        public Read<T> From<T>() where T : CanQuery
        {
            return new SRead<T>(_mx);
        }

        private TraceCollector _tc = null;
        /// <summary>
        /// Begins trace collection
        /// </summary>
        public void BeginTrace()
        {
            _tc = new TraceCollector();
            _pipeline.TraceCollector = _tc;
            _aux.TraceCollector = _tc;

        }

        /// <summary>
        /// Finishes trace collection
        /// </summary>
        /// <returns></returns>
        public Trace EndTrace()
        {
            if (_tc==null)
                throw new TectureException(".EndTrace is called, but trace has not been collected");
            return _tc.Finish();
        }

        private IOuterTransaction ObtainTransaction(
            OuterTransactionMode transaction,
            OuterTransactionIsolationLevel level)
        {
            if (_tranManager == null)
                return new FakeOuterTransaction(level);

            if (transaction == OuterTransactionMode.DbTransaction)
                return _tranManager.BeginDbTransaction(level);
            if (transaction == OuterTransactionMode.TransactionScope)
                return _tranManager.BeginDbTransaction(level);
            return new FakeOuterTransaction(level);
        }

        /// <summary>
        /// Runs commands queue
        /// </summary>
        public void Save(OuterTransactionMode transaction = OuterTransactionMode.None,
            OuterTransactionIsolationLevel level = OuterTransactionIsolationLevel.Chaos)
        {
            if (_actions.IsRunning) throw new Exception(".SaveChanges cannot be called within post-save action");
            if (_finallyActions.IsRunning) throw new Exception(".SaveChanges cannot be called within finally action");
            IOuterTransaction tran = ObtainTransaction(transaction, level);

            Exception thrown = null;
            try
            {
                _serviceManager.OnSave();

                CommandsDispatcher dispatcher = new CommandsDispatcher(_mx, _aux.TraceCollector);
                dispatcher.Dispatch(_pipeline, _actions);

                _serviceManager.OnFinally();
                _finallyActions.Run();

                tran?.Commit();
            }
            catch (Exception ex)
            {
                _exceptionHandler?.Invoke(ex);
                thrown = ex;
            }
            finally
            {
                try
                {
                    tran?.Dispose();
                }
                catch (Exception)
                {
                    if (thrown == null) throw;
                }
                if (thrown != null) throw thrown;
            }
        }

        /// <summary>
        /// Runs async commands queue
        /// </summary>
        /// <returns></returns>
        public async Task SaveAsync(OuterTransactionMode transaction = OuterTransactionMode.None,
            OuterTransactionIsolationLevel level = OuterTransactionIsolationLevel.Chaos)
        {
            if (_actions.IsRunning) throw new Exception(".SaveAsync cannot be called within post-save action");
            if (_finallyActions.IsRunning) throw new Exception(".SaveAsync cannot be called within finally action");
            IOuterTransaction tran = ObtainTransaction(transaction, level);
            Exception thrown = null;
            try
            {

                await _serviceManager.OnSaveAsync();

                CommandsDispatcher dispatcher = new CommandsDispatcher(_mx, _aux.TraceCollector);
                await dispatcher.DispatchAsync(_pipeline, _actions);

                await _serviceManager.OnFinallyAsync();
                await _finallyActions.RunAsync();

                tran?.Commit();
                //CleanupAfterSave();
            }
            catch (Exception ex)
            {
                _exceptionHandler?.Invoke(ex);
                thrown = ex;
            }
            finally
            {
                try
                {
                    tran?.Dispose();
                }
                catch (Exception)
                {
                    if (thrown == null) throw;
                }
                if (thrown != null) throw thrown;
            }
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            _mx.Dispose();
        }
    }
}
