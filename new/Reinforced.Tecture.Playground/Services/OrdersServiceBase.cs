using Reinforced.Tecture.Features.Orm.Commands.Add;
using Reinforced.Tecture.Playground.Entities;
using Reinforced.Tecture.Services;

namespace Reinforced.Tecture.Playground.Services
{
    public class Orders : TectureService<Order>, INoContext
    {
        private Orders() { }

        public Order CreateTestOrder()
        {
            var o = new Order { Name = "Test"};
            To<Db>().Add(o);
            return o;
        }
    }
}
