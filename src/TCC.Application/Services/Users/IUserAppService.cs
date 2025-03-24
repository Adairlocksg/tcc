using TCC.Application.Dtos;
using TCC.Application.Views;
using TCC.Business.Base;

namespace TCC.Application.Services.Users
{
    public interface IUserAppService
    {
        Task<Result<UserView>> GetById(Guid id);
        Task<Result<UserView>> Add(UserDto dto);
        Task<Result<UserView>> Update(Guid id, UserDto dto);
    }
}
