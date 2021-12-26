using System;
using System.Linq.Expressions;
using Reinforced.Tecture.Channels;
using static Reinforced.Tecture.Aspects.Orm.Orm;

namespace Reinforced.Tecture.Aspects.Orm.Commands.Relate
{
    /// <summary>
    /// Extensions for relate command
    /// </summary>
    public static partial class Extensions
    {
        private static Relate RelateCore(Write<CommandChannel<Command>> channel, object primary, string keySpecifier, object secondary)
        {
            if (primary == null) throw new TectureOrmAspectException("Relation between entity and null is impossible. Use Delete/Derelate instead");
            if (secondary == null) throw new TectureOrmAspectException("Relation between null and entity is impossible. Use Derelate instead");

            var primaryType = primary.GetType();
            var secondaryType = secondary.GetType();
            var fe = channel.Aspect();

            if (!fe.IsSubjectCore(primaryType)) throw new TectureOrmAspectException($"Entity {primary} can not be referenced in corresponding service");
            if (!fe.IsSubjectCore(secondaryType)) throw new TectureOrmAspectException($"Entity {secondary} can not make references in corresponding service");

            return channel.Put(new Relate()
            {
                Primary = primary,
                PrimaryType = primaryType,
                Secondary = secondary,
                SecondaryType = secondaryType,
                ForeignKeySpecifier = keySpecifier
            });
        }

        private static string GetKey(LambdaExpression ex)
        {
            var pex = ex.Body as MemberExpression;
            if (pex==null)
                throw new Exception($"Property lambda expected in {ex}");

            return pex.Member.Name;
        }
    }
}
