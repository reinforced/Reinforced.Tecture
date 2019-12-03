using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reinforced.Storage.Testing
{
    public class TestInMemoryCache : IStorageCache
    {
        private static readonly ConcurrentDictionary<string, object> _cache = new ConcurrentDictionary<string, object>();
        private static readonly ConcurrentDictionary<string, HashSet<string>> _sets = new ConcurrentDictionary<string, HashSet<string>>();
        private static readonly Dictionary<string, object> _syncRoots = new Dictionary<string, object>();

        /// <summary>
        /// Cache state
        /// </summary>
        public static ConcurrentDictionary<string, object> Cache
        {
            get { return _cache; }
        }

        /// <summary>
        /// Sets state
        /// </summary>
        public static ConcurrentDictionary<string, HashSet<string>> Sets
        {
            get { return _sets; }
        }

        private string GetRandomKey(string prefix = null)
        {
            return string.IsNullOrEmpty(prefix)
                ? Guid.NewGuid().ToString()
                : string.Format("{0}:{1}", prefix, Guid.NewGuid());
        }

        public IDisposable Lock(string lockKey)
        {
            if (!_syncRoots.ContainsKey(lockKey))
            {
                _syncRoots[lockKey] = new object();
            }
            return new TestCacheLocker(_syncRoots[lockKey]);
        }

        public Task<IDisposable> LockAsync(string lockKey)
        {
            return Task.FromResult(Lock(lockKey));
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
            _cache[key] = value;
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? ttl = null)
        {
            Set(key, value, ttl);
        }

        public T Get<T>(string key)
        {
            if (!_cache.ContainsKey(key)) return default(T);
            object value;
            bool result = _cache.TryGetValue(key, out value);
            while (!result)
            {
                result = _cache.TryGetValue(key, out value);
            }
            return (T)value;
        }

        public Task<T> GetAsync<T>(string key)
        {
            return Task.FromResult(Get<T>(key));
        }

        public bool Contains(string key)
        {
            return _cache.ContainsKey(key);
        }

        public Task<bool> ContainsAsync(string key)
        {
            return Task.FromResult(Contains(key));
        }

        public bool Delete(string key)
        {
            if (!_cache.ContainsKey(key)) return false;
            object value;
            bool result = _cache.TryRemove(key, out value);
            while (!result)
            {
                result = _cache.TryRemove(key, out value);
            }
            return true;
        }

        public Task<bool> DeleteAsync(string key)
        {
            return Task.FromResult(Delete(key));
        }

        private HashSet<string> GetSet(string setKey)
        {
            HashSet<string> set = null;
            if (!_sets.ContainsKey(setKey))
            {
                set = new HashSet<string>();
                _sets.AddOrUpdate(setKey, set, (k, v) => v);
            }
            else
            {
                object val = null;
                bool b = _cache.TryGetValue(setKey, out val);
                if (!b)
                {
                    set = new HashSet<string>();
                    _sets.AddOrUpdate(setKey, set, (k, v) => v);
                }
                else
                {
                    set = (HashSet<string>)val;
                }
            }
            return set;
        }

        public void AddToSet(string setKey, string member)
        {
            var set = GetSet(setKey);
            if (!set.Contains(member)) set.Add(member);
        }

        public void RemoveFromSet(string setKey, string member)
        {
            var set = GetSet(setKey);
            if (!set.Contains(member)) set.Remove(member);
        }

        public void EmptySet(string setKey)
        {
            var set = GetSet(setKey);
            set.Clear();
        }

        public string[] SetContents(string setKey)
        {
            var set = GetSet(setKey);
            return set.ToArray();
        }

        public bool SetContains(string setKey, string member)
        {
            var set = GetSet(setKey);
            return set.Contains(member);
        }
    }
}
