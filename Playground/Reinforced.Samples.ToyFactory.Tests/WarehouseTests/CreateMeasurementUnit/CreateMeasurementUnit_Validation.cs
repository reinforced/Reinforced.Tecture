using System;
using Reinforced.Tecture.Testing.Validation;
using Reinforced.Tecture.Tracing;
using Reinforced.Tecture.Testing.Validation.Assertion;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities;
using Reinforced.Tecture.Aspects.Orm.Commands.Add;
using Reinforced.Samples.ToyFactory.Logic.Channels;

namespace Reinforced.Samples.ToyFactory.Tests.WarehouseTests.CreateMeasurementUnit
{
		class CreateMeasurementUnit_Validation : ValidationBase
		{
			protected override void Validate(TraceValidator flow)
			{ 
				flow.Then<Db, Add>
				(c=>
					{ 
						Assert.Equal(As<MeasurementUnit>(c.Entity).Id, 0, "Id of property Entity of command 'create measurement unit 'Kilograms' (kG)' has invalid value");
						Assert.Equal(As<MeasurementUnit>(c.Entity).ShortName, @"kG", "ShortName of property Entity of command 'create measurement unit 'Kilograms' (kG)' has invalid value");
						Assert.Equal(As<MeasurementUnit>(c.Entity).Name, @"Kilograms", "Name of property Entity of command 'create measurement unit 'Kilograms' (kG)' has invalid value");
						Assert.Equal(c.EntityType, typeof(MeasurementUnit), "property EntityType of command 'create measurement unit 'Kilograms' (kG)' has invalid value");
						Assert.Equal(c.Annotation, @"create measurement unit 'Kilograms' (kG)", "Annotation has invalid value");
					}
				);
				flow.TheEnd();
			}

		}
}
