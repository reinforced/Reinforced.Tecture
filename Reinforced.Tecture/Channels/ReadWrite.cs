using Reinforced.Tecture.Channels.Multiplexer;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Query;
using Reinforced.Tecture.Testing;

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
        private readonly AuxilaryContainer _a;
        public SRead(ChannelMultiplexer mx, AuxilaryContainer a)
        {
            _mx = mx;
            _a = a;
        }

        public TFeature GetFeature<TFeature>(out Auxilary a) where TFeature : QueryFeature
        {
            a = _a.ForChannel(typeof(TChannel));
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

    internal struct SWrite<TChannel> : ICommandMultiplexer, Write<TChannel> where TChannel : CanCommand
    {
        private readonly ChannelMultiplexer _cm;
        private readonly Pipeline _pipeline;
        public SWrite(ChannelMultiplexer cm, Pipeline p)
        {
            _cm = cm;
            _pipeline = p;
        }

        public TFeature GetFeature<TFeature>() where TFeature : CommandFeature
        {
            return _cm.GetCommandFeature<TChannel, TFeature>();
        }

        public TCmd Put<TCmd>(TCmd command) where TCmd : CommandBase
        {
            command.ChannelId = typeof(Channel).FullName;
            _pipeline.Enqueue(command);
            return command;
        }

        public ActionsQueue Save { get { return _pipeline.PostSaveActions; } }

        public ActionsQueue Final { get { return _pipeline.FinallyActions; } }
       
    }


}

