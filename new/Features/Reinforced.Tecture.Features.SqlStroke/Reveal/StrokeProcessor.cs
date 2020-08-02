using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Reinforced.Tecture.Features.SqlStroke.Reveal.Data;
using Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal
{
    class StrokeProcessor
    {
        private readonly IMapper _mapper;
        private readonly QueryFiller _queryFillerType;
        public StrokeProcessor(IMapper mapper, QueryFiller queryFillerType)
        {
            _mapper = mapper;
            this._queryFillerType = queryFillerType;
        }

        public RevealedQuery RevealQuery(LambdaExpression expr)
        {
            var prepared = ProcessQuery.Prepare(expr);
            var converted = ProcessQuery.Convert(prepared, x => _mapper.IsEntityType(x));

            _queryFillerType.Init(_mapper, converted);

            return new RevealedQuery(_queryFillerType.Proceed(), _queryFillerType.Parameters.ToArray(), converted.UsedTypes);
        }
    }
}
