using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Storage.Transactions;

namespace Reinforced.Storage.Defaults.EntityFramework
{
    public static class EntityStorageExtensions
    {
        public static int MaxResultsCount = 1000;

        public static T ById<T>(this IQueryFor<T> q, int id) where T : class, IEntity
        {
            return q.All.FirstOrDefault(c => c.Id == id);
        }

        public static TField ById<T, TField>(this IQueryFor<T> q, int id, Expression<Func<T, TField>> selector) where T : class, IEntity
        {
            return q.All.Where(c => c.Id == id).Select(selector).FirstOrDefault();
        }

        public static T ByIdRequired<T>(this IQueryFor<T> q, int id) where T : class, IEntity
        {
            var r = q.All.FirstOrDefault(c => c.Id == id);
            if (r == null) throw new Exception(String.Format("{0} with ID {1} not found", typeof(T), id));
            return r;
        }

        public static TField ByIdRequired<T, TField>(this IQueryFor<T> q, int id, Expression<Func<T, TField>> selector) where T : class, IEntity
        {
            try
            {
                var r = q.All.Where(c => c.Id == id).Select(selector).First();
                return r;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0} with ID {1} not found", typeof(T), id));
            }
        }

        public static async Task<T> ByIdAsync<T>(this IQueryFor<T> q, int id) where T : class, IEntity
        {
            return await q.All.FirstOrDefaultAsync(c => c.Id == id);
        }

        public static IQueryable<T> All<T>(this IStorage s) where T : class
        {
            return s.Get<T>().All;
        }
    }
}
