using DummbAPI.Exceptions;
using DummbAPI.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace DummbAPI.Persistence.Services
{
    public class DummyService(DummyDbContext context)
    {
        private readonly DummyDbContext _context = context;

        public async Task<IEnumerable<DummyUser>> GetAll() =>
            await _context.DummyUsers.ToListAsync();

        public async Task<DummyUser?> Get(int id) =>
            await _context.DummyUsers.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<bool> Exists(string username) =>
            await _context.DummyUsers.SingleOrDefaultAsync(x => x.Username.ToLower().Equals(username.ToLower())) != null;

        public async Task<int?> Add(DummyUser entity)
        {
            if (Exists(entity.Username.ToLower()).Result)
            {
                throw new EntityExistsException<DummyUser>(entity);
            }

            _context.DummyUsers.Add(entity);

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> Remove(int id)
        {
            DummyUser? toRemove = _context.DummyUsers.FirstOrDefault(x => x.Id == id);
            if (toRemove != null)
            {
                _context.DummyUsers.Remove(toRemove);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<DummyUser?> Update(DummyUser entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(Exists(entity.Username).Result))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return entity;
        }

        public async Task<IEnumerable<DummyUserRight>> GetUserRights(int id) =>
            await _context.DummyUserRights.Where(x => x.UserId == id).ToListAsync();

        public async Task<IEnumerable<DummyRole>> GetRoles() =>
            await _context.DummyRoles.ToListAsync();

        public async Task<DummyRole?> GetRole(int id) =>
            await _context.DummyRoles.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<DummyRight>> GetRights() =>
            await _context.DummyRights.ToListAsync();

        public async Task<DummyRight?> GetRight(int id) =>
            await _context.DummyRights.FirstOrDefaultAsync(x => x.Id == id);
    }
}
