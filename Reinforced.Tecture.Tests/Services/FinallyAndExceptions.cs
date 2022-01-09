using System;
using System.Threading.Tasks;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Entry;
using Reinforced.Tecture.Entry.Builders;
using Reinforced.Tecture.Services;
using Reinforced.Tecture.Tests.Channels;
using Reinforced.Tecture.Tests.Channels.Channels;
using Xunit;

namespace Reinforced.Tecture.Tests.Services
{
    public class FinallyAndExceptions
    {
        interface Test:
            CommandQueryChannel<Aspect101.Command,Aspect101.Query>
        { }
        
        class TestService : TectureService
        {
            private TestService(){}
            public void MethodWithFinallyAndBuggyCommand()
            {
                To<Test>().PutBuggyCommand("hello");
                Then(() =>
                {
                    To<Test>().PutTestCommand("this should not happen");
                });
                Finally(() =>
                {
                    To<Test>().PutTestCommand("final");
                });
            }
            
            public async Task MethodWithFinallyAndBuggyCommandAsync()
            {
                To<Test>().PutBuggyCommand("hello");
                Then(async () =>
                {
                    To<Test>().PutTestCommand("this should not happen");
                });
                Finally(async () =>
                {
                    To<Test>().PutTestCommand("final");
                });
            }
            
            public void MethodWithExceptionInThen()
            {
                To<Test>().PutTestCommand("initial");
                
                Finally(() =>
                {
                    To<Test>().PutTestCommand("final");
                });
                
                Then(() =>
                {
                    throw new Exception("buggy");
                });
            }
        }
        
        private Aspect101.Command CommandAspect { get; set; }
        
        private ITecture Configure()
        {
            var tb = new TectureBuilder();
            CommandAspect = new Aspect101.Command();
            
            tb.WithChannel<Test>(x =>
            {
                x.ForCommand(CommandAspect);
                x.ForQuery(new Aspect101.Query());
            });
            tb.WithExceptionHandler(e => true);
            return tb.Build();
        }
        
        [Fact]
        public void Tecture_always_calls_Finally_Actions_even_on_exceptions_in_commands()
        {
            var tecture = Configure();
            
            tecture.Let<TestService>().MethodWithFinallyAndBuggyCommand();
            
            tecture.Save();
            
            Assert.NotNull(CommandAspect.BuggyRunnerInstance);
            Assert.NotNull(CommandAspect.TestRunnerInstance);
            
            Assert.True(CommandAspect.BuggyRunnerInstance.Invoked,"buggy runner not invoked in non-async way");
            Assert.False(CommandAspect.BuggyRunnerInstance.InvokedAsync,"async command runner invoked");
            
            Assert.Single(CommandAspect.TestRunnerInstance.CommandsRun);
            Assert.Empty(CommandAspect.TestRunnerInstance.CommandsRunAsync);
        }
        
        [Fact]
        public async Task Tecture_always_calls_Finally_Actions_even_on_exceptions_in_commands_Async()
        {
            var tecture = Configure();
            
            await tecture.Let<TestService>().MethodWithFinallyAndBuggyCommandAsync();
            
            await tecture.SaveAsync();
            
            Assert.NotNull(CommandAspect.BuggyRunnerInstance);
            Assert.NotNull(CommandAspect.TestRunnerInstance);
            
            Assert.False(CommandAspect.BuggyRunnerInstance.Invoked,"buggy runner not invoked in non-async way");
            Assert.True(CommandAspect.BuggyRunnerInstance.InvokedAsync,"async command runner invoked");
            
            Assert.Empty(CommandAspect.TestRunnerInstance.CommandsRun);
            Assert.Single(CommandAspect.TestRunnerInstance.CommandsRunAsync);
        }
        
        [Fact]
        public async Task Tecture_always_calls_Finally_Actions_even_on_exceptions_in_Then()
        {
            var tecture = Configure();
            
            tecture.Let<TestService>().MethodWithExceptionInThen();
            
            await tecture.SaveAsync();
            
            Assert.Null(CommandAspect.BuggyRunnerInstance);
            Assert.NotNull(CommandAspect.TestRunnerInstance);

            Assert.Empty(CommandAspect.TestRunnerInstance.CommandsRun);
            Assert.Collection(CommandAspect.TestRunnerInstance.CommandsRunAsync,
                x=>Assert.Equal("initial",x.Payload),
                x=>Assert.Equal("final",x.Payload));
        }
    }
}