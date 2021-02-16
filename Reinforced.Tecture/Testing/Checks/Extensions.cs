using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Testing.Checks
{
    /// <summary>
    /// Unit test generator extensions
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Obtains checks builder for particular command
        /// </summary>
        /// <typeparam name="TCommand">Command type</typeparam>
        /// <param name="tg">Test generator</param>
        /// <returns>Checks builder</returns>
        public static ChecksBuilderFor<TCommand> For<TCommand>(this ValidationGenerator tg) where TCommand : CommandBase
        {
            return new ChecksBuilderFor<TCommand>(tg._checksForCommands);
        }
    }
}
