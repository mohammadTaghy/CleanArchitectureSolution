using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence.DI;
using Microsoft.AspNetCore.Hosting;

namespace Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddHandlers();
            services.AddPersistence(configuration);
            
            return services;
        }
    }
}
