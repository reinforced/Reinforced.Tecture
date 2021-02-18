using System;
using System.Linq;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities;
using Reinforced.Tecture.Aspects.Orm.Commands.Add;
using Reinforced.Tecture.Aspects.Orm.PrimaryKey;
using Reinforced.Tecture.Aspects.Orm.Queries;
using Reinforced.Tecture.Aspects.Orm.Toolings;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Services;

namespace Reinforced.Samples.ToyFactory.Logic.Warehouse.Services
{
    public class ResourcesService: TectureService<Modifies<Resource>>
    {
        private ResourcesService() { }
        public IAddition<Resource> CreateResource(string name, string measurementUnit)
        {
            if (From<Db>().Get<Resource>().All.Describe("check resource existence").Any(x => x.Name == name))
            {
                throw new Exception($"Cannot add resource '{name}' because it already exists");
            }

            var unit = From<Db>().All<MeasurementUnit>()
                .Describe($"lookup measurement unit by name ({measurementUnit})")
                .Where(x => x.Name == measurementUnit || x.ShortName == measurementUnit)
                .Select(x => x.Id)
                .First();

            var res = new Resource() { Name = name, MeasurementUnitId = unit };
            var ex = To<Db>().Add(res).Annotate($"new resource {name}");
            return ex;
        }
        
        public IQueryable<Resource> GetAllResources()
        {
            return From<Db>().Get<Resource>().All;
        }
    }
}