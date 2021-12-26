using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Cloning;
using Reinforced.Tecture.Entry;
using Reinforced.Tecture.Entry.Builders;
using Reinforced.Tecture.Services;
using Xunit;

namespace Reinforced.Tecture.Tests.Channels.Channels
{
    public class ChannelTests
    {
        interface Test:
            CommandQueryChannel<Aspect101.Command,Aspect101.Query>
        { }

        class Service : TectureService
        {
            private Service(){}
            
            public void Action()
            {
                var str = From<Test>().GetRandomString();
                
                To<Test>().PutTestCommand(str);
            }
        }

        [Fact]
        public void General_Tecture_Setup_Test()
        {
            var tb = new TectureBuilder();
            tb.WithChannel<Test>(x =>
            {
                x.ForCommand(new Aspect101.Command());
                x.ForQuery(new Aspect101.Query());
            });

            var tecture = tb.Build();
            
            tecture.Let<Service>().Action();
            tecture.Save();
        }
        
        class Service2 : TectureService
        {
            private Service2(){}
            
            public Aspect101.Query GetTestQueryAspect() => From<Test>().Aspect();
            public Aspect101.Command GetTestCommandAspect() => To<Test>().Aspect();
        }
        
        [Fact]
        public void Tecture_Returns_Specified_Query_Aspect_On_Aspect_Call()
        {
            var tb = new TectureBuilder();
            var commandAspect = new Aspect101.Command();
            var queryAspect = new Aspect101.Query();
            
            tb.WithChannel<Test>(x =>
            {
                x.ForCommand(commandAspect);
                x.ForQuery(queryAspect);
            });

            var tecture = tb.Build();
            
            Assert.Equal(commandAspect, tecture.Let<Service2>().GetTestCommandAspect());
            Assert.Equal(commandAspect, tecture.Let<Service2>().GetTestCommandAspect());
            
            Assert.Equal(queryAspect, tecture.Let<Service2>().GetTestQueryAspect());
            Assert.Equal(queryAspect, tecture.Let<Service2>().GetTestQueryAspect());
            
        }
        
        
       
        
    }
}