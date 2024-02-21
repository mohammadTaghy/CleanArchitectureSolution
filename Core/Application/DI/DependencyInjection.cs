using Application.Common.Behaviours;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using NetCore.AutoRegisterDi;
using Domain.DI;
using Microsoft.OData.Edm;
using AutoMapper;
using Application.Mappings;
using AutoMapper.Internal;

namespace Application.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDependency(this IServiceCollection services, IConfiguration configuration)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.Internal().MethodMappingEnabled = false;
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
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
