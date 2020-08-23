using System;
using System.Collections.Generic;
using System.Reflection;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Testing.Checks
{
    /// <summary>
    /// Implement 
    /// </summary>
    public interface IValidationGenerator
    {
        void Visit(CommandBase command, CheckDescription[] checks);
    }
    public class UnitTestGenerator
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

        protected CheckDescription[] GetChecks(CommandBase cmb)
        {
            List<CheckDescription> result = new List<CheckDescription>();
            AppendChecks(cmb.GetType(),result);
            return result.ToArray();
        }

        public void Proceed(IEnumerable<CommandBase> commands, IValidationGenerator generator)
        {
            foreach (var commandBase in commands)
            {
                generator.Visit(commandBase,GetChecks(commandBase));
            }
        }

    }
}
