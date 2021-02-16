using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Testing.Validation
{
    /// <summary>
    /// Do not inherit this interface, use <see cref="CommandCheck{TCommand}"/> instead
    /// </summary>
    public interface ICommandCheck
    {
        /// <summary>
        /// Checks whether command instance is valid. Throws exception if it is not.
        /// </summary>
        /// <param name="command">Command instance</param>
        void Assert(CommandBase command);

        /// <summary>
        /// Checks whether command instance is valid. Returns false if it is not.
        /// </summary>
        /// <param name="command">Command instance</param>
        /// <returns>True when command is valid, false otherwise</returns>
        bool IsValid(CommandBase command);

        /// <summary>
        /// Gets type of command that is being checked within this particular check
        /// </summary>
        Type CommandType { get; }

    }

    /// <summary>
    /// Do not inherit this interface, use <see cref="CommandCheck{TCommand}"/> instead
    /// </summary>
    public interface ICommandCheck<in TCommand> : ICommandCheck
    {

    }
}
