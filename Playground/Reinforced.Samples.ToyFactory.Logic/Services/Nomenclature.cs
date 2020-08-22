using System;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Samples.ToyFactory.Logic.Entities;
using Reinforced.Tecture.Features.Orm.Commands.Add;
using Reinforced.Tecture.Features.Orm.PrimaryKey;
using Reinforced.Tecture.Services;

namespace Reinforced.Samples.ToyFactory.Logic.Services
{
    public class Nomenclature : TectureService<ToyType, Blueprint, Resource, BlueprintResources>, INoContext
    {
        private Nomenclature() { }

        public IAddition<ToyType> CreateType(String name)
        {
            var tt = new ToyType() { Name = name };
            var ex = To<Db>().Add(tt);
            return ex;
        }
    }
}
