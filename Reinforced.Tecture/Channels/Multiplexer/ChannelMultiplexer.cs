using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Query;
using Reinforced.Tecture.Savers;

namespace Reinforced.Tecture.Channels.Multiplexer
{
    //string key here is full type name
    class ChannelMultiplexer : IDisposable
    {
        private readonly AuxiliaryContainer _auxiliary;

        private readonly Dictionary<string, Type> _namesCache = new Dictionary<string, Type>();
        internal bool IsKnown(Type channel)
        {
            return _knownChannels.Contains(channel);
        }
        private void Known(Type channel)
        {
            if (!_knownChannels.Contains(channel)) _knownChannels.Add(channel);
        }
        private readonly HashSet<Type> _knownChannels = new HashSet<Type>();
        private void CheckChannel(Type tchannel)
        {
            if (!_knownChannels.Contains(tchannel)) throw new TectureException($"Unknown channel {tchannel.Name}");
        }


        #region Query
        private readonly Dictionary<string, Dictionary<string, QueryAspect>> _channelAspectQuery
            = new Dictionary<string, Dictionary<string, QueryAspect>>();


        internal void RegisterQueryAspect(Type channelType, Type queryAspectType, QueryAspect qf)
        {
            Known(channelType);

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

            qf._aux = _auxiliary.ForChannel(channelType);
            qf._channel = channelType;
            qf.CallOnRegister();
        }

        internal TAspect GetQueryAspect<TChannel, TAspect>()
            where TChannel : CanQuery
            where TAspect : QueryAspect
        {
            var ct = typeof(TChannel);
            CheckChannel(ct);

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
        private readonly Dictionary<string, Dictionary<string, CommandAspect>> _channelAspectsCommand
            = new Dictionary<string, Dictionary<string, CommandAspect>>();
        internal void RegisterCommandAspect(Type channelType, Type commandAspectType, CommandAspect cf)
        {
            Known(channelType);

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

            cf._aux = _auxiliary.ForChannel(channelType);
            cf._channel = channelType;
            cf.CallOnRegister();
        }

        internal TAspect GetCommandAspect<TChannel, TAspect>()
            where TChannel : CanCommand
            where TAspect : CommandAspect
        {
            var ct = typeof(TChannel);
            CheckChannel(ct);

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

        #endregion

        #region Savers
        private readonly Dictionary<string, Dictionary<Type, SaverBase>> _saversPerCommandsChannel
            = new Dictionary<string, Dictionary<Type, SaverBase>>();

        private readonly Dictionary<string, HashSet<SaverBase>> _saversPerChannels = new Dictionary<string, HashSet<SaverBase>>();

        public ChannelMultiplexer(AuxiliaryContainer aux)
        {
            _auxiliary = aux;
        }

        internal void RegisterSaver(Type channelType, SaverBase saver)
        {
            Known(channelType);
            saver.Channel = channelType;
            saver._Aux = _auxiliary.ForChannel(channelType);


            if (!_saversPerChannels.ContainsKey(channelType.FullName))
            {
                _saversPerChannels[channelType.FullName] = new HashSet<SaverBase>();
            }

            var chSavers = _saversPerChannels[channelType.FullName];

            if (!chSavers.Contains(saver)) chSavers.Add(saver);

            if (!_saversPerCommandsChannel.ContainsKey(channelType.FullName))
            {
                _saversPerCommandsChannel[channelType.FullName] = new Dictionary<Type, SaverBase>();
            }

            var commandSavers = _saversPerCommandsChannel[channelType.FullName];

            foreach (var saverServingCommandType in saver.ServingCommandTypes)
            {
                if (commandSavers.ContainsKey(saverServingCommandType))
                {
                    throw new TectureException($"Trying to register {saver.GetType().FullName} saver, but there is already runner {commandSavers[saverServingCommandType].GetType().FullName} registered for command '{saverServingCommandType.Name}' ");
                }

                commandSavers[saverServingCommandType] = saver;
            }
            saver.CallOnRegister();
        }

        private SaverBase GetSaverWithCommand(Type commandType, string channelId)
        {
            if (!_saversPerCommandsChannel.ContainsKey(channelId))
            {
                throw new TectureException($"Trying to obtain runner for command '{commandType.Name}' didn't suceed: unknown channel {channelId}");
            }

            var savers = _saversPerCommandsChannel[channelId];


            if (!savers.ContainsKey(commandType))
            {
                var bt = commandType.GetTypeInfo().BaseType;
                if (bt != null)
                {
                    return GetSaverWithCommand(bt, channelId);
                }
                throw new TectureException($"Trying to obtain runner for command '{commandType.Name}' didn't suceed: no saver serving such command registered for {channelId}");
            }

            return savers[commandType];

        }
        internal CommandRunner GetRunner(CommandBase command)
        {
            var saver = GetSaverWithCommand(command.GetType(), command.ChannelId);
            return saver.GetRunner(command);
        }

        internal IEnumerable<SaverBase> GetSavers(IEnumerable<string> channels)
        {
            foreach (var channel in channels)
            {
                if (_saversPerChannels.ContainsKey(channel))
                {
                    foreach (var saverBase in _saversPerChannels[channel])
                    {
                        yield return saverBase;
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
            foreach (var value in _saversPerChannels.Values)
            {
                foreach (var saverBase in value)
                {
                    saverBase.Dispose();
                }
            }
        }
    }
}
