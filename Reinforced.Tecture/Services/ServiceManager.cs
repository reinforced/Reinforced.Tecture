using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reinforced.Tecture.Channels.Multiplexer;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Query;

namespace Reinforced.Tecture.Services
{

    class ServiceManager
    {
        private readonly Pipeline _pipeline;
        private readonly ChannelMultiplexer _mux;
        private readonly AuxiliaryContainer _aux;
        class ServiceContextEntry
        {
            public Type[] ContextTypes { get; set; }
            public object[] Context { get; set; }
            public TectureServiceBase ServiceBaseInstance { get; set; }
        }
        private readonly List<TectureServiceBase> _allServices = new List<TectureServiceBase>();

        public void OnSave()
        {
            foreach (var srv in _allServices)
            {
                srv.CallOnSave();
            }
        }

        public void OnFinally()
        {
            foreach (var srv in _allServices)
            {
                srv.CallOnFinally();
            }
        }

        public async Task OnSaveAsync()
        {
            foreach (var srv in _allServices)
            {
                await srv.CallOnSaveAsync();
            }
        }

        public async Task OnFinallyAsync()
        {
            foreach (var srv in _allServices)
            {
                await srv.CallOnFinallyAsync();
            }
        }

        private readonly Dictionary<Type, TectureServiceBase> _noContextServicesCache = new Dictionary<Type, TectureServiceBase>();

        public ServiceManager(Pipeline pipeline, ChannelMultiplexer mux, AuxiliaryContainer aux)
        {
            _pipeline = pipeline;
            _mux = mux;
            _aux = aux;
        }

        private TService CreateService<TService>() where TService : TectureServiceBase
        {
            var service = (TService)typeof(TService).InstanceNonpublic();
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
    }
}
