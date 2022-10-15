using API.Controllers;
using API.Test.Common;
using Application.Common.Model;
using Application.UseCases.UserCase.Command.Create;
using Common;
using Common.JWT;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace API.Test.Controllers.User
{
    public class CreateUser : BaseController_Test, IDisposable
    {
        private readonly UserController _controller;
        public CreateUser()
        {

            _controller = new UserController(_mediator.Object,_currentUserService.Object,_configuration.Object);
        }
        [Fact]
        public async Task CreateUserAPI_ReturnsError_ResultTest()
        {
           var result= await  Assert.ThrowsAsync<ArgumentNullException>(() => _controller.CreateUser(null, CancellationToken.None));
            Assert.Equal(string.Format(CommonMessage.NullException, "اطلاعات کاربر"), result.Message);

        }
        [Fact]
        public async Task CreateUserAPI_ReturnsSuccessStatusCode_ResultTest()
        {
            var result = new CommandResponse<string>(true,
                  JWTToken<BaseJwtPayload>.CreateToken(new BaseJwtPayload
                  {
                      CurrentVersionCode = 1,
                      IsManagerConfirm = false,
                      IsSecondRegister = false,
                      IsUsrConfirm = true,
                      UserId = 1
                  }, _configuration.Object["Jwt:Key"]));

            var command = new CreateUserCommand
            {
                Email="mohammad@gmail.com",
                MobileNumber="0230",
                Password="123456",
                UserName="MTY"
            };

             _mediator.Setup(p => p.Send<CommandResponse<int>>(It.IsAny<IRequest<CommandResponse<int>>>(), CancellationToken.None))
                .Returns(Task.FromResult(new CommandResponse<int>(true,1)));


            var response =await _controller.CreateUser(command, CancellationToken.None);

            Assert.True(response.IsSuccess);
            Assert.Equal(response.Result, result.Result);
        }
        public void Dispose()
        {
            
        }
    }
}
