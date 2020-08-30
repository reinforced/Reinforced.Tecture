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
        private readonly Dictionary<string, Dictionary<string, QueryFeature>> _channelFeaturesQuery
            = new Dictionary<string, Dictionary<string, QueryFeature>>();


        internal void RegisterQueryFeature(Type channelType, Type queryFeatureType, QueryFeature qf)
        {
            Known(channelType);

            var channeId = channelType.FullName;

            if (!_channelFeaturesQuery.ContainsKey(channeId))
            {
                _channelFeaturesQuery[channeId] = new Dictionary<string, QueryFeature>();
            }

            var features = _channelFeaturesQuery[channeId];
            var featureId = queryFeatureType.FullName;
            if (!features.ContainsKey(featureId))
            {
                features[featureId] = qf;
            }
            else
            {
                throw new TectureException($"Query feature {queryFeatureType.Name} is already implemented for {channelType.Name}");
            }

            qf._aux = _auxilary.ForChannel(channelType);
            qf.CallOnRegister();
        }

        internal TFeature GetQueryFeature<TChannel, TFeature>()
            where TChannel : CanQuery
            where TFeature : QueryFeature
        {
            var ct = typeof(TChannel);
            CheckChannel(ct);

            var tf = typeof(TFeature);

            if (!_channelFeaturesQuery.ContainsKey(ct.FullName))
            {
                throw new TectureException($"No runtime for query feature {tf.Name} of channel {ct.Name} found");
            }

            var features = _channelFeaturesQuery[ct.FullName];
            if (!features.ContainsKey(tf.FullName))
            {
                throw new TectureException($"No runtime for query feature {tf.Name} of channel {ct.Name} found");
            }

            return (TFeature)features[tf.FullName];
        }
        #endregion

        #region Command
        private readonly Dictionary<string, Dictionary<string, CommandFeature>> _channelFeaturesCommand
            = new Dictionary<string, Dictionary<string, CommandFeature>>();
        internal void RegisterCommandFeature(Type channelType, Type queryFeatureType, CommandFeature cf)
        {
            Known(channelType);

            var channeId = channelType.FullName;

            if (!_channelFeaturesCommand.ContainsKey(channeId))
            {
                _channelFeaturesCommand[channeId] = new Dictionary<string, CommandFeature>();
            }

            var features = _channelFeaturesCommand[channeId];
            var featureId = queryFeatureType.FullName;
            if (!features.ContainsKey(featureId))
            {
                features[featureId] = cf;
            }
            else
            {
                throw new TectureException($"Command feature {queryFeatureType.Name} is already implemented for {channelType.Name}");
            }

            cf._aux = _auxilary.ForChannel(channelType);
            cf.CallOnRegister();
        }

        internal TFeature GetCommandFeature<TChannel, TFeature>()
            where TChannel : CanCommand
            where TFeature : CommandFeature
        {
            var ct = typeof(TChannel);
            CheckChannel(ct);

            var tf = typeof(TFeature);

            if (!_channelFeaturesCommand.ContainsKey(ct.FullName))
            {
                throw new TectureException($"No runtime for command feature {tf.Name} of channel {ct.Name} found");
            }

            var features = _channelFeaturesCommand[ct.FullName];
            if (!features.ContainsKey(tf.FullName))
            {
                throw new TectureException($"No runtime for command feature {tf.Name} of channel {ct.Name} found");
            }

            return (TFeature)features[tf.FullName];
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


        internal void ValidateConfiguredChannel<TChannel>()
        {
            var ct = typeof(TChannel);
            CheckChannel(ct);

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
