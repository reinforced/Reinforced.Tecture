using System;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IAddition<ToyType>> CreateType(string name)
        {
            if (From<Db>().Get<ToyType>().All.Describe("check toy type existence").Any(x => x.Name == name))
            {
                throw new Exception($"Cannot add toy type '{name}' because it already exists");
            }
            var tt = new ToyType() { Name = name };
            var ex = To<Db>().Add(tt).Annotate("Create new toy type");
            await Save;
            var tw = From<Db>().Get<ToyType>().All.First();
            return ex;
        }

        public IAddition<Resource> CreateResource(string name)
        {
            if (From<Db>().Get<Resource>().All.Describe("check resource existence").Any(x => x.Name == name))
            {
                throw new Exception($"Cannot add resource '{name}' because it already exists");
            }

            var res = new Resource() { Name = name };
            var ex = To<Db>().Add(res).Annotate("Create new resource");
            return ex;
        }
    }
}
