using System;
using System.Linq;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Samples.ToyFactory.Logic.Entities;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Features.Orm.Commands.Add;
using Reinforced.Tecture.Features.Orm.PrimaryKey;
using Reinforced.Tecture.Features.Orm.Queries;
using Reinforced.Tecture.Services;

namespace Reinforced.Samples.ToyFactory.Logic.Services
{
    public class Nomenclature : TectureService<ToyType, Blueprint, Resource, BlueprintResources>, INoContext
    {
        private Nomenclature() { }

        public IAddition<ToyType> CreateType(String name)
        {
            if (From<Db>().Get<ToyType>().All.Describe("check toy type existence").Any(x => x.Name == name))
            {
                throw new Exception($"Cannot add toy type '{name}' because it already exists");
            }
            var tt = new ToyType() { Name = name };
            var ex = To<Db>().Add(tt).Annotate("Create new toy type");
            return ex;
        }
    }
}
