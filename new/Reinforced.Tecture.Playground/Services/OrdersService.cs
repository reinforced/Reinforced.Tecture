using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Features.Orm.Command.Add;
using Reinforced.Tecture.Features.Orm.Querу;
using Reinforced.Tecture.Features.SqlStroke;
using Reinforced.Tecture.Features.SqlStroke.Commands;
using Reinforced.Tecture.Playground.Entities;
using Reinforced.Tecture.Services;

namespace Reinforced.Tecture.Playground.Services
{
    interface Db : 
        QueryChannel<Orm>, 
        CommandChannel<Features.Orm.Command.Orm>,
        CommandQueryChannel<Features.SqlStroke.Queries.DirectSql, DirectSql>
    {}
    
    public class OrdersService : TectureService<User,Order,Item>, INoContext
    {
        private OrdersService() { }
        
        private UserService Users => Do<UserService>();

        public async void Operation()
        {
            var q = From<Db>().Get<User>();

            To<Db>().Add(new Item());
            To<Db>().SqlStroke<Order>(x => $"DELETE FROM {x}");

        }
    }
}
