using System;
using System.Threading.Tasks;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Channels.Multiplexer;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Queries;
using Reinforced.Tecture.Services;
using Reinforced.Tecture.Testing;
using Reinforced.Tecture.Tracing;
using Reinforced.Tecture.Transactions;


namespace Reinforced.Tecture.Entry
{
    internal class Tecture : ITecture
    {

        private readonly ServiceManager _serviceManager;
        private readonly ChannelMultiplexer _mx;
        internal readonly Pipeline _pipeline;
        internal readonly ActionsQueue _actions = new ActionsQueue(true);
        internal readonly ActionsQueue _finallyActions = new ActionsQueue(false);
        private readonly TransactionManager _tranManager;
        private readonly Func<Exception, bool> _exceptionHandler;
        private readonly TestingContextContainer _aux;
        
        public Tecture(
            ChannelMultiplexer mx,
            TestingContextContainer aux,
            bool debugMode = false,
            TransactionManager tranManager = null,
            Func<Exception, bool> exceptionHandler = null,
            Func<Type,object> iocResolver = null)
        {
            _mx = mx;
            _aux = aux;
            _pipeline = new Pipeline(debugMode, _actions, _finallyActions);
            _tranManager = tranManager;
            _exceptionHandler = exceptionHandler;
            _serviceManager = new ServiceManager(_pipeline, _mx, _aux, iocResolver);
        }

        /// <summary>
        /// Obtains instance of uncontexted service to make it to do something
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        /// <returns>Service <typeparamref name="T"/></returns>
        public T Do<T>() where T : TectureServiceBase
        {
            return _serviceManager.Do<T>();
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
            if (_tc == null)
                throw new TectureException(".EndTrace is called, but trace has not been collected");
            return _tc.Finish();
        }


        /// <summary>
        /// Runs commands queue
        /// </summary>
        public void Save()
        {
            if (_actions.IsRunning) throw new Exception(".SaveChanges cannot be called within post-save action");
            if (_finallyActions.IsRunning) throw new Exception(".SaveChanges cannot be called within finally action");


            Exception thrown = null;
            try
            {
                _serviceManager.OnSave();

                CommandsDispatcher dispatcher = new CommandsDispatcher(_mx, _aux.TraceCollector, _tranManager);
                dispatcher.Dispatch(_pipeline, _actions);

                _serviceManager.OnFinally();
                _finallyActions.Run();
                if (_pipeline.HasEffects) dispatcher.Dispatch(_pipeline, _actions);
            }
            catch (Exception ex)
            {
                if (_exceptionHandler == null || !_exceptionHandler(ex))
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Runs async commands queue
        /// </summary>
        /// <returns></returns>
        public async Task SaveAsync()
        {
            if (_actions.IsRunning) throw new Exception(".SaveAsync cannot be called within post-save action");
            if (_finallyActions.IsRunning) throw new Exception(".SaveAsync cannot be called within finally action");
            Exception thrown = null;
            try
            {

                await _serviceManager.OnSaveAsync();

                CommandsDispatcher dispatcher = new CommandsDispatcher(_mx, _aux.TraceCollector, _tranManager);
                await dispatcher.DispatchAsync(_pipeline, _actions);

                await _serviceManager.OnFinallyAsync();
                await _finallyActions.RunAsync();
                if (_pipeline.HasEffects) await dispatcher.DispatchAsync(_pipeline, _actions);
            }
            catch (Exception ex)
            {
                if (_exceptionHandler == null || !_exceptionHandler(ex))
                {
                    throw;
                }
            }
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            _mx.Dispose();
            _serviceManager.Dispose();
        }
    }
}
