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

            tc.Put("abc", new User()
            {
                BirthDate = DateTime.Now,
                FirstName = "Vasya",
                Gender = Gender.Male,
                LastName = "Pupkin"
            });

            tc.Put("abc2", new User()
            {
                BirthDate = DateTime.Now,
                FirstName = "Masya",
                Gender = Gender.Female,
                LastName = "Pupkin"
            });

            tc.Put("count", 10);

            tc.Put("somestr","asdfasdfasdf");
            tc.Put("asdfasdfsaf",Gender.Female);

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
            u1.Orders = new List<Order>(){o};

            tc.Put("adsfasdfasdf",new User[] { u1 });

            var clas = tc.Proceed("TestData","Test.Data");
            var result = clas.NormalizeWhitespace(elasticTrivia: true).ToFullString();
            Console.ReadLine();
        }
    }
}
