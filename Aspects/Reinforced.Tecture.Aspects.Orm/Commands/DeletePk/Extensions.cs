using System;
using Reinforced.Tecture.Aspects.Orm.PrimaryKey;
using Reinforced.Tecture.Channels;

namespace Reinforced.Tecture.Aspects.Orm.Commands.DeletePk
{
    class DeletePkOperationBase
    {
        internal Write Write;
        internal Type EntityType;
    }
    class DeletePkOperation<T> : DeletePkOperationBase, IPrimaryKeyOperation<DeletePk, T>
    {

    }

    /// <summary>
    /// Extensions for Delete by Primary key command
    /// </summary>
    public static partial class Extensions
    {
        private static DeletePk DeletePkCore(Write channel, Type entityType, params object[] keyValues)
        {
            var f = channel.PleaseAspect<Command>();
            if (!f.IsSubjectCore(entityType))
                throw new TectureOrmAspectException($"Entity {entityType} is not a subject for deletion in corresponding service");

            return channel.Put(new DeletePk()
            {
                EntityType = entityType,
                KeyValues = keyValues
            });
        }
    }
}
