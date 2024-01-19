using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.OData.ModelBuilder;
using Microsoft.OData.Edm;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.DI
{
    public static class DomainDependencyInjection
    {
        public static IEdmModel GetDomainModel(this IServiceCollection services)
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            //services.RegisterAssemblyPublicNonGenericClasses()
            //  .Where(c => c.IsAssignableFrom(typeof(IEntity)) && !c.IsAbstract)
            //  .Services.ToList()
            Assembly.GetExecutingAssembly().GetTypes()
                .Where(p => p.IsSubclassOf(typeof(Entity)) && !p.IsAbstract)
                .ToList().ForEach(p =>
                        builder.AddComplexType(p.GetType())
                    );
            return builder.GetEdmModel();
        }
    }
}
