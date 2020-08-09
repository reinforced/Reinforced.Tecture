using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Testing.Stories;
using Reinforced.Tecture.Testing.Validation;

namespace Reinforced.Tecture.Testing.Generation
{
    public static class Extensions
    {
        public static string GenerateTest<TGenerator>(this StorageStory s, Action<TGenerator> configure) where TGenerator : TestGenerator, new()
        {
            var gen = new TGenerator();
            configure(gen);

            return null;//todo

        }

        public static ChecksConfigurator<TCommand> For<TCommand>(this TestGenerator tg) where TCommand : CommandBase
        {
            return new ChecksConfigurator<TCommand>(tg._checksForCommands);
        }


    }

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
        public void Add(CheckDescription<TCommand> description)
        {
            if (!_checksForCommands.ContainsKey(typeof(TCommand)))
            {
                _checksForCommands[typeof(TCommand)] = new List<CheckDescription>();
            }

            _checksForCommands[typeof(TCommand)].Add(description);
        }
    }

}
