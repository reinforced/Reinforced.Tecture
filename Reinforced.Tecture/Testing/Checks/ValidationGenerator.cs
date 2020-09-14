using System;
using System.Collections.Generic;
using System.Reflection;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Testing.Checks
{
    /// <summary>
    /// Validation generator interface 
    /// </summary>
    public interface IValidationGenerator
    {
        /// <summary>
        /// Generates validation for particular command
        /// </summary>
        /// <param name="command">Command instance</param>
        /// <param name="checks">Checks descriptions to be used</param>
        void Visit(CommandBase command, CheckDescription[] checks);
    }

    /// <summary>
    /// Unit test generator instance
    /// </summary>
    public class ValidationGenerator
    {
        internal readonly Dictionary<Type, List<CheckDescription>> _checksForCommands = new Dictionary<Type, List<CheckDescription>>();

        private void AppendChecks(Type commandType, List<CheckDescription> result)
        {
            if (commandType == typeof(object)) return;
            if (_checksForCommands.ContainsKey(commandType))
            {
                result.AddRange(_checksForCommands[commandType]);
            }

            commandType = commandType.GetTypeInfo().BaseType;
            AppendChecks(commandType, result);
        }

        /// <summary>
        /// Retrieves checks descriptions for particular command
        /// </summary>
        /// <param name="cmb">Command instance</param>
        /// <returns>Descriptions of checks to be generated</returns>
        protected CheckDescription[] GetChecks(CommandBase cmb)
        {
            List<CheckDescription> result = new List<CheckDescription>();
            AppendChecks(cmb.GetType(),result);
            return result.ToArray();
        }

        /// <summary>
        /// Runs the process of validation generation against particular validation generator
        /// </summary>
        /// <param name="commands">Commands set</param>
        /// <param name="generator">Validation generator</param>
        public void Proceed(IEnumerable<CommandBase> commands, IValidationGenerator generator)
        {
            foreach (var commandBase in commands)
            {
                generator.Visit(commandBase,GetChecks(commandBase));
            }
        }

    }
}
