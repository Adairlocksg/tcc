using AutoMapper;
using TCC.Application.Dtos;
using TCC.Application.Views;
using TCC.Business.Base;
using TCC.Business.Interfaces;
using TCC.Business.Models;

namespace TCC.Application.Services.Users
{
    public class UserAppService(IUserRepository userRepository,
                                IUserService userService,
                                IMapper mapper,
                                INotifier notifier,
                                IUnityOfWork unityOfWork) : IUserAppService
    {
        public async Task<Result<UserView>> Register(UserDto dto)
        {
            var user = mapper.Map<User>(dto);

            user.EncryptPassword();

            await userService.Add(user);

            if (notifier.HasNotification())
                return Result.Failure<UserView>(new Error("400", notifier.GetNotificationMessage()));

            await unityOfWork.Commit();

            return Result.Success(mapper.Map<UserView>(user));
        }

        public async Task<Result<UserView>> GetById(Guid id)
        {
            var user = await userRepository.GetById(id);

            if (user is null)
                return Result.Failure<UserView>(new Error("404", $"Usuário de código {id} não encontrado"));

            return Result.Success(mapper.Map<UserView>(user));
        }

        public async Task<Result<UserView>> Update(Guid id, UserDto dto)
        {
            var user = await userRepository.GetById(id);

            if (user is null)
                return Result.Failure<UserView>(new Error("404", $"Usuário de código {id} não encontrado"));

            user.Update(
                dto.FirstName,
                dto.LastName,
                dto.Email,
                dto.UserName,
                dto.Password
            );

            await userService.Update(user);

            if (notifier.HasNotification())
                return Result.Failure<UserView>(new Error("400", notifier.GetNotificationMessage()));

            await unityOfWork.Commit();

            return Result.Success(mapper.Map<UserView>(user));
        }
    }
}