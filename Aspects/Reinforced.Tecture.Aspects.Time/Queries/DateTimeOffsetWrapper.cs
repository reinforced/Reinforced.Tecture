using System;
using Reinforced.Tecture.Queries;
using Reinforced.Tecture.Testing;
using Reinforced.Tecture.Tracing.Promises;

namespace Reinforced.Tecture.Aspects.Time.Queries
{
    public class DateTimeOffsetWrapper
    {
        private readonly TestingContext _testingContext;
        private int _order = 0;
        public DateTimeOffsetWrapper(TestingContext testingContext)
        {
            _testingContext = testingContext;
        }

        /// <summary>Gets a <see cref="T:System.DateTimeOffset" /> object that is set to the current date and time on the current computer, with the offset set to the local time's offset from Coordinated Universal Time (UTC).</summary>
        /// <returns>A <see cref="T:System.DateTimeOffset" /> object whose date and time is the current local time and whose offset is the local time zone's offset from Coordinated Universal Time (UTC).</returns>
        public DateTimeOffset Now =>
            _testingContext.Promise<DateTimeOffset>()
                .ResolveValue(() => DateTimeOffset.Now, () => $"DateTimeOffset_Now_{_order++}");
        
        /// <summary>Gets a <see cref="T:System.DateTimeOffset" /> object whose date and time are set to the current Coordinated Universal Time (UTC) date and time and whose offset is <see cref="F:System.TimeSpan.Zero" />.</summary>
        /// <returns>An object whose date and time is the current Coordinated Universal Time (UTC) and whose offset is <see cref="F:System.TimeSpan.Zero" />.</returns>
        public DateTimeOffset UtcNow =>
            _testingContext.Promise<DateTimeOffset>()
                .ResolveValue(() => DateTimeOffset.Now, () => $"DateTimeOffset_UtcNow_{_order++}");
    }
}