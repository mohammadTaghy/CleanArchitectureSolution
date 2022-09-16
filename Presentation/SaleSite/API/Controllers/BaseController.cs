using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public abstract class BaseController : ControllerBase, IBaseController
    {
        protected const string secretKey = "";
        protected readonly IMediator _mediator;
        public BaseController() { }
        public BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
    public interface IBaseController
    {

    }
}
