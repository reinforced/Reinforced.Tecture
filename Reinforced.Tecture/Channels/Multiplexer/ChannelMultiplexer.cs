using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Reinforced.Tecture.Aspects;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Queries;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Channels.Multiplexer
{
    //string key here is full type name
    class ChannelMultiplexer : IDisposable
    {
        private readonly TestingContextContainer _testingContext;
        
        public ChannelMultiplexer(TestingContextContainer testingContext)
        {
            _testingContext = testingContext;
        }

        #region Channels

        /// <summary>
        /// Gets whether the channel of specific type is known and configured
        /// </summary>
        /// <param name="channel">Type of channel</param>
        /// <returns>True when channel is known, false otherwise</returns>
        internal bool IsKnown(Type channel) => _knownChannels.Contains(channel);

        private void EnsureKnown(Type channel) => _knownChannels.Add(channel);

        private readonly HashSet<Type> _knownChannels = new HashSet<Type>();
        
        private void AssertChannelKnown(Type tchannel)
        {
            if (!_knownChannels.Contains(tchannel)) throw new TectureException($"Unknown channel {tchannel.Name}");
        }

        #endregion

        #region Query
        private readonly Dictionary<string, Dictionary<string, QueryAspect>> _channelAspectQuery = new Dictionary<string, Dictionary<string, QueryAspect>>();
        internal void RegisterQueryAspect(Type channelType, Type queryAspectType, QueryAspect qf)
        {
            EnsureKnown(channelType);

            var channelId = channelType.FullName;

            if (!_channelAspectQuery.ContainsKey(channelId))
            {
                _channelAspectQuery[channelId] = new Dictionary<string, QueryAspect>();
            }

            var aspects = _channelAspectQuery[channelId];
            var aspectId = queryAspectType.FullName;
            if (!aspects.ContainsKey(aspectId))
            {
                aspects[aspectId] = qf;
            }
            else
            {
                throw new TectureException($"Attempt to bind query aspect {queryAspectType.Name} twice for channel {channelType.Name}");
            }

            qf._context = _testingContext.ForChannel(channelType);
            qf._channel = channelType;
            qf.CallOnRegister();
        }

        internal TAspect GetQueryAspect<TChannel, TAspect>()
            where TChannel : CanQuery
            where TAspect : QueryAspect
        {
            var ct = typeof(TChannel);
            AssertChannelKnown(ct);

            var tf = typeof(TAspect);

            if (!_channelAspectQuery.ContainsKey(ct.FullName))
            {
                throw new TectureException($"Query aspect {tf.FullName} of channel {ct.Name} is not bound");
            }

            var aspects = _channelAspectQuery[ct.FullName];
            if (!aspects.ContainsKey(tf.FullName))
            {
                throw new TectureException($"Query aspect {tf.FullName} of channel {ct.Name} is not bound");
            }

            return (TAspect)aspects[tf.FullName];
        }
        #endregion

        #region Command
        
        private readonly Dictionary<string, Dictionary<string, CommandAspect>> _channelAspectsCommand = new Dictionary<string, Dictionary<string, CommandAspect>>();
        private readonly Dictionary<string, Dictionary<Type, CommandAspect>> _channelCommandAspect = new Dictionary<string, Dictionary<Type, CommandAspect>>();

        

        internal void RegisterCommandAspect(Type channelType, Type commandAspectType, CommandAspect cf)
        {
            EnsureKnown(channelType);

            var channelId = channelType.FullName;

            if (!_channelAspectsCommand.ContainsKey(channelId))
            {
                _channelAspectsCommand[channelId] = new Dictionary<string, CommandAspect>();
            }

            var aspects = _channelAspectsCommand[channelId];
            var aspectId = commandAspectType.FullName;
            
            if (!aspects.ContainsKey(aspectId))
            {
                aspects[aspectId] = cf;
            }
            else
            {
                throw new TectureException($"Attempt to bind command aspect {commandAspectType.FullName} twice for channel {channelType.Name}");
            }

            if (!_channelCommandAspect.ContainsKey(channelId))
            {
                _channelCommandAspect[channelId] = new Dictionary<Type, CommandAspect>();
            }

            var servingCommands = _channelCommandAspect[channelId];
            foreach (var cfServingCommandType in cf.ServingCommandTypes)
            {
                if (servingCommands.ContainsKey(cfServingCommandType))
                {
                    throw new TectureException(
                        $"Command {cfServingCommandType.Name} is already being served by command aspect {commandAspectType.FullName} in context of channel {channelId}");
                }

                servingCommands[cfServingCommandType] = cf;
            }

            cf._context = _testingContext.ForChannel(channelType);
            cf._channel = channelType;
            cf.CallOnRegister();
        }

        internal TAspect GetCommandAspect<TChannel, TAspect>()
            where TChannel : CanCommand
            where TAspect : CommandAspect
        {
            var ct = typeof(TChannel);
            AssertChannelKnown(ct);

            var tf = typeof(TAspect);

            if (!_channelAspectsCommand.ContainsKey(ct.FullName))
            {
                throw new TectureException($"Command aspect {tf.FullName} of channel {ct.Name} is not bound");
            }

            var aspects = _channelAspectsCommand[ct.FullName];
            if (!aspects.ContainsKey(tf.FullName))
            {
                throw new TectureException($"Command aspect {tf.FullName} of channel {ct.Name} is not bound");
            }

            return (TAspect)aspects[tf.FullName];
        }

        private CommandAspect GetAspectWithCommand(Type commandType, string channelId)
        {
            if (!_channelCommandAspect.ContainsKey(channelId))
            {
                throw new TectureException($"Trying to obtain runner for command '{commandType.Name}' didn't suceed: unknown channel {channelId}");
            }

            var savers = _channelCommandAspect[channelId];


            if (!savers.ContainsKey(commandType))
            {
                var bt = commandType.GetTypeInfo().BaseType;
                if (bt != null)
                {
                    return GetAspectWithCommand(bt, channelId);
                }
                throw new TectureException($"Trying to obtain runner for command '{commandType.Name}' didn't suceed: no command aspect servicing command registered for {channelId}");
            }

            return savers[commandType];

        }
        internal CommandRunner GetRunner(CommandBase command)
        {
            var saver = GetAspectWithCommand(command.GetType(), command.ChannelId);
            return saver.GetRunner(command);
        }

        internal IEnumerable<CommandAspect> GetCommandAspectsForChannels(IEnumerable<string> channels)
        {
            foreach (var channel in channels)
            {
                if (_channelAspectsCommand.ContainsKey(channel))
                {
                    foreach (var commandAspect in _channelAspectsCommand[channel].Values)
                    {
                        yield return commandAspect;
                    }
                }
            }
        }

        #endregion


        internal void Validate()
        {
            foreach (var knownChannel in _knownChannels)
            {
                var name = knownChannel.FullName;

                var iFaces = knownChannel.GetInterfaces();
                var commandAspects = iFaces.Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(CommandChannel<>))
                    .Select(x => x.GetGenericArguments()[0]).ToArray();
                var queryAspects = iFaces.Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(QueryChannel<>))
                    .Select(x => x.GetGenericArguments()[0]).ToArray();

                if (commandAspects.Length > 0)
                {
                    if (!_channelAspectsCommand.ContainsKey(name))
                        throw new TectureValidationException($"Channel {knownChannel.Name} has unbound command aspects: {string.Join(", ",commandAspects.Select(x=>x.Name))}");
                    var aspects = _channelAspectsCommand[name];

                    foreach (var commandAspect in commandAspects)
                    {
                        if (!aspects.ContainsKey(commandAspect.FullName))
                        {
                            throw new TectureValidationException($"Channel {knownChannel.Name} has unbound command aspect '{commandAspect.FullName}'");
                        }
                    }

                }

                if (queryAspects.Length > 0)
                {
                    if (!_channelAspectQuery.ContainsKey(name))
                        throw new TectureValidationException($"Channel {knownChannel.Name} has unbound query aspects: {string.Join(", ", queryAspects.Select(x => x.Name))}");

                    var aspects = _channelAspectQuery[name];

                    foreach (var queryAspect in queryAspects)
                    {
                        if (!aspects.ContainsKey(queryAspect.FullName))
                        {
                            throw new TectureValidationException($"Channel {knownChannel.Name} has unbound query aspect '{queryAspect.FullName}'");
                        }
                    }
                }

            }
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            foreach (var channelQueryAspects in _channelAspectQuery.Values)
            {
                foreach (var queryAspect in channelQueryAspects.Values)
                {
                    queryAspect.Dispose();
                }
            }

            foreach (var channelCommandAspects in _channelAspectsCommand.Values)
            {
                foreach (var commandAspect in channelCommandAspects.Values)
                {
                    commandAspect.Dispose();
                }
            }
        }
    }
}
