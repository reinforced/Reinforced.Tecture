using System;
using Reinforced.Tecture.Testing.Validation;
using Reinforced.Tecture.Tracing;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities;
using Reinforced.Tecture.Features.Orm.Commands.Add;
using Reinforced.Tecture.Tracing.Commands;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities.Suppliement;
using Reinforced.Tecture.Features.Orm.Commands.Relate;
using Reinforced.Tecture.Features.SqlStroke.Commands;
using static Reinforced.Tecture.Features.Orm.Testing.Checks.Add.AddChecks;
using static Reinforced.Tecture.Testing.BuiltInChecks.CommonChecks;
using static Reinforced.Tecture.Features.SqlStroke.Testing.Checks.SqlChecks;

namespace Reinforced.Samples.ToyFactory.Tests.WarehouseTests.SupplyCreationPipeline
{
		class SupplyCreationPipeline_Validation : ValidationBase
		{
			protected override void Validate(TraceValidator flow)
			{ 
				flow.Then<Add>
				(
					Add<MeasurementUnit>(x=>
					{ 
						if (x.Name != @"Kilograms") return false;
						if (x.ShortName != @"kG") return false;
						return true;
					}, @"create measurement unit 'Kilograms' (kG)"), 
					Annotated(@"create measurement unit 'Kilograms' (kG)")
				);
				flow.Then<Save>();
				flow.Then<Add>
				(
					Add<Resource>(x=>
					{ 
						if (x.MeasurementUnitId != 103) return false;
						if (x.StockQuantity != 0) return false;
						if (x.Name != @"resource1") return false;
						return true;
					}, @"new resource resource1"), 
					Annotated(@"new resource resource1")
				);
				flow.Then<Add>
				(
					Add<Resource>(x=>
					{ 
						if (x.MeasurementUnitId != 103) return false;
						if (x.StockQuantity != 0) return false;
						if (x.Name != @"resource2") return false;
						return true;
					}, @"new resource resource2"), 
					Annotated(@"new resource resource2")
				);
				flow.Then<Add>
				(
					Add<Resource>(x=>
					{ 
						if (x.MeasurementUnitId != 103) return false;
						if (x.StockQuantity != 0) return false;
						if (x.Name != @"resource3") return false;
						return true;
					}, @"new resource resource3"), 
					Annotated(@"new resource resource3")
				);
				flow.Then<Save>();
				flow.Then<Add>
				(
					Add<ResourceSupply>(x=>
					{ 
						if (x.ItemsCount != 0) return false;
						if (x.Status != ResourceSupplyStatus.Open) return false;
						//if (x.CreationDate != new DateTime(2020, 8, 31, 6, 16, 5, 232, DateTimeKind.Utc)) return false;
						if (x.Name != @"Supply1") return false;
						return true;
					}, @"add resource supply"), 
					Annotated(@"add resource supply")
				);
				flow.Then<Relate>();
				flow.Then<Add>
				(
					Add<ResourceSupplyItem>(x=>
					{ 
						if (x.Quantity != 10) return false;
						if (x.ResourceSupplyId != 0) return false;
						if (x.ResourceId != 181) return false;
						return true;
					}, @"add resource supply item"), 
					Annotated(@"add resource supply item")
				);
				flow.Then<Relate>();
				flow.Then<Add>
				(
					Add<ResourceSupplyItem>(x=>
					{ 
						if (x.Quantity != 10) return false;
						if (x.ResourceSupplyId != 0) return false;
						if (x.ResourceId != 182) return false;
						return true;
					}, @"add resource supply item"), 
					Annotated(@"add resource supply item")
				);
				flow.Then<Relate>();
				flow.Then<Add>
				(
					Add<ResourceSupplyItem>(x=>
					{ 
						if (x.Quantity != 10) return false;
						if (x.ResourceSupplyId != 0) return false;
						if (x.ResourceId != 183) return false;
						return true;
					}, @"add resource supply item"), 
					Annotated(@"add resource supply item")
				);
				flow.Then<Save>();
				flow.Then<Sql>
				(
					SqlCommand(@"UPDATE [r] SET [r].[ItemsCount] = (SELECT COUNT(*) FROM [ResourceSupplyItem] [item]
 WHERE [item].[ResourceSupplyId] = {0}) FROM [ResourceSupply] [r]
"), 
					SqlParameters(46)
				);
				flow.Then<Save>();
				flow.Then<Sql>
				(
					SqlCommand(@"
    UPDATE [res]
    SET [res].[StockQuantity] = ([res].[StockQuantity] + [item].[Quantity])
    FROM [Resource] [res]

    INNER JOIN [ResourceSupplyItem] [item]
 ON [item].[ResourceId] = [res].[Id]
    WHERE [item].[ResourceSupplyId] = {0}
"), 
					SqlParameters(46)
				);
				flow.Then<Sql>
				(
					SqlCommand(@"UPDATE [r] SET [r].[Status] = {0} FROM [ResourceSupply] [r]
 WHERE [r].[Id] = {1}"), 
					SqlParameters(4, 46)
				);
				flow.Then<Save>();
				flow.TheEnd();
			}

		}
}
