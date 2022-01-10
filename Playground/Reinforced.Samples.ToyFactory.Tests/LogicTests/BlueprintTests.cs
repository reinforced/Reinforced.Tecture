using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Samples.ToyFactory.Logic.Services;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Services;
using Reinforced.Samples.ToyFactory.Tests.Infrastructure;
using Reinforced.Samples.ToyFactory.Tests.LogicTests.CreateAndRenameBlueprintWorks;
using Reinforced.Samples.ToyFactory.Tests.LogicTests.CreateTypeWorks;
using Reinforced.Tecture;
using Reinforced.Tecture.Aspects.Orm.Queries;
using Xunit;
using Xunit.Abstractions;

namespace Reinforced.Samples.ToyFactory.Tests.LogicTests
{
    public class BlueprintTests : TectureTestBase
    {
        public BlueprintTests(ITestOutputHelper output) : base(output)
        {
        }
        
        [Fact]
        public void CreateAndRenameBlueprintWorks()
        {
            using var c = Case
               <CreateAndRenameBlueprintWorks_TestData>
                (out ITecture ctx);

            var a = ctx.Do<Nomenclature>().CreateType("Test Toy Type 1");
            var b = ctx.Do<Nomenclature>().CreateType("Test Toy Type 2");
            ctx.Save();
            var aId = ctx.From<Db>().Key(a);
            var bId = ctx.From<Db>().Key(b);

            var bp = ctx.Let<Blueprints>().CreateOne(aId, "test");
            ctx.Save();

            var blueprintId = ctx.From<Db>().Key(bp);
            ctx.Let<Blueprints>().ChangeBlueprint(blueprintId,bId);
            ctx.Save();
            
            ctx.Let<Blueprints>().RemoveBlueprint(blueprintId);
            ctx.Let<Blueprints>().RemoveToyType(aId);
            ctx.Let<Blueprints>().RemoveToyType(bId);
            
            ctx.Save();
            Output.WriteLine(c.Text());
            c.Validate<CreateAndRenameBlueprintWorks_Validation>();
        }
    }
}