using TCC.Business.Models;

namespace TCC.Business.Interfaces
{
    public interface IUserGroupRepository : IRepository<UserGroup>
    {
        Task<IEnumerable<UserGroup>> GetByGroups(List<Guid> groupIds);
        Task<IEnumerable<UserGroup>> GetByUser(Guid userId);
        Task<UserGroup> GetByUserAndGroup(Guid userId, Guid groupId);
    }
}
