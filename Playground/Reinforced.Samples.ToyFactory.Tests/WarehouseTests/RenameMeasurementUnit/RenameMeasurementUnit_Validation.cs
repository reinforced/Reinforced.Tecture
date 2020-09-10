using System;
using Reinforced.Tecture.Testing.Validation;
using Reinforced.Tecture.Tracing;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities;
using Reinforced.Tecture.Aspects.Orm.Commands.Add;
using Reinforced.Tecture.Tracing.Commands;
using System.Collections.Generic;
using Reinforced.Tecture.Aspects.Orm.Commands.Update;
using Reinforced.Tecture.Aspects.Orm.Commands.DeletePk;
using static Reinforced.Tecture.Aspects.Orm.Testing.Checks.Add.AddChecks;
using static Reinforced.Tecture.Testing.BuiltInChecks.CommonChecks;
using static Reinforced.Tecture.Aspects.Orm.Testing.Checks.Update.UpdateChecks;
using static Reinforced.Tecture.Aspects.Orm.Testing.Checks.DeletePk.DeletePKChecks;

namespace Reinforced.Samples.ToyFactory.Tests.WarehouseTests.RenameMeasurementUnit
{
		class RenameMeasurementUnit_Validation : ValidationBase
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
				flow.Then<Update>
				(
					Update<MeasurementUnit>(new Dictionary<String, Object>() {  { @"Name", @"Kilo" } ,  { @"ShortName", @"kg" }  } , @"")
				);
				flow.Then<Save>();
				flow.Then<DeletePk>
				(
					DeleteByPK<MeasurementUnit>(@"remove measurement unit#108", 108), 
					Annotated(@"remove measurement unit#108")
				);
				flow.Then<Save>();
				flow.TheEnd();
			}

		}
}
