using System;
using System.Collections.Generic;
using Reinforced.Tecture.Aspects.DirectSql.Commands;
using Reinforced.Tecture.Aspects.DirectSql.Parse;
using Reinforced.Tecture.Aspects.DirectSql.Reveal;
using Reinforced.Tecture.Aspects.DirectSql.Reveal.LanguageInterpolate;
using Reinforced.Tecture.Aspects.DirectSql.Reveal.SchemaInterpolate;
using Reinforced.Tecture.Aspects.DirectSql.Reveal.Visit;
using Reinforced.Tecture.Queries;
using Reinforced.Tecture.Testing;

namespace Reinforced.Tecture.Aspects.DirectSql.Infrastructure
{
    public class SqlToolingWrapper
    {
        private readonly IStrokeRuntime _runtime;
        internal TestingContext _aux;
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
                throw new DirectSqlException($"Sql Stroke for channel '{_runtime.Channel.Name}' does not work with following types: {string.Join(", ", NotSuitableTypes(usedTypes))} ");

        }

        private readonly HashSet<Type> _types;

        internal SqlToolingWrapper(IStrokeRuntime runtime, TestingContext aux, HashSet<Type> types)
        {
            _runtime = runtime;
            _aux = aux;
            _types = types;
        }

        public InterpolatedQuery Compile(Sql command)
        {
            if (!_aux.ProvidesTestData)
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
