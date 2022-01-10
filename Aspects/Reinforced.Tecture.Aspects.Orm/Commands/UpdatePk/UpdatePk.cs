using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Reinforced.Tecture.Cloning;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Aspects.Orm.Commands.UpdatePk
{
    /// <summary>
    /// Update-by-primary key command
    /// </summary>
    public class UpdatePk : CommandBase
    {
        public override string Code => "UPK";
        internal UpdatePk(Dictionary<PropertyInfo, object> updateValues)
        {
            _updateValues = updateValues;
        }

        /// <summary>
        /// Gets primary key values
        /// </summary>
        [Validated("primary key")]
        public object[] KeyValues { get; internal set; }

        /// <summary>
        /// Gets type of entity that is going to be updated
        /// </summary>
        [Validated("type of entity to update")]
        public Type EntityType { get; internal set; }

        private readonly Dictionary<PropertyInfo, object> _updateValues;

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
            get { return _updateValues.ToDictionary(x => x.Key.Name, x => x.Value); }
        }

        protected override string ToStringActually()
        {
            var sb = new StringBuilder();
            using (var tw = new StringWriter(sb))
            {
                Describe(tw);
                tw.Flush();
            }

            return sb.ToString();
        }

        /// <inheritdoc cref="CommandBase" />
        private void Describe(TextWriter tw)
        {
            if (!string.IsNullOrEmpty(Annotation)) return;
            
            string properties = string.Join(", ", _updateValues.Keys.Select(d => d.Name));

            var description = $"entity of type {EntityType.Name}";
            
            if (_updateValues.Count > 0) description = $"{properties} of {description}";

            tw.Write($"Update {description}");
            if (Debug != null) tw.Write($" ({Debug.Location})");
        }

        internal void RegisterUpdate(PropertyInfo pi, object valueToSet)
        {
            _updateValues[pi] = valueToSet;
            foreach (var commandBase in KnownClones)
            {
                if (commandBase is UpdatePk u)
                {
                    u.RegisterUpdate(pi, valueToSet);
                }
            }
        }

        /// <summary>
        /// Clones command for tracing purposes
        /// </summary>
        /// <returns>Command clone</returns>
        protected override CommandBase DeepCloneForTracing()
        {
            var r = new UpdatePk(_updateValues.ToDictionary(x=>x.Key,x=>DeepCloner.DeepClone(x.Value)))
            {
                KeyValues = KeyValues.Select(x => DeepCloner.DeepClone(x)).ToArray(),
                EntityType = EntityType
            };
            return r;
        }
    }
}
