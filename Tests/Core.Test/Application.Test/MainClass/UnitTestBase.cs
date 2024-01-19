using Application.Common;
using Application.Mappings;
using AutoMapper;
using AutoMapper.Internal;
using Domain;

namespace Application
{
    public class UnitTestBase<TEntity,TRepo>
        where TEntity : class,IEntity
        where TRepo : class, IRepositoryBase<TEntity>
    {
        protected readonly ITestOutputHelper _testOutputHelper;
        protected readonly Mock<TRepo> _repoMock;
        protected readonly IMapper _mapper;
        protected readonly Mock<ICacheManager> _cacheManager;
        protected readonly Mock<IRabbitMQUtility> _rabbitMQUtility;

        public UnitTestBase(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _repoMock = new Mock<TRepo>();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.Internal().MethodMappingEnabled = false;
                mc.AddProfile(new MappingProfile());
            });
            _mapper = mapperConfig.CreateMapper();
            _cacheManager =new Mock<ICacheManager>();
            _rabbitMQUtility = new Mock<IRabbitMQUtility>();

        }
    }
}