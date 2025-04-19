using TCC.Business.Models;

namespace TCC.Business.Interfaces
{
    public interface IUserGroupRepository : IRepository<UserGroup>
    {
        Task<IEnumerable<UserGroup>> GetByUserAndGroup(Guid userId, Guid groupId);
    }
}
