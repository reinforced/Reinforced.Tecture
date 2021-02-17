using Microsoft.AspNetCore.Mvc;
using Reinforced.Tecture;

namespace Reinforced.Samples.ToyFactory.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController: Controller
    {
        private readonly ITecture _tecture;

        public SampleController(ITecture tecture)
        {
            _tecture = tecture;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return new OkResult();
        }
    }
}