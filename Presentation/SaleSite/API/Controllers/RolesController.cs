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
   
    public class RolesController : BaseController
    {
        public RolesController(IMediator mediator, ICurrentUserService currentUserService) : base(mediator,currentUserService)
        {
        }
        #region GetAPI
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [CMSAuthorize]
        public async Task<QueryResponse<List<RolesDto>>> GetRoles(GetRolesQuery getRolesQuery,CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(getRolesQuery, cancellationToken);
            return result;
        }
        #endregion
        #region ManipulateAPI
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [CMSAuthorize]
        public async Task<CommandResponse<Membership_Roles>> Insert(CreateRoleCommand createRoleCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(createRoleCommand, cancellationToken);
            return result;
        }
        #endregion
    }
}
