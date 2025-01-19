using DumAPI.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace DumAPI.Persistence.Services
{
    public class RoleService(DummyDbContext context)
    {
        private readonly DummyDbContext _context = context;

        public async Task<IEnumerable<DummyRole>> GetAll() =>
            await _context.DummyRoles.ToListAsync();

        public async Task<DummyRole?> Get(int id) =>
            await _context.DummyRoles.FirstOrDefaultAsync(x => x.Id == id);
    }
}
