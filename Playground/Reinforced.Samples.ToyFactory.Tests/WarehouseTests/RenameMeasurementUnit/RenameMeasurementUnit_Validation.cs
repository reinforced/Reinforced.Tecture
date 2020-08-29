using System;
using Reinforced.Tecture.Testing.Validation;
using Reinforced.Tecture.Tracing;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities;
using Reinforced.Tecture.Features.Orm.Commands.Add;
using Reinforced.Tecture.Tracing.Commands;
using Reinforced.Tecture.Features.Orm.Commands.Update;
using static Reinforced.Tecture.Features.Orm.Testing.Checks.Add.AddChecks;
using static Reinforced.Tecture.Testing.BuiltInChecks.CommonChecks;

namespace Reinforced.Samples.ToyFactory.Tests.WarehouseTests.RenameMeasurementUnit
{
		class RenameMeasurementUnit_Validation : ValidationBase
		{
			protected override void Validate(TraceValidator flow)
			{ 
				flow.Then<Add<MeasurementUnit>>
				(
					Add<MeasurementUnit>(x=>
					{ 
						if (x.Name != @"Kilo") return false;
						if (x.ShortName != @"kg") return false;
						return true;
					}, @"create measurement unit 'Kilograms' (kG)"), 
					Annotated(@"create measurement unit 'Kilograms' (kG)")
				);
				flow.Then<Save>
				(
					Saved(), 
					Annotated(@"")
				);
				flow.Then<Update>
				(
					Annotated(@"")
				);
				flow.Then<Save>
				(
					Saved(), 
					Annotated(@"")
				);
				flow.TheEnd();
			}

		}
}
