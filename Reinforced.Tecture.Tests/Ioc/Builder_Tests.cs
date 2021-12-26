using Moq;
using Reinforced.Tecture.Entry;
using Xunit;
using Xunit.Sdk;

namespace Reinforced.Tecture.Tests.Ioc
{
    public class IocTests
    {
        [Fact]
        public void IoC_Resolver_Works_As_Expected()
        {
            var tb = new TectureBuilder();

            var dependencyMock = new Mock<TestService1.IDependency>();

            tb.WithIoc(x =>
            {
                if (x == typeof(TestService1.IDependency)) return dependencyMock.Object;
                throw new XunitException("Invalid type provided for IoC call");
            });


            var tecture = tb.Build();
            
            tecture.Let<TestService1>().Action();
            
            dependencyMock.Verify(x=>x.Action(),Times.Once);
        }
        
        [Fact]
        public void Without_IoC_Tecture_Choses_Parameterless_Constructor()
        {
            var tb = new TectureBuilder();
            var tecture = tb.Build();
            
            tecture.Let<TestService1>().Action();
        }
        
        [Fact]
        public void Tecture_Tries_To_Find_Appropriate_Constructor_For_IoC()
        {
            {
                var tb = new TectureBuilder();
                var dependency1Mock = new Mock<TestService2.IDependency1>();
                tb.WithIoc(x =>
                {
                    if (x == typeof(TestService2.IDependency1)) return dependency1Mock.Object;
                    throw new XunitException("Invalid type provided for IoC call");
                });
                var tecture = tb.Build();

                tecture.Let<TestService2>().Action();

                dependency1Mock.Verify(x => x.Action(), Times.Once);
            }
            
            {
                var tb = new TectureBuilder();
                var dependency2Mock = new Mock<TestService2.IDependency2>();
                tb.WithIoc(x =>
                {
                    
                    if (x == typeof(TestService2.IDependency2)) return dependency2Mock.Object;
                    throw new XunitException("Invalid type provided for IoC call");
                });
                var tecture = tb.Build();

                tecture.Let<TestService2>().Action();

                dependency2Mock.Verify(x => x.Action(), Times.Once);
            }
            
            {
                var tb = new TectureBuilder();
                var dependency1Mock = new Mock<TestService2.IDependency1>();
                var dependency2Mock = new Mock<TestService2.IDependency2>();
                tb.WithIoc(x =>
                {
                    if (x == typeof(TestService2.IDependency1)) return dependency1Mock.Object;
                    if (x == typeof(TestService2.IDependency2)) return dependency2Mock.Object;
                    throw new XunitException("Invalid type provided for IoC call");
                });
                var tecture = tb.Build();

                tecture.Let<TestService2>().Action();

                dependency1Mock.Verify(x => x.Action(), Times.Once);
                dependency2Mock.Verify(x => x.Action(), Times.Once);
            }
        }
    }
}