using TCC.Business.Models;

namespace TCC.Business.Interfaces
{
    public interface IInviteRepository : IRepository<Invite>
    {
        Task<Invite> GetByUserAndGroup(Guid userId, Guid groupId);
        Task<IEnumerable<Invite>> GetPendingInvitesForAdmin(Guid userId);
    }
}
