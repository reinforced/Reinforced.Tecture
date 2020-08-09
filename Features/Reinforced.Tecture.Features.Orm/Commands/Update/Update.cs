using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Commands.Exact;

namespace Reinforced.Tecture.Features.Orm.Commands.Update
{
    public sealed class Update : CommandBase
    {
        public object Entity { get; internal set; }

        public Type EntityType { get; internal set; }

        public PropertyInfo[] PropertiesToUpdate { get; private set; } = new PropertyInfo[0];

        internal Update(object entity, Type entityType)
        {
            Entity = entity;
            EntityType = entityType;
        }

        internal Update(object entity, Type entityType, LambdaExpression[] properties)
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
