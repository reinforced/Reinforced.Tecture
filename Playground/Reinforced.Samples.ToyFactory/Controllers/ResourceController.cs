using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reinforced.Samples.ToyFactory.Dto.Resource;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Services;
using Reinforced.Tecture;
using Reinforced.Tecture.Aspects.Orm.Queries;

namespace Reinforced.Samples.ToyFactory.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : Controller
    {
        private readonly ITecture _tecture;

        public ResourceController(ITecture tecture)
        {
            _tecture = tecture;
        }
        
   
        
        [HttpPost]
        [Route("CreateResource")]
        public async Task<ActionResult<int>> CreateResource([FromBody] CreateResourceDto req)
        {
            _tecture.BeginTrace();
            int result;
            try
            {
                var a = _tecture.Do<ResourcesService>().CreateResource(req.Name, req.MeasurementUnit);
                await _tecture.SaveAsync();
                result = _tecture.From<Db>().Key(a);
                
            }
            finally
            {
                _tecture.EndTrace();
            }

            return result;
        }

        [HttpGet]
        [Route("GetResources")]
        public async Task<List<ResourceDto>> GetResources()
        {
            _tecture.BeginTrace();
            List<Resource> ret;
            try
            {
                ret = await _tecture.Do<ResourcesService>().GetAllResources().ToListAsync();
            }
            finally
            {
                _tecture.EndTrace();
            }

            return ret.ConvertAll(t => new ResourceDto {Id = t.Id, Name = t.Name, StockQuantity = t.StockQuantity});
        }
        
        
        [HttpPost]
        [Route("ChangeSupplyStatus")]
        public async Task<ActionResult<int>> ChangeSupplyStatus([FromBody] ChangeStatusDto req)
        {
            _tecture.BeginTrace();
            int result;
            try
            {
                var a = _tecture.Do<ResourcesService>().AddEntity(req.Id, req.NewStatus);
                await _tecture.SaveAsync();
                result = _tecture.From<Db>().Key(a);
            }
            finally
            {
                _tecture.EndTrace();
            }

            return result;
        }
        
    }
}