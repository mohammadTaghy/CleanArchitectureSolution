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
        

        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[CMSAuthorize]
        //public async Task<QueryResponse<List<PermissionTreeDto>>> Permissions(CancellationToken cancellationToken)
        //{
        //    var result = await _mediator.Send(new CurrentUserPermissionsAsTreeQuery { UserId = _currentUserService.UserId.Value },cancellationToken);
        //    return result;
        //}
        

        [HttpGet]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [CMSAuthorize]
        public async Task<QueryResponse<List<PermissionTreeDto>>> Permissions([FromQuery]PermissionsAsTreeQuery permissionsAsTreeQuery,CancellationToken cancellationToken)
        {
            if(permissionsAsTreeQuery == null||permissionsAsTreeQuery.RoleId==null)
            {
                return await _mediator.Send(new CurrentUserPermissionsAsTreeQuery { UserId = _currentUserService.UserId.Value }, cancellationToken);
            }
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
        public async Task<CommandResponse<Membership_Permission>> Insert(CreatePermissionCommand createPermissionCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(createPermissionCommand,cancellationToken);
            return result;
        }
        #endregion

    }
}
