using System;
using StackExchange.Redis;

namespace Reinforced.Storage.Defaults.Redis
{
    public class RedisCacheLock : IDisposable
    {
        private readonly string _lockKey;
        private readonly byte[] _lockToken;
        private readonly IDatabase _db;

        public RedisCacheLock(IDatabase db, byte[] lockToken, string lockKey)
        {
            _db = db;
            _lockToken = lockToken;
            _lockKey = lockKey;
        }

        public void Dispose()
        {
            _db.LockRelease(_lockKey, _lockToken, CommandFlags.FireAndForget);
        }
    }
}
