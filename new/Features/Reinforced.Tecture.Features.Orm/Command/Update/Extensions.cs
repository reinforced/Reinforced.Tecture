using System;
using System.Linq.Expressions;
using Reinforced.Tecture.Channels;

namespace Reinforced.Tecture.Features.Orm.Command.Update
{
    public static partial class Extensions
    {
        private static Update UpdateCore(Write<CommandChannel<Orm>> channel, object entity)
        {
            if (entity == null)
                throw new TectureOrmFeatureException("Entity going to be added cannot be null");

            var t = entity.GetType();
            var fe = channel.Feature();

            if (!fe.IsSubjectCore(t))
                throw new TectureOrmFeatureException($"Entity {entity} is not a subject for addition in corresponding service");

            return channel.Put(new Update(entity,t));
        }

        private static Update UpdateCore(Write<CommandChannel<Orm>> ppl, object entity, LambdaExpression[] properties)
        {
            if (entity == null)
                throw new TectureOrmFeatureException("Entity going to be updated cannot be null");

            var t = entity.GetType();
            var fe = ppl.Feature();

            if (!fe.IsSubjectCore(t))
                throw new TectureOrmFeatureException($"Entity {entity} is not a subject for updating in corresponding service");

            return ppl.Put(new Update(entity, t, properties));
        }
    }
}
