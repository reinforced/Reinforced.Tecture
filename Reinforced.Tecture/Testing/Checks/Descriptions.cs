using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Commands.Exact;
using Reinforced.Tecture.Testing.Generation;
using Reinforced.Tecture.Testing.Stories;
using Reinforced.Tecture.Tracing;

namespace Reinforced.Tecture.Testing.Checks
{
    public static class Descriptions
    {
        public static void Basic(this ChecksConfigurator<CommandBase> c) => c.Enlist(new AnnotationCheckDescription());
        public static void Basic(this ChecksConfigurator<Comment> c) => c.Enlist(new CommentCheckDescription());
        public static void Basic(this ChecksConfigurator<Save> c) => c.Enlist(new SaveCheckDescription());

        public static void Basics(this UnitTestGenerator tg)
        {
            tg.For<CommandBase>().Basic();
            tg.For<Comment>().Basic();
            tg.For<Save>().Basic();
        }
    }

    sealed class AnnotationCheckDescription : CheckDescription<CommandBase>
    {
        public override MethodInfo Method =>
            UseMethod(() => CommonChecks.Annotated(null));

        protected override object[] GetArguments(CommandBase command)
        {
            return new[] { command.Annotation };
        }
    }

    sealed class CommentCheckDescription : CheckDescription<Comment>
    {
        public override MethodInfo Method =>
            UseMethod(() => CommonChecks.Comment(null));

        protected override object[] GetArguments(Comment command)
        {
            return new[] { command.Annotation };
        }
    }

    sealed class SaveCheckDescription : CheckDescription<Save>
    {
        public override MethodInfo Method =>
            UseMethod(() => CommonChecks.Saved());
    }

}
