﻿using System.Linq;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Samples.ToyFactory.Logic.Dto;
using Reinforced.Samples.ToyFactory.Logic.Services;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Services;
using Reinforced.Samples.ToyFactory.Tests.Infrastructure;
using Reinforced.Samples.ToyFactory.Tests.LogicTests.CreateBlueprintWorks;
using Reinforced.Samples.ToyFactory.Tests.LogicTests.CreateTypeWorks;
using Reinforced.Tecture;
using Reinforced.Tecture.Aspects.Orm.Queries;
using Xunit;
using Xunit.Abstractions;

namespace Reinforced.Samples.ToyFactory.Tests.LogicTests
{

    public class CreateTypeTests : TectureTestBase
    {
        [Fact]
        public void CreateTypeWorks()
        {
            using var c = Case<CreateTypeWorks_TestData>(out ITecture ctx);

            ctx.Do<ToyTypeService>().CreateType("test type");
            ctx.Save();

            c.Validate<CreateTypeWorks_Validation>();
        }
        
        [Fact]
        public void CreateBlueprintWorks()
        {
            using var c = Case<CreateBlueprintWorks_TestData>(out ITecture ctx);

            var a = ctx.Do<BlueprintService>().CreateBlueprint(1002);
            ctx.Save();

            Output.WriteLine(c.Text());
            c.Validate<CreateBlueprintWorks_Validation>();
        }

        public CreateTypeTests(ITestOutputHelper helper) : base(helper)
        {
        }
    }
}
