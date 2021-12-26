using Reinforced.Tecture.Services;

namespace Reinforced.Tecture.Tests
{
    public class TestService2 : TectureService
    {
        public interface IDependency1 { void Action(); }
        public interface IDependency2 { void Action(); }

        private readonly IDependency1 _dep1;
        private readonly IDependency2 _dep2;

        private TestService2(IDependency1 dep1, IDependency2 dep2) { _dep1 = dep1; _dep2 = dep2; }
        private TestService2(IDependency1 dep1) { _dep1 = dep1; }
        private TestService2(IDependency2 dep) { _dep2 = dep; }

        private TestService2() { }

        public void Action()
        {
            if (_dep1 != null) _dep1.Action();
            if (_dep2 != null) _dep2.Action();
        }
    }
}