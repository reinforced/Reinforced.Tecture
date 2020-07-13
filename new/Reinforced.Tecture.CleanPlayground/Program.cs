using System;
using System.Linq;
using Reinforced.Tecture.CleanPlayground.Models;

namespace Reinforced.Tecture.CleanPlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dc = new TestDbContext())
            {
                var users = new FakeQueryable<User>(dc.Users);

                var needed = users.Where(x => x.FirstName.Contains("7"))
                    .GroupBy(x=>x.BirthDate)
                    .Select(x=>x.Count());

                var qs = needed.FirstOrDefault();
                var arr = needed.ToArray();

                foreach (var n in needed)
                {
                    Console.WriteLine(n);
                }
            }

        }
    }
}
