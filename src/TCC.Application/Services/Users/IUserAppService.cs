﻿using TCC.Application.Dtos;
using TCC.Application.Views;
using TCC.Business.Base;

namespace TCC.Application.Services.Users
{
    public interface IUserAppService : IDisposable
    {
        Task<Result<UserView>> Register(UserDto dto);
        Task<Result<UserView>> GetById(Guid id);
        Task<Result<UserView>> Update(Guid id, UserDto dto);
        Task<Result<string>> Login(LoginDto dto);
    }
}
