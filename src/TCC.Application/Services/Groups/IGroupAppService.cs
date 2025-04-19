using TCC.Application.Dtos;
using TCC.Application.Views;
using TCC.Business.Base;

namespace TCC.Application.Services.Groups
{
    public interface IGroupAppService : IDisposable
    {
        Task<Result<GroupView>> Add(GroupDto dto);
    }
}
