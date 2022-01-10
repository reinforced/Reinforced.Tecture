using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Channels.Multiplexer;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Queries;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Services
{

    internal class ServiceManager : IDisposable
    {
        private readonly Pipeline _pipeline;
        private readonly ChannelMultiplexer _mux;
        private readonly TestingContextContainer _aux;
        private readonly Func<Type, object> _resolver;
        
        private readonly List<TectureServiceBase> _allServices = new List<TectureServiceBase>();

        public void OnSave()
        {
            foreach (var srv in _allServices)
            {
                srv.CallOnSave();
            }
        }

        public void OnFinally(Exception exceptionHappened)
        {
            foreach (var srv in _allServices)
            {
                srv.CallOnFinally(exceptionHappened);
            }
        }

        public async Task OnSaveAsync(CancellationToken token = default)
        {
            foreach (var srv in _allServices)
            {
                await srv.CallOnSaveAsync();
            }
        }

        public async Task OnFinallyAsync(Exception exceptionHappened,CancellationToken token = default)
        {
            foreach (var srv in _allServices)
            {
                await srv.CallOnFinallyAsync(exceptionHappened,token);
            }
        }

        private readonly Dictionary<Type, TectureServiceBase> _noContextServicesCache = new Dictionary<Type, TectureServiceBase>();

        public ServiceManager(Pipeline pipeline, ChannelMultiplexer mux, TestingContextContainer aux, Func<Type, object> resolver)
        {
            _pipeline = pipeline;
            _mux = mux;
            _aux = aux;
            _resolver = resolver;
        }

        private TService CreateService<TService>() where TService : TectureServiceBase
        {
            var service = (TService)typeof(TService).InstanceNonpublic(_resolver);
            service.ServiceManager = this;
            service.Pipeline = _pipeline;
            service.ChannelMultiplexer = _mux;
            service.Aux = _aux;
            return service;
        }

        public void DestroyService(TectureServiceBase serviceBase)
        {
            serviceBase.ServiceManager = null;
            serviceBase.Pipeline = null;
            serviceBase.ChannelMultiplexer = null;

            var st = serviceBase.GetType();
            if (_noContextServicesCache.ContainsKey(st)) _noContextServicesCache.Remove(st);

            _allServices.Remove(serviceBase);
        }

        public T Do<T>() where T : TectureServiceBase
        {
            if (_noContextServicesCache.ContainsKey(typeof(T))) return (T)_noContextServicesCache[typeof(T)];
            var service = CreateService<T>();
            _noContextServicesCache[typeof(T)] = service;
            service.CallInit();
            _allServices.Add(service);
            return service;
        }

        public void Dispose()
        {
            var allSvcs = _allServices.ToArray();
            foreach (var serviceBase in allSvcs)
            {
                serviceBase.Dispose();
            }
        }
    }
}
