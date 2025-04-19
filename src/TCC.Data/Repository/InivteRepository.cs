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
    }
}
