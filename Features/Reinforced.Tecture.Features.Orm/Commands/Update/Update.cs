using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Reinforced.Tecture.Cloning;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Features.Orm.PrimaryKey;

namespace Reinforced.Tecture.Features.Orm.Commands.Update
{
    /// <summary>
    /// Entity update command
    /// </summary>
    public class Update : CommandBase
    {
        /// <summary>
        /// Entity that is going to be updated
        /// </summary>
        public object Entity { get; internal set; }

        /// <summary>
        /// Type of entity that is going to be updated
        /// </summary>
        public Type EntityType { get; internal set; }


        private readonly Dictionary<PropertyInfo, object> _updateValues = new Dictionary<PropertyInfo, object>();

        /// <summary>
        /// Properties that are going to be updated
        /// </summary>
        public IReadOnlyDictionary<PropertyInfo, object> UpdateValues
        {
            get { return _updateValues; }
        }

        internal Update(object entity, Type entityType)
        {
            Entity = entity;
            EntityType = entityType;
        }

        internal void RegisterUpdate(PropertyInfo pi, object valueToSet)
        {
            _updateValues[pi] = valueToSet;
            foreach (var commandBase in KnownClones)
            {
                if (commandBase is Update u)
                {
                    u.RegisterUpdate(pi, valueToSet);
                }
            }
        }

        /// <summary>
        /// Describes actions that are being performed within side effect
        /// </summary>
        /// <param name="tw"></param>
        public override void Describe(TextWriter tw)
        {
            if (!string.IsNullOrEmpty(Annotation))
            {
                tw.Write(Annotation);
                return;
            }

            string properties = string.Join(", ", _updateValues.Keys.Select(d => d.Name));

            var description = $"entity of type {EntityType.Name}";
            if (Entity is IDescriptive e) description = e.Descibe();
            if (_updateValues.Count > 0) description = $"{properties} of {description}";

            if (Annotation != null) description = Annotation;
            tw.Write($"Update {description}");
            if (Debug != null) tw.Write($" ({Debug.Location})");
        }

        /// <summary>
        /// Clones command for tracing purposes
        /// </summary>
        /// <returns>Command clone</returns>
        protected override CommandBase DeepCloneForTracing()
        {
            var r = new Update(Entity.DeepClone(), EntityType);
            foreach (var updateValue in _updateValues)
            {
                r.RegisterUpdate(updateValue.Key, updateValue.Value.DeepClone());
            }

            return r;
        }
    }

    /// <summary>
    /// Entity update command
    /// </summary>
    public class Update<T> : Update
    {
        internal Update(T entity) : base(entity, typeof(T))
        {
        }

        /// <summary>
        /// Update exact field of the entity
        /// </summary>
        /// <typeparam name="TVal">Property type</typeparam>
        /// <param name="property">Property to update</param>
        /// <param name="value">Value to set updated property to</param>
        /// <returns>Fluent</returns>
        public Update<T> Set<TVal>(Expression<Func<T, TVal>> property, TVal value)
        {
            var prop = property.AsPropertyExpression();
            RegisterUpdate(prop, value);
            return this;
        }
    }
}
