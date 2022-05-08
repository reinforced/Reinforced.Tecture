using System;
using Reinforced.Tecture.Entry;
using Reinforced.Tecture.Services;
using Xunit;

namespace Reinforced.Tecture.Tests.Services
{
    public class ServicesTests
    {
        class TestService1 : TectureService
        {
            public void Action(){}
        }
        
        class TestService2 : TectureService
        {
            private TestService2() {}

            public void Action(){}
        }

        
        
        [Fact]
        public void Service_Must_Contain_AtLeast_One_Private_Constructor()
        {
            var tb = new TectureBuilder();
            var tecture = tb.Build();

            Assert.Throws<MissingMethodException>(() =>
            {
                tecture.Let<TestService1>().Action();
            });
            
            tecture.Let<TestService2>().Action();
        }
    }
}