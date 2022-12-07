using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Model;
using Application.UseCases.UserCase.Command.Create;
using Application.UseCases.UserCase.Query.SignIn;
using Application.UseCases.UserProfileCase.Query.GetUserItem;
using Application.UseCases.UserProfileCase.Query.GetUserList;
using Common;
using Common.JWT;
using Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OData.Edm;
using Application.UseCases;
using Domain.Entities;
using Domain;
using Application.UseCases.UserProfileCase.Command.Create;

namespace API.Controllers
{

    public class UserController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IEdmModel _edmModel;
        private readonly IEdmType _edmType;
        public UserController(IMediator mediator, ICurrentUserService currentUserService, IConfiguration configuration
           ) : base(mediator, currentUserService)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [CMSAuthorize]
        public async Task<CommandResponse<UserProfileListDto>> Users([FromBody] CreateUserProfileCommand createUserCommand, CancellationToken cancellationToken)
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
        public async Task<CommandResponse<UserItemDto>> SignIn([FromBody] SignInQuery signInQuery, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(signInQuery, cancellationToken);
            if (result > 0)
            {
                _currentUserService.SetUserId(result);
                QueryResponse<UserItemDto> userProfile = await _mediator.Send(new UserItemQuery() { Id = result });

                //CurrentUserSession.UserInfo = new CurrentUserSessionDto(result, userProfile.IsUserConfirm ? Constants.YesNo.Yes : Constants.YesNo.No
                //    , "", userProfile.Gender == 0 ? Constants.Gender.Male : Constants.Gender.Female,
                //    $"{userProfile.FirstName} {userProfile.LastName}", userProfile.Email, Constants.UserPermissionType.FrontUser
                //    , userProfile.UserName, null, userProfile.PicturePath);
                HttpContext.Response.Headers["Token"] = JWTToken<BaseJwtPayload>.CreateToken(new BaseJwtPayload
                {
                    CurrentVersionCode = 1,
                    IsManagerConfirm = false,
                    IsSecondRegister = false,
                    IsUsrConfirm = true,
                    UserId = result
                }, _configuration["Jwt:Key"]);
                return new CommandResponse<UserItemDto>(true, userProfile?.Result ?? new UserItemDto { Id = result });
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
        public async Task<CommandResponse<UserProfileListDto>> Users([FromBody] UpdateUserProfileCommand UpdateUserProfileCommand, CancellationToken cancellationToken)
        {
            if (UpdateUserProfileCommand == null)
                throw new ArgumentNullException("", string.Format(CommonMessage.NullException, "اطلاعات کاربر"));

            return await _mediator.Send(UpdateUserProfileCommand, cancellationToken);
        }
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [CMSAuthorize]
        public async Task<CommandResponse<bool>> DeleteUser(int id, CancellationToken cancellationToken)
        {

            return await _mediator.Send(new DeleteUserProfileCommand { Id=id}, cancellationToken);
        }
        

        [HttpGet]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [CMSAuthorize]
        public async Task<QueryResponse<List<UserProfileListDto>>> Users([FromQuery] UserListQuery userListQuery, CancellationToken cancellationToken)
        {
            //var context = new ODataQueryContext(_edmModel, _edmType, null);
            //var queryOption = new ODataQueryOptions(context, Request);
            return await _mediator.Send(userListQuery, cancellationToken);
        }
        [HttpGet("{id:int}")]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [CMSAuthorize]
        public async Task<QueryResponse<UserItemDto>> Users(int id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new UserItemQuery {Id=id }, cancellationToken);
        }

    }
}
