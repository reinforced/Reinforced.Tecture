using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Tecture.Integrate;
using Reinforced.Tecture.Testing.Stories;

namespace Reinforced.Tecture.Testing
{
    /// <summary>
    /// Provides testing environment for your code and services
    /// </summary>
    public class TestingEnvironment
    {
        internal readonly RuntimeMultiplexer _mx = new RuntimeMultiplexer();

        private void OnException(Exception ex)
        {
            throw new TestRunException(ex);
        }

        /// <summary>
        /// Tells story about particular piece of code
        /// </summary>
        /// <param name="code">Tecture code (without Save)</param>
        /// <returns>Storage story</returns>
        public StorageStory TellStory(Action<ITectureNoSave> code)
        {
            var tcd = new TestingCommandsDispatcher(_mx);
            var tec = new Entry.Tecture(_mx, tcd, true, null, OnException);
            code(tec);
            tcd.BeginStory();
            tec.Save();
            return tcd.EndStory(this);
        }

        /// <summary>
        /// Tells story about particular piece of code (async)
        /// </summary>
        /// <param name="code">Tecture code (without Save)</param>
        /// <returns>Storage story</returns>
        public async Task<StorageStory> TellStoryAsync(Func<ITectureNoSave,Task> code)
        {
            var tcd = new TestingCommandsDispatcher(_mx);
            var tec = new Entry.Tecture(_mx, tcd, true, null, OnException);
            await code(tec);
            tcd.BeginStory();
            await tec.SaveAsync();
            return tcd.EndStory(this);
        }
    }
}
