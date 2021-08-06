using System;
using System.Reflection;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Testing.Checks;
using Reinforced.Tecture.Tracing.Commands;

namespace Reinforced.Tecture.Testing.BuiltInChecks
{
    /// <summary>
    /// Descriptions
    /// </summary>
    public static class Descriptions
    {
        /// <summary>
        /// Basic checks for all commands
        /// </summary>
        /// <param name="c">Checks builder</param>
        public static void Annotated(this ChecksBuilderFor<CommandBase> c) => c.Enlist(new AnnotationCheckDescription());

        /// <summary>
        /// Basic checks for all commands
        /// </summary>
        /// <param name="c">Checks builder</param>
        public static void Type(this ChecksBuilderFor<CommandBase> c) => c.Enlist(new ChannelTypeCheckDescription());

        /// <summary>
        /// Basic checks for comment commands
        /// </summary>
        /// <param name="c">Checks builder</param>
        public static void Basic(this ChecksBuilderFor<Comment> c) => c.Enlist(new CommentCheckDescription());

        /// <summary>
        /// Basic Tecture checks
        /// </summary>
        /// <param name="tg">Test generator</param>
        public static void Basics(this ValidationGenerator tg)
        {
            tg.For<CommandBase>().Type();
            tg.For<CommandBase>().Annotated();
            tg.For<Comment>().Basic();
        }
    }

    sealed class AnnotationCheckDescription : CheckDescription<CommandBase>
    {
        public override MethodInfo Method =>
            UseMethod((a, c) => CommonChecks.Annotated(c.Annotation));

        protected override bool IsCheckNeeded(CommandBase command)
        {
            return !string.IsNullOrEmpty(command.Annotation);
        }
    }

    sealed class ChannelTypeCheckDescription : CheckDescription<CommandBase>
    {
        public override MethodInfo Method =>
            UseMethod((a, c) => CommonChecks.To<NoChannel>());

        /// <summary>
        /// Gets list of check factory method type parameters from particular command
        /// </summary>
        /// <param name="command">Command instance</param>
        /// <returns>Types array to parametrize check factory by</returns>
        protected override Type[] GetTypeArguments(CommandBase command)
        {
            return new[] {command.Channel};
        }

        protected override bool IsCheckNeeded(CommandBase command)
        {
            return command.Channel != null;
        }
    }

    sealed class CommentCheckDescription : CheckDescription<Comment>
    {
        public override MethodInfo Method =>
            UseMethod((a, c) => CommonChecks.Comment(c.Annotation));
    }
}
