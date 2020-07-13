using Reinforced.Tecture.Testing.Query;

namespace Reinforced.Tecture.Channels
{
    interface IQueryMultiplexer
    {
        TFeature GetFeature<TFeature>() where TFeature : QueryFeature;
        IQueryStore GetStore();
    }

    interface ICommandMultiplexer
    {
        TFeature GetFeature<TFeature>() where TFeature : CommandFeature;
    }

}
