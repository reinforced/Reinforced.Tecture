using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Features.Orm.PrimaryKey;

namespace Reinforced.Tecture.Features.Orm.Commands.DeletePk
{
    public static partial class Extensions
    {
        struct DeletePkOperation<T> : IPrimaryKeyOperation<DeletePk, T>
        {
            internal Write Write;
            internal Type EntityType;
        }

        /// <summary>
        /// Deletes 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channel"></param>
        /// <returns></returns>
        public static IPrimaryKeyOperation<DeletePk, T> Delete<T>(this Write<CommandChannel<Command>> channel)
        {
            return new DeletePkOperation<T>() { Write = channel, EntityType = typeof(T) };
        }

        private static DeletePk DeletePkCore(Write channel, Type entityType, params object[] keyValues)
        {
            var f = channel.PleaseFeature<Command>();
            if (!f.IsSubjectCore(entityType))
                throw new TectureOrmFeatureException($"Entity {entityType} is not a subject for deletion in corresponding service");

            return channel.Put(new DeletePk()
            {
                EntityType = entityType,
                KeyValues = keyValues
            });
        }
    }
}
