using Reinforced.Tecture.Services;

namespace Reinforced.Tecture.Tests
{
    public class TestService1 : TectureService
    {
        public interface IDependency
        {
            void Action();
        }

        private readonly IDependency _dep;

        private TestService1(IDependency dep)
        {
            _dep = dep;
        }

        private TestService1()
        {
        }

        public void Action()
        {
            if (_dep != null) _dep.Action();
        }
    }
}