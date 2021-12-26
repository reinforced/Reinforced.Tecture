using System.Threading.Tasks;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Entry;
using Reinforced.Tecture.Entry.Builders;
using Reinforced.Tecture.Services;
using Xunit;

namespace Reinforced.Tecture.Tests.Channels
{
    public class SaversTests
    {
        interface Test :
            CommandQueryChannel<Aspect101.Command, Aspect101.Query>
        { }

        class Service : TectureService
        {
            private Service() { }

            public void Action(string parameter)
            {
                To<Test>().PutTestCommand(parameter);
            }
        }

        [Fact]
        public void Tecture_Invokes_Save_For_Commands_Enqueued()
        {
            var tb = new TectureBuilder();
            
            var commandAspect = new Aspect101.Command();
            tb.WithChannel<Test>(x =>
            {
                x.ForCommand(commandAspect);
                x.ForQuery(new Aspect101.Query());
            });

            var tecture = tb.Build();

            const string parameter = "AAA";

            tecture.Let<Service>().Action(parameter);
            tecture.Save();
            
            Assert.NotNull(commandAspect.TestRunnerInstance);
            Assert.Single(commandAspect.TestRunnerInstance.CommandsRun);
            Assert.Empty(commandAspect.TestRunnerInstance.CommandsRunAsync);
            Assert.Equal(parameter,commandAspect.TestRunnerInstance.CommandsRun[0].Payload);
        }
        
        [Fact]
        public async Task Tecture_Invokes_Save_For_Commands_Enqueued_ASYNC()
        {
            var tb = new TectureBuilder();
            
            var commandAspect = new Aspect101.Command();
            tb.WithChannel<Test>(x =>
            {
                x.ForCommand(commandAspect);
                x.ForQuery(new Aspect101.Query());
            });

            var tecture = tb.Build();

            const string parameter = "AAA";

            tecture.Let<Service>().Action(parameter);
            await tecture.SaveAsync();
            
            Assert.NotNull(commandAspect.TestRunnerInstance);
            Assert.Single(commandAspect.TestRunnerInstance.CommandsRunAsync);
            Assert.Empty(commandAspect.TestRunnerInstance.CommandsRun);
            Assert.Equal(parameter,commandAspect.TestRunnerInstance.CommandsRunAsync[0].Payload);
        }
    }
}