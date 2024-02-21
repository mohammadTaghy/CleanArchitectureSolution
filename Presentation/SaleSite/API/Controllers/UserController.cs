using Application.Common.Exceptions;
using Application.Common.Model;
using Application.UseCases.UserCase.Query.SignIn;
using Common;
using Common.JWT;
using Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Application.UseCases.Membership.UserCase.Query.GetUserList;
using Application.UseCases.UserCase.Command.Create;
using Application.UseCases.Membership.UserCase.Query.GetUserItem;
using Application.UseCases.Membership.UserCase;
using Application.UseCases.UserCase.Command.Update;
using System.Security.Claims;
using static Common.Constants;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Infrastructure.Authentication.Authorize("CmsClaim")]
    public class UserController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IJWTTokenHelper jWTTokenHelper;

        public UserController(
            IMediator mediator,
            ICurrentUserSession currentUserSession, 
            IConfiguration configuration,
            IJWTTokenHelper jWTTokenHelper
           ) : base(mediator, currentUserSession)
        {
            _configuration = configuration;
            this.jWTTokenHelper = jWTTokenHelper;
        }

        [HttpPost]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<CommandResponse<int>> Users([FromBody] CreateUserCommand createUserCommand, CancellationToken cancellationToken)
        {
            if (createUserCommand == null)
                throw new ArgumentNullException("", string.Format(CommonMessage.NullException, "اطلاعات کاربر"));

            return await _mediator.Send(createUserCommand, cancellationToken);
        }


        [HttpPost]
        [AllowAnonymous]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task SignIn([FromBody] SignInQuery signInQuery, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(signInQuery, cancellationToken);
            if (result > 0)
            {
                HttpContext.Response.Headers.Authorization = jWTTokenHelper.CreateToken(GetUserClaims(result));
            }
            else
                throw new NotFoundException(signInQuery.UserName, "");
        }
        private IEnumerable<Claim> GetUserClaims(int userId)
        {
            List<Claim> claims = new List<Claim>();
            Claim _claim;
            _claim = new Claim(TokenClaimType.UserId.ToString(), userId.ToString(), ClaimValueTypes.Integer);
            claims.Add(_claim);
            return claims.AsEnumerable<Claim>();
        }

        [HttpPut]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<CommandResponse<bool>> Users([FromBody] UpdateUserCommand UpdateUserCommand, CancellationToken cancellationToken)
        {
            if (UpdateUserCommand == null)
                throw new ArgumentNullException("", string.Format(CommonMessage.NullException, "اطلاعات کاربر"));

            return await _mediator.Send(UpdateUserCommand, cancellationToken);
        }

        //[HttpDelete("{id:int}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize]
        //public async Task<CommandResponse<bool>> DeleteUser(int id, CancellationToken cancellationToken)
        //{

        //    return await _mediator.Send(new DeleteUserCommand { Id=id}, cancellationToken);
        //}


        [HttpGet]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<QueryResponse<List<UserDto>>> Users([FromQuery] UserListQuery userListQuery, CancellationToken cancellationToken)
        {

            //return await _mediator.Send(new UserListQuery
            //{
            //    Filter= oDataQueryOptions.Filter?.RawValue??"",
            //    Columns=oDataQueryOptions.SelectExpand?.RawExpand??"",
            //    Orderby=oDataQueryOptions.OrderBy?.RawValue??"",
            //    Skip=oDataQueryOptions.Skip.Value,
            //    Top=oDataQueryOptions.Top.Value,
            //}, cancellationToken);
            return await _mediator.Send(userListQuery, cancellationToken);
        }
        [HttpGet("{id:int}")]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<QueryResponse<UserDto>> Users(int id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new UserItemQuery { Id = id }, cancellationToken);
        }

    }
}
