using System;
using Reinforced.Tecture.Testing.Validation;
using Reinforced.Tecture.Tracing;
using Reinforced.Tecture.Testing.Validation.Assertion;
using Reinforced.Samples.ToyFactory.Logic.Entities;
using Reinforced.Tecture.Aspects.Orm.Commands.Add;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Tecture.Aspects.Orm.Commands.Relate;
using Reinforced.Tecture.Tracing.Commands;
using Reinforced.Tecture.Channels;

namespace Reinforced.Samples.ToyFactory.Tests.LogicTests.CreateBlueprintWorks
{
		class CreateBlueprintWorks_Validation : ValidationBase
		{
			protected override void Validate(TraceValidator flow)
			{ 
				flow.Then<Db, Add>
				(c=>
					{ 
						Assert.Equal(As<Blueprint>(c.Entity).Id, 0, "Id of property Entity of command 'Create blueprint' has invalid value");
						Assert.Null(As<Blueprint>(c.Entity).ToyType, "ToyType of property Entity of command 'Create blueprint' must be null");
						Assert.Equal(As<Blueprint>(c.Entity).ToyTypeId, 32, "ToyTypeId of property Entity of command 'Create blueprint' has invalid value");
						Assert.Equal(c.EntityType, typeof(Blueprint), "property EntityType of command 'Create blueprint' has invalid value");
						Assert.Equal(c.Annotation, @"Create blueprint", "Annotation has invalid value");
					}
				);
				flow.Then<Db, Relate>
				(c=>
					{ 
						Assert.Equal(As<Blueprint>(c.Primary).Id, 0, "Id of property Primary of command 'Ensure association with toy type' has invalid value");
						Assert.Null(As<Blueprint>(c.Primary).ToyType, "ToyType of property Primary of command 'Ensure association with toy type' must be null");
						Assert.Equal(As<Blueprint>(c.Primary).ToyTypeId, 32, "ToyTypeId of property Primary of command 'Ensure association with toy type' has invalid value");
						Assert.Equal(c.PrimaryType, typeof(Blueprint), "property PrimaryType of command 'Ensure association with toy type' has invalid value");
						Assert.Equal(As<ToyType>(c.Secondary).Id, 32, "Id of property Secondary of command 'Ensure association with toy type' has invalid value");
						Assert.Equal(As<ToyType>(c.Secondary).Name, @"test type2", "Name of property Secondary of command 'Ensure association with toy type' has invalid value");
						Assert.Equal(c.SecondaryType, typeof(ToyType), "property SecondaryType of command 'Ensure association with toy type' has invalid value");
						Assert.Equal(c.ForeignKeySpecifier, @"ToyType", "property ForeignKeySpecifier of command 'Ensure association with toy type' has invalid value");
						Assert.Equal(c.Annotation, @"Ensure association with toy type", "Annotation has invalid value");
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
