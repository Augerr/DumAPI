using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DummbAPI.Persistence;
using DummbAPI.Persistence.Models;

namespace DummbAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DummyRightsController : ControllerBase
    {
        private readonly DummyDbContext _context;

        public DummyRightsController(DummyDbContext context)
        {
            _context = context;
        }

        // GET: api/DummyRights
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DummyRight>>> GetDummyRights()
        {
            return await _context.DummyRights.ToListAsync();
        }

        // GET: api/DummyRights/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DummyRight>> GetDummyRight(int id)
        {
            var dummyRight = await _context.DummyRights.FindAsync(id);

            if (dummyRight == null)
            {
                return NotFound();
            }

            return dummyRight;
        }
    }
}
