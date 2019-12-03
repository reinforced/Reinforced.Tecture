using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Reinforced.Storage.Defaults.Redis
{
    public class RedisCache : IStorageCache
    {
        private readonly IDatabase _db;
        private readonly string _lockFormat = "lock:{0}";
        private readonly TimeSpan _lockSpan;

        public static TimeSpan DefaultLockSpan = TimeSpan.FromSeconds(5);

        public RedisCache(IDatabase db
            , TimeSpan? lockSpan = null)
        {
            _db = db;
            _lockSpan = lockSpan ?? DefaultLockSpan;
        }

        private T Deserialize<T>(RedisValue val)
        {
            if (!val.HasValue) return default(T);
            return JsonConvert.DeserializeObject<T>(val);
        }

        private string Serialize<T>(T val)
        {
            return JsonConvert.SerializeObject(val);
        }

        private string GetRandomKey(string prefix = null)
        {
            return string.IsNullOrEmpty(prefix)
                ? Guid.NewGuid().ToString()
                : string.Format("{0}:{1}", prefix, Guid.NewGuid().ToString());
        }

        public IDisposable Lock(string key)
        {
            var timespan = TimeSpan.FromMilliseconds(75);
            var end = DateTime.UtcNow + _lockSpan;
            var lockToken = Guid.NewGuid().ToByteArray();
            var lockKey = String.Format(_lockFormat, key);
            while (DateTime.UtcNow < end)
            {
                if (_db.LockTake(lockKey, lockToken, _lockSpan))
                {
                    return new RedisCacheLock(_db, lockToken, lockKey);
                }
                Thread.Sleep(timespan);
            }
            throw new TimeoutException("Timeout exceeded while taking distributed lock");
        }

        public async Task<IDisposable> LockAsync(string key)
        {
            var timespan = TimeSpan.FromMilliseconds(75);
            var end = DateTime.UtcNow + _lockSpan;
            var lockToken = Guid.NewGuid().ToByteArray();
            var lockKey = String.Format(_lockFormat, key);
            while (DateTime.UtcNow < end)
            {
                if (await _db.LockTakeAsync(lockKey, lockToken, _lockSpan))
                {
                    return new RedisCacheLock(_db, lockToken, lockKey);
                }
                await Task.Delay(timespan);
            }
            throw new TimeoutException("Timeout exceeded while asynchronously taking distributed lock");
        }

        public string Put<T>(T value, string prefix = null, TimeSpan? ttl = null)
        {
            var k = GetRandomKey(prefix);
            Set(k, value, ttl);
            return k;
        }

        public async Task<string> PutAsync<T>(T value, string prefix = null, TimeSpan? ttl = null)
        {
            var k = GetRandomKey(prefix);
            await SetAsync(k, value, ttl);
            return k;
        }

        public void Set<T>(string key, T value, TimeSpan? ttl = null)
        {
            using (Lock(key))
            {
                _db.StringSet(key, Serialize(value), flags: CommandFlags.FireAndForget);
                if (ttl.HasValue) _db.KeyExpire(key, ttl.Value, CommandFlags.FireAndForget);
            }
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? ttl = null)
        {
            using (await LockAsync(key))
            {
                await _db.StringSetAsync(key, Serialize(value), flags: CommandFlags.FireAndForget);
                if (ttl.HasValue)
                    await _db.KeyExpireAsync(key, ttl.Value, CommandFlags.FireAndForget);
            }
        }

        public T Get<T>(string key)
        {
            var val = _db.StringGet(key);
            return Deserialize<T>(val);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var val = await _db.StringGetAsync(key);
            return Deserialize<T>(val);
        }

        public bool Contains(string key)
        {
            return _db.KeyExists(key);
        }

        public async Task<bool> ContainsAsync(string key)
        {
            return await _db.KeyExistsAsync(key);
        }

        public bool Delete(string key)
        {
            using (Lock(key))
            {
                return _db.KeyDelete(key, CommandFlags.FireAndForget);
            }
        }

        public async Task<bool> DeleteAsync(string key)
        {
            using (await LockAsync(key))
            {
                return await _db.KeyDeleteAsync(key, CommandFlags.FireAndForget);
            }
        }

        #region Woring with sets
        public void AddToSet(string setKey, string member)
        {
            using (Lock(string.Format("set:{0}", setKey)))
            {
                _db.SetAdd(setKey, member, CommandFlags.FireAndForget);
            }
        }

        public void RemoveFromSet(string setKey, string member)
        {
            using (Lock(string.Format("set:{0}", setKey)))
            {
                _db.SetRemove(setKey, member, CommandFlags.FireAndForget);
            }
        }

        public void EmptySet(string setKey)
        {
            using (Lock(string.Format("set:{0}", setKey)))
            {
                _db.KeyDelete(setKey, CommandFlags.FireAndForget);
            }
        }

        public string[] SetContents(string setKey)
        {
            return _db.SetMembers(setKey).Select(c => c.ToString()).ToArray();
        }

        public bool SetContains(string setKey, string member)
        {
            return _db.SetContains(setKey, member);
        }
        #endregion
    }
}
