using Application.Common.Model;
using Application.UseCases.UserCase.Command.Create;
using Application.UseCases.UserCase.Query.SignIn;
using Common;
using Common.JWT;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //[Route("api/User")]
    //[EnableCors("AllowOrigin")]
    //[Authorize]
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        //[Route("CreateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<CommandResponse<string>> CreateUser([FromBody] CreateUserCommand createUserCommand,CancellationToken cancellationToken)
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
                })
            );
        }
        [HttpPost]
        //[Route("CreateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<CommandResponse<string>> SignIn(SignInQuery signInQuery,CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(signInQuery, cancellationToken);
            return new CommandResponse<string>(true,
                JWTToken<BaseJwtPayload>.CreateToken(new BaseJwtPayload
                {
                    CurrentVersionCode = 1,
                    IsManagerConfirm = false,
                    IsSecondRegister = false,
                    IsUsrConfirm = true,
                    UserId = result
                })
            );
        }
    }
}
