using Reinforced.Tecture.Channels.Multiplexer;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Testing.Query;

namespace Reinforced.Tecture.Channels
{
    public interface Read { }

    /// <summary>
    /// Contextual reading interface
    /// </summary>
    /// <typeparam name="DataChannel">Type of data channel</typeparam>
    public interface Read<out DataChannel> : Read where DataChannel : CanQuery { }

    internal struct SRead<TChannel> : IQueryMultiplexer, Read<TChannel> where TChannel : CanQuery
    {
        private readonly ChannelMultiplexer _mx;
        private readonly IQueryStore _qs;
        public SRead(ChannelMultiplexer mx, IQueryStore qs)
        {
            _mx = mx;
            _qs = qs;
        }

        public TFeature GetFeature<TFeature>() where TFeature : QueryFeature
        {
            return _mx.GetQueryFeature<TChannel, TFeature>();
        }

        public IQueryStore GetStore()
        {
            return _qs;
        }
    }

    public interface Write
    {
        TCmd Put<TCmd>(TCmd command) where TCmd : CommandBase;

        ActionsQueue Save { get; }

        ActionsQueue Final { get; }
    }

    /// <summary>
    /// Contextual writing interface
    /// </summary>
    /// <typeparam name="DataChannel"></typeparam>
    public interface Write<out DataChannel> : Write where DataChannel : CanCommand { }

    
}

