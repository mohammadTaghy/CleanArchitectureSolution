using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Model;
using Application.UseCases.UserCase.Command.Create;
using Application.UseCases.UserCase.Query.SignIn;
using Application.UseCases.UserProfileCase.Query.GetUserItem;
using Common;
using Common.JWT;
using Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class UserController : BaseController
    {
        private readonly IConfiguration _configuration;

        public UserController(IMediator mediator, ICurrentUserService currentUserService,IConfiguration configuration) : base(mediator, currentUserService)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [CMSAuthorize]
        public async Task<CommandResponse<string>> CreateUser([FromBody] CreateUserCommand createUserCommand, CancellationToken cancellationToken)
        {
            if (createUserCommand == null)
                throw new ArgumentNullException("", string.Format(CommonMessage.NullException, "اطلاعات کاربر"));

            var result = await _mediator.Send(createUserCommand, cancellationToken);

            return new CommandResponse<string>(true,
                JWTToken<BaseJwtPayload>.CreateToken(new BaseJwtPayload
                {
                    CurrentVersionCode = 1,
                    IsManagerConfirm = false,
                    IsSecondRegister = false,
                    IsUsrConfirm = true,
                    UserId = result.Result
                },_configuration["Jwt:Key"])
            );
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<CommandResponse<UserItemDto>> SignIn(SignInQuery signInQuery, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(signInQuery, cancellationToken);
            if (result > 0)
            {
                _currentUserService.SetUserId(result);
                var userProfile = await _mediator.Send(new GetUserItemQuery() { Id = result });
                
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
                return new CommandResponse<UserItemDto>(true, userProfile ?? new() { Id=result});
            }
            else
                throw new NotFoundException(signInQuery.UserName, "");
        }
    }
}
