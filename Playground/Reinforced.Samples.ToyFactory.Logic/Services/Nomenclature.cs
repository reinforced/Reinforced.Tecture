using System;
using System.Linq;
using System.Threading.Tasks;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Samples.ToyFactory.Logic.Entities;
using Reinforced.Samples.ToyFactory.Logic.Queries;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Features.Orm.Commands.Add;
using Reinforced.Tecture.Features.Orm.Commands.Delete;
using Reinforced.Tecture.Features.Orm.Commands.Derelate;
using Reinforced.Tecture.Features.Orm.Commands.Relate;
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

        public IAddition<Blueprint> CreateBlueprint(int toyTypeId)
        {
            From<Db>().All<ToyType>().EnsureExists(toyTypeId);

            if (From<Db>().All<Blueprint>().Describe("check blueprint existence").Any(x => x.ToyTypeId == toyTypeId))
            {
                throw new Exception($"Cannot add blueprint for toyType#{toyTypeId} because it already exists");
            }

            var blueprint = new Blueprint() { ToyTypeId = toyTypeId };
            var toyType = From<Db>().Get<ToyType>().ById(toyTypeId);

            var ex = To<Db>().Add(blueprint).Annotate("Create blueprint");

            To<Db>().Relate(blueprint, x => x.ToyType, toyType).Annotate("Ensure association with toy type");
            return ex;
        }
    }
}
