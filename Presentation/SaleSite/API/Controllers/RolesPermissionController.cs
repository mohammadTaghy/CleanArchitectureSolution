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

    public class RolesPermissionController : BaseController
    {
        public RolesPermissionController(IMediator mediator, ICurrentUserService currentUserService) : base(mediator, currentUserService)
        {
        }

        #region ManipulateAPI
        

        [HttpPost]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [CMSAuthorize]
        public async Task<CommandResponse<bool>> Insert(CreateRolesPermissionCommand createRolesPermissionCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(createRolesPermissionCommand, cancellationToken);
            return result;
        }
        #endregion
    }
}
