using System;
using Reinforced.Tecture.Testing.Validation;
using Reinforced.Tecture.Tracing;
using Reinforced.Tecture.Testing.Validation.Assertion;
using Reinforced.Samples.ToyFactory.Logic.Entities;
using Reinforced.Tecture.Aspects.Orm.Commands.Add;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Tecture.Tracing.Commands;
using Reinforced.Tecture.Channels;
using Reinforced.Tecture.Aspects.Orm.Commands.UpdatePk;
using Reinforced.Tecture.Aspects.Orm.Commands.DeletePk;

namespace Reinforced.Samples.ToyFactory.Tests.LogicTests.CreateAndRenameBlueprintWorks
{
		class CreateAndRenameBlueprintWorks_Validation : ValidationBase
		{
			protected override void Validate(TraceValidator flow)
			{ 
				flow.Then<Db, Add>
				(c=>
					{ 
						Assert.Equal(As<ToyType>(c.Entity).Id, 0, "Id of added entity has invalid value");
						Assert.Equal(As<ToyType>(c.Entity).Name, @"Test Toy Type 1", "Name of added entity has invalid value");
						Assert.Equal(c.EntityType, typeof(ToyType), "type of added entity has invalid value");
						Assert.Equal(c.Annotation, @"Create new toy type", "Annotation has invalid value");
					}
				);
				flow.Then<Db, Add>
				(c=>
					{ 
						Assert.Equal(As<ToyType>(c.Entity).Id, 0, "Id of added entity has invalid value");
						Assert.Equal(As<ToyType>(c.Entity).Name, @"Test Toy Type 2", "Name of added entity has invalid value");
						Assert.Equal(c.EntityType, typeof(ToyType), "type of added entity has invalid value");
						Assert.Equal(c.Annotation, @"Create new toy type", "Annotation has invalid value");
					}
				);
				flow.Then<NoChannel, Save>
				(c=>
					{ 
						Assert.Equal(c.Annotation, @"", "Annotation has invalid value");
					}
				);
				flow.Then<Db, Add>
				(c=>
					{ 
						Assert.Equal(As<Blueprint>(c.Entity).Id, 0, "Id of added entity has invalid value");
						Assert.Null(As<Blueprint>(c.Entity).ToyType, "ToyType of added entity must be null");
						Assert.Equal(As<Blueprint>(c.Entity).ToyTypeId, 43, "ToyTypeId of added entity has invalid value");
						Assert.Equal(As<Blueprint>(c.Entity).Name, @"test", "Name of added entity has invalid value");
						Assert.Equal(c.EntityType, typeof(Blueprint), "type of added entity has invalid value");
						Assert.Equal(c.Annotation, @"", "Annotation has invalid value");
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
						Assert.Collection(c.KeyValues, "primary key must be composed correctly", r0_0=>
						{ 
							Assert.Equal(As<Int32>(r0_0), 7, "item #0 of primary key has invalid value");
						});
						Assert.Equal(c.EntityType, typeof(Blueprint), "type of entity to update has invalid value");
						Assert.Equal(c.UpdateValuesStringKeys.Count, 1, "updated values has invalid size");
						Assert.Equal(As<Int32>(c.UpdateValuesStringKeys[@"ToyTypeId"]), 44, "value for ToyTypeId in updated values has invalid value");
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
						Assert.Equal(c.EntityType, typeof(Blueprint), "type of entity to delete has invalid value");
						Assert.Collection(c.KeyValues, "primary key must be composed correctly", r0_0=>
						{ 
							Assert.Equal(As<Int32>(r0_0), 7, "item #0 of primary key has invalid value");
						});
						Assert.Equal(c.Annotation, @"", "Annotation has invalid value");
					}
				);
				flow.Then<Db, DeletePk>
				(c=>
					{ 
						Assert.Equal(c.EntityType, typeof(ToyType), "type of entity to delete has invalid value");
						Assert.Collection(c.KeyValues, "primary key must be composed correctly", r0_0=>
						{ 
							Assert.Equal(As<Int32>(r0_0), 43, "item #0 of primary key has invalid value");
						});
						Assert.Equal(c.Annotation, @"", "Annotation has invalid value");
					}
				);
				flow.Then<Db, DeletePk>
				(c=>
					{ 
						Assert.Equal(c.EntityType, typeof(ToyType), "type of entity to delete has invalid value");
						Assert.Collection(c.KeyValues, "primary key must be composed correctly", r0_0=>
						{ 
							Assert.Equal(As<Int32>(r0_0), 44, "item #0 of primary key has invalid value");
						});
						Assert.Equal(c.Annotation, @"", "Annotation has invalid value");
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
