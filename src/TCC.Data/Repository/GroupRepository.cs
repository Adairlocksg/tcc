using TCC.Business.Interfaces;
using TCC.Business.Models;
using TCC.Data.Context;

namespace TCC.Data.Repository
{
    public class GroupRepository(MyDbContext db) : Repository<Group>(db), IGroupRepository
    {
    }
}
