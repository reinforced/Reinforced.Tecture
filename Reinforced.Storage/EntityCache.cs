using System;
using System.Linq;
using System.Threading.Tasks;

namespace Reinforced.Storage
{
    public class EntityCache<T> where T : class
    {
        internal static string CacheFriendlyTypeName(Type type)
        {
            if (type.IsGenericType)
            {
                return string.Format("{0}<{1}>", type.Name,
                    string.Join(",", type.GetGenericArguments().Select(CacheFriendlyTypeName)));
            }
            return type.Name;
        }

        private readonly IStorageCache _cache;

        public IStorageCache RawCache
        {
            get { return _cache; }
        }

        public EntityCache(IStorageCache cache, IQueryFor<T> fromDb)
        {
            _cache = cache;
            FromDatabase = fromDb;
        }

        public string EntityName { get { return typeof(T).Name; } }

        public EntityCache<T> Joined()
        {
            return new EntityCache<T>(_cache, FromDatabase);
        }

        public IQueryFor<T> FromDatabase { get; private set; }

        private string Key(string key)
        {
            return string.Format("{0}:{1}", EntityName, key);
        }

        private string SetKey(string key)
        {
            return string.Format("{0}:sets:{1}", EntityName, key);
        }

        private string LockKey(string key)
        {
            return string.Format("entitylock:{0}.{1}", EntityName, key);
        }

        #region Read operations
        public T Get(string key)
        {
            return _cache.Get<T>(Key(key));
        }

        public async Task<T> GetAsync(string key)
        {
            return await _cache.GetAsync<T>(Key(key));
        }

        public bool Contains(string key)
        {
            return _cache.Contains(Key(key));
        }

        public async Task<bool> ContainsAsync(string key)
        {
            return await _cache.ContainsAsync(Key(key));
        }

        #endregion

        #region Write operations

        public bool Drop(string key)
        {
            return _cache.Delete(Key(key));
        }

        public async Task<bool> DropAsync(string key)
        {
            return await _cache.DeleteAsync(Key(key));
        }

        #endregion

        #region Set operations
        public void AddToSet(string setKey, string member)
        {
            _cache.AddToSet(SetKey(setKey), member);
        }

        public void RemoveFromSet(string setKey, string member)
        {
            _cache.RemoveFromSet(SetKey(setKey), member);
        }

        public void EmptySet(string setKey)
        {
            _cache.EmptySet(SetKey(setKey));
        }

        public string[] SetContents(string setKey)
        {
            return _cache.SetContents(SetKey(setKey));
        }

        public bool SetContains(string setKey, string member)
        {
            return _cache.SetContains(SetKey(setKey), member);
        }

        public IDisposable Lock(string lockKey)
        {
            return _cache.Lock(LockKey(lockKey));
        }

        public async Task<IDisposable> LockAsync(string lockKey)
        {
            return await _cache.LockAsync(LockKey(lockKey));
        }

        #endregion

        #region Untyped entity-related
        public T2 Get<T2>(string key)
        {
            return _cache.Get<T2>(Key(key));
        }

        public async Task<T2> GetAsync<T2>(string key)
        {
            return await _cache.GetAsync<T2>(Key(key));
        }

        public string Put<T2>(T2 value, string prefix = null, TimeSpan? ttl = null)
        {
            return _cache.Put(value, EntityName, ttl);
        }

        public async Task<string> PutAsync<T2>(T2 value, string prefix = null, TimeSpan? ttl = null)
        {
            return await _cache.PutAsync(value, EntityName, ttl);
        }

        public void Set<T2>(string key, T2 value, TimeSpan? ttl = null)
        {
            _cache.Set(Key(key), value, ttl);
        }

        public async Task SetAsync<T2>(string key, T2 value, TimeSpan? ttl = null)
        {
            await _cache.SetAsync(Key(key), value, ttl);
        }
        #endregion
    }
}
