using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Reinforced.Tecture;
using Reinforced.Tecture.Entry;
using Reinforced.Tecture.Features.Orm.Command.Add;
using Reinforced.Tecture.Features.Orm.Testing;
using Reinforced.Tecture.Features.Orm.Testing.Assumptions;
using Reinforced.Tecture.Features.Orm.Testing.Checks.Add;
using Reinforced.Tecture.Playground.Entities;
using Reinforced.Tecture.Playground.Services;
using Reinforced.Tecture.Testing;
using Reinforced.Tecture.Testing.Checks;
using Reinforced.Tecture.Testing.Generation;
using Reinforced.Tecture.Testing.Stories;
using Reunforced.Tecture.Runtimes.EFCore;

namespace Reinforced.Tecture.Playground
{
    
    class Program
    {
        static void Main(string[] args)
        {
            StorageStory ss = null;
            ss.Begins().Then<Add>(
                
                );

            var bld = new TectureBuilder();

            bld.WithChannel<Db>(x =>
            {
                
            });

            ITecture tc = bld.Build();

            tc.Do<OrdersService>().Operation();
            tc.Do<UserService>().Operation();

            tc.Save();


        }

    }
}
