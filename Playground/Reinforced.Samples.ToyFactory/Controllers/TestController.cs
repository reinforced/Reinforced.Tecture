using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reinforced.Samples.ToyFactory.Data;
using Reinforced.Samples.ToyFactory.Dto.ToyType;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Samples.ToyFactory.Logic.Entities;
using Reinforced.Samples.ToyFactory.Logic.Services;

namespace Reinforced.Samples.ToyFactory.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        private readonly ToyFactoryDbContext _context;
        private readonly ILogger<ToyTypeController> _logger;

        public TestController(ToyFactoryDbContext context, ILogger<ToyTypeController> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        [HttpGet]
        [Route("GetTypes")]
        public List<ToyTypeDto> GetTypes()
        {
            var start = DateTime.Now;
            var ret = _context.ToyTypes.ToList();
            
            var diff = (DateTime.Now - start);
            _logger.LogDebug($"Get request took {diff.TotalMilliseconds}"); //  Get request took  1416,9069

            return ret.ConvertAll(t => new ToyTypeDto{Id = t.Id,Name = t.Name}); 
        }
        
        [HttpPost]
        [Route("CreateToyType")]
        public async Task<ActionResult<int>> CreateToyType([FromBody]CreateToyTypeDto req)
        {
            var tt = new ToyType {Name = req.Name};
            if (_context.ToyTypes.Any(t => t.Name == req.Name))
                return new BadRequestResult();
            var add = await _context.ToyTypes.AddAsync(tt);
            await _context.SaveChangesAsync();
            return add.Entity.Id;
        }

    }
}