using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reinforced.Samples.ToyFactory.Dto.MeasurementUnit;
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
    public class MeasurementUnitsController : Controller
    {
        private readonly ITecture _tecture;

        public MeasurementUnitsController(ITecture tecture)
        {
            _tecture = tecture;
        }

        [HttpPost]
        [Route("CreateMeasurementUnit")]
        public async Task<ActionResult<int>> CreateMeasurementUnit([FromBody] CreateMeasurementUnitDto req)
        {
            _tecture.BeginTrace();
            int result;
            try
            {
                var a = _tecture.Do<MeasurementUnitService>().CreateMeasurementUnit(req.Name, req.ShortName);
                await _tecture.SaveAsync();
                result = _tecture.From<Db>().Key(a);
            }
            finally
            {
                _tecture.EndTrace();
            }

            return result;
        }

        [HttpPut]
        [Route("RenameMeasurementUnit")]
        public async Task<IActionResult> RenameMeasurementUnit([FromBody] RenameMeasurementUnitDto req)
        {
            _tecture.BeginTrace();
            try
            {
                _tecture.Do<MeasurementUnitService>().RenameMeasurementUnit(req.ID, req.Name, req.ShortName);
                await _tecture.SaveAsync();
            }
            finally
            {
                _tecture.EndTrace();
            }

            return new OkResult();
        }

        [HttpPost]
        [Route("DeleteMeasurementUnit")]
        public async Task<IActionResult> DeleteMeasurementUnit([FromBody] DeleteMeasurementUnitDto req)
        {
            _tecture.BeginTrace();
            try
            {
                _tecture.Do<MeasurementUnitService>().DeleteMeasurementUnit(req.Id);
                await _tecture.SaveAsync();
            }
            finally
            {
                _tecture.EndTrace();
            }

            return new OkResult();
        }

        [HttpGet]
        [Route("GetMeasurementUnits")]
        public async Task<List<MeasurementUnitDto>> GetUnits()
        {
            _tecture.BeginTrace();
            List<MeasurementUnit> ret;
            try
            {
                ret = await _tecture.Do<MeasurementUnitService>().GetMeasurementUnits().ToListAsync();
            }
            finally
            {
                _tecture.EndTrace();
            }

            return ret.ConvertAll(t => new MeasurementUnitDto {Id = t.Id, Name = t.Name, ShortName = t.ShortName});
        }
    }
}