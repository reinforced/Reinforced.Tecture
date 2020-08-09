using System.Linq;
using System.Threading.Tasks;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Features.Orm.Queries;
using Reinforced.Tecture.Features.SqlStroke.Commands;
using Reinforced.Tecture.Features.SqlStroke.Queries;
using Reinforced.Tecture.Playground.Entities;
using Reinforced.Tecture.Services;

namespace Reinforced.Tecture.Playground.Services
{
    
    class UserService : TectureService<User,Order>, INoContext
    {
        private UserService() { }

        private Read<Db, User, Order> Db => From<Db>();

        public async void Operation()
        {
            var o = Do<Orders>().CreateTestOrder();

            var q = From<Db>().Get<User>().That(x => x.IsBlocked).All.ToArray();

            var q2 = From<Db>().SqlQuery<User>(x => $"SELECT * FROM {x}").As<User>();

            To<Db>().SqlStroke<User>(x => $"UPDATE {x} SET {x.FirstName == "Vasya"}");
            
            await Save;
            
        }
    }
}
