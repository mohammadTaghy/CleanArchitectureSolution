using Common;
using Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.OData;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using NetCore.AutoRegisterDi;
using System.Reflection;

namespace Persistence.DI
{
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddHandlers();
            services.AddDbContextPool<PersistanceDBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MainConnectionString"))
                );
            services.RegisterAssemblyPublicNonGenericClasses()
              .Where(c => c.Name.EndsWith("Repo"))  //optional
              .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);
            
            services.AddSingleton<ICacheManager, RedisCacheService>();

        }


    }
}
