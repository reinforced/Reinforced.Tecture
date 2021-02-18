using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reinforced.Samples.ToyFactory.Dto.ResourceSupply;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Samples.ToyFactory.Logic.Services;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Services;
using Reinforced.Tecture;
using Reinforced.Tecture.Aspects.Orm.Queries;

namespace Reinforced.Samples.ToyFactory.Controllers
{
    public class ResourceSupply : Controller
    {
        private readonly ITecture _tecture;

        public ResourceSupply(ITecture tecture)
        {
            _tecture = tecture;
        }
        
        [HttpPost]
        [Route("CreateSupply")]
        public async Task<ActionResult<int>> CreateSupply([FromBody] CreateResourceSupplyDto input)
        {
            _tecture.BeginTrace();
            ActionResult<int> result;
            try
            {
                var a = _tecture.Do<SupplyService>().CreateResourceSupply(input.Name,input.Items);
                await _tecture.SaveAsync();
                result = _tecture.From<Db>().Key(a);
            }
            catch
            {
                return new BadRequestResult();
            }
            finally
            {
                _tecture.EndTrace();
            }

            return result;
        }
        
        [HttpPost]
        [Route("RemoveResourceSupply")]
        public async Task<IActionResult> DeleteMeasurementUnit([FromBody] RemoveResourceSupplyDto req)
        {
            _tecture.BeginTrace();
            try
            {
                _tecture.Do<SupplyService>().RemoveResourceSupply(req.Id);
                await _tecture.SaveAsync();
            }
            finally
            {
                _tecture.EndTrace();
            }

            return new OkResult();
        }
    }
}