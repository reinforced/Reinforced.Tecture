using System;
using System.Runtime.CompilerServices;
using Reinforced.Tecture.Aspects.Time.Queries;

namespace Reinforced.Tecture.Aspects.Time
{
    public class Query : QueryAspect
    {
        internal Query()
        {
            _dateTimeWrapper = new Lazy<DateTimeWrapper>(() => new DateTimeWrapper(this.Context));
            _dateTimeOffsetWrapper = new Lazy<DateTimeOffsetWrapper>(() => new DateTimeOffsetWrapper(Context));
        }

        private readonly Lazy<DateTimeWrapper> _dateTimeWrapper;
        private readonly Lazy<DateTimeOffsetWrapper> _dateTimeOffsetWrapper;

        internal DateTimeWrapper DateTimeWrapper => _dateTimeWrapper.Value;
        internal DateTimeOffsetWrapper DateTimeOffsetWrapper => _dateTimeOffsetWrapper.Value;
        
        public override void Dispose()
        { }
    }
}