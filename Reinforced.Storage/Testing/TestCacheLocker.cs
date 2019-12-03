using System;
using System.Threading;

namespace Reinforced.Storage.Testing
{
    public class TestCacheLocker : IDisposable
    {
        private readonly object _syncRoot;

        public TestCacheLocker(object syncRoot)
        {
            _syncRoot = syncRoot;
            Monitor.Enter(syncRoot);
        }

        public void Dispose()
        {
            Monitor.Exit(_syncRoot);
        }
    }
}
