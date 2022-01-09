using System;
using Reinforced.Tecture.Testing.Validation;
using Reinforced.Tecture.Tracing;
using Reinforced.Tecture.Testing.Validation.Assertion;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities;
using Reinforced.Tecture.Aspects.Orm.Commands.Add;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Tecture.Tracing.Commands;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Aspects.Orm.Commands.UpdatePk;
using Reinforced.Tecture.Aspects.Orm.Commands.DeletePk;

namespace Reinforced.Samples.ToyFactory.Tests.WarehouseTests.RenameMeasurementUnit
{
		class RenameMeasurementUnit_Validation : ValidationBase
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
				flow.Then<NoChannel, Save>
				(c=>
					{ 
						Assert.Equal(c.Annotation, @"", "Annotation has invalid value");
					}
				);
				flow.Then<Db, UpdatePk>
				(c=>
					{ 
						Assert.Collection(c.KeyValues, "property KeyValues of command UpdatePk must be composed correctly", r0_0=>
						{ 
							Assert.Equal(As<Int32>(r0_0), 29, "item #0 of property KeyValues of command UpdatePk has invalid value");
						});
						Assert.Equal(c.EntityType, typeof(MeasurementUnit), "property EntityType of command UpdatePk has invalid value");
						Assert.Equal(c.UpdateValuesStringKeys.Count, 2, "property UpdateValuesStringKeys of command UpdatePk has invalid size");
						Assert.Equal(c.UpdateValuesStringKeys[@"Name"], @"Kilo", "value for Name in property UpdateValuesStringKeys of command UpdatePk has invalid value");
						Assert.Equal(c.UpdateValuesStringKeys[@"ShortName"], @"kg", "value for ShortName in property UpdateValuesStringKeys of command UpdatePk has invalid value");
						Assert.Equal(c.Annotation, @"", "Annotation has invalid value");
					}
				);
				flow.Then<NoChannel, Save>
				(c=>
					{ 
						Assert.Equal(c.Annotation, @"", "Annotation has invalid value");
					}
				);
				flow.Then<Db, DeletePk>
				(c=>
					{ 
						Assert.Equal(c.EntityType, typeof(MeasurementUnit), "property EntityType of command 'remove measurement unit#29' has invalid value");
						Assert.Collection(c.KeyValues, "property KeyValues of command 'remove measurement unit#29' must be composed correctly", r0_0=>
						{ 
							Assert.Equal(As<Int32>(r0_0), 29, "item #0 of property KeyValues of command 'remove measurement unit#29' has invalid value");
						});
						Assert.Equal(c.Annotation, @"remove measurement unit#29", "Annotation has invalid value");
					}
				);
				flow.Then<NoChannel, Save>
				(c=>
					{ 
						Assert.Equal(c.Annotation, @"", "Annotation has invalid value");
					}
				);
				flow.TheEnd();
			}

		}
}
