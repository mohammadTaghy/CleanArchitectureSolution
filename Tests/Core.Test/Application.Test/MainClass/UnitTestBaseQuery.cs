using Application.Common;
using Application.Mappings;
using AutoMapper;
using Domain;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Microsoft.AspNetCore.OData.Batch;
using Microsoft.OData.ModelBuilder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Extensions;
using AutoMapper.Internal;

namespace Application
{
    public class UnitTestBaseQuery<TEntity, TRepo, TValidation>
        where TEntity : class, IEntity
        where TRepo : class, IRepositoryReadBase<TEntity>
        where TValidation : class
    {
        protected readonly ITestOutputHelper _testOutputHelper;
        protected readonly Mock<TValidation> _validationMock;
        protected readonly Mock<TRepo> _repoMock;
        protected readonly IMapper _mapper;
        protected readonly Mock<ICacheManager> _cashManager;
        protected readonly Mock<IRabbitMQUtility> _rabbitMQUtility;

        public UnitTestBaseQuery(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _repoMock = new Mock<TRepo>();
            _validationMock = new Mock<TValidation>();
            
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.Internal().MethodMappingEnabled = false;
                mc.AddProfile(new MappingProfile());
            });
            _mapper = mapperConfig.CreateMapper();

            _cashManager = new Mock<ICacheManager>();
            _rabbitMQUtility = new Mock<IRabbitMQUtility>();

        }
        protected ODataQueryOptions<TEntity> makeOdataQueryOption(string url)
        {
            IEdmModel edmModel = CreateEdmModel();
            HttpRequest request = CreateHttpRequest(url, edmModel);
            var oDataQueryContext = new ODataQueryContext(edmModel, typeof(TEntity), new ODataPath());
            return new ODataQueryOptions<TEntity>(oDataQueryContext, request);


        }
        private HttpRequest CreateHttpRequest(string url, IEdmModel edmModel)
        {

            const string routeName = "odata";
            IEdmEntitySet entitySet = edmModel.EntityContainer.FindEntitySet(typeof(TEntity).Name);
            ODataPath path = new ODataPath(new EntitySetSegment(entitySet));

            var request = RequestFactory.Create("GET",
                url,
                dataOptions => dataOptions.AddRouteComponents(routeName, edmModel));

            request.ODataFeature().Model = edmModel;
            request.ODataFeature().Path = path;
            request.ODataFeature().RoutePrefix = routeName;
            return request;
        }
        private static IEdmModel CreateEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<TEntity>(typeof(TEntity).Name);
            return builder.GetEdmModel();
        }
        public static class RequestFactory
        {
            /// <summary>
            /// Creates the <see cref="HttpRequest"/> with OData configuration.
            /// </summary>
            /// <param name="method">The http method.</param>
            /// <param name="uri">The http request uri.</param>
            /// <param name="setupAction"></param>
            /// <returns>The HttpRequest.</returns>
            public static HttpRequest Create(string method, string uri, Action<ODataOptions> setupAction)
            {
                HttpContext context = new DefaultHttpContext();
                HttpRequest request = context.Request;

                IServiceCollection services = new ServiceCollection();
                services.Configure(setupAction);
                context.RequestServices = services.BuildServiceProvider();

                request.Method = method;
                var requestUri = new Uri(uri);
                request.Scheme = requestUri.Scheme;
                request.Host = requestUri.IsDefaultPort ? new HostString(requestUri.Host) : new HostString(requestUri.Host, requestUri.Port);
                request.QueryString = new QueryString(requestUri.Query);
                request.Path = new PathString(requestUri.AbsolutePath);

                return request;
            }
        }
    }
}