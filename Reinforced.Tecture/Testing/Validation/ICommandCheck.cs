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
        void Assert(CommandBase command);
        bool IsValid(CommandBase command);
        Type CommandType { get; }

    }

    /// <summary>
    /// Do not inherit this interface, use <see cref="CommandCheck{TCommand}"/> instead
    /// </summary>
    public interface ICommandCheck<in TCommand> : ICommandCheck
    {

    }
}
