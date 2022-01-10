using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Samples.ToyFactory.Logic.Channels.Queries;
using Reinforced.Samples.ToyFactory.Logic.Dto;
using Reinforced.Samples.ToyFactory.Logic.Entities;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities.Suppliement;
using Reinforced.Tecture;
using Reinforced.Tecture.Aspects.Orm.Commands.Add;
using Reinforced.Tecture.Aspects.Orm.Commands.Delete;
using Reinforced.Tecture.Aspects.Orm.Commands.DeletePk;
using Reinforced.Tecture.Aspects.Orm.Commands.Derelate;
using Reinforced.Tecture.Aspects.Orm.Commands.Relate;
using Reinforced.Tecture.Aspects.Orm.Commands.Update;
using Reinforced.Tecture.Aspects.Orm.Commands.UpdatePk;
using Reinforced.Tecture.Aspects.Orm.PrimaryKey;
using Reinforced.Tecture.Aspects.Orm.Queries;
using Reinforced.Tecture.Aspects.Orm.Toolings;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Services;

namespace Reinforced.Samples.ToyFactory.Logic.Services
{
    class Sample : TectureService
        <
            Updates<Resource,ResourceSupply>
        >
    {
        public void Test()
        {
            To<Db>().Update<Resource>().Set(x => x.MeasurementUnitId, 50).ByPk(1);
            To<Db>().Update<ResourceSupply>().Set(x => x.Name, "111").ByPk(10);
            
        }
    }

    public class Nomenclature : TectureService
        <
            Adds<ToyType, BlueprintResources>,
            Deletes<Resource>,
            Modifies<Blueprint>
        >
    {
        private Nomenclature() { }

        public IAddition<ToyType> CreateType(string name)
        {

            if (From<Db>().Get<ToyType>().All.Describe("check toy type existence").Any(x => x.Name == name))
            {
                throw new Exception($"Cannot add toy type '{name}' because it already exists");
            }
            var tt = new ToyType() { Name = name };
            var ex = To<Db>().Add(tt).Annotate("Create new toy type");
            Then(() =>
            {
                var tw = From<Db>().Get<ToyType>().All.First();
            });

            return ex;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="toyTypeId"></param>
        /// <returns></returns>
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

        public void AddResourceToBlueprint(int blueprintId, IEnumerable<ResourceWithQuantity> rwq)
        {


            From<Db>().All<Blueprint>().EnsureExists(blueprintId);

            var resourceIds = rwq.Select(x => x.ResourceId).ToArray();

            var count = From<Db>()
                .All<Resource>()
                .Describe("check that all the supplied resources are in DB")
                .Count(x => resourceIds.Contains(x.Id));

            if (count < resourceIds.Length)
            {
                throw new Exception("Some supplied resourced does not exist in DB");
            }

            using (var c = Cycle("adding resources"))
            {
                foreach (var resourceWithQuantity in rwq)
                {
                    To<Db>().Add(new BlueprintResources()
                    {
                        BlueprintId = blueprintId,
                        ResourceId = resourceWithQuantity.ResourceId,
                        Quantity = resourceWithQuantity.Quantity
                    });
                    c.Iteration($"resource {resourceWithQuantity.ResourceId} added");
                }
            }
        }
    }
}
