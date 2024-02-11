using Application.Common;
using Domain;
using Infrastructure.Authentication;
using Infrastructure.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using NetCore.AutoRegisterDi;
using System.Reflection;

namespace Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddHandlers();
            services.AddSingleton<IRabbitMQUtility, DirectExchangeRabbitMQ>();
        }
        
    }
}
