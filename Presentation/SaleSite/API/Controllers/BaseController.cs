using Application.Common.Interfaces;
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
        protected readonly ICurrentUserService _currentUserService;

        public BaseController() { }
        public BaseController(IMediator mediator, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
        }
    }
    public interface IBaseController
    {

    }
}
