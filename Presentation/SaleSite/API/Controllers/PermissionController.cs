using API.Services;
using Application.Common.Interfaces;
using Application.Common.Model;
using Application.UseCases;
using Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class PermissionController : BaseController
    {
        private readonly ICurrentUserService _currentUserService;

        public PermissionController() { }
        public PermissionController(IMediator mediator, ICurrentUserService currentUserService) :base(mediator)
        {
            _currentUserService = currentUserService;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<QueryResponse<List<PermissionTreeDto>>> GetPermissions(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CurrentUserPermissionsAsTreeQuery(_currentUserService.UserId.Value) );
            return result;
        }
    }
}
