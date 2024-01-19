using Application.Common.Behaviours;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using NetCore.AutoRegisterDi;
using Domain.DI;
using Microsoft.OData.Edm;

namespace Application.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            
            services.RegisterAssemblyPublicNonGenericClasses()
              .Where(c => c.Name.EndsWith("Validation"))  //optional
              .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);
            
            return services;
        }
        public static IEdmModel GetModel(this IServiceCollection services)
        {
            
            return services.GetDomainModel();
        }
    }
}
