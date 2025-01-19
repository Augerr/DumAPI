using Microsoft.AspNetCore.Mvc;
using DumAPI.Persistence.Models;
using DumAPI.Persistence.Services;

namespace DumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DummyRightsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly RightService _service;

        public DummyRightsController(RightService service, ILogger<DummyRightsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET: api/DummyRights
        [HttpGet]
        public ActionResult<IEnumerable<DummyRight>> GetDummyRights()
        {
            return Ok(_service.GetAll());
        }

        // GET: api/DummyRights/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DummyRight>> GetDummyRight(int id)
        {
            var dummyUser = await _service.Get(id);

            if (dummyUser == null)
            {
                return Ok("Object not found");
            }

            return Ok(dummyUser);
        }
    }
}
