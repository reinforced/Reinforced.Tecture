using System;
using Reinforced.Tecture.Queries;
using Reinforced.Tecture.Testing;
using Reinforced.Tecture.Tracing.Promises;

namespace Reinforced.Tecture.Aspects.Time.Queries
{
    public class DateTimeWrapper
    {
        private readonly TestingContext _testingContext;
        private int _order = 0;
        
        internal DateTimeWrapper(TestingContext testingContext)
        {
            _testingContext = testingContext;
        }
        
        
        /// <summary>Gets a <see cref="T:System.DateTime" /> object that is set to the current date and time on this computer, expressed as the local time.</summary>
        /// <returns>An object whose value is the current local date and time.</returns>
        public DateTime Now
        {
            get
            {
                var p = _testingContext.Promise<DateTime>();
                return p.ResolveValue(() => DateTime.Now, () => $"DateTime_Now_{_order++}");
            }
        }
        
        /// <summary>Gets the current date.</summary>
        /// <returns>An object that is set to today's date, with the time component set to 00:00:00.</returns>
        public DateTime Today
        {
            get
            {
                var p = _testingContext.Promise<DateTime>();
                return p.ResolveValue(() => DateTime.Today, () => $"DateTime_Today_{_order++}");
            }
        }
        
        /// <summary>Gets the current date.</summary>
        /// <returns>An object that is set to today's date, with the time component set to 00:00:00.</returns>
        public DateTime UtcNow
        {
            get
            {
                var p = _testingContext.Promise<DateTime>();
                return p.ResolveValue(() => DateTime.UtcNow, () => $"DateTime_UtcNow_{_order++}");
            }
        }
    }
}