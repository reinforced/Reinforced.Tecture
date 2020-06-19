using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Methodics.Orm;

namespace Reinforced.Tecture.Features.Orm.Command.Add
{
    /// <summary>
    /// Addition extensions
    /// </summary>
    public static partial class Extensions
    {
        private static Add AddCore(Write<CommandChannel<Features.Orm.Command.Orm>> channel, object entity)
        {
            if (entity==null)
                throw new TectureOrmFeatureException("Entity going to be added cannot be null");

            var t = entity.GetType();
            var fe = channel.Feature();

            if (!fe.IsSubjectCore(t)) 
                throw new TectureOrmFeatureException($"Entity {entity} is not a subject for addition in corresponding service");

            return channel.Put(new Add()
            {
                EntityType = t,
                Entity = entity
            });
        }
    }
}
