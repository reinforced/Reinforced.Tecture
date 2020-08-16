using System;
using System.Collections.Generic;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Testing.Generation
{
    /// <summary>
    /// Checks configurator for particular command
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public class ChecksConfigurator<TCommand> where TCommand : CommandBase
    {
        private readonly Dictionary<Type, List<CheckDescription>> _checksForCommands;

        internal ChecksConfigurator(Dictionary<Type, List<CheckDescription>> checksForCommands)
        {
            _checksForCommands = checksForCommands;
        }

        /// <summary>
        /// Adds check description to be used
        /// </summary>
        /// <param name="description"></param>
        public void Enlist(CheckDescription<TCommand> description)
        {
            if (!_checksForCommands.ContainsKey(typeof(TCommand)))
            {
                _checksForCommands[typeof(TCommand)] = new List<CheckDescription>();
            }

            _checksForCommands[typeof(TCommand)].Add(description);
        }
    }
}