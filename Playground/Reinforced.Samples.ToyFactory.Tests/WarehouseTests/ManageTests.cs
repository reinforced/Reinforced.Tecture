using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Services;
using Reinforced.Samples.ToyFactory.Tests.Infrastructure;
using Reinforced.Samples.ToyFactory.Tests.LogicTests.CreateBlueprintWorks;
using Reinforced.Samples.ToyFactory.Tests.WarehouseTests.CreateMeasurementUnit;
using Reinforced.Samples.ToyFactory.Tests.WarehouseTests.RenameMeasurementUnit;
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
        public ManageTests(ITestOutputHelper output) : base(output)
        {
        }
    }
}
