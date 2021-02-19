using System;
using Reinforced.Tecture.Testing.Validation;
using Reinforced.Tecture.Tracing;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities;
using Reinforced.Tecture.Aspects.Orm.Commands.Add;
using Reinforced.Tecture.Tracing.Commands;
using static Reinforced.Tecture.Aspects.Orm.Testing.Checks.Add.AddChecks;
using static Reinforced.Tecture.Testing.BuiltInChecks.CommonChecks;

namespace Reinforced.Samples.ToyFactory.Tests.WarehouseTests.Test
{
		class Test_Validation : ValidationBase
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
						if (x.MeasurementUnitId != 6) return false;
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
						if (x.MeasurementUnitId != 6) return false;
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
						if (x.MeasurementUnitId != 6) return false;
						if (x.StockQuantity != 0) return false;
						if (x.Name != @"resource3") return false;
						return true;
					}, @"new resource resource3"), 
					Annotated(@"new resource resource3")
				);
				flow.Then<Add>
				(
					Add<Resource>(x=>
					{ 
						if (x.MeasurementUnitId != 6) return false;
						if (x.StockQuantity != 0) return false;
						if (x.Name != @"resource4") return false;
						return true;
					}, @"new resource resource4"), 
					Annotated(@"new resource resource4")
				);
				flow.Then<Save>();
				flow.TheEnd();
			}

		}
}
