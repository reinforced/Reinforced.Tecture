using System;
using System.Collections.Generic;
using System.Reflection;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Testing.Checks.ParameterDescription;

namespace Reinforced.Tecture.Testing.Checks
{
    /// <summary>
    /// Check description for unit test generator
    /// </summary>
    public abstract class CheckDescription
    {
        internal CheckDescription() { }

        /// <summary>
        /// Method to be invoked to create particular check
        /// </summary>
        public abstract MethodInfo Method { get; }

        /// <summary>
        /// Additional usings to be added to test class
        /// </summary>
        public virtual IEnumerable<string> AdditionalUsings
        {
            get
            {
                yield break;
            }
        }

        /// <summary>
        /// Gets list of check factory method type parameters from particular command
        /// </summary>
        /// <param name="command">Command instance</param>
        /// <returns>Types array to parametrize check factory by</returns>
        public abstract Type[] MethodTypeArgumentsEvaluator(CommandBase command);

        /// <summary>
        /// Gets list of parameters to be passed to check factory method
        /// </summary>
        /// <param name="commandBase">Command instance</param>
        /// <returns>Parameters set</returns>
        public abstract IEnumerable<ICheckParameter> GetCheckParameters(CommandBase commandBase);

        /// <summary>
        /// Gets whether particular check is needed on supplied command
        /// </summary>
        /// <param name="command">Command instance</param>
        /// <returns>True if this check is needed on the command, false otherwise</returns>
        public abstract bool IsNeeded(CommandBase command);

        internal static readonly Type[] EmptyTypes = new Type[0];
        internal static readonly object[] EmptyObjects = new object[0];
    }

    
}