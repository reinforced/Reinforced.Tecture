using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Testing.Validation;

namespace Reinforced.Tecture.Testing.BuiltInChecks
{
    public class ChannelTypeCheck : CommandCheck<CommandBase>
    {
        private readonly Type _channelType;

        internal ChannelTypeCheck(Type channelType)
        {
            _channelType = channelType;
        }

        /// <inheritdoc cref="CommandCheck{TCommand}.GetMessage"/>
        protected override string GetMessage(CommandBase effect)
        {
            if (effect == null) return $"expected command for channel '{_channelType.Name}', but story unexpectedly ends";
            return $"expected command for channel '{_channelType.Name}', but got command to '{effect.Channel.Name}' one";
        }

        /// <inheritdoc cref="CommandCheck{TCommand}.IsActuallyValid"/>
        protected override bool IsActuallyValid(CommandBase effect)
        {
            if (effect == null) return false;
            return effect.Channel == _channelType;
        }
    }
}
