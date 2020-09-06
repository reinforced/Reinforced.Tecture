using System.Linq.Expressions;
using Reinforced.Tecture.Channels;

namespace Reinforced.Tecture.Features.Orm.Commands.Update
{
    public static partial class Extensions
    {
        private static Update<T> UpdateCore<T>(Write<CommandChannel<Command>> channel, T entity)
        {
            if (entity == null)
                throw new TectureOrmFeatureException("Entity going to be added cannot be null");

            var t = entity.GetType();
            var fe = channel.Feature();

            if (!fe.IsSubjectCore(t))
                throw new TectureOrmFeatureException($"Entity {entity} is not a subject for addition in corresponding service");

            return channel.Put(new Update<T>(entity));
        }
    }
}
