using TCC.Application.Services.Users;
using TCC.Business.Interfaces;
using TCC.Business.Notifications;
using TCC.Business.Services;
using TCC.Data.Context;
using TCC.Data.Repository;

namespace TCC.Api.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            //Data
            services.AddScoped<MyDbContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnityOfWork, UnitOfWork>();

            //Business
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INotifier, Notifier>();

            //Application
            services.AddScoped<IUserAppService, UserAppService>();

            return services;
        }
    }
}
