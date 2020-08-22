using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Samples.ToyFactory.Logic.Services;
using Reinforced.Tecture;
using Reinforced.Tecture.Features.Orm.Queries;

namespace Reinforced.Samples.ToyFactory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NomenclatureController : ControllerBase
    {
        
        private readonly ILogger<NomenclatureController> _logger;
        private readonly ITecture _tecture;

        public NomenclatureController(ILogger<NomenclatureController> logger, ITecture tecture)
        {
            _logger = logger;
            _tecture = tecture;
        }

        [HttpPost]
        public int CreateType(string name)
        {
            var a = _tecture.Do<Nomenclature>().CreateType(name);
            _tecture.Save();
            var r = _tecture.From<Db>().Key(a);
            return r;
        }
    }
}
