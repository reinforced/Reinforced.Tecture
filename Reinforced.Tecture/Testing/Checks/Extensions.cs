using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Testing.Checks
{
    public static class Extensions
    {
        public static ChecksConfigurator<TCommand> For<TCommand>(this UnitTestGenerator tg) where TCommand : CommandBase
        {
            return new ChecksConfigurator<TCommand>(tg._checksForCommands);
        }
    }
}
