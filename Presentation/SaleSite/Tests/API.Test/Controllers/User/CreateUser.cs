using API.Controllers;
using API.Test.Common;
using Application.Common.Model;
using Application.UseCases.UserCase.Command.Create;
using Common;
using Common.JWT;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace API.Test.Controllers.User
{
    public class CreateUser :IClassFixture<WebApplicationFactory<Startup>>, IDisposable
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly Mock<IMediator> _mediator;
        private readonly UserController _userController;
        private readonly HttpClient _client;

        public CreateUser(WebApplicationFactory<Startup> factory)
        {
            _factory= factory;
            //_client = _factory.CreateClient();
            _mediator = new Mock<IMediator>();
            _userController = new UserController(_mediator.Object);
        }
        [Fact]
        public async Task CreateUserAPI_ReturnsError_ResultTest()
        {
           var result=  Assert.ThrowsAsync<ArgumentNullException>(() => _userController.CreateUser(null, CancellationToken.None));
            Assert.Equal(string.Format(CommonMessage.NullException, "اطلاعات کاربر"), result.Result.Message);

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
                  }));

            var command = new CreateUserCommand
            {
                Email="mohammad@gmail.com",
                MobileNumber="0230",
                Password="123456",
                UserName="MTY"
            };

             _mediator.Setup(p => p.Send<CommandResponse<int>>(It.IsAny<IRequest<CommandResponse<int>>>(), CancellationToken.None))
                .Returns(Task.FromResult(new CommandResponse<int>(true,1)));


            var response = _userController.CreateUser(command, CancellationToken.None);

            Assert.True(response.IsCompleted);
            Assert.Equal(response.Result.Result, result.Result);
        }
        public void Dispose()
        {
            
        }
    }
}
