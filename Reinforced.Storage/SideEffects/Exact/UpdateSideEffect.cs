using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Reinforced.Storage.SideEffects.Exact
{
    /// <summary>
    /// Side effect for updating entity
    /// </summary>
    [SideEffectCode("UPD")]
    public class UpdateSideEffect : SideEffectBase
    {
        public object Entity { get; internal set; }

        public Type EntityType { get; internal set; }

        public PropertyInfo[] PropertiesToUpdate { get; private set; } = new PropertyInfo[0];

        internal UpdateSideEffect(object entity, Type entityType)
        {
            Entity = entity;
            EntityType = entityType;
        }

        internal UpdateSideEffect(object entity, Type entityType, LambdaExpression[] properties)
        {
            Entity = entity;
            EntityType = entityType;
            PropertiesToUpdate = properties.Select(ReflectionCache.ParsePropertyLambda).ToArray();
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

            string properties = string.Join(", ", PropertiesToUpdate.Select(d => d.Name));

            var description = $"entity of type {EntityType.Name}";
            if (Entity is IDescriptive e) description = e.Descibe();
            if (PropertiesToUpdate.Length > 0) description = $"{properties} of {description}";

            if (Annotation != null) description = Annotation;
            tw.Write($"Update {description}");
            if (Debug != null) tw.Write($" ({Debug.Location})");
        }
    }
}
