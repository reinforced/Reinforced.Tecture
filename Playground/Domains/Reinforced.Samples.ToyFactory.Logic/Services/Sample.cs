using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities.Suppliement;
using Reinforced.Tecture.Aspects.Orm.Commands.Update;
using Reinforced.Tecture.Aspects.Orm.Commands.UpdatePk;
using Reinforced.Tecture.Aspects.Orm.Toolings;
using Reinforced.Tecture.Services;

namespace Reinforced.Samples.ToyFactory.Logic.Services
{
    public class Sample: 
        TectureService<Updates<ResourceSupply,Resource>>
    {
        public void Test()
        {
           // To<Db>().Update<Resource>().Set(x => x.StockQuantity, 50).ByPk(1);
          //  To<Db>().Update<ResourceSupply>().Set(x => x.Status, ResourceSupplyStatus.Closed).ByPk(2);
        }
    }
}