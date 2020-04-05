using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Methodics.Orm.Commands.Delete;
using Reinforced.Tecture.Playground.Entities;
using Reinforced.Tecture.Services;

namespace Reinforced.Tecture.Playground.Services
{
    public class OrdersService : TectureService<Order>, INoContext
    {
        private OrdersService() { }


        public void Operation()
        {
            Q.Delete(new Order() {Id = 10});
        }
    }
}
