using Application.Common.Interfaces;
using Application.Common.Model;
using Application.UseCases;
using Asp.Versioning;
using Common;
using Domain.Entities;
using Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class RolesController : BaseController
    {
        public RolesController(IMediator mediator, ICurrentUserSession currentUserSession) : base(mediator,currentUserSession)
        {
        }
        #region GetAPI
        

        [HttpGet]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<QueryResponse<List<RolesDto>>> Roles([FromQuery]RolesQuery getRolesQuery,CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(getRolesQuery, cancellationToken);
            return result;
        }
        #endregion
        #region ManipulateAPI
        

        [HttpPost]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<CommandResponse<Membership_Roles>> Roles(CreateRoleCommand createRoleCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(createRoleCommand, cancellationToken);
            return result;
        }
        [HttpPut("{id:int}")]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<CommandResponse<Membership_Roles>> Roles([FromBody]UpdateRoleCommand createRoleCommand, [FromRoute]int id, CancellationToken cancellationToken)
        {
            createRoleCommand.Id = id;
            var result = await _mediator.Send(createRoleCommand, cancellationToken);
            return result;
        }
        #endregion
    }
}
