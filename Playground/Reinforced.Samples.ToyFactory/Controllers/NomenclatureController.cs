using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reinforced.Samples.ToyFactory.Dto.Nomenclature;
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

        [HttpPost]
        [Route("CreateToyType")]
        public ActionResult<int> CreateToyType([FromBody]CreateToyTypeDto req)
        {
            _tecture.BeginTrace();
            int result = 0;
            try
            {
                var a = _tecture.Do<Nomenclature>().CreateType(req.Name);
                _tecture.Save();
                var x = a.Result;
                result = _tecture.From<Db>().Key(x);
            }
            finally
            {
                var t = _tecture.EndTrace();
                var text = t.Explain();
            }
            return result;
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

        [HttpGet(Name = "Test")]
        public Task<int> Test()
        {
            _tecture.BeginTrace();
            int result = 0;
            try
            {
                var a = _tecture.Do<Manage>().CreateMeasurementUnit("testBig","testShort");
                _tecture.Save();
                result = _tecture.From<Db>().Key(a);
            }
            finally
            {
                var t = _tecture.EndTrace();
                var text = t.Explain();
            }
            return Task.FromResult(result);
        }
    }
}
