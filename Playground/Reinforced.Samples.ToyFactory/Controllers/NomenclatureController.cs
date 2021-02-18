using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reinforced.Samples.ToyFactory.Dto.ToyType;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Samples.ToyFactory.Logic.Channels.Queries;
using Reinforced.Samples.ToyFactory.Logic.Entities;
using Reinforced.Samples.ToyFactory.Logic.Services;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Services;
using Reinforced.Samples.ToyFactory.Queries;
using Reinforced.Tecture;
using Reinforced.Tecture.Aspects.Orm.Queries;

namespace Reinforced.Samples.ToyFactory.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class NomenclatureController : Controller
    {
      
        private readonly ITecture _tecture;

        public NomenclatureController( ITecture tecture)
        {
            _tecture = tecture;
        }


        [HttpGet]
        [Route("Test")]
        public async Task<ToyType> GetToyType(int id)
        {
            _tecture.BeginTrace();
            var r = await _tecture.From<Db>().Get<ToyType>().ByIdAsync(id);
            var trc = _tecture.EndTrace();
            var text = trc.Explain();
            return r;
        }

       
    }
}
