using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Mosahm.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Mosahm.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<MosahmDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("MainConnection")));

            return services;
        }
    }
}
