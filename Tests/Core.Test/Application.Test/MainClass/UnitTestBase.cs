using Application.Common.Interfaces;
using Application.Mappings;
using Application.Validation;
using AutoMapper;
using Domain;
using Moq;
using Xunit.Abstractions;

namespace Application
{
    public class UnitTestBase<TEntity,TRepo,TValidation>
        where TEntity : class,IEntity
        where TRepo : class, IRepositoryBase<TEntity>
        where TValidation:class, IValidationRuleBase<TEntity>
    {
        protected readonly ITestOutputHelper _testOutputHelper;
        protected readonly Mock<TValidation> _validationMock;
        protected readonly Mock<TRepo> _repoMock;
        protected readonly Mock<IMapper> _mapper;
        public UnitTestBase(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _repoMock = new Mock<TRepo>();
            _validationMock = new Mock<TValidation>();
            _mapper = new Mock<IMapper>();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper.SetReturnsDefault(configurationProvider);
            _mapper.Setup(p => p.ConfigurationProvider.CreateMapper());
            
        }
    }
}