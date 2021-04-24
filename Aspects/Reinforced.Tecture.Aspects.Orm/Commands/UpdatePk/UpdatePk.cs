using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Reinforced.Tecture.Cloning;
using Reinforced.Tecture.Commands;

namespace Reinforced.Tecture.Aspects.Orm.Commands.UpdatePk
{
    /// <summary>
    /// Update-by-primary key command
    /// </summary>
    [CommandCode("UPK")]
    public class UpdatePk : CommandBase
    {
        internal UpdatePk(Dictionary<PropertyInfo, object> updateValues)
        {
            _updateValues = updateValues;
        }

        /// <summary>
        /// Gets primary key values
        /// </summary>
        public object[] KeyValues { get; internal set; }

        /// <summary>
        /// Gets type of entity that is going to be updated
        /// </summary>
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
        public Dictionary<string, object> UpdateValuesStringKeys
        {
            get { return _updateValues.ToDictionary(x => x.Key.Name, x => x.Value); }
        }

        /// <summary>
        /// Describes actions that are being performed within command
        /// </summary>
        /// <param name="tw">Log writer</param>
        public override void Describe(TextWriter tw)
        {
            base.Describe(tw);
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
            var r = new UpdatePk(_updateValues.ToDictionary(x=>x.Key,x=>x.Value.DeepClone()))
            {
                KeyValues = KeyValues.Select(x => x.DeepClone()).ToArray(),
                EntityType = EntityType
            };
            return r;
        }
    }
}
