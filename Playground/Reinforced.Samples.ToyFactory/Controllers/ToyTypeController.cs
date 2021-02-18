using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reinforced.Samples.ToyFactory.Dto.ToyType;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Samples.ToyFactory.Logic.Entities;
using Reinforced.Samples.ToyFactory.Logic.Services;
using Reinforced.Tecture;
using Reinforced.Tecture.Aspects.Orm.Queries;

namespace Reinforced.Samples.ToyFactory.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ToyTypeController: Controller
    {
        private readonly ITecture _tecture;
        private readonly ILogger<ToyTypeController> _logger;

        public ToyTypeController(ITecture tecture, ILogger<ToyTypeController> logger)
        {
            _tecture = tecture;
            _logger = logger;
        }

        [HttpPost]
        [Route("CreateToyType")]
        public async Task<ActionResult<int>> CreateToyType([FromBody]CreateToyTypeDto req)
        {
            _tecture.BeginTrace();
            int result;
            try
            {
                var a = await _tecture.Do<ToyTypeService>().CreateType(req.Name);
                await _tecture.SaveAsync();
                result = _tecture.From<Db>().Key(a);
            }
            finally
            {
                _tecture.EndTrace();
            }
            return result;
        }

        [HttpPost]
        [Route("DeleteToyType")]
        public async Task<IActionResult> DeleteToyType([FromBody] DeleteToyTypeDto req)
        {
            _tecture.BeginTrace();
            try
            {
                await _tecture.Do<ToyTypeService>().DeleteType(req.Name);
                await _tecture.SaveAsync();
            }
            finally
            {
                _tecture.EndTrace();
            }
            return new OkResult();
        }

        [HttpGet]
        [Route("GetTypes")]
        public List<ToyTypeDto> GetTypes()
        {
            var start = DateTime.Now;
            _tecture.BeginTrace();
            List<ToyType> ret;
            try
            {
                ret = _tecture.Do<ToyTypeService>().GetToyTypes().ToList();
                
            }
            finally
            {
                _tecture.EndTrace();
            }

            var diff = (DateTime.Now - start);
            _logger.LogDebug($"Get request took {diff.TotalMilliseconds}");//Get request took 1639,8445 (cold start)


            return ret.ConvertAll(t => new ToyTypeDto{Id = t.Id,Name = t.Name});
        }
    }
}