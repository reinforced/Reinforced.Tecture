using System;
using Reinforced.Tecture.Testing.Validation;
using Reinforced.Tecture.Tracing;
using Reinforced.Samples.ToyFactory.Logic.Entities;
using Reinforced.Tecture.Features.Orm.Commands.Add;
using static Reinforced.Tecture.Features.Orm.Testing.Checks.Add.AddChecks;
using static Reinforced.Tecture.Testing.BuiltInChecks.CommonChecks;

namespace Reinforced.Samples.ToyFactory.Tests.CreateTypeWorks
{
		class CreateTypeWorks_Validation : ValidationBase
		{
			protected override void Validate(TraceValidator flow)
			{ 
				flow.Then<Add<ToyType>>
				(
					Add<ToyType>(x=>
					{ 
						if (x.Name != @"test type") return false;
						return true;
					}, @"Create new toy type"), 
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
