using System;
using System.Linq.Expressions;
using Reinforced.Tecture.Channels;

namespace Reinforced.Tecture.Features.Orm.Commands.Relate
{
    public static partial class Extensions
    {
        private static Relate RelateCore(Write<CommandChannel<Command>> channel, object primary, string keySpecifier, object secondary)
        {
            if (primary == null) throw new TectureOrmFeatureException("Relation between entity and null is impossible. Use Delete/Derelate instead");
            if (secondary == null) throw new TectureOrmFeatureException("Relation between null and entity is impossible. Use Derelate instead");

            var primaryType = primary.GetType();
            var secondaryType = secondary.GetType();
            var fe = channel.Feature();

            if (!fe.IsSubjectCore(primaryType)) throw new TectureOrmFeatureException($"Entity {primary} can not be referenced in corresponding service");
            if (!fe.IsSubjectCore(secondaryType)) throw new TectureOrmFeatureException($"Entity {secondary} can not make references in corresponding service");

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
