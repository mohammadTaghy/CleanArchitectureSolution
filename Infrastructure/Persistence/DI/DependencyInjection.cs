using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Common.DI;
using Microsoft.Extensions.Hosting;
using Application.DI;
using System.Reflection;
using NetCore.AutoRegisterDi;

namespace Persistence.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddHandlers();
            services.AddDbContextPool<PersistanceDBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MainConnectionString"))
                );
            services.RegisterAssemblyPublicNonGenericClasses()
              .Where(c => c.Name.EndsWith("Repo"))  //optional
              .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);
            return services;
        }
    }
}
