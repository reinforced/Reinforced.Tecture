using System;

namespace Reinforced.Tecture.Tracing.Promises
{
    class LightDemands<T> : Catching<T>, NotifyCompleted<T>
    {
        private PromisedQuery<T> _promised;
        
        public LightDemands(TraceCollector traceCollector, Type channelType)
        {
            _promised = traceCollector.PromiseQuery<T>(channelType);
        }

        public void Fulfill(Exception error, string description = null)
        {
            _promised.FulfillError(error,description);
        }

        public void Fulfill(string description = null)
        {
            _promised.LightFulfill(description);
        }
    }
}