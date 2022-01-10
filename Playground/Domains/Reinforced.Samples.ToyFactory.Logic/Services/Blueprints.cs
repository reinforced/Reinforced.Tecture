using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Samples.ToyFactory.Logic.Entities;
using Reinforced.Tecture.Aspects.Orm.Commands.Add;
using Reinforced.Tecture.Aspects.Orm.Commands.Delete;
using Reinforced.Tecture.Aspects.Orm.Commands.DeletePk;
using Reinforced.Tecture.Aspects.Orm.Commands.Update;
using Reinforced.Tecture.Aspects.Orm.Commands.UpdatePk;
using Reinforced.Tecture.Aspects.Orm.PrimaryKey;
using Reinforced.Tecture.Aspects.Orm.Toolings;
using Reinforced.Tecture.Services;

namespace Reinforced.Samples.ToyFactory.Logic.Warehouse.Services
{
    public class Blueprints : TectureService
    <
        Modifies<Blueprint>,
        Modifies<ToyType>
    >
    {
        private Blueprints(){}

        public IAddition<Blueprint> CreateOne(int toyTypeId, string name)
        {
            return To<Db>().Add(new Blueprint()
            {
                ToyTypeId = toyTypeId,
                Name = name
            });
        }

        public void ChangeBlueprint(int id, int newToyTypeId)
        {
            To<Db>()
                .Update<Blueprint>()
                .Set(x => x.ToyTypeId, newToyTypeId)
                .ByPk(id);
            
        }
        
        public void RemoveBlueprint(int id)
        {
            To<Db>()
                .Delete<Blueprint>()
                .ByPk(id);
        }
        
        public void RemoveToyType(int id)
        {
            To<Db>()
                .Delete<ToyType>()
                .ByPk(id);
        }
    }
}