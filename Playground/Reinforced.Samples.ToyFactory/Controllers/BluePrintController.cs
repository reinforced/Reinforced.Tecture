using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reinforced.Samples.ToyFactory.Dto.Blueprint;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Samples.ToyFactory.Logic.Services;
using Reinforced.Tecture;
using Reinforced.Tecture.Aspects.Orm.Queries;

namespace Reinforced.Samples.ToyFactory.Controllers
{
    public class BluePrintController : Controller
    {
        private readonly ITecture _tecture;

        public BluePrintController(ITecture tecture)
        {
            _tecture = tecture;
        }
        
        [HttpPost]
        [Route("CreateBluePrint")]
        public async Task<ActionResult<int>> CreateResource([FromBody] int toyTypeId)
        {
            _tecture.BeginTrace();
            ActionResult<int> result;
            try
            {
                var a = _tecture.Do<BlueprintService>().CreateBlueprint(toyTypeId);
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
        [Route("AddResourcesToBlueprint")]
        public async Task<ActionResult> AddResource([FromBody] AddResourceDto data)
        {
            _tecture.BeginTrace();
            ActionResult ret = new OkResult();
            
            try
            {
                _tecture.Do<BlueprintService>().AddResourceToBlueprint(data.BlueprintId, data.Resources);
                await _tecture.SaveAsync();
            }
            catch (Exception)
            {
                ret = new BadRequestResult();
            }
            finally
            {
                _tecture.EndTrace();
            }

            return ret;
        }
    }
}