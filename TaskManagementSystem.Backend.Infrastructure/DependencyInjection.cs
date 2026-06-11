using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskManagementSystem.Backend.Application.Abstractions;
using TaskManagementSystem.Backend.Infrastructure.Extensions;
using TaskManagementSystem.Backend.Infrastructure.Persistence;

namespace TaskManagementSystem.Backend.Infrastructure
{
    public static class DependencyInjection
    {
        public static void SetUpInfrastructure(this IHostApplicationBuilder builder)
        {
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.SetUpAppContext();
        }
    }
}
