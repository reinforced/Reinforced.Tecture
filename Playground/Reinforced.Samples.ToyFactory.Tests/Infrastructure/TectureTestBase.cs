using System.Diagnostics;
using System.IO;
using System.Reflection;
using Reinforced.Samples.ToyFactory.Data;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Tecture;
using Reinforced.Tecture.Entry;
using Reinforced.Tecture.Runtimes.EFCore.Aspects.DirectSql;
using Reinforced.Tecture.Runtimes.EFCore.Aspects.Orm;
using Reinforced.Tecture.Testing;
using Xunit.Abstractions;

namespace Reinforced.Samples.ToyFactory.Tests.Infrastructure
{
    public class TectureTestBase
    {
        protected ITest CurrentTest { get; private set; }
        protected ITestOutputHelper Output { get; private set; }
        
        protected TectureTestBase(ITestOutputHelper output)
        {
            Output = output;
            var type = output.GetType();
            var testMember = type.GetField("test", BindingFlags.Instance | BindingFlags.NonPublic);
            CurrentTest = (ITest)testMember.GetValue(output);
        }

        protected string Location
        {
            get
            {
                var st = new StackTrace();
                var frames = st.GetFrames();
                var frame = st.GetFrame(1);
                var fName = frame.GetFileName();
                if (string.IsNullOrEmpty(fName)) return string.Empty;
                return Path.GetDirectoryName(fName);
            }
        }

        private TectureBuilder Configure(bool fake)
        {
            TectureBuilder tb = new TectureBuilder();
            tb.WithChannel<Db>(c =>
            {

                LazyDisposable<ToyFactoryDbContext> ld = fake
                    ? LazyDisposable<ToyFactoryDbContext>.Default()
                    : new LazyDisposable<ToyFactoryDbContext>(() => new ToyFactoryDbContext());

                c.UseEfCoreOrm(ld);
                c.UseEfCoreDirectSql(ld);
            });
            return tb;
        }

        protected TectureCase Case(out ITecture tec)
        {
            var t = this.GetType();
            var tb = Configure(false);
            tec = tb.Build();
            var name = CurrentTest.TestCase.TestMethod.Method.Name;
            var location = Path.Combine(Root.FolderWithType(CurrentTest.TestCase.TestMethod.TestClass.Class.ToRuntimeType()),name);
            if (!Directory.Exists(location)) Directory.CreateDirectory(location);
            return new TectureCase(tec, name, $"{t.Namespace}.{name}", location, generateStuff: true);
        }

        protected TectureCase Case<T>(out ITecture tec, bool capture = false) where T : ITestDataSource, new()
        {
            var t = this.GetType();
            var tb = Configure(true);
            if (!capture) tb.WithTestData(new T());
            tec = tb.Build();
            var name = CurrentTest.TestCase.TestMethod.Method.Name;
            var location = Path.Combine(Root.FolderWithType(CurrentTest.TestCase.TestMethod.TestClass.Class.ToRuntimeType()), name);
            if (!Directory.Exists(location)) Directory.CreateDirectory(location);
            return new TectureCase(tec, name, $"{t.Namespace}.{name}", location, generateStuff: capture);
        }


    }
}
