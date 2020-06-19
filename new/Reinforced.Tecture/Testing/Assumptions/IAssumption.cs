using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Testing.Assumptions
{
    public interface IAssumption : CommandRunner
    {
        Type CommandType { get; }

        bool Should(CommandBase cmd);

        void Assume(CommandBase cmd);
    }
}
