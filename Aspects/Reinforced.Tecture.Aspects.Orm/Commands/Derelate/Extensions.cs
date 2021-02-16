using System;
using System.Linq.Expressions;
using Reinforced.Tecture.Channels;

namespace Reinforced.Tecture.Aspects.Orm.Commands.Derelate
{
    /// <summary>
    /// Extensions for derelate command
    /// </summary>
    public static partial class Extensions
    {
        private static Derelate DerelateCore(Write<CommandChannel<Command>> channel, object primary, string keySpecifier)
        {
            if (primary == null) throw new TectureOrmAspectException("Derelation between entity and null is impossible");
            
            var primaryType = primary.GetType();
            var fe = channel.Aspect();

            if (!fe.IsSubjectCore(primaryType)) throw new TectureOrmAspectException($"Entity {primary} can not be de-related in corresponding service");

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
