using DumAPI.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace DumAPI.Persistence.Services
{
    public class RightService(DummyDbContext context)
    {
        private readonly DummyDbContext _context = context;

        public async Task<IEnumerable<DummyRight>> GetAll() =>
            await _context.DummyRights.ToListAsync();

        public async Task<DummyRight?> Get(int id) =>
            await _context.DummyRights.FirstOrDefaultAsync(x => x.Id == id);
    }
}
