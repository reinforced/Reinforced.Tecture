using System;
using System.Collections.Generic;
using System.Reflection;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Query;
using Reinforced.Tecture.Savers;

namespace Reinforced.Tecture.Channels.Multiplexer
{
    //string key here is full type name
    class ChannelMultiplexer : IDisposable
    {
        private readonly AuxilaryContainer _auxilary;

        private readonly Dictionary<string, Type> _namesCache = new Dictionary<string, Type>();
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

            var channeId = channelType.FullName;

            if (!_channelAspectQuery.ContainsKey(channeId))
            {
                _channelAspectQuery[channeId] = new Dictionary<string, QueryAspect>();
            }

            var aspects = _channelAspectQuery[channeId];
            var aspectId = queryAspectType.FullName;
            if (!aspects.ContainsKey(aspectId))
            {
                aspects[aspectId] = qf;
            }
            else
            {
                throw new TectureException($"Query aspect {queryAspectType.Name} is already implemented for {channelType.Name}");
            }

            qf._aux = _auxilary.ForChannel(channelType);
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
                throw new TectureException($"No runtime for query aspect {tf.Name} of channel {ct.Name} found");
            }

            var aspects = _channelAspectQuery[ct.FullName];
            if (!aspects.ContainsKey(tf.FullName))
            {
                throw new TectureException($"No runtime for query aspect {tf.Name} of channel {ct.Name} found");
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

            var channeId = channelType.FullName;

            if (!_channelAspectsCommand.ContainsKey(channeId))
            {
                _channelAspectsCommand[channeId] = new Dictionary<string, CommandAspect>();
            }

            var aspects = _channelAspectsCommand[channeId];
            var aspectId = commandAspectType.FullName;
            if (!aspects.ContainsKey(aspectId))
            {
                aspects[aspectId] = cf;
            }
            else
            {
                throw new TectureException($"Command aspect {commandAspectType.Name} is already implemented for {channelType.Name}");
            }

            cf._aux = _auxilary.ForChannel(channelType);
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
                throw new TectureException($"No runtime for command aspect {tf.Name} of channel {ct.Name} found");
            }

            var aspects = _channelAspectsCommand[ct.FullName];
            if (!aspects.ContainsKey(tf.FullName))
            {
                throw new TectureException($"No runtime for command aspect {tf.Name} of channel {ct.Name} found");
            }

            return (TAspect)aspects[tf.FullName];
        }

        #endregion

        #region Savers
        private readonly Dictionary<string, Dictionary<Type, SaverBase>> _saversPerCommandsChannel
            = new Dictionary<string, Dictionary<Type, SaverBase>>();

        private readonly Dictionary<string, HashSet<SaverBase>> _saversPerChannels = new Dictionary<string, HashSet<SaverBase>>();

        public ChannelMultiplexer(AuxilaryContainer aux)
        {
            _auxilary = aux;
        }

        internal void RegisterSaver(Type channelType, SaverBase saver)
        {
            Known(channelType);
            saver._Aux = _auxilary.ForChannel(channelType);

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
                var iFaces = knownChannel.GetInterfaces();

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
