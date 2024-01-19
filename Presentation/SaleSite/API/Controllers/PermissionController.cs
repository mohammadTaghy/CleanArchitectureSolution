using Application.Common.Model;
using Application.UseCases;
using Application.UseCases.Membership.Permission.Command.Delete;
using Application.UseCases.Membership.Permission.Command.Update;
using Asp.Versioning;
using Common;
using Domain.Entities;
using Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PermissionController : BaseController
    {

        public PermissionController(IMediator mediator, ICurrentUserSession currentUserSession) : base(mediator, currentUserSession)
        {
        }
        #region GetAPI


        [HttpGet]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [CMSAuthorize]
        public async Task<QueryResponse<List<PermissionTreeDto>>> CurrentUserPermissions(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CurrentUserPermissionsAsTreeQuery { UserId = _currentUserSession.UserId.Value }, cancellationToken);
            return result;
        }


        [HttpGet]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [CMSAuthorize]
        public async Task<QueryResponse<List<PermissionTreeDto>>> Permissions([FromQuery]PermissionsAsTreeQuery permissionsAsTreeQuery,CancellationToken cancellationToken)
        {
            return await _mediator.Send(permissionsAsTreeQuery,cancellationToken);
        }
        #endregion
        #region ManipulateAPI
        

        [HttpPost]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [CMSAuthorize]
        public async Task<CommandResponse<Membership_Permission>> Permissions(CreatePermissionCommand createPermissionCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(createPermissionCommand,cancellationToken);
            return result;
        }
        [HttpDelete("{id:int}")]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [CMSAuthorize]
        public async Task<CommandResponse<bool>> Permissions(int id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new DeletePermissionCommand { Id=id}, cancellationToken);
        }
        [HttpPut("{id:int}")]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [CMSAuthorize]
        public async Task<CommandResponse<Membership_Permission>> Permissions(UpdatePermissionCommand updatePermissionCommand, int id, CancellationToken cancellationToken)
        {
            updatePermissionCommand.Id= id;
            return await _mediator.Send(updatePermissionCommand, cancellationToken);
        }
        #endregion

    }
}
