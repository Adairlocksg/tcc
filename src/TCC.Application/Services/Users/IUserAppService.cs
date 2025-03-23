using TCC.Application.ViewModels;
using TCC.Business.Base;

namespace TCC.Application.Services.Users
{
    public interface IUserAppService
    {
        Task<Result<UserViewModel>> GetById(Guid id);
        Task<Result<UserViewModel>> Add(UserViewModel userVwm);
        Task<Result<UserViewModel>> Update(Guid id, UserViewModel userVwm);
    }
}
