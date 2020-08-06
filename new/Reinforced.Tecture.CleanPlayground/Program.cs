using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Reinforced.Tecture.CleanPlayground.Models;
using Reinforced.Tecture.Testing.Data;

namespace Reinforced.Tecture.CleanPlayground
{
    interface IO<out T> { }

    class D<T> : IO<T>
    {

    }

    class Base { }

    class Exact : Base { }

    class Program
    {

        static void Main(string[] args)
        {
            CSharpCodeTestCollector tc = new CSharpCodeTestCollector();

            tc.Put("adfasdf", (10, 20));

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

            var clas = tc.Proceed("SampleTestData", "Reinforced.Tecture.CleanPlayground");
            var result = clas.NormalizeWhitespace(elasticTrivia: true).ToFullString();
            Console.ReadLine();
        }
    }
}
