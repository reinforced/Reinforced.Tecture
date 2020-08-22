using System;
using Reinforced.Tecture.Testing.Validation;
using Reinforced.Tecture.Tracing;
using Reinforced.Samples.ToyFactory.Logic.Entities;
using Reinforced.Tecture.Features.Orm.Commands.Add;
using static Reinforced.Tecture.Features.Orm.Testing.Checks.Add.AddChecks;

namespace Reinforced.Samples.ToyFactory.Tests.CreateTypeWorks
{
		class Validation:ValidationBase
		{
			protected override void Validate(TraceValidator flow)
			{ 
				flow.Then<Add<ToyType>>(Add<ToyType>());
				flow.SomethingHappens();
				flow.SomethingHappens();
				flow.TheEnd();
			}

		}
}
