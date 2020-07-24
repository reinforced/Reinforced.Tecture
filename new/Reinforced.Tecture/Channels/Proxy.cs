using Reinforced.Tecture.Testing.Query;

namespace Reinforced.Tecture.Channels
{
    interface IQueryMultiplexer
    {
        TFeature GetFeature<TFeature>(out IQueryStore qs) where TFeature : QueryFeature;
    }

    interface ICommandMultiplexer
    {
        TFeature GetFeature<TFeature>() where TFeature : CommandFeature;
    }

}
