using AutoMapper;
using TCC.Application.ViewModels;
using TCC.Business.Base;
using TCC.Business.Interfaces;
using TCC.Business.Models;

namespace TCC.Application.Services.Users
{
    public class UserAppService(IUserRepository userRepository,
                                IUserService userService,
                                IMapper mapper,
                                INotifier notifier) : IUserAppService
    {
        public async Task<Result<UserViewModel>> Add(UserViewModel userVwm)
        {
            var user = mapper.Map<User>(userVwm);

            await userService.Add(user);

            if (notifier.HasNotification())
                return Result.Failure<UserViewModel>(new Error("400", notifier.GetNotificationMessage()));

            return Result.Success(mapper.Map<UserViewModel>(user));
        }

        public async Task<Result<UserViewModel>> GetById(Guid id)
        {
            var user = await userRepository.GetById(id);

            if (user is null)
                return Result.Failure<UserViewModel>(new Error("404", $"Usuário de código {id} não encontrado"));

            return Result.Success(mapper.Map<UserViewModel>(user));
        }

        public async Task<Result<UserViewModel>> Update(Guid id, UserViewModel userVwm)
        {
            var user = await userRepository.GetById(id);

            if (user is null)
                return Result.Failure<UserViewModel>(new Error("404", $"Usuário de código {id} não encontrado"));

            user.Update(
                userVwm.FirstName,
                userVwm.LastName,
                userVwm.Email,
                userVwm.UserName,
                userVwm.Password
            );

            await userService.Update(user);

            if (notifier.HasNotification())
                return Result.Failure<UserViewModel>(new Error("400", notifier.GetNotificationMessage()));

            return Result.Success(mapper.Map<UserViewModel>(user));
        }
    }
}