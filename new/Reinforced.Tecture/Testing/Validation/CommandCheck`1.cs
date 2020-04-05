using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Testing.Validation
{
    /// <summary>
    /// Generic base for command assertion
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public abstract class CommandCheck<TCommand> : CommandCheck where TCommand : CommandBase
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

        protected virtual void Assert(TCommand command)
        {
            if (!IsValid(command)) throw new AssertionException(GetMessage(command));
        }

        public override void Assert(CommandBase command)
        {
            if (command is TCommand cmd) Assert(cmd);
        }

        public override Type CommandType
        {
            get { return typeof(TCommand); }
        }

        public override bool IsValid(CommandBase command)
        {
            if (command is TCommand) return IsValid((TCommand)command);
            return false;
        }
    }
}
