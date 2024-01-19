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

namespace API.Controllers
{

    public class UserController : BaseController
    {
        private readonly IConfiguration _configuration;
        public UserController(IMediator mediator, ICurrentUserSession currentUserSession, IConfiguration configuration
           ) : base(mediator, currentUserSession)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [CMSAuthorize]
        public async Task<CommandResponse<int>> Users([FromBody] CreateUserCommand createUserCommand, CancellationToken cancellationToken)
        {
            if (createUserCommand == null)
                throw new ArgumentNullException("", string.Format(CommonMessage.NullException, "اطلاعات کاربر"));

            return await _mediator.Send(createUserCommand, cancellationToken);
        }
        

        [HttpPost]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<CommandResponse<UserDto>> SignIn([FromBody] SignInQuery signInQuery, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(signInQuery, cancellationToken);
            if (result > 0)
            {
                _currentUserSession.SetUserId(result);
                QueryResponse<UserDto> User = await _mediator.Send(new UserItemQuery() { Id = result });

                //CurrentUserSession.UserInfo = new CurrentUserSessionDto(result, User.IsUserConfirm ? Constants.YesNo.Yes : Constants.YesNo.No
                //    , "", User.Gender == 0 ? Constants.Gender.Male : Constants.Gender.Female,
                //    $"{User.FirstName} {User.LastName}", User.Email, Constants.UserPermissionType.FrontUser
                //    , User.UserName, null, User.PicturePath);
                HttpContext.Response.Headers["Token"] = JWTToken<BaseJwtPayload>.CreateToken(new BaseJwtPayload
                {
                    CurrentVersionCode = 1,
                    IsManagerConfirm = false,
                    IsSecondRegister = false,
                    IsUsrConfirm = true,
                    UserId = result
                }, _configuration["Jwt:Key"]);
                return new CommandResponse<UserDto>(true, User?.Result ?? new UserDto { Id = result });
            }
            else
                throw new NotFoundException(signInQuery.UserName, "");
        }
        

        [HttpPut]
        [ApiVersion("1.0")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [CMSAuthorize]
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
        //[CMSAuthorize]
        //public async Task<CommandResponse<bool>> DeleteUser(int id, CancellationToken cancellationToken)
        //{

        //    return await _mediator.Send(new DeleteUserCommand { Id=id}, cancellationToken);
        //}
        

        [HttpGet]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [CMSAuthorize]

        public async Task<QueryResponse<List<UserDto>>> Users([FromQuery]UserListQuery userListQuery, CancellationToken cancellationToken)
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
        [CMSAuthorize]
        public async Task<QueryResponse<UserDto>> Users(int id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new UserItemQuery {Id=id }, cancellationToken);
        }

    }
}
