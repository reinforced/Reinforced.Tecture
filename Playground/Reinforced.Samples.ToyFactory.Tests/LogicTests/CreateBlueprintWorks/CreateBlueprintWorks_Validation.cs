using Reinforced.Samples.ToyFactory.Logic.Entities;
using Reinforced.Tecture.Features.Orm.Commands.Add;
using Reinforced.Tecture.Features.Orm.Commands.Relate;
using Reinforced.Tecture.Features.Orm.Testing.Checks.Add;
using Reinforced.Tecture.Testing.BuiltInChecks;
using Reinforced.Tecture.Testing.Validation;
using Reinforced.Tecture.Tracing.Commands;

namespace Reinforced.Samples.ToyFactory.Tests.LogicTests.CreateBlueprintWorks
{
		class CreateBlueprintWorks_Validation : ValidationBase
		{
			protected override void Validate(TraceValidator flow)
			{ 
				flow.Then<Add<Blueprint>>
				(
					AddChecks.Add<Blueprint>(x=>
					{ 
						if (x.ToyTypeId != 79) return false;
						return true;
					}, @"Create blueprint"), 
					CommonChecks.Annotated(@"Create blueprint")
				);
				flow.Then<Relate>
				(
					CommonChecks.Annotated(@"Ensure association with toy type")
				);
				flow.Then<Save>();
				flow.TheEnd();
			}

		}
}
