using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Common.DI;
using Application.IUtils;
using Microsoft.Extensions.Hosting;
using Application.DI;

namespace Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostBuilder environment)
        {
            services.AddSingleton<IMessages>();
            services.AddSingleton<IConfiguration>(configuration);
            //services.AddHandlers();
            services.AddDbContextPool<PersistanceDBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MainConnectionString"))
                );
            services.AddCommonDependency(configuration, environment);
            services.AddApplicationDependency(configuration, environment);
            return services;
        }
    }
}
