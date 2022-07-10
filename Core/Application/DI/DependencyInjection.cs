using Application.Common.Behaviours;
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
using AutoMapper;
using NetCore.AutoRegisterDi;

namespace Application.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDependency(this IServiceCollection services, IConfiguration configuration, IHostBuilder environment)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            
            services.RegisterAssemblyPublicNonGenericClasses()
              .Where(c => c.Name.EndsWith("Validation"))  //optional
              .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);
            return services;
        }
    }
}
