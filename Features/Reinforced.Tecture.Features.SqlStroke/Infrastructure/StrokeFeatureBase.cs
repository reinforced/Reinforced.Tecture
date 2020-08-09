using System;
using System.Collections.Generic;
using Reinforced.Tecture.Features.SqlStroke.Reveal.LanguageInterpolate;

namespace Reinforced.Tecture.Features.SqlStroke.Infrastructure
{
    public class StrokeFeatureBase
    {
        private readonly IStrokeRuntime _runtime;

        private bool CheckTypes(Type[] usedTypes)
        {
            foreach (var usedType in usedTypes)
            {
                if (!Types.Contains(usedType)) return false;
            }

            return true;
        }

        private IEnumerable<Type> NotSuitableTypes(Type[] usedTypes)
        {
            foreach (var usedType in usedTypes)
            {
                if (!Types.Contains(usedType)) yield return usedType;
            }
        }

        internal void ThrowCheckTypes(Type[] usedTypes)
        {
            if (!CheckTypes(usedTypes))
                throw new SqlStrokeException($"Sql Stroke for channel '{_runtime.Channel.Name}' does not work with following types: {string.Join(", ", NotSuitableTypes(usedTypes))} ");

        }

        private HashSet<Type> _types = null;

        protected StrokeFeatureBase(IStrokeRuntime runtime)
        {
            _runtime = runtime;
        }
        internal HashSet<Type> Types
        {
            get { return _types ?? (_types = new HashSet<Type>(_runtime.ServingTypes)); }
        }
    }
}
