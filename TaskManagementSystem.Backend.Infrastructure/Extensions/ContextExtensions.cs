using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskManagementSystem.Backend.Infrastructure.Persistence;

namespace TaskManagementSystem.Backend.Infrastructure.Extensions
{
    public static class ContextExtensions
    {
        public static void SetUpAppContext(this IHostApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer_01")); // რეალურ პროექტზე აქ Options pattern უნდა იყოს ჰარდ ქოუდინგის ნაცვლად
            });
        }
    }
}
