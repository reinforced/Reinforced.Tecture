using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Samples.ToyFactory.Logic.Entities;
using Reinforced.Tecture.Aspects.Orm.Commands.Add;
using Reinforced.Tecture.Aspects.Orm.Commands.Delete;
using Reinforced.Tecture.Aspects.Orm.PrimaryKey;
using Reinforced.Tecture.Aspects.Orm.Queries;
using Reinforced.Tecture.Aspects.Orm.Toolings;
using Reinforced.Tecture.Commands;
using Reinforced.Tecture.Services;

namespace Reinforced.Samples.ToyFactory.Logic.Services
{
    public class ToyTypeService : TectureService 
    <
        Adds<ToyType>,
        Deletes<ToyType>
    >
    {
        private ToyTypeService(){}
        private void CheckAndThrowIfToyTypeExists(string name)
        {
            if (From<Db>().Get<ToyType>().All.Describe("check toy type existence").Any(x => x.Name == name))
            {
                throw new Exception($"Cannot add toy type '{name}' because it already exists");
            }
        }
        
        public async Task<IAddition<ToyType>> CreateType(string name)
        {
            CheckAndThrowIfToyTypeExists(name);

            var tt = new ToyType {Name = name};
            var ex = To<Db>().Add(tt).Annotate("Create new toy type");
            await Save;

            return ex;
        }

        public async Task DeleteType(string name)
        {
            CheckAndThrowIfToyTypeExists(name);
            var toDel = await From<Db>().Get<ToyType>().That(t => t.Name == name).All.FirstAsync();
            To<Db>().Delete(toDel).Annotate($"Deleting toy type {toDel.Id} {toDel.Name}");
        }

        public IQueryable<ToyType> GetToyTypes()
        {
           return From<Db>().Get<ToyType>().All;
        }
    }
}