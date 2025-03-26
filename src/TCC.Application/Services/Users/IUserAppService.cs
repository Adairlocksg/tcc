using TCC.Application.Dtos;
using TCC.Application.Views;
using TCC.Business.Base;

namespace TCC.Application.Services.Users
{
    public interface IUserAppService
    {
        Task<Result<UserView>> Register(UserDto dto);
        Task<Result<UserView>> GetById(Guid id);
        Task<Result<UserView>> Update(Guid id, UserDto dto);
    }
}
