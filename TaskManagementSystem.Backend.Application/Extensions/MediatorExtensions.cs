using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskManagementSystem.Backend.Application.Commands.UserCommands;

namespace TaskManagementSystem.Backend.Application.Extensions
{
    public static class MediatorExtensions
    {
        public static void SetUpMediatr(this IHostApplicationBuilder builder)
        {
            builder.Services.AddMediatR(config => 
            {
                config.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly);
            });
        }
    }
}
