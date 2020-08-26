using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Samples.ToyFactory.Tests.Infrastructure;
using Reinforced.Samples.ToyFactory.Tests.Nomenclature.CreateBlueprintWorks;
using Reinforced.Samples.ToyFactory.Tests.Nomenclature.CreateTypeWorks;
using Reinforced.Tecture;
using Reinforced.Tecture.Features.Orm.Queries;
using Xunit;
using Xunit.Abstractions;

namespace Reinforced.Samples.ToyFactory.Tests.Nomenclature
{

    public class NomenclatureTests : TectureTestBase
    {
        [Fact]
        public void CreateTypeWorks()
        {
            using var c = Case<CreateTypeWorks_TestData>(out ITecture ctx);

            var a = ctx.Do<Logic.Services.Nomenclature>().CreateType("test type");
            ctx.Save();
            var r = a.Result;
            var id = ctx.From<Db>().Key(r);
            
            Output.WriteLine(c.Text());
            c.Validate<CreateTypeWorks_Validation>();
        }

        [Fact]
        public void CreateBlueprintWorks()
        {
            using var c = Case<CreateBlueprintWorks_TestData>(out ITecture ctx);
            
            var a = ctx.Do<Logic.Services.Nomenclature>().CreateBlueprint(79);
            ctx.Save();

            Output.WriteLine(c.Text());
            c.Validate<CreateBlueprintWorks_Validation>();
        }

        public NomenclatureTests(ITestOutputHelper helper) : base(helper)
        {
        }
    }
}
