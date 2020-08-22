using System;
using Reinforced.Tecture.Testing.Validation;
using Reinforced.Tecture.Tracing;
using Reinforced.Samples.ToyFactory.Logic.Entities;
using Reinforced.Tecture.Features.Orm.Commands.Add;
using static Reinforced.Tecture.Features.Orm.Testing.Checks.Add.AddChecks;
using static Reinforced.Tecture.Testing.Checks.CommonChecks;

namespace Reinforced.Samples.ToyFactory.Tests.CreateTypeWorks
{
		class CreateTypeWorks_Validation:ValidationBase
		{
			protected override void Validate(TraceValidator flow)
			{ 
				flow.Then<Add<ToyType>>
				(
					Add<ToyType>(),
					Annotated(@"Create new toy type")
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
