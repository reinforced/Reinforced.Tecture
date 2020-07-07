using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Reinforced.Tecture.Channels;

namespace Reinforced.Tecture.Features.Orm.Command.Derelate
{
    public static partial class Extensions
    {
        private static Derelate DeRelateCore(Write<CommandChannel<Features.Orm.Command.Orm>> channel, object primary, string keySpecifier, object secondary)
        {
            if (primary == null) throw new TectureOrmFeatureException("Derelation between entity and null is impossible");
            if (secondary == null) throw new TectureOrmFeatureException("Derelation between null and entity is impossible");

            var primaryType = primary.GetType();
            var secondaryType = primary.GetType();
            var fe = channel.Feature();

            if (!fe.IsSubjectCore(primaryType)) throw new TectureOrmFeatureException($"Entity {primary} can not be de-related in corresponding service");
            if (!fe.IsSubjectCore(secondaryType)) throw new TectureOrmFeatureException($"Entity {secondary} can not de-relate anything in corresponding service");

            return channel.Put(new Derelate()
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
            if (pex == null)
                throw new Exception($"Property lambda expected in {ex}");

            return pex.Member.Name;
        }
    }
}
