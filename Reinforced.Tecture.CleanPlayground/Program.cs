using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Reinforced.Tecture.CleanPlayground.Models;
using Reinforced.Tecture.Testing.Data;

namespace Reinforced.Tecture.CleanPlayground
{
    interface IPrimaryKey { }
    interface IPrimaryKey<T> : IPrimaryKey
    {
        Expression<Func<T>> Key { get; }
    }

    class Entity : IPrimaryKey<int>
    {
        public int Id { get; set; }

        public Expression<Func<int>> Key
        {
            get { return () => Id; }
        }
    }

    interface AddTest<out T> where T:IPrimaryKey
    {
        
    }

    static class Ext
    {
        public static void Do<T1>(this AddTest<IPrimaryKey<T1>> val)
        {

        }
    }
    class Program
    {

        static void Main(string[] args)
        {
            AddTest<Entity> g = null;

            g.Do();

            var tc =
                CSharpTestCollectorSetup.Create("SampleTestData", typeof(Program).Namespace)
                    .ToFile("W:\\test.cs");

            var u1 = new User()
            {
                BirthDate = DateTime.Now,
                FirstName = "Vasya",
                Gender = Gender.Male,
                LastName = "Pupkin"
            };
            var o = new Order()
            {
                Id = 10,
                Title = "aaa",
                UserId = 10
            };
            o.User = u1;
            u1.Orders = new List<Order>() { o };

            tc.Put("adsfasdfasdf", new User[] { u1 });//<<<
            tc.Put("asfasdfasd", (10, 20));
            tc.Finish();
        }
    }
}
