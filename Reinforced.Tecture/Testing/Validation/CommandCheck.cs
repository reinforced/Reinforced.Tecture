using System;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Testing.Validation
{
    /// <summary>
    /// Generic base for command assertion
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public abstract class CommandCheck<TCommand> : ICommandCheck<TCommand> where TCommand : CommandBase
    {
        /// <summary>
        /// Gets error message (called only if command is not valid)
        /// </summary>
        /// <param name="command">Command instance</param>
        /// <returns>Error message</returns>
        protected abstract string GetMessage(TCommand command);

        /// <summary>
        /// Gets whether particular command instance is valid or not
        /// </summary>
        /// <param name="command">Command instance</param>
        /// <returns>True if command is valid, false otherwise</returns>
        protected abstract bool IsActuallyValid(TCommand command);

        /// <inheritdoc cref="ICommandCheck.Assert"/>
        protected virtual void Assert(TCommand command)
        {
            if (!IsValid(command)) 
                throw new TectureCheckException(GetMessage(command));
        }

        /// <inheritdoc cref="ICommandCheck.Assert"/>
        public void Assert(CommandBase command)
        {
            if (command is TCommand cmd) Assert(cmd);
        }

        /// <inheritdoc cref="ICommandCheck.CommandType"/>
        public Type CommandType
        {
            get { return typeof(TCommand); }
        }

        /// <inheritdoc cref="ICommandCheck.IsValid"/>
        public bool IsValid(CommandBase command)
        {
            if (command is TCommand cmd) return IsActuallyValid(cmd);
            return false;
        }
    }
}