using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Samples.ToyFactory.Logic.Channels.Queries;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities.Suppliement;
using Reinforced.Tecture.Features.Orm.Commands.Add;
using Reinforced.Tecture.Features.Orm.PrimaryKey;
using Reinforced.Tecture.Features.Orm.Queries;
using Reinforced.Tecture.Services;

namespace Reinforced.Samples.ToyFactory.Logic.Services
{
    public class Supply : TectureService<Resource, ResourceSupply, ResourceSupplyItem,ResourceSupplyStatusHistoryItem>
    {
        private Supply() { }

        //public IAddition<ResourceSupply> CreateResourceSupply(string name)
        //{
        //    var rs = new ResourceSupply()
        //    {
        //        CreationDate = DateTime.UtcNow,
        //        Name = name,
        //        Status = ResourceSupplyStatus.Open
        //    };
        //}
    }
}
