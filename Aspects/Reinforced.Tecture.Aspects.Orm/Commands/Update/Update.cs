using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Reinforced.Tecture.Aspects.Orm.PrimaryKey;
using Reinforced.Tecture.Cloning;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Aspects.Orm.Commands.Update
{
    /// <summary>
    /// Entity update command
    /// </summary>
    [CommandCode("UPD")]
    public class Update : CommandBase
    {
        internal Update() { }
        /// <summary>
        /// Gets entity that is going to be updated
        /// </summary>
        [Validated("entity to be update")]
        public object Entity { get; internal set; }

        /// <summary>
        /// Gets type of entity that is going to be updated
        /// </summary>
        [Validated("type of entity to update")]
        public Type EntityType { get; internal set; }


        private readonly Dictionary<PropertyInfo, object> _updateValues = new Dictionary<PropertyInfo, object>();

        /// <summary>
        /// Properties that are going to be updated
        /// </summary>
        public IReadOnlyDictionary<PropertyInfo, object> UpdateValues
        {
            get { return _updateValues; }
        }

        /// <summary>
        /// Properties that are going to be updated (with string key for quick check)
        /// </summary>
        [Validated("updated values")]
        public Dictionary<string, object> UpdateValuesStringKeys
        {
            get { return _updateValues.ToDictionary(x=>x.Key.Name,x=>x.Value); }
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

        /// <inheritdoc />
        public override void Describe(TextWriter tw)
        {
            base.Describe(tw);
            if (!string.IsNullOrEmpty(Annotation)) return;

            string properties = string.Join(", ", _updateValues.Keys.Select(d => d.Name));

            var description = $"entity of type {EntityType.Name}";
            if (Entity is IDescriptive e) description = e.Describe();
            if (_updateValues.Count > 0) description = $"{properties} of {description}";

            tw.Write($"Update {description}");
            if (Debug != null) tw.Write($" ({Debug.Location})");
        }

        /// <inheritdoc />
        protected override CommandBase DeepCloneForTracing()
        {
            var r = new Update(DeepCloner.DeepClone(Entity), EntityType);
            foreach (var updateValue in _updateValues)
            {
                r.RegisterUpdate(updateValue.Key, DeepCloner.DeepClone(updateValue.Value));
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
