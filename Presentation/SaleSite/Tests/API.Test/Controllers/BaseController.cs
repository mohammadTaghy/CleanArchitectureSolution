using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Test.Controllers
{
    public class BaseController
    {
        protected readonly Mock<IMediator> _mediator;
        public BaseController()
        {
            _mediator = new Mock<IMediator>();

        }
    }
}
