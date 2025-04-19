using Microsoft.EntityFrameworkCore;
using TCC.Business.Interfaces;
using TCC.Business.Models;
using TCC.Data.Context;

namespace TCC.Data.Repository
{
    public class GroupRepository(MyDbContext db) : Repository<Group>(db), IGroupRepository
    {
        public async Task<IEnumerable<Category>> GetCategories(Guid groupId)
        {
            var group = await DbSet.AsNoTracking()
                .Include(g => g.Categories)
                .FirstOrDefaultAsync(g => g.Id == groupId);

            return group?.Categories ?? [];
        }

        public async Task<Group> GetWithCategories(Guid id)
        {
            return await DbSet.AsNoTracking()
                .Include(g => g.Categories)
                .FirstOrDefaultAsync(g => g.Id == id);
        }
    }
}
