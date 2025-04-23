using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<ITasksService, TasksService>();
            services.AddTransient<IUserService, UserService>();
            return services;
        }
    }
}
