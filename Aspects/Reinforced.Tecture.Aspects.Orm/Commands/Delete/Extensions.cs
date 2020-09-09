using Reinforced.Tecture.Channels;

namespace Reinforced.Tecture.Aspects.Orm.Commands.Delete
{
    /// <summary>
    /// Extensions for Delete command
    /// </summary>
    public static partial class Extensions
    {
        private static Delete DeleteCore(Write<CommandChannel<Command>> channel, object entity)
        {
            if (entity == null)
                throw new TectureOrmAspectException("Entity going to be deleted cannot be null");

            var t = entity.GetType();
            var f = channel.Aspect();
            if (!f.IsSubjectCore(t))
                throw new TectureOrmAspectException($"Entity {entity} is not a subject for deletion in corresponding service");

            return channel.Put(new Delete()
            {
                EntityType = t,
                Entity = entity
            });
        }
    }
}
