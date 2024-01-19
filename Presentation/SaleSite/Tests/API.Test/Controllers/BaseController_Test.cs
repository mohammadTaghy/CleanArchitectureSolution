using Microsoft.Extensions.Configuration;

namespace API.Test.Controllers
{
    public abstract class BaseController_Test : IDisposable
    {
        protected readonly Mock<IMediator> _mediator;

        protected readonly Mock<ICurrentUserSession> _currentUserService;
        protected readonly Mock<IConfiguration> _configuration;

        public BaseController_Test()
        {
            _configuration= new Mock<IConfiguration>();
            _mediator = new Mock<IMediator>();
            _currentUserService = new Mock<ICurrentUserSession>();
        }

        public void Dispose()
        {

        }
    }
}
