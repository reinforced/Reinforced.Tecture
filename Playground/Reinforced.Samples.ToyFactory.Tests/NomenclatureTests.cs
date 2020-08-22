using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Samples.ToyFactory.Logic.Services;
using Reinforced.Samples.ToyFactory.Tests.CreateTypeWorks;
using Reinforced.Samples.ToyFactory.Tests.Infrastructure;
using Reinforced.Tecture;
using Reinforced.Tecture.Features.Orm.Queries;
using Xunit;
using Xunit.Abstractions;

namespace Reinforced.Samples.ToyFactory.Tests
{

    public class NomenclatureTests : TectureTestBase
    {
        [Fact]
        public void CreateTypeWorks()
        {
            using (var c = Case
                <CreateTypeWorks_TestData>
                (out ITecture ctx))
            {
                var a = ctx.Do<Nomenclature>().CreateType("test type");
                ctx.Save();
                var id = ctx.From<Db>().Key(a);

                c.Validate<CreateTypeWorks_Validation>();
            }
        }

        public NomenclatureTests(ITestOutputHelper helper) : base(helper)
        {
        }
    }
}
