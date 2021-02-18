using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reinforced.Samples.ToyFactory.Data;
using Reinforced.Samples.ToyFactory.Dto.ToyType;

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

    }
}