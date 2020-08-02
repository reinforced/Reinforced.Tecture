using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Features.SqlStroke.Reveal;
using Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor;

namespace Reinforced.Tecture.Features.SqlStroke
{
    public class StrokeFeatureBase
    {
        private readonly IStrokeRuntime _runtime;
        
        internal bool CheckTypes(Type[] usedTypes)
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

        internal StrokeProcessor GetProcessor(Type[] usedTypes)
        {
            ThrowCheckTypes(usedTypes);
           
            return new StrokeProcessor(_runtime.Mapper, GetQueryFiller());
        }

        private HashSet<Type> _types = null;

        protected StrokeFeatureBase(IStrokeRuntime runtime)
        {
            _runtime = runtime;
        }

        protected QueryFiller GetQueryFiller()
        {
            return new QueryFiller();
        }

        internal HashSet<Type> Types
        {
            get { return _types ?? (_types = new HashSet<Type>(_runtime.ServingTypes)); }
        }
    }
}
