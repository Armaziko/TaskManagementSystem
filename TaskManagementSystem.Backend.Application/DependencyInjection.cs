using FluentValidation;
using Microsoft.Extensions.Hosting;
using TaskManagementSystem.Backend.Application.Extensions;
using TaskManagementSystem.Backend.Application.Validators.UserValidators;

namespace TaskManagementSystem.Backend.Application
{
    public static class DependencyInjection
    {
        public static void SetUpApplication(this IHostApplicationBuilder builder)
        {
            builder.SetUpMediatr();
            builder.Services.AddValidatorsFromAssembly(typeof(UpdateUserValidator).Assembly);
        }
    }
}
