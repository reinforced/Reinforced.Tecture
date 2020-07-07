using Reinforced.Tecture.Channels;

namespace Reinforced.Tecture.Features.Orm.Command.Delete
{
    /// <summary>
    /// Delete extensions
    /// </summary>
    public static partial class Extensions
    {
        private static Delete DeleteCore(Write<CommandChannel<Features.Orm.Command.Orm>> channel, object entity)
        {
            if (entity == null)
                throw new TectureOrmFeatureException("Entity going to be deleted cannot be null");

            var t = entity.GetType();
            var f = channel.Feature();
            if (!f.IsSubjectCore(t))
                throw new TectureOrmFeatureException($"Entity {entity} is not a subject for deletion in corresponding service");

            return channel.Put(new Delete()
            {
                EntityType = t,
                Entity = entity
            });
        }
    }
}
