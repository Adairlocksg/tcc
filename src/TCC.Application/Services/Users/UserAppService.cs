using AutoMapper;
using Microsoft.Extensions.Configuration;
using TCC.Application.Dtos;
using TCC.Application.Views;
using TCC.Business.Base;
using TCC.Business.Interfaces;
using TCC.Business.Models;
using TCC.Infra.Helpers;
using TCC.Infra.Services;

namespace TCC.Application.Services.Users
{
    public class UserAppService(IUserRepository userRepository,
                                IUserService userService,
                                IMapper mapper,
                                INotifier notifier,
                                IConfiguration configuration,
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

        public async Task<Result<string>> Login(LoginDto dto)
        {
            var user = await userRepository.GetByUsername(dto.UserName);

            if (user is null)
                return Result.Failure<string>(new Error("404", $"Usuário {dto.UserName} não encontrado"));

            if (!HashService.IsEqual(dto.Password, user.Password))
                return Result.Failure<string>(new Error("401", "Usuário ou senha inválidos"));

            var token = TokenHelper.Create(configuration, user.Id);

            return Result.Success(token);
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