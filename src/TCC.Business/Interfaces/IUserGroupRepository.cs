using TCC.Business.Models;

namespace TCC.Business.Interfaces
{
    public interface IUserGroupRepository : IRepository<UserGroup>
    {
        IEnumerable<UserGroup> GetGroupsByUserId(Guid userId);
    }
}
