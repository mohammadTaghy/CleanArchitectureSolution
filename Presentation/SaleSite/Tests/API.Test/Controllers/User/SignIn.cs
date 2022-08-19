using Application.Common.Model;
using Application.UseCases.UserCase.Query.SignIn;
using Common.JWT;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Test.Controllers.User
{
    public class SignIn : BaseUserController, IDisposable
    {
        public SignIn()
        {

        }
        [Fact]
        public async Task SignIn_ReturnSuccess_ResultTest()
        {
            var result = new CommandResponse<string>(true,
                  JWTToken<BaseJwtPayload>.CreateToken(new BaseJwtPayload
                  {
                      CurrentVersionCode = 1,
                      IsManagerConfirm = false,
                      IsSecondRegister = false,
                      IsUsrConfirm = true,
                      UserId = 1
                  }));
            _mediator.Setup(p=>p.Send(It.IsAny<IRequest<int>>(),CancellationToken.None)).
                Returns(Task.FromResult(1));
            var query=new SignInQuery()
            {
                Password="1234",
                UserName="test",
            };
            var response = await _userController.SignIn(query, CancellationToken.None);
            Assert.NotNull(response);
            Assert.True(response.IsSuccess);
            Assert.Equal(response.Result, result.Result);
        }
        public void Dispose()
        {
        }
    }
}
