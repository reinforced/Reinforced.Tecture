using Reinforced.Tecture.Channels.Multiplexer;
using Reinforced.Tecture.Commands;

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

        public SRead(ChannelMultiplexer mx)
        {
            _mx = mx;
        }

        public TFeature GetFeature<TFeature>() where TFeature : QueryFeature
        {
            return _mx.GetQueryFeature<TChannel, TFeature>();
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

