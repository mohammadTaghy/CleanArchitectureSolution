using API.Controllers;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Test.Controllers
{
    public abstract class BaseController_Test<T> : IDisposable
        where T : class, IBaseController, new()
    {
        protected readonly Mock<IMediator> _mediator;
        protected readonly T? _controller;
        public BaseController_Test()
        {
            _mediator = new Mock<IMediator>();
            _controller =(T) Activator.CreateInstance(typeof(T), new object[] { _mediator.Object });

        }

        public void Dispose()
        {

        }
    }
}
