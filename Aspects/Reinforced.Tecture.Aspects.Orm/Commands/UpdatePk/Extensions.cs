using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Reinforced.Tecture.Aspects.Orm.PrimaryKey;
using Reinforced.Tecture.Channels;

namespace Reinforced.Tecture.Aspects.Orm.Commands.UpdatePk
{
    class UpdatePkOperationBase
    {
        internal Write Write;
        internal Type EntityType;
        internal readonly Dictionary<PropertyInfo, object> _updateValues = new Dictionary<PropertyInfo, object>();
        internal void RegisterUpdate(PropertyInfo pi, object valueToSet)
        {
            _updateValues[pi] = valueToSet;
        }

    }
    class UpdatePkOperation<T> : UpdatePkOperationBase, IPrimaryKeyOperation<UpdatePk, T>
    {
        
        
    }
    public static partial class Extensions
    {
        /// <summary>
        /// Update exact field of the entity
        /// </summary>
        /// <typeparam name="TVal">Property type</typeparam>
        /// <param name="property">Property to update</param>
        /// <param name="value">Value to set updated property to</param>
        /// <returns>Fluent</returns>
        public static IPrimaryKeyOperation<UpdatePk, T> Set<T,TVal>(this IPrimaryKeyOperation<UpdatePk,T> op, Expression<Func<T, TVal>> property, TVal value)
        {
            var upd = op as UpdatePkOperationBase;
            var prop = property.AsPropertyExpression();
            upd.RegisterUpdate(prop, value);
            return op;
        }

        private static UpdatePk UpdatePkCore(Write channel, Type entityType, Dictionary<PropertyInfo, object> values, params object[] keyValues)
        {
            var f = channel.PleaseAspect<Command>();
            if (!f.IsSubjectCore(entityType))
                throw new TectureOrmAspectException($"Entity {entityType} is not a subject for updating in corresponding channel");

            return channel.Put(new UpdatePk(values)
            {
                EntityType = entityType,
                KeyValues = keyValues,

            });
        }
    }
}
