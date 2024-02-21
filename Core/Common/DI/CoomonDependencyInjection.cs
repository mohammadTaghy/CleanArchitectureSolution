using Common.JWT;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DI
{
    public static class CoomonDependencyInjection
    {
        public static IServiceCollection AddCommonDependency(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddSingleton<IMemoryCache>();
            //services.AddSingleton<IMemoryCacheManager, MemoryCacheManager>();
            services.AddScoped<ICurrentUserSession, CurrentUserSession>();
            services.AddSingleton<IJWTTokenHelper, JWTTokenHelper>();
            return services;
        }
    }
}
