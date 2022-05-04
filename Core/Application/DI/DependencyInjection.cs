using Application.Mappings;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDependency(this IServiceCollection services, IConfiguration configuration, IHostBuilder environment)
        {
            services.AddSingleton<MappingProfile>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
