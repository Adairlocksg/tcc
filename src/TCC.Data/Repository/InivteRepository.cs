using Microsoft.EntityFrameworkCore;
using TCC.Business.Interfaces;
using TCC.Business.Models;
using TCC.Data.Context;

namespace TCC.Data.Repository
{
    public class InivteRepository(MyDbContext db) : Repository<Invite>(db), IInviteRepository
    {
        public Task<Invite> GetByUserAndGroup(Guid userId, Guid groupId)
        {
            return DbSet
                .AsNoTracking()
                .Include(i => i.User)
                .Include(i => i.Group)
                .Where(i => i.UserId == userId && i.GroupId == groupId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Invite>> GetPendingInvitesForAdmin(Guid userId)
        {
            var groupIds = await Db.Set<UserGroup>()
                .AsNoTracking()
                .Where(ug => ug.UserId == userId && ug.Admin)
                .Select(ug => ug.GroupId)
                .ToListAsync();

            return await DbSet
                .AsNoTracking()
                .Include(i => i.User)
                .Include(i => i.Group)
                .Where(i => groupIds.Contains(i.GroupId) && i.Status == InviteStatus.Pending)
                .ToListAsync();
        }
    }
}
