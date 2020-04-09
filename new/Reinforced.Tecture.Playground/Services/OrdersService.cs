using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reinforced.Tecture.Integrate;
using Reinforced.Tecture.Methodics.Orm.Commands.Add;
using Reinforced.Tecture.Methodics.Orm.Commands.Delete;
using Reinforced.Tecture.Methodics.Orm.Commands.Update;
using Reinforced.Tecture.Methodics.Orm.Queries;
using Reinforced.Tecture.Methodics.SqlStroke;
using Reinforced.Tecture.Methodics.SqlStroke.Commands;
using Reinforced.Tecture.Methodics.SqlStroke.Queries;
using Reinforced.Tecture.Playground.Entities;
using Reinforced.Tecture.Queries;
using Reinforced.Tecture.Services;

namespace Reinforced.Tecture.Playground.Services
{
    class Db : ISqlSource, IOrmSource
    {
        public SqlStrokeRuntimeBase GetStrokeRuntime(Type[] usedTypes)
        {
            throw new NotImplementedException();
        }

        public T Runtime<T>() where T : class, ITectureRuntime
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves query builder
        /// </summary>
        /// <typeparam name="T">Entity to query from</typeparam>
        /// <returns>Query builder</returns>
        public IQueryFor<T> Get<T>() where T : class
        {
            throw new NotImplementedException();
        }
    }

    internal static class QQ
    {
        public static User ById(this IQueryFor<User> q, int id)
        {
            return q.All.FirstOrDefault(x => x.Id == id);
        }
    }

    public class OrdersService : TectureService<User,Order>, INoContext
    {
        private OrdersService() { }
        
        private UserService Users => Do<UserService>();

        public async void Operation()
        {
            Users.Operation();

            var user = From<Db>().Get<User>().ById(10);

            user.FirstName = "Vasya";

            Q.SqlStroke<User>(u => $"UPDATE {u} SET {u.FirstName=="aaa"} WHERE {u.Id == 10}");
        }
    }
}
