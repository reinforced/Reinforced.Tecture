using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Testing.Validation
{
    /// <summary>
    /// Assertion to be made about particular command in queue
    /// </summary>
    public abstract class CommandCheck
    {
        /// <summary>
        /// Determie whether command is valid in this context
        /// </summary>
        /// <param name="command">Command</param>
        /// <returns>True if command is valid, false otherwise</returns>
        public abstract bool IsValid(CommandBase command);

        /// <summary>
        /// Determine whether command is valid and throw exception if not
        /// </summary>
        /// <param name="command"></param>
        public abstract void Assert(CommandBase command);

        /// <summary>
        /// Gets asserting command type
        /// </summary>
        public abstract Type CommandType { get; }

        /// <summary>
        /// Gets reference to testing environment
        /// </summary>
        public TestingEnvironment Environment { get; internal set; }
    }
}
