using Microsoft.Extensions.Hosting;
using TaskManagementSystem.Backend.Application.Extensions;

namespace TaskManagementSystem.Backend.Application
{
    public static class DependencyInjection
    {
        public static void SetUpApplication(this IHostApplicationBuilder builder)
        {
            builder.SetUpMediatr();
        }
    }
}
