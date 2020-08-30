using System.Reflection;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Testing.Checks;
using Reinforced.Tecture.Tracing;
using Reinforced.Tecture.Tracing.Commands;

namespace Reinforced.Tecture.Testing.BuiltInChecks
{
    public static class Descriptions
    {
        public static void Basic(this ChecksConfigurator<CommandBase> c) => c.Enlist(new AnnotationCheckDescription());
        public static void Basic(this ChecksConfigurator<Comment> c) => c.Enlist(new CommentCheckDescription());

        public static void Basics(this UnitTestGenerator tg)
        {
            tg.For<CommandBase>().Basic();
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

    sealed class CommentCheckDescription : CheckDescription<Comment>
    {
        public override MethodInfo Method =>
            UseMethod((a, c) => CommonChecks.Comment(c.Annotation));
    }
}
