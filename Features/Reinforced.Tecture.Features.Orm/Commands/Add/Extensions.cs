using System.Data;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Features.Orm.PrimaryKey;

namespace Reinforced.Tecture.Features.Orm.Commands.Add
{
    /// <summary>
    /// Addition extensions
    /// </summary>
    public static partial class Extensions
    {
        private static Add<T> AddCore<T>(Write<CommandChannel<Command>> channel, T entity)
        {
            if (entity==null)
                throw new TectureOrmFeatureException("Entity going to be added cannot be null");

            var t = entity.GetType();
            var fe = channel.Feature();

            if (!fe.IsSubjectCore(t)) 
                throw new TectureOrmFeatureException($"Entity {entity} is not a subject for addition in corresponding service");

            return channel.Put(new Add<T>(entity));
        }
    }
}
