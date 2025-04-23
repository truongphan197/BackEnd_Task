using Application.Abstractions;
using Infrastructure.Authencation;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfratructureServices(this IServiceCollection services)
        {
            services.AddTransient<ITasksRepository, TasksRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IJwtProvider, JwtProvider>();

            return services;
        }
    }
}
