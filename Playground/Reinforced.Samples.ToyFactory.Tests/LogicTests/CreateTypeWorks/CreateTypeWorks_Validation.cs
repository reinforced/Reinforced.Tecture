using System;
using Reinforced.Tecture.Testing.Validation;
using Reinforced.Tecture.Tracing;
using Reinforced.Tecture.Testing.Validation.Assertion;
using Reinforced.Samples.ToyFactory.Logic.Entities;
using Reinforced.Tecture.Aspects.Orm.Commands.Add;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Tecture.Tracing.Commands;
using Reinforced.Tecture.Channels;

namespace Reinforced.Samples.ToyFactory.Tests.LogicTests.CreateTypeWorks
{
		class CreateTypeWorks_Validation : ValidationBase
		{
			protected override void Validate(TraceValidator flow)
			{ 
				flow.Then<Db, Add>
				(c=>
					{ 
						Assert.Equal(As<ToyType>(c.Entity).Id, 0, "Id of property Entity of command 'Create new toy type' has invalid value");
						Assert.Equal(As<ToyType>(c.Entity).Name, @"test type2", "Name of property Entity of command 'Create new toy type' has invalid value");
						Assert.Equal(c.EntityType, typeof(ToyType), "property EntityType of command 'Create new toy type' has invalid value");
						Assert.Equal(c.Annotation, @"Create new toy type", "Annotation has invalid value");
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
