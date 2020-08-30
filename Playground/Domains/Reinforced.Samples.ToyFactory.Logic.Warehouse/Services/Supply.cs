using System;
using System.Collections.Generic;
using System.Linq;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Samples.ToyFactory.Logic.Channels.Queries;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Dto;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities.Suppliement;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Features.Orm.Commands.Add;
using Reinforced.Tecture.Features.Orm.PrimaryKey;
using Reinforced.Tecture.Features.Orm.Queries;
using Reinforced.Tecture.Features.SqlStroke;
using Reinforced.Tecture.Features.SqlStroke.Commands;
using Reinforced.Tecture.Features.SqlStroke.Queries;
using Reinforced.Tecture.Services;

namespace Reinforced.Samples.ToyFactory.Logic.Warehouse.Services
{
    public class Supply : TectureService<Resource, ResourceSupply, ResourceSupplyItem, ResourceSupplyStatusHistoryItem>
    {
        private Supply() { }

        public void RemoveResourceSupply(int id)
        {
            From<Db>().All<ResourceSupply>().EnsureExists(id);

            To<Db>().SqlStroke<ResourceSupplyItem>(x => $"DELETE FROM {x} WHERE {x.ResourceSupplyId == id}");
            To<Db>().SqlStroke<ResourceSupply>(x => $"DELETE FROM {x} WHERE {x.Id == id}");
        }

        public void FinishResourceSupply(int id)
        {
            To<Db>().SqlStroke<Resource, ResourceSupplyItem>((res, item) => $@"
    UPDATE {res.Alias()}
    SET {res.StockQuantity == res.StockQuantity + item.Quantity}
    FROM {res}
    INNER JOIN {item} ON {item.ResourceId == res.Id}
    WHERE {item.ResourceSupplyId==id}
");
            To<Db>().SqlStroke<ResourceSupply>(r =>
                $"UPDATE {r.Alias()} SET {r.Status == ResourceSupplyStatus.Closed} FROM {r} WHERE {r.Id == id}");
        }

        private void UpdateResourceSupplyItemsCount(int supplyId)
        {
            Final.ContinueWith(() =>
            {
                To<Db>().SqlStroke<ResourceSupply, ResourceSupplyItem>((r, item) =>
                    $"UPDATE {r} SET {r.ItemsCount} = (SELECT COUNT(*) FROM {item} WHERE {item.ResourceSupplyId == supplyId})");
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

            using (Cycle("add items to suppliemnt"))
            {
                foreach (var resourceItemDto in toBeAdded)
                {
                    int respurceId;
                    if (resourceItemDto.Id.HasValue)
                    {
                        Comment($"looking resource by id {resourceItemDto.Id}");
                        if (!byIdsSet.Contains(resourceItemDto.Id.Value))
                        {
                            throw new Exception($"could not locate resource with id#{resourceItemDto.Id.Value}");
                        }

                        respurceId = resourceItemDto.Id.Value;
                    }
                    else
                    {
                        if (!byNamesDict.ContainsKey(resourceItemDto.Name))
                        {
                            throw new Exception($"could not locate resource with name '{resourceItemDto.Name}'");
                        }

                        respurceId = byNamesDict[resourceItemDto.Name].Id;
                    }

                    var rsi = new ResourceSupplyItem()
                    {
                        ResourceId = respurceId,
                        Quantity = resourceItemDto.Quantity,
                    };

                    To<Db>().Add(rsi).Annotate("add resource supply item");
                }
            }

            var r = To<Db>().Add(rs).Annotate("add resource supply");

            Save.ContinueWith(() =>
            {
                var id = From<Db>().Key(r);
                UpdateResourceSupplyItemsCount(id);
            });
            return r;

        }
    }
}
