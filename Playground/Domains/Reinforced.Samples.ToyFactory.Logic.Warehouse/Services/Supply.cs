using System;
using System.Collections.Generic;
using System.Linq;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Samples.ToyFactory.Logic.Channels.Queries;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Dto;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities.Suppliement;
using Reinforced.Tecture.Aspects.DirectSql;
using Reinforced.Tecture.Aspects.DirectSql.Commands;
using Reinforced.Tecture.Aspects.DirectSql.Queries;
using Reinforced.Tecture.Aspects.DirectSql.Toolings;
using Reinforced.Tecture.Aspects.Orm.Commands.Add;
using Reinforced.Tecture.Aspects.Orm.Commands.Relate;
using Reinforced.Tecture.Aspects.Orm.PrimaryKey;
using Reinforced.Tecture.Aspects.Orm.Queries;
using Reinforced.Tecture.Aspects.Orm.Toolings;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Services;

namespace Reinforced.Samples.ToyFactory.Logic.Warehouse.Services
{
    public class Supply : 
        TectureService<
            Adds<Resource,ResourceSupply,ResourceSupplyItem>,
            Updates<ResourceSupplyItem>,
            MakesSqlCommands
        >
    {
        private Supply() { }

        public void RemoveResourceSupply(int id)
        {
            From<Db>().All<ResourceSupply>().EnsureExists(id);

            To<Db>().Sql<ResourceSupplyItem>(x => $"DELETE {x.NoExpand()} FROM {x} WHERE {x.ResourceSupplyId == id}");
            To<Db>().Sql<ResourceSupply>(x => $"DELETE {x.NoExpand()} FROM {x} WHERE {x.Id == id}");
        }

        public void FinishResourceSupply(int id)
        {
            To<Db>().Sql<Resource, ResourceSupplyItem>((res, item) => $@"
    UPDATE {res.NoExpand()}
    SET {res.StockQuantity == res.StockQuantity + item.Quantity}
    FROM {res}
    INNER JOIN {item} ON {item.ResourceId == res.Id}
    WHERE {item.ResourceSupplyId==id}
");
            To<Db>().Sql<ResourceSupply>(r =>
                $"UPDATE {r.NoExpand()} SET {r.Status == ResourceSupplyStatus.Closed} FROM {r} WHERE {r.Id == id}");
        }

        private void UpdateResourceSupplyItemsCount(int supplyId)
        {
            Finally(() =>
            {
                To<Db>().Sql<ResourceSupply, ResourceSupplyItem>((r, item) =>
                    $"UPATE {r.NoExpand()} SET {r.ItemsCount} = (SELECT COUNT(*) FROM {item} WHERE {item.ResourceSupplyId == supplyId}) FROM {r}");
            });
            
        }

        public IAddition<ResourceSupply> CreateResourceSupply(string name, IEnumerable<ResourceItemDto> items)
        {
            var rs = new ResourceSupply()
            {
                CreationDate = DateTime.UtcNow,
                Name = name,
                Status = ResourceSupplyStatus.Open
            };
            var r = To<Db>().Add(rs).Annotate("add resource supply");
            var toBeAdded = items.ToArray();
            var knownIds = toBeAdded.Where(x => x.Id.HasValue).Select(x => x.Id.Value).ToArray();
            var names = toBeAdded.Where(x => !x.Id.HasValue).Select(x => x.Name).ToArray();

            var byNames = From<Db>().All<Resource>()
                    .Describe($"lookup resources by {names.Length} names")
                    .Where(x => names.Contains(x.Name))
                ;

            var byIds = From<Db>().SqlQuery<Resource>(x => $"SELECT * FROM {x} WHERE {knownIds.Contains(x.Id)}")
                .As<Resource>();

            var byNamesDict = byNames.ToDictionary(x => x.Name);
            var byIdsSet = new HashSet<int>(byIds.Select(x => x.Id));

            using (var c = Cycle("add items to suppliemnt"))
            {
                foreach (var resourceItemDto in toBeAdded)
                {
                    int resourceId;
                    if (resourceItemDto.Id.HasValue)
                    {
                        Comment($"looking resource by id {resourceItemDto.Id}");
                        if (!byIdsSet.Contains(resourceItemDto.Id.Value))
                        {
                            throw new Exception($"could not locate resource with id#{resourceItemDto.Id.Value}");
                        }

                        resourceId = resourceItemDto.Id.Value;
                    }
                    else
                    {
                        if (!byNamesDict.ContainsKey(resourceItemDto.Name))
                        {
                            throw new Exception($"could not locate resource with name '{resourceItemDto.Name}'");
                        }

                        resourceId = byNamesDict[resourceItemDto.Name].Id;
                    }

                    var rsi = new ResourceSupplyItem()
                    {
                        ResourceId = resourceId,
                        Quantity = resourceItemDto.Quantity
                    };

                    To<Db>().Relate(rsi, x => x.ResourceSupply, rs);
                    To<Db>().Add(rsi).Annotate("add resource supply item");
                    c.Iteration("added resource item");
                }
            }

            Then(() =>
            {
                var id = From<Db>().Key<int>(r);
                UpdateResourceSupplyItemsCount(id);
            });
            return r;

        }
    }
}
