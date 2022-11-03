using API.Services;
using Application.Common.Interfaces;
using Application.Common.Model;
using Application.UseCases;
using Domain.Entities;
using Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class PermissionController : BaseController
    {

        public PermissionController(IMediator mediator, ICurrentUserService currentUserService) :base(mediator,currentUserService)
        {
        }
        #region GetAPI
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [CMSAuthorize]
        public async Task<QueryResponse<List<PermissionTreeDto>>> GetCurrentUserPermissions(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CurrentUserPermissionsAsTreeQuery(_currentUserService.UserId.Value),cancellationToken);
            return result;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [CMSAuthorize]
        public async Task<QueryResponse<List<PermissionTreeDto>>> GetPermissions([FromQuery]PermissionsAsTreeQuery permissionsAsTreeQuery,CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(permissionsAsTreeQuery,cancellationToken);
            return result;
        }
        #endregion
        #region ManipulateAPI
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [CMSAuthorize]
        public async Task<CommandResponse<Membership_Permission>> Insert(CreatePermissionCommand createPermissionCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(createPermissionCommand,cancellationToken);
            return result;
        }
        #endregion

    }
}
