using System.Linq;
using Reinforced.Tecture.Entry;
using Reinforced.Tecture.Services;
using Reinforced.Tecture.Tracing.Commands;
using Xunit;

namespace Reinforced.Tecture.Tests.Channels.Commands
{
    public class CommandsTests
    {
        class Service : TectureService
        {
            private Service()
            {
            }

            public void Action()
            {
                Comment("Test");
            }
        }

        [Fact]
        public void Comment_Command_Works()
        {
            var tb = new TectureBuilder();
            var tecture = tb.Build();

            tecture.BeginTrace();

            tecture.Let<Service>().Action();
            tecture.Save();

            var trace = tecture.EndTrace();

            Assert.Equal(3, trace.All.Count());
            var cmd = trace.All.FirstOrDefault() as Comment;

            Assert.NotNull(cmd);
        }

        class Service2 : TectureService
        {
            private Service2()
            {
            }

            public void Action()
            {
                using (var c = Cycle("Test Cycle"))
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Comment("Test");

                        c.Iteration();    
                    }
                }
            }
        }

        [Fact]
        public void Trace_Can_Describe_Default_Commands()
        {
            var tb = new TectureBuilder();
            var tecture = tb.Build();

            tecture.BeginTrace();

            tecture.Let<Service2>().Action();
            tecture.Save();

            var trace = tecture.EndTrace();

            var explanation = trace.Explain();
            const string expectedExplanation =
@"1.   ↻  In cycle Test Cycle:
2.  // Test
3.   ↺  --- Cycle iteration ---
4.   ◎  Test Cycle ends in 3 iterations and 0 commands
5.  ◀ Save
6.  ◼ End
";              
            
            Assert.Equal(expectedExplanation, explanation);
        }
    }
}