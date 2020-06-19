using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Reinforced.Tecture.Features.Orm.Testing
{
    class TestingOrmSource : OrmSourceBase, ICollectionProvider, IPrefetch
    {
        private readonly bool _strict;
        private readonly Dictionary<Type, IList> _prefetchedCollections = new Dictionary<Type, IList>();
        private readonly TestingOrmRuntime _runtime;
        public TestingOrmSource(TestingOrmRuntime runtime, bool strict)
        {
            _strict = strict;
            _runtime = runtime;
        }

        protected override IQueryable<T> ProvideSet<T>()
        {
            if (!_prefetchedCollections.ContainsKey(typeof(T)))
            {
                if (_strict)
                    throw new Exception(String.Format("Please prefetch collection (maybe empty) for type {0}",
                        typeof(T).Name));
                else
                {
                    _prefetchedCollections[typeof(T)] = new List<T>();
                }
            }
            return ((ICollection<T>)_prefetchedCollections[typeof(T)]).AsQueryable();
        }

        

        public override T Runtime<T>()
        {
            if (_runtime is T rt) return rt;
            return null;
        }

        /// <summary>
        /// Adds prefetched data to test data collection
        /// </summary>
        public IPrefetch Prefetched<T>(IEnumerable<T> collection)
        {
            _prefetchedCollections[(typeof(T))] = collection.ToList();
            return this;
        }

        /// <summary>
        /// Adds prefetched data to test data collection
        /// </summary>
        public IPrefetch Prefetched<T>(IQueryable<T> collection) where T : class
        {
            var coll = collection.AsEnumerable();
            _prefetchedCollections[(typeof(T))] = coll.ToList();
            return this;
        }

        public IList<T> GetCollection<T>()
        {
            if (!_prefetchedCollections.ContainsKey(typeof(T))) _prefetchedCollections[typeof(T)] = new List<T>();
            return (IList<T>)_prefetchedCollections[typeof(T)];
        }

        public IList GetCollection(Type t)
        {
            if (!_prefetchedCollections.ContainsKey(t)) _prefetchedCollections[t] = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(t));
            return _prefetchedCollections[t];
        }
    }
}
