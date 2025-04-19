using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TCC.Business.Interfaces;
using TCC.Business.Models;
using TCC.Data.Context;

namespace TCC.Data.Repository
{
    public class UserGroupRepository : Repository<UserGroup>, IUserGroupRepository
    {
        public UserGroupRepository(MyDbContext db) : base(db)
        {
        }

        public async Task<IEnumerable<UserGroup>> GetByUserAndGroup(Guid userId, Guid groupId)
        {
            return await DbSet
                .AsNoTracking()
                .Where(ug => ug.UserId == userId && ug.GroupId == groupId)
                .ToListAsync();
        }
    }
}
