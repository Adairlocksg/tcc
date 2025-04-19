using Microsoft.EntityFrameworkCore;
using TCC.Business.Interfaces;
using TCC.Business.Models;
using TCC.Data.Context;

namespace TCC.Data.Repository
{
    public class UserGroupRepository(MyDbContext db) : Repository<UserGroup>(db), IUserGroupRepository
    {
        public async Task<UserGroup> GetByUserAndGroup(Guid userId, Guid groupId)
        {
            return await DbSet
                .AsNoTracking()
                .Include(ug => ug.User)
                .Include(ug => ug.Group)
                .Where(ug => ug.UserId == userId && ug.GroupId == groupId)
                .FirstOrDefaultAsync();
        }
    }
}
