using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Channels.Multiplexer;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Testing.Query;

namespace Reinforced.Tecture.Services
{

    class ServiceManager
    {
        private readonly Pipeline _pipeline;
        private readonly ChannelMultiplexer _mux;
        private readonly TestDataHolder _testData;
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
        private readonly Dictionary<Type, List<ServiceContextEntry>> _contextServices = new Dictionary<Type, List<ServiceContextEntry>>();
        private readonly Dictionary<Type, LetBuilder> _letCache = new Dictionary<Type, LetBuilder>();

        private TectureServiceBase LocateExistingContextService(Type serviceType, Type[] contextTypes, object[] contextValues)
        {
            if (!_contextServices.ContainsKey(serviceType)) return null;
            var entries = _contextServices[serviceType];
            var serviceEntry = entries.FirstOrDefault(d =>
                d.ContextTypes.SequenceEqual(contextTypes) && d.Context.SequenceEqual(contextValues));
            if (serviceEntry == null) return null;
            return serviceEntry.ServiceBaseInstance;
        }

        private void SaveExistingContextService(Type serviceType, Type[] contextTypes, object[] contextValues, TectureServiceBase instance)
        {
            var entry = new ServiceContextEntry()
            {
                ContextTypes = contextTypes,
                Context = contextTypes,
                ServiceBaseInstance = instance
            };
            if (!_contextServices.ContainsKey(serviceType))
            {
                _contextServices[serviceType] = new List<ServiceContextEntry>();
            }

            var sd = _contextServices[serviceType];
            sd.Add(entry);
        }

        public ServiceManager(Pipeline pipeline, ChannelMultiplexer mux, TestDataHolder testData)
        {
            _pipeline = pipeline;
            _mux = mux;
            _testData = testData;
        }

        private TService CreateService<TService>() where TService : TectureServiceBase
        {
            var service = (TService)typeof(TService).InstanceNonpublic();
            service.ServiceManager = this;
            service.Pipeline = _pipeline;
            service.ChannelMultiplexer = _mux;
            service.TestData = _testData;
            return service;
        }

        internal TService CreateWithContext<TService>(Type[] paramTypes, object[] context) where TService : TectureServiceBase, IWithContext
        {
            var service = LocateExistingContextService(typeof(TService), paramTypes, context);
            if (service != null) return (TService)service;

            service = CreateService<TService>();

            var contextMethod = typeof(TService).GetRuntimeMethod("Context", paramTypes);
            if (contextMethod == null)
                throw new Exception($"Cannot find context method of {typeof(TService).FullName} having arguments of types {string.Join(", ", paramTypes.Select(d => d.Name))} ");
            try
            {
                contextMethod.Invoke(service, context);
            }
            catch (Exception ex)
            {
                throw new Exception($"Cannot set context for {typeof(TService)}. See inner exception for details.", ex);
            }

            SaveExistingContextService(typeof(TService), paramTypes, context, service);
            service.CallInit();
            _allServices.Add(service);
            return (TService)service;
        }

        public void DestroyService(TectureServiceBase serviceBase)
        {
            serviceBase.ServiceManager = null;
            serviceBase.Pipeline = null;
            serviceBase.ChannelMultiplexer = null;

            var st = serviceBase.GetType();
            if (serviceBase is INoContext)
            {
                if (_noContextServicesCache.ContainsKey(st)) _noContextServicesCache.Remove(st);
            }

            if (serviceBase is IWithContext)
            {
                if (_contextServices.ContainsKey(st))
                {
                    var lst = _contextServices[st];
                    lst.RemoveAll(d => d.ServiceBaseInstance == serviceBase);
                }
            }

            _allServices.Remove(serviceBase);
        }

        public T Do<T>() where T : TectureServiceBase, INoContext
        {
            if (_noContextServicesCache.ContainsKey(typeof(T))) return (T)_noContextServicesCache[typeof(T)];
            var service = CreateService<T>();
            _noContextServicesCache[typeof(T)] = service;
            service.CallInit();
            _allServices.Add(service);
            return service;
        }


        public LetBuilder<T> Let<T>() where T : TectureServiceBase, IWithContext
        {
            if (_letCache.ContainsKey(typeof(T))) return (LetBuilder<T>)_letCache[typeof(T)];
            var lb = new LetBuilder<T>(this);
            _letCache[typeof(T)] = lb;
            return lb;
        }
    }
}
