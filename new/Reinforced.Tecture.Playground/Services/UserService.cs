using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Methodics.Orm.Commands.Add;
using Reinforced.Tecture.Playground.Entities;
using Reinforced.Tecture.Services;

namespace Reinforced.Tecture.Playground.Services
{
    class UserService : TectureService<User,Order>, INoContext
    {
        private UserService() { }

        public void Operation()
        {
            Q.Add(new User());

            Do<OrdersService>().Operation();
        }
    }
}
