using TCC.Application.Services.Groups;
using TCC.Application.Services.Users;
using TCC.Business.Interfaces;
using TCC.Business.Notifications;
using TCC.Business.Services;
using TCC.Data.Context;
using TCC.Data.Repository;
using TCC.Infra.Helpers;

namespace TCC.Api.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            //Data
            services.AddScoped<MyDbContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserGroupRepository, UserGroupRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IUnityOfWork, UnitOfWork>();

            //Business
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IUserGroupService, UserGroupService>();

            //Application
            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<IGroupAppService, GroupAppService>();

            //Infra
            services.AddScoped<ITokenHelper, TokenHelper>();

            return services;
        }
    }
}
