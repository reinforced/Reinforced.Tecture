using System.Linq;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Samples.ToyFactory.Logic.Entities;
using Reinforced.Samples.ToyFactory.Logic.Services;
using Reinforced.Samples.ToyFactory.Tests.Infrastructure;
using Reinforced.Tecture;
using Reinforced.Tecture.Features.Orm.Queries;
using Xunit;
using Xunit.Abstractions;

namespace Reinforced.Samples.ToyFactory.Tests
{

    public class NomenclatureTests : TectureTestBase
    {
        [Fact]
        public void CreateTypeWorks()
        {
            using var c = Case(out ITecture ctx);

            var a = ctx.Do<Nomenclature>().CreateType("test type");
            ctx.Save();
            var r = a.Result;
            var id = ctx.From<Db>().Key(r);
            
            Output.WriteLine(c.Text());
            //c.Validate<CreateTypeWorks_Validation>();
        }

        public NomenclatureTests(ITestOutputHelper helper) : base(helper)
        {
        }
    }
}
