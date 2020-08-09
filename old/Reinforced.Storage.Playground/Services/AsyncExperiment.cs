using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Storage.Playground.Entities;

namespace Reinforced.Storage.Playground.Services
{
    //public static class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        AsyncExperiment ac = new AsyncExperiment();
    //        var t = ac.Combine();
    //        Console.WriteLine("Press enter to save changes");
    //        Console.ReadLine();
    //        Console.WriteLine("Okay, saving changes...");
    //        ac.Act();
    //        var r = t.Result;
    //        Console.WriteLine("Done");
    //        Console.ReadLine();
    //    }
    //}

    public struct MyAwaiter : INotifyCompletion
    {
        private readonly AsyncExperiment _exp;

        public MyAwaiter(AsyncExperiment exp)
        {
            _exp = exp;
            IsCompleted = false;
        }

        public void GetResult() { }
        public bool IsCompleted { get; private set; }

        public void OnCompleted(Action continuation)
        {
            _exp._continuations.Add(continuation);
            IsCompleted = true;
        }
    }

    internal struct MyTask
    {
        private readonly AsyncExperiment _exp;

        public MyTask(AsyncExperiment exp)
        {
            _exp = exp;
        }

        public MyAwaiter GetAwaiter() => new MyAwaiter(_exp);
    }

    public class AsyncExperiment
    {
        internal readonly List<Action> _continuations = new List<Action>();

        private MyTask Save { get { return new MyTask(this); } }

        public async void Do()
        {
            Console.WriteLine("Before save changes");
            await Save;
            Console.WriteLine("After save changes");
            await Save;
            Console.WriteLine("After save changes (2)");
        }

        public async Task<string> Combine()
        {
            var users = await Task.WhenAll(CreateUser(), CreateUser2());
            return $"{users.Length} users created";
        }

        public async Task<User> CreateUser()
        {
            Console.WriteLine("Creating user");
            var u = new User();
            await Save;
            u.Id = 50;
            Console.WriteLine("User has been created");
            return u;
        }

        public async Task<User> CreateUser2()
        {
            Console.WriteLine("Creating user");
            var u = new User();
            await Save;
            u.Id = 51;
            Console.WriteLine("User has been created");
            return u;
        }

        public async void DoThings()
        {
            Console.WriteLine("Starting doing things");
            var user = await CreateUser();
            Console.WriteLine($"We use user Id {user.Id}");
        }

        public void Act()
        {
            var carr = _continuations.ToArray();
            _continuations.Clear();
            foreach (var cont in carr) cont();
            Console.WriteLine("SaveChanges happened!");
        }
    }
}
