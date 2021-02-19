﻿using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Samples.ToyFactory.Logic.Channels.Queries;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Dto;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities.Suppliement;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Services;
using Reinforced.Samples.ToyFactory.Tests.Infrastructure;
using Reinforced.Samples.ToyFactory.Tests.WarehouseTests.CreateMeasurementUnit;

using Reinforced.Samples.ToyFactory.Tests.WarehouseTests.SupplyCreationPipeline;
using Reinforced.Samples.ToyFactory.Tests.WarehouseTests.Test;
using Reinforced.Samples.ToyFactory.Tests.WarehouseTests.TestAnonymousQuery;
using Reinforced.Tecture;
using Reinforced.Tecture.Aspects.DirectSql.Queries;
using Reinforced.Tecture.Aspects.Orm.Queries;
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

            ctx.Do<MeasurementUnitService>().CreateMeasurementUnit("Kilograms", "kG");

            Output.WriteLine(c.Text());

            c.Validate<CreateMeasurementUnit_Validation>();
        }

        [Fact]
        public void RenameMeasurementUnit()
        {
            using var c = Case
                //<RenameMeasurementUnit_TestData>
                (out ITecture ctx);

            var a = ctx.Do<MeasurementUnitService>().CreateMeasurementUnit("Kilograms", "kG");
            ctx.Save();
            var mu = ctx.From<Db>().Key(a);
            ctx.Do<MeasurementUnitService>().RenameMeasurementUnit(mu,"Kilo","kg");
            ctx.Save();
            ctx.Do<MeasurementUnitService>().DeleteMeasurementUnit(mu);
            ctx.Save();

            Output.WriteLine(c.Text());

            //c.Validate<RenameMeasurementUnit_Validation>();
        }

        // [Fact]
        // public void Test()
        // {
        //     using var c = Case
        //         <Test_TestData>
        //         (out ITecture ctx);
        //     var m = ctx.Do<Manage>();
        //     var unit = m.CreateMeasurementUnit("Kilograms", "kG");
        //     ctx.Save();
        //     var res1 = m.CreateResource("resource1", "kG");
        //     var res2 = m.CreateResource("resource2", "kG");
        //     var res3 = m.CreateResource("resource3", "kG");
        //     var res4 = m.CreateResource("resource4", "kG");
        //     ctx.Save();
        //     Output.WriteLine(c.Text());
        //     c.Validate<Test_Validation>();
        //     
        //   
        // }
        
        // [Fact]
        // public void SupplyCreationPipeline()
        // {
        //     using var c = Case<SupplyCreationPipeline_TestData>(out ITecture ctx);
        //     var m = ctx.Do<Manage>();
        //     var unit = m.CreateMeasurementUnit("Kilograms", "kG");
        //     ctx.Save();
        //     var res1 = m.CreateResource("resource1", "kG");
        //     var res2 = m.CreateResource("resource2", "kG");
        //     var res3 = m.CreateResource("resource3", "kG");
        //   
        //     ctx.Save();
        //     var id = ctx.From<Db>().Key(res2);
        //     var supply = ctx.Do<Supply>();
        //
        //     var supp = supply.CreateResourceSupply("Supply1", new[]
        //     {
        //         new ResourceItemDto()
        //         {
        //             Name = "resource1", Quantity = 10
        //         },
        //         new ResourceItemDto()
        //         {
        //             Id = id, Quantity = 10
        //         },
        //         new ResourceItemDto()
        //         {
        //             Name = "resource3", Quantity = 10
        //          }
        //     });
        //
        //     ctx.Save();
        //     var supplyId = ctx.From<Db>().Key(supp);
        //     supply.FinishResourceSupply(supplyId);
        //     ctx.Save();
        //     c.Validate<SupplyCreationPipeline_Validation>();
        //     Output.WriteLine(c.Text());
        // }

        [Fact]
        public void TestAnonymousQuery()
        {
            using var c = Case
                <TestAnonymousQuery_TestData>
                (out ITecture ctx);
            var re = ctx.From<Db>().Get<Resource>().ById(183, x => new {x.Name, x.StockQuantity});

            c.Validate<TestAnonymousQuery_Validation>();
            Output.WriteLine(c.Text());
        }

        
        [Fact(Skip = "For debug purposes")]
        public void TestNestedSQLQuery()
        {
            using var c = Case(out ITecture ctx);
            var re = ctx.From<Db>()
                .SqlQuery<ResourceSupplyItem>
                    (r=>$"SELECT {r.Resource.Name}, {r.ResourceSupply.ItemsCount} FROM {r} WHERE {r.ResourceSupplyId>10}")
                .As<Resource>();

            Output.WriteLine(c.Text());
        }

        public ManageTests(ITestOutputHelper output) : base(output)
        {
        }
    }
}
