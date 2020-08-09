using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Reinforced.Tecture.CleanPlayground.Models;
using Reinforced.Tecture.Testing;
using Reinforced.Tecture.Testing.Data;
using Reinforced.Tecture.Testing.Data.Format;

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
            (int, int) v = (10, 10);
            var t = v.GetType();

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
