using System;
using System.Threading.Tasks;

namespace Reinforced.Storage
{
    public interface IStorageCache
    {
        IDisposable Lock(string lockKey);
        Task<IDisposable> LockAsync(string lockKey);

        string Put<T>(T value, string prefix = null, TimeSpan? ttl = null);
        Task<string> PutAsync<T>(T value, string prefix = null, TimeSpan? ttl = null);


        void Set<T>(string key, T value, TimeSpan? ttl = null);
        Task SetAsync<T>(string key, T value, TimeSpan? ttl = null);

        T Get<T>(string key);
        Task<T> GetAsync<T>(string key);

        bool Contains(string key);
        Task<bool> ContainsAsync(string key);

        bool Delete(string key);
        Task<bool> DeleteAsync(string key);

        void AddToSet(string setKey, string member);
        void RemoveFromSet(string setKey, string member);
        void EmptySet(string setKey);
        string[] SetContents(string setKey);
        bool SetContains(string setKey, string member);
    }
}
