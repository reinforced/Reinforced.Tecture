using System;
using System.Linq.Expressions;
using System.Text;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Testing.Stories;
using Reinforced.Tecture.Testing.Validation;
using Reinforced.Tecture.Tracing;

namespace Reinforced.Tecture.Testing.Generation
{
    public static class Extensions
    {
        public static ChecksConfigurator<TCommand> For<TCommand>(this UnitTestGenerator tg) where TCommand : CommandBase
        {
            return new ChecksConfigurator<TCommand>(tg._checksForCommands);
        }
    }
}
