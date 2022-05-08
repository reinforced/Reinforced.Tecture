using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Reinforced.Tecture.Aspects;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Tracing.Promises;

namespace Reinforced.Tecture.Tests.Channels
{
    public static class Aspect101Extensions
    {
        public static Aspect101.TestCommand PutTestCommand(this Write<CommandChannel<Aspect101.Command>> w, string payload)
        {
            var ca = w.Aspect();
            ca.CommandCounter++;
            return w.Put(new Aspect101.TestCommand(payload, ca.CommandCounter));
        }
        
        public static Aspect101.BuggyCommand PutBuggyCommand(this Write<CommandChannel<Aspect101.Command>> w, string payload)
        {
            var ca = w.Aspect();
            ca.CommandCounter++;
            return w.Put(new Aspect101.BuggyCommand(payload, ca.CommandCounter));
        }

        public static string GetRandomString(this Read<QueryChannel<Aspect101.Query>> r) => r.Aspect().GetRandomString(r);
    }

    public class Aspect101
    {
        public class Query : QueryAspect
        {
            public int RegisterCount { get; private set; }
            protected override void OnRegister() => RegisterCount++;
            
            private int _queryCounter = 0;
            public string GetRandomString(Read r)
            {
                var promised = Context.Promise<string>(r);
                return promised.ResolveReference(() => Guid.NewGuid().ToString(), () => $"Rng_{_queryCounter++}");
            }
            
            public override void Dispose() { }
        }

        public class Command : CommandAspect<TestCommand, BuggyCommand>
        {
            public int CommandCounter { get; set; }
            
            public int RegisterCount { get; private set; }
            protected override void OnRegister() => RegisterCount++;
            public TestRunner TestRunnerInstance { get; private set; }
            public BuggyRunner BuggyRunnerInstance { get; private set; }
            protected override CommandRunner<TestCommand> GetRunner1(TestCommand command)
            {
                if(TestRunnerInstance==null) TestRunnerInstance = new TestRunner();
                return TestRunnerInstance;
            }

            protected override CommandRunner<BuggyCommand> GetRunner2(BuggyCommand command)
            {
                if(BuggyRunnerInstance==null) BuggyRunnerInstance = new BuggyRunner();
                return BuggyRunnerInstance;
            }

            public int Saves { get; private set; }
            protected override void Save() => Saves++;
            
            public int SavesAsync { get; private set; }
            protected override async Task SaveAsync(CancellationToken token = default) => SavesAsync++;
            
            public int DisposeCount { get; private set; }
            public override void Dispose() => DisposeCount++;
        }

        public class TestRunner : CommandRunner<TestCommand>
        {
            public List<TestCommand> CommandsRun { get; } = new List<TestCommand>();
            protected override void Run(TestCommand cmd) => CommandsRun.Add(cmd);

            public List<TestCommand> CommandsRunAsync { get; } = new List<TestCommand>();
            protected override async Task RunAsync(TestCommand cmd,CancellationToken token = default) => CommandsRunAsync.Add(cmd);
        }
        
        public class BuggyRunner : CommandRunner<BuggyCommand>
        {
            public bool Invoked { get; private set; }
            public bool InvokedAsync { get; private set; }
            
            protected override void Run(BuggyCommand cmd)
            {
                Invoked = true;
                throw new Exception("buggy");
            }

            protected override async Task RunAsync(BuggyCommand cmd,CancellationToken token = default)
            {
                InvokedAsync = true;
                throw new Exception("async buggy");
            }
        }

        public class TestCommand : CommandBase
        {
            public string Payload { get; }
            public int GlobalOrder { get; }

            public TestCommand(string payload, int order)
            {
                Payload = payload;
                GlobalOrder = order;
            }

            protected override CommandBase DeepCloneForTracing()
            {
                return new TestCommand(Payload, GlobalOrder);
            }
        }
        
        public class BuggyCommand : CommandBase
        {
            public string Payload { get; }
            public int GlobalOrder { get; }

            public BuggyCommand(string payload, int order)
            {
                Payload = payload;
                GlobalOrder = order;
            }

            protected override CommandBase DeepCloneForTracing()
            {
                return new TestCommand(Payload, GlobalOrder);
            }
        }
    }
}