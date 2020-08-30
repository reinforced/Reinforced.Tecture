using System;
using System.Collections.Generic;
using Reinforced.Tecture.Features.SqlStroke.Commands;
using Reinforced.Tecture.Features.SqlStroke.Parse;
using Reinforced.Tecture.Features.SqlStroke.Reveal;
using Reinforced.Tecture.Features.SqlStroke.Reveal.LanguageInterpolate;
using Reinforced.Tecture.Features.SqlStroke.Reveal.SchemaInterpolate;
using Reinforced.Tecture.Features.SqlStroke.Reveal.Visit;
using Reinforced.Tecture.Query;

namespace Reinforced.Tecture.Features.SqlStroke.Infrastructure
{
    public class StrokeToolingWrapper
    {
        private readonly IStrokeRuntime _runtime;
        internal Auxilary _aux;
        private bool CheckTypes(Type[] usedTypes)
        {
            foreach (var usedType in usedTypes)
            {
                if (!_types.Contains(usedType)) return false;
            }

            return true;
        }

        private IEnumerable<Type> NotSuitableTypes(Type[] usedTypes)
        {
            foreach (var usedType in usedTypes)
            {
                if (!_types.Contains(usedType)) yield return usedType;
            }
        }

        internal void ThrowCheckTypes(Type[] usedTypes)
        {
            if (!CheckTypes(usedTypes))
                throw new SqlStrokeException($"Sql Stroke for channel '{_runtime.Channel.Name}' does not work with following types: {string.Join(", ", NotSuitableTypes(usedTypes))} ");

        }

        private readonly HashSet<Type> _types;

        internal StrokeToolingWrapper(IStrokeRuntime runtime, Auxilary aux, HashSet<Type> types)
        {
            _runtime = runtime;
            _aux = aux;
            _types = types;
        }

        public InterpolatedQuery Compile(Sql command)
        {
            if (_aux.IsEvaluationNeeded || _aux.IsCommandRunNeeded)
            {
                return command.StrokeExpression
                    .ParseStroke()
                    .VisitStroke(_runtime.Mapper.IsEntityType)
                    .LanguageInterpolateStroke(_runtime.GetLanguageInterpolator())
                    .SchemaInterpolateStroke(_runtime.GetSchemaInterpolator());
            }
            else
            {
                return command.Preview;
            }
        }
    }
}
