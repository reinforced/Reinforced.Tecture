using System;
using System.Linq;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Samples.ToyFactory.Logic.Channels.Queries;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities;
using Reinforced.Tecture.Aspects.Orm.Commands.Add;
using Reinforced.Tecture.Aspects.Orm.Commands.Delete;
using Reinforced.Tecture.Aspects.Orm.Commands.DeletePk;
using Reinforced.Tecture.Aspects.Orm.Commands.Update;
using Reinforced.Tecture.Aspects.Orm.Commands.UpdatePk;
using Reinforced.Tecture.Aspects.Orm.PrimaryKey;
using Reinforced.Tecture.Aspects.Orm.Queries;
using Reinforced.Tecture.Aspects.Orm.Toolings;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Services;

namespace Reinforced.Samples.ToyFactory.Logic.Warehouse.Services
{
    public class MeasurementUnitService: TectureService<Modifies<MeasurementUnit>>
    {
        private MeasurementUnitService() { }
        public IAddition<MeasurementUnit> CreateMeasurementUnit(string name, string shortName)
        {
            if (From<Db>().Get<MeasurementUnit>().All
                .Describe("check unit existence")
                .Any(x => x.Name == name || x.ShortName == shortName))
            {
                throw new Exception($"Cannot add measurement unit '{name}' because it already exists");
            }

            
            return To<Db>().Add(new MeasurementUnit
                {
                    Name = name,
                    ShortName = shortName
                })
                .Annotate($"create measurement unit '{name}' ({shortName})");
        }
        
        public void RenameMeasurementUnit(int id, string name, string shortName)
        {
            To<Db>().Update()
                .Set(x => x.Name, name)
                .Set(x => x.ShortName, shortName)
                .ByPk(id);
        }

        public void DeleteMeasurementUnit(int id)
        {
            From<Db>().Get<MeasurementUnit>().All.EnsureExists(id);
            To<Db>().Delete().ByPk(id).Annotate($"remove measurement unit # {id}");
        }
        
        public IQueryable<MeasurementUnit> GetMeasurementUnits()
        {
            return From<Db>().Get<MeasurementUnit>().All;
        }
    }
}