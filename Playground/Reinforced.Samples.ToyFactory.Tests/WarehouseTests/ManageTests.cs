using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Dto;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Services;
using Reinforced.Samples.ToyFactory.Tests.Infrastructure;
using Reinforced.Samples.ToyFactory.Tests.WarehouseTests.CreateMeasurementUnit;
using Reinforced.Samples.ToyFactory.Tests.WarehouseTests.RenameMeasurementUnit;
using Reinforced.Samples.ToyFactory.Tests.WarehouseTests.SupplyCreationPipeline;
using Reinforced.Tecture;
using Reinforced.Tecture.Features.Orm.Queries;
using Xunit;
using Xunit.Abstractions;

namespace Reinforced.Samples.ToyFactory.Tests.WarehouseTests
{
    public class ManageTests : TectureTestBase
    {
        [Fact]
        public void CreateMeasurementUnit()
        {
            using var c = Case<CreateMeasurementUnit_TestData>(out ITecture ctx);

            ctx.Do<Manage>().CreateMeasurementUnit("Kilograms", "kG");

            Output.WriteLine(c.Text());

            c.Validate<CreateMeasurementUnit_Validation>();
        }

        [Fact]
        public void RenameMeasurementUnit()
        {
            using var c = Case<RenameMeasurementUnit_TestData>(out ITecture ctx);

            var a = ctx.Do<Manage>().CreateMeasurementUnit("Kilograms", "kG");
            ctx.Save();
            var mu = ctx.From<Db>().Key(a);
            ctx.Do<Manage>().RenameMeasurementUnit(mu,"Kilo","kg");
            ctx.Save();
            ctx.Do<Manage>().DeleteMeasurementUnit(mu);
            ctx.Save();

            Output.WriteLine(c.Text());

            c.Validate<RenameMeasurementUnit_Validation>();
        }

        [Fact]
        public void SupplyCreationPipeline()
        {
            using var c = Case<SupplyCreationPipeline_TestData>(out ITecture ctx);
            var m = ctx.Do<Manage>();
            var unit = m.CreateMeasurementUnit("Kilograms", "kG");
            ctx.Save();
            var res1 = m.CreateResource("resource1", "kG");
            var res2 = m.CreateResource("resource2", "kG");
            var res3 = m.CreateResource("resource3", "kG");
            ctx.Save();
            var id = ctx.From<Db>().Key(res2);
            var supply = ctx.Do<Supply>();

            var supp = supply.CreateResourceSupply("Supply1", new[]
            {
                new ResourceItemDto()
                {
                    Name = "resource1", Quantity = 10
                },
                new ResourceItemDto()
                {
                    Id = id, Quantity = 10
                },
                new ResourceItemDto()
                {
                    Name = "resource3", Quantity = 10
                },
            });

            ctx.Save();
            var supplyId = ctx.From<Db>().Key(supp);
            supply.FinishResourceSupply(supplyId);
            ctx.Save();
            //supply.RemoveResourceSupply(supplyId);
            //ctx.Save();
            c.Validate<SupplyCreationPipeline_Validation>();
            Output.WriteLine(c.Text());
        }

        public ManageTests(ITestOutputHelper output) : base(output)
        {
        }
    }
}
