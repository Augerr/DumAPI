using Microsoft.AspNetCore.Mvc;
using DumAPI.Persistence.Models;
using DumAPI.Persistence.Services;

namespace DumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DummyRolesController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IService<DummyRole> _service;

        public DummyRolesController(IService<DummyRole> service, ILogger<DummyRolesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET: api/DummyRoles
        [HttpGet]
        public ActionResult<IEnumerable<DummyRole>> GetDummyRoles()
        {
            return Ok(_service.GetAll());
        }

        // GET: api/DummyRoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DummyRole>> GetDummyRole(int id)
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
