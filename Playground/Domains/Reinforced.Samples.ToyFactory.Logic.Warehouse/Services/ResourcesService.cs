using System;
using System.Linq;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Samples.ToyFactory.Logic.Channels.Queries;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities.Suppliement;
using Reinforced.Tecture.Aspects.Orm.Commands.Add;
using Reinforced.Tecture.Aspects.Orm.Commands.Update;
using Reinforced.Tecture.Aspects.Orm.Commands.UpdatePk;
using Reinforced.Tecture.Aspects.Orm.PrimaryKey;
using Reinforced.Tecture.Aspects.Orm.Queries;
using Reinforced.Tecture.Aspects.Orm.Toolings;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Services;

namespace Reinforced.Samples.ToyFactory.Logic.Warehouse.Services
{
    public class ResourcesService: TectureService
    <
        Modifies<ResourceSupply>
        , Adds<ResourceSupplyStatusHistoryItem,Resource>
     
    >
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

        public IAddition<ResourceSupplyStatusHistoryItem> AddEntity(int resourceSupplyId, ResourceSupplyStatus newStatus)
        {
            var rs = From<Db>().Get<ResourceSupply>().ById(resourceSupplyId);
            ResourceSupplyStatusHistoryItem prev = null;
            try
            {
                prev =
                    From<Db>().All<ResourceSupplyStatusHistoryItem>().Last(item => item.ResourceSupplyId == rs.Id);
            }
            catch (InvalidOperationException) //no history yet
            {
                
            }

            ResourceSupplyStatusHistoryItem current = new ResourceSupplyStatusHistoryItem();
            current.Comment = ""; //it's a sample app anyway
            current.CreatedDate = DateTime.Now.Date;
            current.ResourceSupplyId = rs.Id;
            if (prev != null)
            {
                current.Previous = prev.Next;
            }
            current.Next = newStatus;
            
            To<Db>().Update()
                .Set(x => x.Status, newStatus)
                .ByPk(rs.Id).Annotate($"Updating status = {newStatus} to resource {rs.Name}");

           var ret = To<Db>().Add(current).
               Annotate($"Adding new status in history tracker, prev = {current.Previous}, new = {current.Next}");

           return ret;
        }
    }
}