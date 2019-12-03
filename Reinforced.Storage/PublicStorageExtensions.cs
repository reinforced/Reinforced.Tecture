using System.Linq;

namespace Reinforced.Storage
{
    public static class PublicStorageExtensions
    {
        public static IQueryable<T> Q<T>(this IStorage s) where T : class
        {
            return s.Get<T>().All;
        }
    }
}
