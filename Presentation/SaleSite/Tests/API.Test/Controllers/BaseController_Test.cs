using API.Controllers;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace API.Test.Controllers
{
    public abstract class BaseController_Test : IDisposable
    {
        protected readonly Mock<IMediator> _mediator;

        protected readonly Mock<ICurrentUserService> _currentUserService;
        protected readonly Mock<IConfiguration> _configuration;

        public BaseController_Test()
        {
            _configuration= new Mock<IConfiguration>();
            _mediator = new Mock<IMediator>();
            _currentUserService = new Mock<ICurrentUserService>();
        }

        public void Dispose()
        {

        }
    }
}
