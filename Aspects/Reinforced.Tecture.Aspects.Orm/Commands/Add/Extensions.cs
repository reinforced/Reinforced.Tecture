using Reinforced.Tecture.Channels;

namespace Reinforced.Tecture.Aspects.Orm.Commands.Add
{
    /// <summary>
    /// Extensions for Add command
    /// </summary>
    public static partial class Extensions
    {
        private static Add<T> AddCore<T>(Write<CommandChannel<Orm.Command>> channel, T entity)
        {
            if (entity==null)
                throw new TectureOrmAspectException("Entity going to be added cannot be null");

            var t = entity.GetType();
            var fe = channel.Aspect();

            if (!fe.IsSubjectCore(t)) 
                throw new TectureOrmAspectException($"Entity {entity} is not a subject for addition in corresponding service");

            return channel.Put(new Add<T>(entity));
        }
    }
}
