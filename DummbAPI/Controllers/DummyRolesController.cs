using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DummbAPI.Persistence;
using DummbAPI.Persistence.Models;

namespace DummbAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DummyRolesController : ControllerBase
    {
        private readonly DummyDbContext _context;

        public DummyRolesController(DummyDbContext context)
        {
            _context = context;
        }

        // GET: api/DummyRoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DummyRole>>> GetDummyRoles()
        {
            return await _context.DummyRoles.ToListAsync();
        }

        // GET: api/DummyRoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DummyRole>> GetDummyRole(int id)
        {
            var dummyRole = await _context.DummyRoles.FindAsync(id);

            if (dummyRole == null)
            {
                return NotFound();
            }

            return dummyRole;
        }
    }
}
