using Reinforced.Samples.ToyFactory.Logic.Entities;
using Reinforced.Tecture.Features.Orm.Commands.Add;
using Reinforced.Tecture.Features.Orm.Testing.Checks.Add;
using Reinforced.Tecture.Testing.BuiltInChecks;
using Reinforced.Tecture.Testing.Validation;
using Reinforced.Tecture.Tracing.Commands;

namespace Reinforced.Samples.ToyFactory.Tests.LogicTests.CreateTypeWorks
{
		class CreateTypeWorks_Validation : ValidationBase
		{
			protected override void Validate(TraceValidator flow)
			{ 
				flow.Then<Add<ToyType>>
				(
					AddChecks.Add<ToyType>(x=>
					{ 
						if (x.Name != @"test type") return false;
						return true;
					}, @"Create new toy type"), 
					CommonChecks.Annotated(@"Create new toy type")
				);
				flow.Then<Save>();
				flow.TheEnd();
			}

		}
}
