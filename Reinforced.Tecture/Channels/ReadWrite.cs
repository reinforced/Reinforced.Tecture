using Reinforced.Tecture.Channels.Multiplexer;
using Reinforced.Tecture.Commands;
// ReSharper disable UnusedTypeParameter

namespace Reinforced.Tecture.Channels
{
    /// <summary>
    /// Channel's read end
    /// </summary>
    public interface Read { }

    /// <summary>
    /// Channel's read end
    /// </summary>
    /// <typeparam name="TChannel">Type of data channel</typeparam>
    public interface Read<out TChannel> : Read where TChannel : CanQuery { }

    internal struct SRead<TChannel> : IQueryMultiplexer, Read<TChannel> where TChannel : CanQuery
    {
        private readonly ChannelMultiplexer _mx;
        public SRead(ChannelMultiplexer mx)
        {
            _mx = mx;
        }

        public TAspect GetAspect<TAspect>() where TAspect : QueryAspect
        {
            return _mx.GetQueryAspect<TChannel, TAspect>();
        }
    }

    /// <summary>
    /// Channel's write end
    /// </summary>
    public interface Write
    {
        /// <summary>
        /// Puts command into commands queue
        /// </summary>
        /// <typeparam name="TCommand">Type of command to put</typeparam>
        /// <param name="command">Command instance</param>
        /// <returns>Fluent</returns>
        TCommand Put<TCommand>(TCommand command) where TCommand : CommandBase;

        /// <summary>
        /// Gets post-actions queue
        /// </summary>
        ActionsQueue Save { get; }

        /// <summary>
        /// Gets final-actions queue
        /// </summary>
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

        public TAspect GetAspect<TAspect>() where TAspect : CommandAspect
        {
            return _cm.GetCommandAspect<TChannel, TAspect>();
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

