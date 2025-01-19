using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DummbAPI.Persistence;
using DummbAPI.Persistence.Models;

namespace DummbAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DummyUsersController : ControllerBase
    {
        private readonly DummyDbContext _context;

        public DummyUsersController(DummyDbContext context)
        {
            _context = context;
        }

        // GET: api/DummyUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DummyUser>>> GetUsers()
        {
            return await _context.DummyUsers.ToListAsync();
        }

        // GET: api/DummyUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DummyUser>> GetDummyUser(int id)
        {
            var dummyUser = await _context.DummyUsers.FindAsync(id);

            if (dummyUser == null)
            {
                return Ok("Object not found");
            }

            return dummyUser;
        }

        // GET: api/DummyUsers/{id}/Rights
        [HttpGet("{id}/Rights")]
        public async Task<ActionResult<ICollection<DummyUserRight>>> GetDummyUserRights(int id)
        {
            var dummyUserRights = await _context.DummyUserRights.AllAsync(u => u.UserId == id);

            if (dummyUserRights == null)
            {
                return Ok("Object not found");
            }

            return Ok(dummyUserRights);
        }

        // PUT: api/DummyUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDummyUser(int id, DummyUser dummyUser)
        {
            if (id != dummyUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(dummyUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DummyUserExists(id))
                {
                    return Ok("Object not found");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/DummyUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DummyUser>> PostDummyUser(DummyUser dummyUser)
        {
            _context.DummyUsers.Add(dummyUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDummyUser", new { id = dummyUser.Id }, dummyUser);
        }

        // DELETE: api/DummyUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDummyUser(int id)
        {
            var dummyUser = await _context.DummyUsers.FindAsync(id);
            if (dummyUser == null)
            {
                return Ok("Object not found");
            }

            _context.DummyUsers.Remove(dummyUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DummyUserExists(int id)
        {
            return _context.DummyUsers.Any(e => e.Id == id);
        }
    }
}
