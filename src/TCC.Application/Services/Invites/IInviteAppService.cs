using TCC.Application.Dtos;
using TCC.Application.Views;
using TCC.Business.Base;
using TCC.Business.Models;

namespace TCC.Application.Services.Invites
{
    public interface IInviteAppService : IDisposable
    {
        Task<Result<IdView>> Add(InviteDto dto);
        Task<Result<IdView>> Accept(Guid id);
        Task<Result<IdView>> Reject(Guid id);
        Task<Result<IEnumerable<InviteView>>> GetPendingInvitesForAdmin();
    }
}
