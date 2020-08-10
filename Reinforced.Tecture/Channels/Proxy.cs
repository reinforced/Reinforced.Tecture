using Reinforced.Tecture.Query;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Channels
{
    interface IQueryMultiplexer
    {
        TFeature GetFeature<TFeature>(out Auxilary a) where TFeature : QueryFeature;
    }

    interface ICommandMultiplexer
    {
        TFeature GetFeature<TFeature>() where TFeature : CommandFeature;
    }

}
