using System;
using System.Linq.Expressions;
using Reinforced.Tecture.Channels;

namespace Reinforced.Tecture.Features.Orm.Commands.Derelate
{
    public static partial class Extensions
    {
        private static Derelate DeRelateCore(Write<CommandChannel<Command>> channel, object primary, string keySpecifier)
        {
            if (primary == null) throw new TectureOrmFeatureException("Derelation between entity and null is impossible");
            
            var primaryType = primary.GetType();
            var fe = channel.Feature();

            if (!fe.IsSubjectCore(primaryType)) throw new TectureOrmFeatureException($"Entity {primary} can not be de-related in corresponding service");

            return channel.Put(new Derelate()
            {
                Primary = primary,
                PrimaryType = primaryType,
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
