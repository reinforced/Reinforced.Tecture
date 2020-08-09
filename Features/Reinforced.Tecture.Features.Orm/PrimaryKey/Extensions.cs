using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Features.Orm.Commands.Add;

namespace Reinforced.Tecture.Features.Orm.PrimaryKey
{
    public static partial class Extensions
    {
        public static T Key<T>(this IAddition<IPrimaryKey<T>> cmd)
        {
            if (cmd is Add c)
            {
                if (c.PkData == null)
                    throw new TectureOrmFeatureException($"Primary key of just added {c.EntityType} could not be obtained. Probably you forgot to do that after save?");

                return (T)c.PkData;
            }

            throw new TectureOrmFeatureException($".Key call is valid only on add command");
        }

        public static Expected<T> Expect<T>(this IAddition<IPrimaryKey<T>> cmd)
        {
            return new Expected<T>(cmd);
        }
    }
}
