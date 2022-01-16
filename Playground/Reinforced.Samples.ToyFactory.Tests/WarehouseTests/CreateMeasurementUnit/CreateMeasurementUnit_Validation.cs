using System;
using Reinforced.Tecture.Testing.Validation;
using Reinforced.Tecture.Tracing;
using Reinforced.Tecture.Testing.Validation.Assertion;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities;
using Reinforced.Tecture.Aspects.Orm.Commands.Add;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Tecture.Tracing.Commands;
using Reinforced.Tecture.Channels;

namespace Reinforced.Samples.ToyFactory.Tests.WarehouseTests.CreateMeasurementUnit
{
		public partial class CreateMeasurementUnit_Validation : ValidationBase
		{
			protected override void Validate(TraceValidator flow)
			{ 
				flow.Then<Db, Add>
				(c=>
					{ 
						Assert.NotNull(As<MeasurementUnit>(c.Entity), "added entity must not be null");
						Assert.Equal(As<MeasurementUnit>(c.Entity).Id, 0, "Id of added entity has invalid value");
						Assert.NotNull(As<MeasurementUnit>(c.Entity).ShortName, "ShortName of added entity must not be null");
						Assert.Equal(As<MeasurementUnit>(c.Entity).ShortName, @"kG", "ShortName of added entity has invalid value");
						Assert.NotNull(As<MeasurementUnit>(c.Entity).Name, "Name of added entity must not be null");
						Assert.Equal(As<MeasurementUnit>(c.Entity).Name, @"Kilograms", "Name of added entity has invalid value");
						Assert.NotNull(c.EntityType, "type of added entity must not be null");
						Assert.Equal(c.EntityType, typeof(MeasurementUnit), "type of added entity has invalid value");
						Assert.NotNull(c.Annotation, "Annotation must not be null");
						Assert.Equal(c.Annotation, @"create measurement unit 'Kilograms' (kG)", "Annotation has invalid value");
					}
				);
				flow.Then<NoChannel, Save>
				(c=>
					{ 
						Assert.NotNull(c.Annotation, "Annotation must not be null");
						Assert.Equal(c.Annotation, @"", "Annotation has invalid value");
					}
				);
				flow.TheEnd();
			}

		}
}
