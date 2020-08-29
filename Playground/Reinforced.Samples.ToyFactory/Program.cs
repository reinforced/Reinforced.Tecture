using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Reinforced.Tecture.Cloning;

namespace Reinforced.Samples.ToyFactory
{
    class User
    {
        public int Id { get; set; }

        public HashSet<Order> Orders { get; set; }
    }

    class Order
    {
        public int Id { get; set; }

        public User User { get; set; }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            User u = new User(){Id = 10};
            Order a1 = new Order()
            {
                Id = 20,
                User = u
            };
            Order a2 = new Order()
            {
                Id = 30,
                User = u
            };
            u.Orders = new HashSet<Order>(){a1,a2};

            var clone = u.DeepClone();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
