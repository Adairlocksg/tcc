using TCC.Business.Models;

namespace TCC.Business.Interfaces
{
    public interface IInviteService
    {
        Task Add(Invite invite);
        Task Accept(Invite invite);
        Task Reject(Invite invite);
    }
}
