using System;
using System.Linq;
using System.Threading.Tasks;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Samples.ToyFactory.Logic.Channels.Queries;
using Reinforced.Samples.ToyFactory.Logic.Entities;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Dto;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities.Suppliement;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Services;
using Reinforced.Samples.ToyFactory.Tests.Infrastructure;
//using Reinforced.Samples.ToyFactory.Tests.WarehouseTests.CreateMeasurementUnit;
using Reinforced.Samples.ToyFactory.Tests.WarehouseTests.RenameMeasurementUnit;
using Reinforced.Samples.ToyFactory.Tests.WarehouseTests.SupplyCreationPipeline;
//using Reinforced.Samples.ToyFactory.Tests.WarehouseTests.RenameMeasurementUnit;
//using Reinforced.Samples.ToyFactory.Tests.WarehouseTests.TestAnonymousQuery;
using Reinforced.Tecture;
using Reinforced.Tecture.Aspects.DirectSql.Queries;
using Reinforced.Tecture.Aspects.Orm.Queries;
using Reinforced.Tecture.Aspects.Time.Queries;
using Reinforced.Tecture.Testing;
using Xunit;
using Xunit.Abstractions;

namespace Reinforced.Samples.ToyFactory.Tests.WarehouseTests
{

    public class ManageTests : TectureTestBase
    {
        [Fact]
        public async Task CreateMeasurementUnit()
        {
            // using var c = Case
            //     <CreateMeasurementUnit_TestData>
            //     (out ITecture ctx);
            //
            // var test = ctx.From<Logic.Channels.System>()
            //     .Date().Test;
            //
            // Output.WriteLine(c.Text());
            //
            // c.Validate<CreateMeasurementUnit_Validation>();
        }

        [Fact]
        public void RenameMeasurementUnit()
        {
            using var c = Case
                <RenameMeasurementUnit_TestData>
                (out ITecture ctx);

            var a = ctx.Do<Manage>().CreateMeasurementUnit("Kilograms", "kG");
            ctx.Save();
            var mu = ctx.From<Db>().Key(a);
            ctx.Let<Manage>().RenameMeasurementUnit(mu,"Kilo","kg");
            ctx.Save();
            ctx.Do<Manage>().DeleteMeasurementUnit(mu);
            ctx.Save();

            Output.WriteLine(c.Text());

            c.Validate<RenameMeasurementUnit_Validation>();
        }

        class IdName
        {
            public IdName(int id, string name)
            {
                Id = id;
                Name = name;
            }

            public int Id { get; set; }
            public string Name { get; set; }
        }
        [Fact]
        public void SupplyCreationPipeline()
        {
            using var c = Case
               //<SupplyCreationPipeline_TestData>
                (out ITecture ctx);
            try
            {
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
            }
            catch(Exception ex)
            {
                var text = c.Text();
                Output.WriteLine(c.Text());
                //c.Validate<SupplyCreationPipeline_Validation>();    
            }

            
            
        }

        [Fact]
        public void TestAnonymousQuery()
        {
            using var c = Case
                //<TestAnonymousQuery_TestData>
                (out ITecture ctx);
            var re = ctx.From<Db>().Get<Resource>().ById(183, x => new {x.Name, x.StockQuantity});

            //c.Validate<TestAnonymousQuery_Validation>();
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
