namespace Reinforced.Tecture.Channels
{
    interface IQueryMultiplexer
    {
        TAspect GetAspect<TAspect>() where TAspect : QueryAspect;
    }

    interface ICommandMultiplexer
    {
        TAspect GetAspect<TAspect>() where TAspect : CommandAspect;
    }

}
