using Reinforced.Tecture.Features.Orm.Commands.Add;
using Reinforced.Tecture.Features.Orm.PrimaryKey;
using Reinforced.Tecture.Playground.Entities;
using Reinforced.Tecture.Services;

namespace Reinforced.Tecture.Playground.Services
{
    public class Orders : TectureService<Order>, INoContext
    {
        private Orders() { }

        public Expected<int> CreateTestOrder()
        {
            var o = new Order { Name = "Test"};
            
            return To<Db>().Add(o).Expect();
        }
    }
}
